using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web.Http;
using ZeroDay.API.Models;
using ZeroDay.DAL.Interfaces;
using ZeroDay.DAL.Models.NatGeo;

namespace ZeroDay.API.Controllers
{
    public class NatGeoImagesController : ApiController
    {
        private readonly IImageRepository _imageRepository;

        public NatGeoImagesController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public IEnumerable<NatGeoImage> Get()
        {
            var images = _imageRepository.GetAll().ToList();
            var models = images.Select(GetImage);
            return models;
        }

        private NatGeoImage GetImage(Image i)
        {
            var ii = new NatGeoImage();
            ii.Date = i.Date;
            ii.Description = i.Description;
            ii.DownloadUrl = i.DownloadUrl;
            ii.Id = i.Id;
            ii.Photographer = i.Photographer;
            ii.PhotographerUrl = i.PhotographerUrl;
            ii.ThumbnailUrl = i.ThumbnailUrl;
            ii.Title = i.Title;
            ii.Url = i.Url;
            return ii;
        }

        public Image Get(int id)
        {
            var image = _imageRepository.GetById(id);
            return image;
        }
    }
}