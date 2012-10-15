using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ZeroDay.DAL.Models.NatGeo
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual List<Image> Images { get; set; }
    }
}