using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZeroDay.API.Models
{
    public class NatGeoImage
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public string ThumbnailUrl { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Date { get; set; }

        public string DownloadUrl { get; set; }

        public string Photographer { get; set; }

        public string PhotographerUrl { get; set; }
    }
}