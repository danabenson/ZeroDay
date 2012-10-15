using System;
using System.Linq;
using ZeroDay.DAL.Contexts;
using ZeroDay.DAL.Models.NatGeo;

namespace test
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var context = new NatGeoContext())
            {
                var image = new Image();
                image.Title = "test";
                context.Images.Add(image);

                context.SaveChanges();

                var query = context.Images.ToList();

                foreach (var image1 in query)
                {
                    Console.WriteLine(image1.Title);
                }
                Console.ReadKey();
            }
        }
    }
}