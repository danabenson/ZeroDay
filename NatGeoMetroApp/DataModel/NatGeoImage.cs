using System;

namespace NatGeoMetroApp.DataModel
{
    public class NatGeoImage : NatGeoDataCommon
    {
        public NatGeoImage(
            string uniqueId, 
            string title, 
            string imagePath, 
            string description)
            : base(uniqueId, title, imagePath, description)
        {
        }

        public string ImageUrl { get; set; }

        public string DownloadUrl { get; set; }

        public string PhotographerName { get; set; }

        public string PhotographerUrl { get; set; }

        public string Date { get; set; }
    }
}