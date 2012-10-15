using Topshelf;

namespace NatGeoScraper
{
    public class Program
    {
        static void Main(string[] args)
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
                x.SetDisplayName("NatGeo Scraper");                       
                x.SetServiceName("NatGeo Scraper");                       
            });  
        }
    }
}
