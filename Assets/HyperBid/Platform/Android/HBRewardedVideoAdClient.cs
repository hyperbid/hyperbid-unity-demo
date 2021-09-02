using System;
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

		public event EventHandler<HBAdEventArgs> onVideoAdLoaded;       // triggers when a rewarded video has been loaded
        public event EventHandler<HBAdEventArgs> onVideoAdLoadFail;     // triggers when a rewarded video has failed to load (or none have been returned)
        public event EventHandler<HBAdEventArgs> onVideoAdPlayStart;    // triggers on video start
        public event EventHandler<HBAdEventArgs> onVideoAdPlayEnd;      // triggers on video end
        public event EventHandler<HBAdEventArgs> onVideoAdPlayFail;     // triggers if the video fails to play
        public event EventHandler<HBAdEventArgs> onVideoAdPlayClosed;   // triggers when the user has closed the ad
        public event EventHandler<HBAdEventArgs> onVideoAdPlayClicked;  // triggers when the user has clicked the ad
        public event EventHandler<HBAdEventArgs> onGiveReward;          // triggers when the user has finsihed watching the ad and should be rewarded

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
            onRewardedVideoAdLoaded(placementId);
            
        }

        //广告加载失败
        public void onRewardedVideoAdFailed(string placementId,string code, string error)
        {
            Debug.Log("onRewardedVideoAdFailed...unity3d.");
            onVideoAdLoadFail?.Invoke(this, new HBAdEventArgs(placementId, true, code, error));
        }

        //开始播放
        public void onRewardedVideoAdPlayStart(string placementId, string callbackJson)
        {
            Debug.Log("onRewardedVideoAdPlayStart...unity3d.");
            onVideoAdPlayStart?.Invoke(this, new HBAdEventArgs(placementId, false, HBAdEventArgs.noValue, HBAdEventArgs.noValue, callbackJson));
        }

        //结束播放
        public void onRewardedVideoAdPlayEnd(string placementId, string callbackJson)
        {
            Debug.Log("onRewardedVideoAdPlayEnd...unity3d.");
            onVideoAdPlayEnd?.Invoke(this, new HBAdEventArgs(placementId, false, HBAdEventArgs.noValue, HBAdEventArgs.noValue, callbackJson));
        }

        //播放失败
        public void onRewardedVideoAdPlayFailed(string placementId,string code, string error)
        {
            Debug.Log("onRewardedVideoAdPlayFailed...unity3d.");
            onVideoAdPlayFail?.Invoke(this, new HBAdEventArgs(placementId, true, error, code));
        }
        //广告关闭
        public void onRewardedVideoAdClosed(string placementId,bool isRewarded, string callbackJson)
        {
            Debug.Log("onRewardedVideoAdClosed...unity3d.");
            onVideoAdPlayClosed?.Invoke(this, new HBAdEventArgs(placementId, false, HBAdEventArgs.noValue, HBAdEventArgs.noValue, callbackJson, isRewarded));
        }
        //广告点击
        public void onRewardedVideoAdPlayClicked(string placementId, string callbackJson)
        {
            Debug.Log("onRewardedVideoAdPlayClicked...unity3d.");
            onVideoAdPlayClicked?.Invoke(this, new HBAdEventArgs(placementId, false, HBAdEventArgs.noValue, HBAdEventArgs.noValue, callbackJson));
        }

        //广告激励下发
        public void onReward(string placementId, string callbackJson)
        {
            Debug.Log("onReward...unity3d.");
            onGiveReward?.Invoke(this, new HBAdEventArgs(placementId, false, HBAdEventArgs.noValue, HBAdEventArgs.noValue, callbackJson));
        }
       
    }
}
