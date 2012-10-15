using System.Timers;

namespace NatGeoScraper
{
    public class ScraperService
    {
        private readonly Logic.NatGeoScraper _scraper;
        private readonly Timer _timer;

        public ScraperService()
        {
            _timer = new Timer(1000*60*60);
            _scraper = new Logic.NatGeoScraper();
            _timer.Elapsed += (sender, args) => _scraper.Scrape();
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Start();
        }
    }
}