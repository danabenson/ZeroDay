using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NatGeoMetroApp.Data;
using Windows.Foundation;
using Windows.UI.Xaml.Data;

namespace NatGeoMetroApp.DataModel
{
    public class NatGeoImageCollection : ObservableCollection<NatGeoImage>, ISupportIncrementalLoading
    {
        public static List<NatGeoImage> AllItems = new List<NatGeoImage>();

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            return new NatGeoImageLoader(this, count, AllItems);
        }

        public bool HasMoreItems
        {
            get { return true; }
        }
    }
}