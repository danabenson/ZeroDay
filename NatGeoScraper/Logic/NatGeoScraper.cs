using System;
using NatGeoScraper.Interfaces;
using ZeroDay.DAL.Interfaces;
using ZeroDay.DAL.Models.NatGeo;
using log4net;

namespace NatGeoScraper.Logic
{
    public class NatGeoScraper : IScraper
    {
        private readonly IHtmlDocumentProvider _docProvider;

        private readonly IImageRepository _natGeoRepository;
        
        private readonly IPhotoOfTheDayParser _potdParser;
        private readonly ILog _logger;

        public NatGeoScraper(
            IHtmlDocumentProvider docProvider, 
            IImageRepository natGeoRepository,
            IPhotoOfTheDayParser potdParser, 
            ILog logger)
        {
            _docProvider = docProvider;
            _natGeoRepository = natGeoRepository;
            _potdParser = potdParser;
            _logger = logger;
        }

        public void Scrape()
        {
            var today = GetImageForDay("http://photography.nationalgeographic.com/photography/photo-of-the-day/", DateTime.Now.Date);
            try
            {
                Image img = today;
                while (true)
                {
                    img = GetImageForDay(img.PreviousDayUrl, img.Date.Date.AddDays(-1));
                }
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
            }
        }

        private Image GetImageForDay(string url, DateTime date)
        {
            var daily = _natGeoRepository.GetForDay(date);
            if (daily == null)
            {
                var doc = _docProvider.GetDocument(url);
                daily = _potdParser.Parse(doc, url);
                daily.Date = date;
                _natGeoRepository.Add(daily);
                _logger.Info("Got potd for " + daily.Date);
            }
            return daily;
        }
    }
}