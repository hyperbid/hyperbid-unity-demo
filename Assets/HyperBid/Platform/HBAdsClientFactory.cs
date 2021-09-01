using UnityEngine;
using HyperBid.Api;
using HyperBid.Common;

using System.Collections;
using System.Collections.Generic;

using System;

namespace HyperBid
{
    public class HBAdsClientFactory
    {
        public static IHBBannerAdClient BuildBannerAdClient()
        {
            #if UNITY_EDITOR
            // Testing UNITY_EDITOR first because the editor also responds to the currently
            // selected platform.
            #elif UNITY_ANDROID
                return new HyperBid.Android.HBBannerAdClient();
            #elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
                return new HyperBid.iOS.HBBannerAdClient();
            #else
                
            #endif
            return new UnityBannerClient();
        }

        public static IHBInterstitialAdClient BuildInterstitialAdClient()
        {
            #if UNITY_EDITOR
            // Testing UNITY_EDITOR first because the editor also responds to the currently
            // selected platform.
            #elif UNITY_ANDROID
                return new HyperBid.Android.HBInterstitialAdClient();
            #elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
                return new HyperBid.iOS.HBInterstitialAdClient();
            #else

            #endif
            return new UnityInterstitialClient();
        }

        public static IHBNativeAdClient BuildNativeAdClient()
        {
           #if UNITY_EDITOR
            // Testing UNITY_EDITOR first because the editor also responds to the currently
            // selected platform.
            #elif UNITY_ANDROID
                return new HyperBid.Android.HBNativeAdClient();
            #elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
                return new HyperBid.iOS.HBNativeAdClient();
            #else

            #endif
            return new UnityNativeAdClient();
        }

        public static IHBNativeBannerAdClient BuildNativeBannerAdClient()
        {
           #if UNITY_EDITOR
            // Testing UNITY_EDITOR first because the editor also responds to the currently
            // selected platform.
            #elif UNITY_ANDROID
                return new HyperBid.Android.HBNativeBannerAdClient();
            #elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
                return new HyperBid.iOS.HBNativeBannerAdClient();
            #else

            #endif
            return new UnityNativeBannerAdClient();
        }

        public static IHBRewardedVideoAdClient BuildRewardedVideoAdClient()
        {
            #if UNITY_EDITOR
            // Testing UNITY_EDITOR first because the editor also responds to the currently
            // selected platform.

            #elif UNITY_ANDROID
                return new HyperBid.Android.HBRewardedVideoAdClient();
            #elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
                return new HyperBid.iOS.HBRewardedVideoAdClient();            
            #else
                            
            #endif
            return new UnityRewardedVideoAdClient();
        }

        public static IHBSDKAPIClient BuildSDKAPIClient()
        {
            Debug.Log("BuildSDKAPIClient");
            #if UNITY_EDITOR
                Debug.Log("Unity Editor");
                        // Testing UNITY_EDITOR first because the editor also responds to the currently
                        // selected platform.

            #elif UNITY_ANDROID
                return new HyperBid.Android.HBSDKAPIClient();
            #elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
                 Debug.Log("Unity:HBAdsClientFactory::Build iOS Client");
                return new HyperBid.iOS.HBSDKAPIClient();         
            #else

            #endif
            return new UnitySDKAPIClient();
        }

    }

    class UnitySDKAPIClient:IHBSDKAPIClient
    {
        public void initSDK(string appId, string appkey){}
        public void initSDK(string appId, string appkey, HBSDKInitListener listener){ }
        public void getUserLocation(ATGetUserLocationListener listener){ }
        public void setGDPRLevel(int level){ }
        public void showGDPRAuth(){ }
        public void addNetworkGDPRInfo(int networkType, string mapJson){ }
        public void setChannel(string channel){ }
        public void setSubChannel(string subchannel){ }
        public void initCustomMap(string cutomMap){ }
        public void setCustomDataForPlacementID(string customData, string placementID){ }
        public void setLogDebug(bool isDebug){ }
        public void setNetworkTerritory(int territory){ }
        public int getGDPRLevel(){ return HBSDKAPI.PERSONALIZED; }
        public bool isEUTraffic() { return false; }
        public void deniedUploadDeviceInfo(string deniedInfo) { }
    }

