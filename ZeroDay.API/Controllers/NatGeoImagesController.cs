using System.Collections.Generic;
using System.Web.Http;
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

        public IEnumerable<Image> Get()
        {
            var images = _imageRepository.GetAll();
            return images;
        }

        public Image Get(int id)
        {
            var image = _imageRepository.GetById(id);
            return image;
        }
    }
}