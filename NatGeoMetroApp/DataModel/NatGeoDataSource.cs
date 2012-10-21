using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

// The data model defined by this file serves as a representative example of a strongly-typed
// model that supports notification when members are added, removed, or modified.  The property
// names chosen coincide with data bindings in the standard item templates.
//
// Applications may use this model as a starting point and build on it, or discard it entirely and
// replace it with something appropriate to their needs.

namespace NatGeoMetroApp.Data
{
    /// <summary>
    /// Creates a collection of groups and items with hard-coded content.
    /// </summary>
    public sealed class NatGeoDataSource
    {
        private static readonly NatGeoDataSource _sampleDataSource = new NatGeoDataSource();

        private readonly ObservableCollection<NatGeoDataGroup> _allGroups = new ObservableCollection<NatGeoDataGroup>();

        public NatGeoDataSource()
        {
            var potd = new NatGeoDataGroup(
                "PicturesOfTheDay",
                "Pictures of the Day",
                string.Empty,
                string.Empty,
                string.Empty);
            _allGroups.Add(potd);
            var provider = new NatGeoImageProvider(potd);
            provider.GetImages();
        }

        public ObservableCollection<NatGeoDataGroup> AllGroups
        {
            get { return _allGroups; }
        }

        public static IEnumerable<NatGeoDataGroup> GetGroups(string uniqueId)
        {
            if (!uniqueId.Equals("AllGroups"))
                throw new ArgumentException("Only 'AllGroups' is supported as a collection of groups");
            return _sampleDataSource.AllGroups;
        }

        public static NatGeoDataGroup GetGroup(string uniqueId)
        {
            // Simple linear search is acceptable for small data sets
            IEnumerable<NatGeoDataGroup> matches =
                _sampleDataSource.AllGroups.Where((group) => group.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        public static NatGeoImage GetItem(string uniqueId)
        {
            // Simple linear search is acceptable for small data sets
            IEnumerable<NatGeoImage> matches =
                _sampleDataSource.AllGroups.SelectMany(group => group.Items).Where(
                    (item) => item.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }
    }
}