    class UnityBannerClient:IHBBannerAdClient
    {
       HBBannerAdListener listener;
       public void loadBannerAd(string unitId, string mapJson){
            if(listener != null)
            {
                listener.onAdLoadFail(unitId, "-1", "Must run on Android or IOS platform!");
            }
       }
     
       public void setListener(HBBannerAdListener listener)
       {
            this.listener = listener;
       }

       public string checkAdStatus(string unitId) { return ""; }
       
       public void showBannerAd(string unitId, string position){ }

       public void showBannerAd(string unitId, string position, string mapJson){ }
       
       public void showBannerAd(string unitId, HBRect rect){ }

       public void showBannerAd(string unitId, HBRect rect, string mapJson){ }

       public  void cleanBannerAd(string unitId){ }
      
       public void hideBannerAd(string unitId){ }
    
       public void showBannerAd(string unitId){ }
      
       public void cleanCache(string unitId){}
   }

    class UnityInterstitialClient : IHBInterstitialAdClient
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

       public void loadInterstitialAd(string unitId, string mapJson){
            onAdLoadFailed?.Invoke(unitId, true, "Unsupported platform, HyperBid only supports Android and iOS");
       }
       
       public void setListener(HBInterstitialAdListener listener){
            this.listener = listener;
       }

       public bool hasInterstitialAdReady(string unitId) { return false; }

        public string checkAdStatus(string unitId) { return ""; }

        public void showInterstitialAd(string unitId, string mapJson){}
        
       public void cleanCache(string unitId){}
    }

    class UnityNativeAdClient : IHBNativeAdClient
    {
        HBNativeAdListener listener;
       public void loadNativeAd(string unitId, string mapJson){
            if(listener != null)
            {
                listener.onAdLoadFail(unitId, "-1", "Must run on Android or IOS platform!");
            }
       }

       public bool hasAdReady(string unitId) { return false; }

       public string checkAdStatus(string unitId) { return ""; }

       public void setListener(HBNativeAdListener listener){
            this.listener = listener;
       }
        
       public void renderAdToScene(string unitId, HBNativeAdView anyThinkNativeAdView){}

       public void renderAdToScene(string unitId, HBNativeAdView anyThinkNativeAdView, string mapJson){}

       public void cleanAdView(string unitId, HBNativeAdView anyThinkNativeAdView){}
       
       public void onApplicationForces(string unitId, HBNativeAdView anyThinkNativeAdView){}
        
       public void onApplicationPasue(string unitId, HBNativeAdView anyThinkNativeAdView){}
        
       public void cleanCache(string unitId){}
        
       public void setLocalExtra(string unitid, string mapJson){}
    }

    class UnityNativeBannerAdClient : IHBNativeBannerAdClient
    {
        HBNativeBannerAdListener listener;
       public void loadAd(string unitId, string mapJson){
            if(listener != null)
            {
                 listener.onAdLoadFail(unitId, "-1", "Must run on Android or IOS platform!");
            }
       }

       public bool adReady(string unitId) { return false; }
        
       public void setListener(HBNativeBannerAdListener listener){
            this.listener = listener;
       }
       
       public void showAd(string unitId, HBRect rect, Dictionary<string, string> pairs){}
        
       public void removeAd(string unitId){}
    }

    class UnityRewardedVideoAdClient : IHBRewardedVideoAdClient
    {
        HBRewardedVideoListener listener;
        public void loadVideoAd(string unitId, string mapJson){
            if (listener != null)
            {
                listener.onRewardedVideoAdLoadFail(unitId, "-1", "Must run on Android or IOS platform!");
            }
       }

        public void setListener(HBRewardedVideoListener listener){
            this.listener = listener;
       }

        public bool hasAdReady(string unitId) { return false; }

        public string checkAdStatus(string unitId) { return ""; }

        public void showAd(string unitId, string mapJson){}

    }
}