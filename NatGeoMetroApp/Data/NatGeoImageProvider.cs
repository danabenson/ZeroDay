using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using NatGeoMetroApp.Common;

namespace NatGeoMetroApp.Data
{
    public class NatGeoImageProvider
    {
        private const string host = "http://ec2-184-73-116-39.compute-1.amazonaws.com/api/NatGeoImages";
        private readonly NatGeoDataGroup _potdGroup;

        private HttpClient _client;

        public NatGeoImageProvider(NatGeoDataGroup potdGroup)
        {
            _potdGroup = potdGroup;
        }

        public async void GetImages()
        {
            _client = new HttpClient();
            HttpResponseMessage response = await _client.GetAsync(host);
            string textRespose = await response.Content.ReadAsStringAsync();

            var items = textRespose.FromJson<List<Image>>();

            foreach (var image in items)
            {
                var oimage = new NatGeoImage(image.Id.ToString(), image.Title, string.Empty, image.Url, image.Description,
                                            string.Empty, _potdGroup);
                _potdGroup.Items.Add(oimage);
            }
        }
    }
}