
using HyperBid.Common;


namespace HyperBid.Api
{
    public class HBDownloadManager
    {
        private static readonly HBDownloadManager instance = new HBDownloadManager();
        private IHBDownloadClient client;

        private HBDownloadManager()
        {
            client = GetHBDownloadClient();
        }

        public static HBDownloadManager Instance
        {
            get
            {
                return instance;
            }
        }

		public void setListener(HBDownloadAdListener listener)
        {   
            client.setListener(listener);
        }

        public IHBDownloadClient GetHBDownloadClient()
        {
            return HyperBid.HBAdsClientFactory.BuildDownloadClient();
        }

    }
}