using System;
using System.IO;
using System.Net;
using HtmlAgilityPack;
using NatGeoScraper.Interfaces;

namespace NatGeoScraper.Logic
{
    public class HtmlDocumentProvider : IHtmlDocumentProvider
    {
        private readonly WebClient webClient;

        public HtmlDocumentProvider()
        {
            webClient = new WebClient();
        }

        public HtmlDocument GetDocument(string url)
        {
            var uri = new Uri(url);
            var doc = new HtmlDocument();
            string content = webClient.DownloadString(uri);
            using (var sr = new StringReader(content))
            {
                doc.Load(sr);
            }
            return doc;
        }
    }
}