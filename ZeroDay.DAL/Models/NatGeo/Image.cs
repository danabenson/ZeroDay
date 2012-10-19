using System;
using System.ComponentModel.DataAnnotations;

namespace ZeroDay.DAL.Models.NatGeo
{
    public class Image
    {
        [Key]
        public int Id { get; set; }

        public string Url { get; set; }

        public string ThumbnailUrl { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public virtual Category Category { get; set; }

        public DateTime Date { get; set; }

        public string PreviousDayUrl { get; set; }

        public string DownloadUrl { get; set; }

        public string Photographer { get; set; }

        public string PhotographerUrl { get; set; }
    }
}