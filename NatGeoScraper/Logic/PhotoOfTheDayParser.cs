using System;
using System.Linq;
using HtmlAgilityPack;
using NatGeoScraper.Interfaces;
using ZeroDay.DAL.Models.NatGeo;

namespace NatGeoScraper.Logic
{
    public class PhotoOfTheDayParser : IPhotoOfTheDayParser
    {
        private Image image;

        private HtmlDocument doc;

        public PhotoOfTheDayParser()
        {
            image = new Image();
        }

        public Image Parse(HtmlDocument d, string url)
        {
            doc = d;
            HtmlNode podright = doc.GetElementbyId("pod_right");

            foreach (HtmlNode childNode in podright.ChildNodes)
            {
                if (childNode.GetAttributeValue("class", string.Empty) == "publication_time")
                {
                    string dateText = childNode.InnerText;
                    DateTime date = DateTime.Parse(dateText);
                    image.Date = date.Date;
                }
            }

            HtmlNode head = doc.GetElementbyId("page_head");

            foreach (HtmlNode childNode in head.ChildNodes)
            {
                if (childNode.Name == "h1")
                {
                    string title = childNode.InnerText;
                    image.Title = title;
                }
            }

            GetDownloadLink();

            HtmlNode primary =
                doc.DocumentNode.Descendants().First(
                    x => x.GetAttributeValue("class", string.Empty) == "primary_photo" && x.Name == "div");

            HtmlNode prevLink = primary.ChildNodes.First(x => x.Name == "a");
            
            string prevLinkRef = prevLink.GetAttributeValue("href", string.Empty);

            image.PreviousDayUrl = "http://photography.nationalgeographic.com" + prevLinkRef;

            HtmlNode potdImage = prevLink.ChildNodes.First(x => x.Name == "img");

            image.Description = potdImage.GetAttributeValue("alt", string.Empty);

            image.Url = potdImage.GetAttributeValue("src", string.Empty);

            GetPhotographer();   

            return image;
        }

        private void GetPhotographer()
        {
            var credit =
                doc.DocumentNode.Descendants().FirstOrDefault(
                    x => x.GetAttributeValue("class", string.Empty) == "credit");
            if (credit == null)
            {
                return;
            }


            var a = credit.ChildNodes.FirstOrDefault(x => x.Name == "a");

            if (a == null)
            {
              return;
            }

            image.Photographer = a.InnerText;
            image.PhotographerUrl = a.GetAttributeValue("href", string.Empty);
        }

        private void GetDownloadLink()
        {
            HtmlNode dl =
                doc.DocumentNode.Descendants().FirstOrDefault(x => x.GetAttributeValue("class", string.Empty) == "download_link");

            if (dl == null)
            {
                return;
            }
            HtmlNode a = dl.ChildNodes.FirstOrDefault();

            if (a == null)
            {
                return;
            }
                string link = a.GetAttributeValue("href", string.Empty);

                image.DownloadUrl = link;    
        }
    }
}