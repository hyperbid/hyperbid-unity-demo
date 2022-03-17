using UnityEngine;

using HyperBid.Common;
using HyperBid.Api;
namespace HyperBid.Android
{
    public class ATDownloadClient : AndroidJavaProxy,IHBDownloadClient
    {

        private AndroidJavaObject downloadHelper;


        private  HBDownloadAdListener anyThinkListener;

        public ATDownloadClient() : base("com.hyperbid.unitybridge.download.DownloadListener")
        {
            
        }

        public void setListener(HBDownloadAdListener listener)
        {
            Debug.Log("HBDownloadClient : setListener");
            anyThinkListener = listener;

            if (downloadHelper == null)
            {
                downloadHelper = new AndroidJavaObject(
                    "com.hyperbid.unitybridge.download.DownloadHelper", this);
            }

        }

        
        public void onDownloadStart(string placementId, string callbackJson, long totalBytes, long currBytes, string fileName, string appName)
        {
            Debug.Log("onDownloadStart...unity3d.");
            if(anyThinkListener != null){
                anyThinkListener.onDownloadStart(placementId, new HBCallbackInfo(callbackJson), totalBytes, currBytes, fileName, appName);
            }
        }

        
        public void onDownloadUpdate(string placementId, string callbackJson, long totalBytes, long currBytes, string fileName, string appName)
        {
            Debug.Log("onDownloadUpdate...unity3d.");
            if (anyThinkListener != null)
            {
                anyThinkListener.onDownloadUpdate(placementId, new HBCallbackInfo(callbackJson), totalBytes, currBytes, fileName, appName);
            }
        }

        
        public void onDownloadPause(string placementId, string callbackJson, long totalBytes, long currBytes, string fileName, string appName)
        {
            Debug.Log("onDownloadPause...unity3d.");
            if (anyThinkListener != null)
            {
                anyThinkListener.onDownloadPause(placementId, new HBCallbackInfo(callbackJson), totalBytes, currBytes, fileName, appName);
            }
        }

       
        public void onDownloadFinish(string placementId, string callbackJson, long totalBytes, string fileName, string appName)
        {
            Debug.Log("onDownloadFinish...unity3d.");
            if (anyThinkListener != null)
            {
                anyThinkListener.onDownloadFinish(placementId, new HBCallbackInfo(callbackJson), totalBytes, fileName, appName);
            }
        }

       
        public void onDownloadFail(string placementId, string callbackJson, long totalBytes, long currBytes, string fileName, string appName)
        {
            Debug.Log("onDownloadFail...unity3d.");
            if (anyThinkListener != null)
            {
                anyThinkListener.onDownloadFail(placementId, new HBCallbackInfo(callbackJson), totalBytes, currBytes, fileName, appName);
            }
        }
       

        public void onInstalled(string placementId, string callbackJson, string fileName, string appName)
        {
            Debug.Log("onInstalled...unity3d.");
            if (anyThinkListener != null)
            {
                anyThinkListener.onInstalled(placementId, new HBCallbackInfo(callbackJson), fileName, appName);
            }
        }
     
    }
}
