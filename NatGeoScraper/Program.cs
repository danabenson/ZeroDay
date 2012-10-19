using System;
using Topshelf;

namespace NatGeoScraper
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                StartService();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }

        private static void StartService()
        {
            HostFactory.Run(x =>
            {
                x.Service<ScraperService>(s =>
                {
                    s.ConstructUsing(name => new ScraperService());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("Scraping Data From Nat Geo");
                x.SetDisplayName("NatGeoScraper");
                x.SetServiceName("NatGeoScraper");
            });
        }
    }
}
