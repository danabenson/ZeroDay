using System;
using System.Linq;
using ZeroDay.DAL.Contexts;
using ZeroDay.DAL.Interfaces;
using ZeroDay.DAL.Models.NatGeo;

namespace ZeroDay.DAL.Repositories
{
    public class ImageRepository : Repository<Image>, IImageRepository
    {
        private readonly NatGeoContext _context;

        public ImageRepository(NatGeoContext context) : base(context)
        {
            _context = context;
        }

        public Image GetForDay(DateTime date)
        {
            return _context.Images.FirstOrDefault(x => x.Date == date.Date);
        }
    }
}