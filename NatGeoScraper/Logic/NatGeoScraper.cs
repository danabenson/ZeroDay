using System;
using NatGeoScraper.Interfaces;
using ZeroDay.DAL.Interfaces;

namespace NatGeoScraper.Logic
{
    public class NatGeoScraper : IScraper
    {
        private readonly IHtmlDocumentProvider _docProvider;

        private readonly IImageRepository _natGeoRepository;
        
        private readonly IPhotoOfTheDayParser _potdParser;

        public NatGeoScraper(
            IHtmlDocumentProvider docProvider, 
            IImageRepository natGeoRepository,
            IPhotoOfTheDayParser potdParser)
        {
            _docProvider = docProvider;
            _natGeoRepository = natGeoRepository;
            _potdParser = potdParser;
        }

        public void Scrape()
        {
            GetDaily();
        }

        private void GetDaily()
        {
            var daily = _natGeoRepository. GetForDay(DateTime.Now);
            if (daily == null)
            {
                var url = "http://photography.nationalgeographic.com/photography/photo-of-the-day/";
                var doc =
                    _docProvider.GetDocument(url);
                var image = _potdParser.Parse(doc, url);
                _natGeoRepository.Update(image);
            }
        }

        private void GetPrevious()
        {
        }

        private void GetImageForDay(DateTime date)
        {
        
        }
    }
}