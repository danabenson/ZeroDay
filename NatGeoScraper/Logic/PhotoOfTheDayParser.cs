using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using NatGeoScraper.Interfaces;
using ZeroDay.DAL.Models.NatGeo;

namespace NatGeoScraper.Logic
{
    public class PhotoOfTheDayParser : IPhotoOfTheDayParser
    {
        public Image Parse(HtmlDocument doc, string url)
        {
            var image = new Image();

            var podright = doc.GetElementbyId("pod_right");

            foreach (var childNode in podright.ChildNodes)
            {
                if (childNode.GetAttributeValue("class", string.Empty) == "publication_time")
                {
                    var dateText = childNode.InnerText;
                    var date = DateTime.Parse(dateText);
                    image.Date = date.Date;
                }
            }


            var head = doc.GetElementbyId("page_head");

            foreach (var childNode in head.ChildNodes)
            {
                if (childNode.Name == "h1")
                {
                    var title = childNode.InnerText;
                    image.Title = title;
                }
            }

            var dl = doc.DocumentNode.Descendants().First(x => x.GetAttributeValue("class", string.Empty) == "download_link");

            var a = dl.ChildNodes.First();
            var link = a.GetAttributeValue("href", string.Empty);

            image.DownloadUrl = link;

            var primary = doc.DocumentNode.Descendants().First(x => x.GetAttributeValue("class", string.Empty) =="primary_photo" && x.Name == "div");

           var prevLink = primary.ChildNodes.First(x=>x.Name == "a");

           var prevLinkRef = prevLink.GetAttributeValue("href", string.Empty);

           image.PreviousDayUrl = "http://photography.nationalgeographic.com" +  prevLinkRef;

           var potdImage = prevLink.ChildNodes.First(x=>x.Name == "img");

           image.Description = potdImage.GetAttributeValue("alt", string.Empty);

           image.Url = potdImage.GetAttributeValue("src", string.Empty);

            return image;
        }
    }
}
