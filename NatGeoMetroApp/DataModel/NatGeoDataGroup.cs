using System.Collections.Generic;

namespace NatGeoMetroApp.DataModel
{
    /// <summary>
    /// Generic group data model.
    /// </summary>
    public class NatGeoDataGroup : NatGeoDataCommon
    {
        public NatGeoDataGroup(string uniqueId, string title, string imagePath, string description)
            : base(uniqueId, title, imagePath, description)
        {
            Items = new List<NatGeoImage>();
            ItemCollection = new NatGeoImageCollection();
        }

        public List<NatGeoImage> Items { get; set; }

        public NatGeoImageCollection ItemCollection { get; set; }
    }
}