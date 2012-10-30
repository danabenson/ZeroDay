using System.Collections.Generic;
using System.Linq;
using NatGeoMetroApp.Data;

// The data model defined by this file serves as a representative example of a strongly-typed
// model that supports notification when members are added, removed, or modified.  The property
// names chosen coincide with data bindings in the standard item templates.
//
// Applications may use this model as a starting point and build on it, or discard it entirely and
// replace it with something appropriate to their needs.

namespace NatGeoMetroApp.DataModel
{
    /// <summary>
    /// Creates a collection of groups and items with hard-coded content.
    /// </summary>
    public static class NatGeoDataSource
    {
        private static readonly NatGeoImageCollection _allItems = new NatGeoImageCollection();

        static NatGeoDataSource()
        {
            var provider = new NatGeoImageProvider(_allItems);
            provider.GetImages();
        }

        public static NatGeoImageCollection Items
        {
            get { return _allItems; }
        }

        public static NatGeoImage GetItem(string uniqueId)
        {
            // Simple linear search is acceptable for small data sets
            IEnumerable<NatGeoImage> matches =
                NatGeoDataSource.Items.Where(
                    (item) => item.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }
    }
}