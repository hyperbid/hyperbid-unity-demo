using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HyperBid.Common;
using HyperBid.Api;

namespace HyperBid.Android
{
    public class HBInterstitialAdClient : AndroidJavaProxy,IHBInterstitialAdClient
    {
        public event EventHandler<HBAdEventArgs> onAdLoad;	// called when the interstitial ad is loaded from the provider
        public event EventHandler<HBAdEventArgs> onAdLoadFailed;  // if no ad has been returned or a network error has occured
        public event EventHandler<HBAdEventArgs> onAdShow;  // called when the ad is shown
        public event EventHandler<HBAdEventArgs> onAdShowFailed;  // called if the ad has failed to be shown
        public event EventHandler<HBAdEventArgs> onAdClose;  // called when the ad is closed
        public event EventHandler<HBAdEventArgs> onAdClick;  // called when an user has clicked an ad
        public event EventHandler<HBAdEventArgs> onAdPlayVideo;  // called when a video ad has started playing
        public event EventHandler<HBAdEventArgs> onAdPlayVideoFailed;  // called if a video as has failed to be displayed
        public event EventHandler<HBAdEventArgs> onAdEndVideo;  // called when ad video has finished


        private Dictionary<string, AndroidJavaObject> interstitialHelperMap = new Dictionary<string, AndroidJavaObject>();

        public HBInterstitialAdClient() : base("com.hyperbid.unitybridge.interstitial.InterstitialListener")
        {
            
        }

        public void loadInterstitialAd(string placementId, string mapJson)
        {

            //如果不存在则直接创建对应广告位的helper
            if(!interstitialHelperMap.ContainsKey(placementId))
            {
                AndroidJavaObject videoHelper = new AndroidJavaObject(
                    "com.hyperbid.unitybridge.interstitial.InterstitialHelper", this);
                videoHelper.Call("initInterstitial", placementId);
                interstitialHelperMap.Add(placementId, videoHelper);
                Debug.Log("HBInterstitialAdClient : no exit helper ,create helper ");
            }

            try
            {
                Debug.Log("HBInterstitialAdClient : loadInterstitialAd ");
                interstitialHelperMap[placementId].Call("loadInterstitialAd", mapJson);
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Exception caught: {0}", e);
				Debug.Log ("HBInterstitialAdClient :  error."+e.Message);
            }


        }

        public void setListener(HBInterstitialAdListener listener)
        {
        }

        public bool hasInterstitialAdReady(string placementId)
        {
			bool isready = false;
			Debug.Log ("HBInterstitialAdClient : hasAdReady....");
			try{
                if (interstitialHelperMap.ContainsKey(placementId)) {
                    isready = interstitialHelperMap[placementId].Call<bool> ("isAdReady");
				}
			}catch(System.Exception e){
				System.Console.WriteLine("Exception caught: {0}", e);
				Debug.Log ("HBInterstitialAdClient :  error."+e.Message);
			}
			return isready; 
        }

        public string checkAdStatus(string placementId)
        {
            string adStatusJsonString = "";
            Debug.Log("HBInterstitialAdClient : checkAdStatus....");
            try
            {
                if (interstitialHelperMap.ContainsKey(placementId))
                {
                    adStatusJsonString = interstitialHelperMap[placementId].Call<string>("getReadyAdInfo");
                }
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Exception caught: {0}", e);
                Debug.Log("HBInterstitialAdClient :  error." + e.Message);
            }

            return adStatusJsonString;
        }

        public void showInterstitialAd(string placementId, string jsonmap)
        {
			Debug.Log("HBInterstitialAdClient : showAd " );

			try{
                if (interstitialHelperMap.ContainsKey(placementId)) {
                    this.interstitialHelperMap[placementId].Call ("showInterstitialAd", jsonmap);
				}
			}catch(System.Exception e){
				System.Console.WriteLine("Exception caught: {0}", e);
				Debug.Log ("HBInterstitialAdClient :  error."+e.Message);

			}
        }

