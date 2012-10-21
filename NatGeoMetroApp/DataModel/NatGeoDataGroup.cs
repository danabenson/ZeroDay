using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace NatGeoMetroApp.Data
{
    /// <summary>
    /// Generic group data model.
    /// </summary>
    public class NatGeoDataGroup : NatGeoDataCommon
    {
        public NatGeoDataGroup(String uniqueId, String title, String subtitle, String imagePath, String description)
            : base(uniqueId, title, subtitle, imagePath, description)
        {
        }

        private ObservableCollection<NatGeoImage> _items = new ObservableCollection<NatGeoImage>();
        
        public ObservableCollection<NatGeoImage> Items
        {
            get { return this._items; }
        }
        
        public IEnumerable<NatGeoImage> TopItems
        {
            // Provides a subset of the full items collection to bind to from a GroupedItemsPage
            // for two reasons: GridView will not virtualize large items collections, and it
            // improves the user experience when browsing through groups with large numbers of
            // items.
            //
            // A maximum of 12 items are displayed because it results in filled grid columns
            // whether there are 1, 2, 3, 4, or 6 rows displayed
            get { return this._items.Take(12); }
        }
    }
}