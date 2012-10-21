using System;
using Topshelf;
using log4net;

namespace NatGeoScraper
{
    public class Program
    {
        private static log4net.ILog log;

        static void Main(string[] args)
        {
            log = LogManager.GetLogger(typeof(Program));
            log4net.Config.XmlConfigurator.Configure();
            
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
                    s.ConstructUsing(name => new ScraperService(log));
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
