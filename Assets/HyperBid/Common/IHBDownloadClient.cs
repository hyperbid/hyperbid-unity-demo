
using HyperBid.Api;

namespace HyperBid.Common
{
    public interface IHBDownloadClient
    {
		
		/**
		 * @param listener 
		 */ 
        void setListener(HBDownloadAdListener listener);
	}
}
