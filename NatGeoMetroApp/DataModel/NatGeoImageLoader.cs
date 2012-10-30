using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Data;

namespace NatGeoMetroApp.DataModel
{
    public class NatGeoImageLoader : IAsyncOperation<LoadMoreItemsResult>
    {
        private readonly List<NatGeoImage> _allItems;

        private AsyncStatus _asyncStatus = AsyncStatus.Started;
    
        private LoadMoreItemsResult _results;

        public NatGeoImageLoader(Collection<NatGeoImage> collection, uint page, List<NatGeoImage> allItems)
        {
            _allItems = allItems;
            DoStuff(collection, page);
        }

        public AsyncOperationCompletedHandler<LoadMoreItemsResult> Completed { get; set; }

        public LoadMoreItemsResult GetResults()
        {
            return _results;
        }

        public void Cancel()
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
        }

        public Exception ErrorCode
        {
            get { throw new NotImplementedException(); }
        }

        public uint Id
        {
            get { throw new NotImplementedException(); }
        }

        public AsyncStatus Status
        {
            get { return _asyncStatus; }
        }

        public async Task DoStuff(Collection<NatGeoImage> collection, uint page)
        {
            if (_allItems.Count == 0)
            {
                return;
            }

            IEnumerable<NatGeoImage> items = _allItems.Skip((int) ((page - 1)*100)).Take(100);

            foreach (NatGeoImage mediaItem in items)
            {
                collection.Add(mediaItem);
            }

            _results.Count = (uint) items.Count();
            _asyncStatus = AsyncStatus.Completed;
            if (Completed != null) Completed(this, _asyncStatus);
        }
    }
}