using System;
using NatGeoMetroApp.Common;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace NatGeoMetroApp.Data
{
    /// <summary>
    /// Base class for <see cref="NatGeoImage"/> and <see cref="NatGeoDataGroup"/> that
    /// defines properties common to both.
    /// </summary>
    [WebHostHidden]
    public abstract class NatGeoDataCommon : BindableBase
    {
        private static readonly Uri _baseUri = new Uri("ms-appx:///");
        
        private string _description = string.Empty;
        
        private ImageSource _image;
        
        private String _url;
        
        private string _subtitle = string.Empty;
        
        private string _title = string.Empty;

        private string _uniqueId = string.Empty;

        public NatGeoDataCommon(String uniqueId, String title, String subtitle, String url, String description)
        {
            _uniqueId = uniqueId;
            _title = title;
            _subtitle = subtitle;
            _description = description;
            _url = url;
        }

        public string UniqueId
        {
            get { return _uniqueId; }
            set { SetProperty(ref _uniqueId, value); }
        }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public string Subtitle
        {
            get { return _subtitle; }
            set { SetProperty(ref _subtitle, value); }
        }

        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        public ImageSource Image
        {
            get
            {
                if (_image == null && !string.IsNullOrWhiteSpace(_url))
                {
                    _image = new BitmapImage(new Uri(_url));
                }
                return _image;
            }

            set
            {
                _url = null;
                SetProperty(ref _image, value);
            }
        }

        public void SetImage(String path)
        {
            _image = null;
            _url = path;
            OnPropertyChanged("Image");
        }
    }
}