using System.Data.Entity;
using ZeroDay.DAL.Models.NatGeo;

namespace ZeroDay.DAL.Contexts
{
    public class NatGeoContext : DbContext
    {
        public NatGeoContext()
            : base("NatGeo")
        {

        }

        public DbSet<Image> Images { get; set; }

        public DbSet<Category> Categories { get; set; }
    }
}