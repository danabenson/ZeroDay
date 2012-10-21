using System;

namespace NatGeoMetroApp.Data
{
    public class NatGeoImage : NatGeoDataCommon
    {
        private string _content = string.Empty;

        private NatGeoDataGroup _group;

        public NatGeoImage(String uniqueId, String title, String subtitle, String imagePath, String description,
                           String content, NatGeoDataGroup group)
            : base(uniqueId, title, subtitle, imagePath, description)
        {
            _content = content;
            _group = group;
        }

        public string Content
        {
            get { return _content; }
            set { SetProperty(ref _content, value); }
        }

        public NatGeoDataGroup Group
        {
            get { return _group; }
            set { SetProperty(ref _group, value); }
        }
    }
}