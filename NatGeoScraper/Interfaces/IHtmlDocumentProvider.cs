using System;
using HtmlAgilityPack;

namespace NatGeoScraper.Interfaces
{
    public interface IHtmlDocumentProvider
    {
        HtmlDocument GetDocument(string url);
    }
}