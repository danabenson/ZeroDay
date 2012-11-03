using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using NatGeoMetroApp.Common;
using NatGeoMetroApp.DataModel;

namespace NatGeoMetroApp.Data
{
    public class NatGeoImageProvider
    {
        private const string host = "http://ec2-184-73-116-39.compute-1.amazonaws.com/api/NatGeoImages";

        private readonly NatGeoImageCollection _natGeoImageCollection;

        private HttpClient _client;

        public NatGeoImageProvider(NatGeoImageCollection natGeoImageCollection)
        {
            _natGeoImageCollection = natGeoImageCollection;
        }

        public async void GetImages()
        {
            _client = new HttpClient();
            HttpResponseMessage response = await _client.GetAsync(host);
            string textRespose = await response.Content.ReadAsStringAsync();

            var items = textRespose.FromJson<List<Image>>().OrderByDescending(x=>x.ParsedDate);

            foreach (Image image in items)
            {
                var oimage = new NatGeoImage(
                    image.Id.ToString(),
                    image.Title,
                    image.Url,
                    image.Description);
                oimage.DownloadUrl = image.DownloadUrl;
                oimage.ImageUrl = image.Url;
                oimage.PhotographerName = image.Photographer;
                oimage.PhotographerUrl = image.PhotographerUrl;
                oimage.Date = image.ParsedDate.ToString("MMMM dd, yyyy");
                NatGeoImageCollection.AllItems.Add(oimage);
                _natGeoImageCollection.Add(oimage);
            }

        }
    }
}