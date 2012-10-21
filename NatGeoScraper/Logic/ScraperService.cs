using System.Timers;
using NatGeoScraper.Logic;
using ZeroDay.DAL.Contexts;
using ZeroDay.DAL.Repositories;
using log4net;

namespace NatGeoScraper
{
    public class ScraperService
    {
        private readonly ILog _logger;

        private Logic.NatGeoScraper _scraper;

        private Timer _timer;

        public ScraperService(ILog logger)
        {
            _logger = logger;
            _timer = new Timer(1000 * 60 * 60);
            _timer.Elapsed += (sender, args) => Scrape();
        }

        private void Scrape()
        {
            _logger.Info("Scraping.");
            if (_scraper == null)
            {
                _scraper = new Logic.NatGeoScraper(
                    new HtmlDocumentProvider(), 
                    new ImageRepository(new NatGeoContext()), new PhotoOfTheDayParser(),
                    _logger);
                _scraper.Scrape();
                _scraper = null;
            }
        }

        public void Start()
        {
            _logger.Info("Starting scraper.");
            _timer.Start();
            Scrape();
        }

        public void Stop()
        {
            _timer.Start();
        }
    }
}