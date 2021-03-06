﻿using System;
using System.Collections.Generic;
using System.Linq;
using NatGeoMetroApp.Common;
using NatGeoMetroApp.Data;
using NatGeoMetroApp.DataModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Grouped Items Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234231

namespace NatGeoMetroApp
{
    /// <summary>
    /// A page that displays a grouped collection of items.
    /// </summary>
    public sealed partial class GroupedItemsPage : LayoutAwarePage
    {
        public GroupedItemsPage()
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
            DefaultViewModel["Items"] = NatGeoDataSource.Items;
            NatGeoDataSource.Items.CollectionChanged += (sender, args) =>
                                                            {
                                                                Waiting.Visibility = Visibility.Collapsed;
                                                                itemGridView.Visibility = Visibility.Visible;
                                                                itemGridView.Focus(FocusState.Keyboard);
                                                            };
        }

        protected override void OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            if (NatGeoDataSource.Items.Count == 0)
            {
                Waiting.Visibility = Visibility.Visible;
                itemGridView.Visibility = Visibility.Collapsed;
            }
            else
            {
                Waiting.Visibility = Visibility.Collapsed;
                itemGridView.Visibility = Visibility.Visible;
            }
            itemGridView.Focus(FocusState.Keyboard);
            base.OnNavigatedTo(e);
        }

        /// <summary>
        /// Invoked when a group header is clicked.
        /// </summary>
        /// <param name="sender">The Button used as a group header for the selected group.</param>
        /// <param name="e">Event data that describes how the click was initiated.</param>
        private void Header_Click(object sender, RoutedEventArgs e)
        {
            // Determine what group the Button instance represents
            object group = (sender as FrameworkElement).DataContext;

            // Navigate to the appropriate destination page, configuring the new page
            // by passing required information as a navigation parameter
            //Frame.Navigate(typeof (GroupDetailPage), ((NatGeoDataGroup) group).UniqueId);
        }

        /// <summary>
        /// Invoked when an item within a group is clicked.
        /// </summary>
        /// <param name="sender">The GridView (or ListView when the application is snapped)
        /// displaying the item clicked.</param>
        /// <param name="e">Event data that describes the item clicked.</param>
        private void ItemView_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Navigate to the appropriate destination page, configuring the new page
            // by passing required information as a navigation parameter
            string itemId = ((NatGeoImage) e.ClickedItem).UniqueId;
            Frame.Navigate(typeof (ItemDetailPage), itemId);
        }
    }
}