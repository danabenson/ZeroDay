using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using NatGeoMetroApp.Common;
using NatGeoMetroApp.DataModel;
using NotificationsExtensions.ToastContent;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Provider;
using Windows.Storage.Streams;
using Windows.System;
using Windows.System.UserProfile;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// The Item Detail Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234232

namespace NatGeoMetroApp
{
    /// <summary>
    /// A page that displays details for a single item within a group while allowing gestures to
    /// flip through other items belonging to the same group.
    /// </summary>
    public sealed partial class ItemDetailPage : LayoutAwarePage
    {
        private DataTransferManager dataTransferManager;

        private Image image;

        public ItemDetailPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            // Allow saved page state to override the initial item to display
            if (pageState != null && pageState.ContainsKey("SelectedItem"))
            {
                navigationParameter = pageState["SelectedItem"];
            }

            NatGeoImage item = NatGeoDataSource.GetItem((String) navigationParameter);
            DefaultViewModel["Items"] = NatGeoDataSource.Items;
            DefaultViewModel["Item"] = flipView.SelectedItem = item;
        }

        protected override void OnKeyUp(KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Escape)
            {
                Frame.Navigate(typeof (GroupedItemsPage));
            }
            base.OnKeyUp(e);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                // Register the current page as a share source.
                dataTransferManager = DataTransferManager.GetForCurrentView();
                dataTransferManager.DataRequested += OnDataRequested;
            }
            catch (Exception)
            {
            }
            base.OnNavigatedTo(e);
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
            var selectedItem = (NatGeoImage) flipView.SelectedItem;
            pageState["SelectedItem"] = selectedItem.UniqueId;
        }

        private void FlipView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DefaultViewModel["Item"] = flipView.SelectedItem;
        }

        private void Share_Click(object sender, RoutedEventArgs e)
        {
            DataTransferManager.ShowShareUI();
        }

        // When share is invoked (by the user or programatically) the event handler we registered will be called to populate the datapackage with the
        // data to be shared.
        private void OnDataRequested(DataTransferManager sender, DataRequestedEventArgs e)
        {
            GetShareContent(e.Request);
        }

        private async void GetShareContent(DataRequest request)
        {
            var item = flipView.SelectedItem as NatGeoImage;
            DataPackage requestData = request.Data;
            requestData.Properties.Title = item.Title;
            requestData.Properties.Description = item.Description; // The description is optional.
            requestData.SetUri(new Uri(item.DownloadUrl ?? item.ImageUrl));

            // It's recommended to use both SetBitmap and SetStorageItems for sharing a single image
            // since the target app may only support one or the other.
            var client = new HttpClient();
            var r = new HttpRequestMessage(
                HttpMethod.Get, item.DownloadUrl ?? item.ImageUrl);
            HttpResponseMessage response = await client.SendAsync(r,
                                                                  HttpCompletionOption.ResponseHeadersRead);


            StorageFile imageFile =
                await
                ApplicationData.Current.LocalFolder.CreateFileAsync("download.jpg",
                                                                    CreationCollisionOption.ReplaceExisting);
            IRandomAccessStream fs = await imageFile.OpenAsync(FileAccessMode.ReadWrite);
            var writer = new DataWriter(fs.GetOutputStreamAt(0));
            writer.WriteBytes(await response.Content.ReadAsByteArrayAsync());
            await writer.StoreAsync();
            writer.DetachStream();
            await fs.FlushAsync();

            var imageItems = new List<IStorageItem>();
            imageItems.Add(imageFile);
            requestData.SetStorageItems(imageItems);

            RandomAccessStreamReference imageStreamRef = RandomAccessStreamReference.CreateFromFile(imageFile);
            requestData.Properties.Thumbnail = imageStreamRef;
            requestData.SetBitmap(imageStreamRef);
        }

        private async void SetAsLockScreen_Click(object sender, RoutedEventArgs e)
        {
            var item = flipView.SelectedItem as NatGeoImage;

            // It's recommended to use both SetBitmap and SetStorageItems for sharing a single image
            // since the target app may only support one or the other.
            var client = new HttpClient();
            var r = new HttpRequestMessage(
                HttpMethod.Get, item.DownloadUrl ?? item.ImageUrl);
            HttpResponseMessage response = await client.SendAsync(r,
                                                                  HttpCompletionOption.ResponseHeadersRead);

            var ims = new InMemoryRandomAccessStream();
            var imsWriter = ims.AsStreamForWrite();
            await response.Content.CopyToAsync(imsWriter);
            
            await LockScreen.SetImageStreamAsync(ims);

            ToashNotification("Lock screen set.");
        }

        private void ToashNotification(string message)
        {
            // Create a toast, then create a ToastNotifier object to show
            // the toast
            IToastNotificationContent toastContent = null;
            IToastText01 templateContent = ToastContentFactory.CreateToastText01();
            templateContent.TextBodyWrap.Text = message;
            toastContent = templateContent;

            ToastNotification toast = toastContent.CreateNotification();

            // If you have other applications in your package, you can specify the AppId of
            // the app to create a ToastNotifier for that application
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }

        async private void Save_Click(object sender, RoutedEventArgs e)
        {
            var item = flipView.SelectedItem as NatGeoImage;
            FileSavePicker savePicker = new FileSavePicker();
            savePicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;

            // Dropdown of file types the user can save the file as
            savePicker.FileTypeChoices.Add("Image", new List<string>() { ".jpg" });
            // Default file name if the user does not type one in or select a file to replace
            savePicker.SuggestedFileName = item.Title.Replace(" ", string.Empty).Replace(",", string.Empty).Replace(".", string.Empty);
            StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                // Prevent updates to the remote version of the file until we finish making changes and call CompleteUpdatesAsync.
                CachedFileManager.DeferUpdates(file);
                // write to file

                // It's recommended to use both SetBitmap and SetStorageItems for sharing a single image
                // since the target app may only support one or the other.
                var client = new HttpClient();
                var r = new HttpRequestMessage(
                    HttpMethod.Get, item.DownloadUrl ?? item.ImageUrl);
                HttpResponseMessage response = await client.SendAsync(r,
                                                                      HttpCompletionOption.ResponseHeadersRead);

                var bytes = await response.Content.ReadAsByteArrayAsync();
                await FileIO.WriteBytesAsync(file, bytes);

                ToashNotification("Image saved.");
            }
        }

        private void Image_Opened(object sender, RoutedEventArgs e)
        {
            var s = sender;
        }

        private void Image_Loaded(object sender, RoutedEventArgs e)
        {
            image = sender as Image;
        }
    }
}