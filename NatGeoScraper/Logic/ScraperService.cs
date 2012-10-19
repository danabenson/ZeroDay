using System.Timers;
using NatGeoScraper.Logic;
using ZeroDay.DAL.Contexts;
using ZeroDay.DAL.Repositories;

namespace NatGeoScraper
{
    public class ScraperService
    {
        private readonly Logic.NatGeoScraper _scraper;

        private readonly Timer _timer;

        public ScraperService()
        {
            _timer = new Timer(1000 * 60 * 60);
            _scraper = new Logic.NatGeoScraper(new HtmlDocumentProvider(), new ImageRepository(new NatGeoContext()), new PhotoOfTheDayParser());
            _timer.Elapsed += (sender, args) => _scraper.Scrape();
        }

        public void Start()
        {
            _timer.Start();
            _scraper.Scrape();
        }

        public void Stop()
        {
            _timer.Start();
        }
    }
}