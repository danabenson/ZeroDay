using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroDay.DAL.Models.NatGeo;

namespace ZeroDay.DAL.Interfaces
{
    public interface IImageRepository : IRepository<Image>
    {
        Image GetForDay(DateTime date);
    }
}
