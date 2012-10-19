using HtmlAgilityPack;
using ZeroDay.DAL.Models.NatGeo;

namespace NatGeoScraper.Interfaces
{
    public interface IPhotoOfTheDayParser
    {
        Image Parse(HtmlDocument doc, string url);
    }
}