        public void cleanCache(string placementId)
        {
			
			Debug.Log("HBInterstitialAdClient : clean" );

			try{
                if (interstitialHelperMap.ContainsKey(placementId)) {
                    this.interstitialHelperMap[placementId].Call ("clean");
				}
			}catch(System.Exception e){
				System.Console.WriteLine("Exception caught: {0}", e);
				Debug.Log ("HBInterstitialAdClient :  error."+e.Message);
			}
        }

        public void onApplicationForces(string placementId)
        {
			Debug.Log ("onApplicationForces.... ");
			try{
				if (interstitialHelperMap.ContainsKey(placementId)) {
					this.interstitialHelperMap[placementId].Call ("onResume");
				}
			}catch(System.Exception e){
				System.Console.WriteLine("Exception caught: {0}", e);
				Debug.Log ("HBInterstitialAdClient :  error."+e.Message);
			}
        }

        public void onApplicationPasue(string placementId)
        {
			Debug.Log ("onApplicationPasue.... ");
			try{
				if (interstitialHelperMap.ContainsKey(placementId)) {
					this.interstitialHelperMap[placementId].Call ("onPause");
				}
			}catch(System.Exception e){
				System.Console.WriteLine("Exception caught: {0}", e);
				Debug.Log ("HBInterstitialAdClient :  error."+e.Message);
			}
        }

        // the ad succesfully loaded
        // @param0: placement id used by your ad
        public void onInterstitialAdLoaded(string placementId)
        {
            Debug.Log("onInterstitialAdLoaded...unity3d.");
            onAdLoad?.Invoke(this, new HBAdEventArgs(placementId));
        }

        // the ad has failed to load
        // @param0: placement id used by your ad 
        public void onInterstitialAdLoadFail(string placementId,string code, string error)
        {
            Debug.Log("onInterstitialAdFailed...unity3d.");
            onAdLoadFailed?.Invoke(this, new HBAdEventArgs(placementId, true, error, code));
        }

        //开始播放
        public void onInterstitialAdVideoStart(string placementId, string callbackJson)
        {
            Debug.Log("onInterstitialAdPlayStart...unity3d.");
            onAdPlayVideo?.Invoke(this, new HBAdEventArgs(placementId, false, HBAdEventArgs.noValue, HBAdEventArgs.noValue, callbackJson));
        }

        //结束播放
        public void onInterstitialAdVideoEnd(string placementId, string callbackJson)
        {
            Debug.Log("onInterstitialAdPlayEnd...unity3d.");
            onAdEndVideo?.Invoke(this, new HBAdEventArgs(placementId, false, HBAdEventArgs.noValue, HBAdEventArgs.noValue, callbackJson));
        }

        //播放失败
        public void onInterstitialAdVideoError(string placementId,string code, string error)
        {
            Debug.Log("onInterstitialAdPlayFailed...unity3d.");
            onAdPlayVideoFailed?.Invoke(this, new HBAdEventArgs(placementId, true, error, code));
        }
        //广告关闭
        public void onInterstitialAdClose(string placementId, string callbackJson)
        {
            Debug.Log("onInterstitialAdClosed...unity3d.");
            onAdClose?.Invoke(this, new HBAdEventArgs(placementId, false, HBAdEventArgs.noValue, HBAdEventArgs.noValue, callbackJson));
        }
        //广告点击
        public void onInterstitialAdClicked(string placementId, string callbackJson)
        {
            Debug.Log("onInterstitialAdClicked...unity3d.");
            onAdClick?.Invoke(this, new HBAdEventArgs(placementId, false, HBAdEventArgs.noValue, HBAdEventArgs.noValue, callbackJson));
        }

        public void onInterstitialAdShow(string placementId, string callbackJson){
            Debug.Log("onInterstitialAdShow...unity3d.");
            onAdShow?.Invoke(this, new HBAdEventArgs(placementId, false, HBAdEventArgs.noValue, HBAdEventArgs.noValue, callbackJson));
        }
       
    }
}
