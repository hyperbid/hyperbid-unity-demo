using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HyperBid.Common;
using HyperBid.Api;
namespace HyperBid.Android
{
    public class HBRewardedVideoAdClient : AndroidJavaProxy,IHBRewardedVideoAdClient
    {

        private Dictionary<string, AndroidJavaObject> videoHelperMap = new Dictionary<string, AndroidJavaObject>();

		//private  AndroidJavaObject videoHelper;
        private  HBRewardedVideoListener anyThinkListener;

        public HBRewardedVideoAdClient() : base("com.hyperbid.unitybridge.videoad.VideoListener")
        {
            
        }


        public void loadVideoAd(string placementId, string mapJson)
        {

            //如果不存在则直接创建对应广告位的helper
            if(!videoHelperMap.ContainsKey(placementId))
            {
                AndroidJavaObject videoHelper = new AndroidJavaObject(
                    "com.hyperbid.unitybridge.videoad.VideoHelper", this);
                videoHelper.Call("initVideo", placementId);
                videoHelperMap.Add(placementId, videoHelper);
                Debug.Log("HBRewardedVideoAdClient : no exit helper ,create helper ");
            }

            try
            {
                Debug.Log("HBRewardedVideoAdClient : loadVideoAd ");
                videoHelperMap[placementId].Call("fillVideo", mapJson);
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Exception caught: {0}", e);
				Debug.Log ("HBRewardedVideoAdClient :  error."+e.Message);
            }


        }

        public void setListener(HBRewardedVideoListener listener)
        {
            anyThinkListener = listener;
        }

        public bool hasAdReady(string placementId)
        {
			bool isready = false;
			Debug.Log ("HBRewardedVideoAdClient : hasAdReady....");
			try{
                if (videoHelperMap.ContainsKey(placementId)) {
                    isready = videoHelperMap[placementId].Call<bool> ("isAdReady");
				}
			}catch(System.Exception e){
				System.Console.WriteLine("Exception caught: {0}", e);
				Debug.Log ("HBRewardedVideoAdClient :  error."+e.Message);
			}
			return isready; 
        }

        public string checkAdStatus(string placementId)
        {
            string adStatusJsonString = "";
            Debug.Log("HBRewardedVideoAdClient : checkAdStatus....");
            try
            {
                if (videoHelperMap.ContainsKey(placementId))
                {
                    adStatusJsonString = videoHelperMap[placementId].Call<string>("getReadyAdInfo");
                }
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Exception caught: {0}", e);
                Debug.Log("HBRewardedVideoAdClient :  error." + e.Message);
            }

            return adStatusJsonString;
        }

        public void showAd(string placementId, string scenario)
        {
			Debug.Log("HBRewardedVideoAdClient : showAd " );

			try{
                if (videoHelperMap.ContainsKey(placementId)) {
                    this.videoHelperMap[placementId].Call ("showVideo", scenario);
				}
			}catch(System.Exception e){
				System.Console.WriteLine("Exception caught: {0}", e);
				Debug.Log ("HBRewardedVideoAdClient :  error."+e.Message);

			}
        }


        //广告加载成功
        public void onRewardedVideoAdLoaded(string placementId)
        {
            Debug.Log("onRewardedVideoAdLoaded...unity3d.");
            if(anyThinkListener != null){
                anyThinkListener.onRewardedVideoAdLoaded(placementId);
            }
        }

        //广告加载失败
        public void onRewardedVideoAdFailed(string placementId,string code, string error)
        {
            Debug.Log("onRewardedVideoAdFailed...unity3d.");
            if (anyThinkListener != null)
            {
                anyThinkListener.onRewardedVideoAdLoadFail(placementId, code, error);
            }
        }

        //开始播放
        public void onRewardedVideoAdPlayStart(string placementId, string callbackJson)
        {
            Debug.Log("onRewardedVideoAdPlayStart...unity3d.");
            if (anyThinkListener != null)
            {
                anyThinkListener.onRewardedVideoAdPlayStart(placementId, new HBCallbackInfo(callbackJson));
            }
        }

        //结束播放
        public void onRewardedVideoAdPlayEnd(string placementId, string callbackJson)
        {
            Debug.Log("onRewardedVideoAdPlayEnd...unity3d.");
            if (anyThinkListener != null)
            {
                anyThinkListener.onRewardedVideoAdPlayEnd(placementId, new HBCallbackInfo(callbackJson));
            }
        }

        //播放失败
        public void onRewardedVideoAdPlayFailed(string placementId,string code, string error)
        {
            Debug.Log("onRewardedVideoAdPlayFailed...unity3d.");
            if (anyThinkListener != null)
            {
                anyThinkListener.onRewardedVideoAdPlayFail(placementId, code, error);
            }
        }
        //广告关闭
        public void onRewardedVideoAdClosed(string placementId,bool isRewarded, string callbackJson)
        {
            Debug.Log("onRewardedVideoAdClosed...unity3d.");
            if (anyThinkListener != null)
            {
                anyThinkListener.onRewardedVideoAdPlayClosed(placementId,isRewarded, new HBCallbackInfo(callbackJson));
            }
        }
        //广告点击
        public void onRewardedVideoAdPlayClicked(string placementId, string callbackJson)
        {
            Debug.Log("onRewardedVideoAdPlayClicked...unity3d.");
            if (anyThinkListener != null)
            {
                anyThinkListener.onRewardedVideoAdPlayClicked(placementId, new HBCallbackInfo(callbackJson));
            }
        }

        //广告激励下发
        public void onReward(string placementId, string callbackJson)
        {
            Debug.Log("onReward...unity3d.");
            if (anyThinkListener != null)
            {
                anyThinkListener.onReward(placementId, new HBCallbackInfo(callbackJson));
            }
        }
       
    }
}
