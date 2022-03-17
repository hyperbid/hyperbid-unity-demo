

namespace HyperBid.Api
{
    public interface HBDownloadAdListener
    {
		
        void onDownloadStart(string placementId, HBCallbackInfo callbackInfo, long totalBytes, long currBytes, string fileName, string appName);
        
        void onDownloadUpdate(string placementId, HBCallbackInfo callbackInfo, long totalBytes, long currBytes, string fileName, string appName);
        
        void onDownloadPause(string placementId, HBCallbackInfo callbackInfo, long totalBytes, long currBytes, string fileName, string appName);
		
        void onDownloadFinish(string placementId, HBCallbackInfo callbackInfo, long totalBytes, string fileName, string appName);
        
        void onDownloadFail(string placementId, HBCallbackInfo callbackInfo, long totalBytes, long currBytes, string fileName, string appName);
        
        void onInstalled(string placementId, HBCallbackInfo callbackInfo, string fileName, string appName);
       
    }
}
