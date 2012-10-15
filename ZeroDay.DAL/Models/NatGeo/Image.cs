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
    }
}