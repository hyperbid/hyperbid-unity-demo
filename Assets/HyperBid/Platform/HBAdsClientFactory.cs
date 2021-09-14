using UnityEngine;
using HyperBid.Api;
using HyperBid.Common;

using System;
using System.Collections;
using System.Collections.Generic;

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
        public event EventHandler<HBAdEventArgs> onAdLoadEvent;
        public event EventHandler<HBAdErrorEventArgs> onAdLoadFailureEvent;
        public event EventHandler<HBAdEventArgs> onAdImpressEvent;
        public event EventHandler<HBAdEventArgs> onAdClickEvent;
        public event EventHandler<HBAdEventArgs> onAdAutoRefreshEvent;
        public event EventHandler<HBAdErrorEventArgs> onAdAutoRefreshFailureEvent;
        public event EventHandler<HBAdEventArgs> onAdCloseEvent;
        public event EventHandler<HBAdEventArgs> onAdCloseButtonTappedEvent;

        public void loadBannerAd(string unitId, string mapJson){
            onAdLoadFailureEvent?.Invoke(this, new HBAdErrorEventArgs(unitId, "Must run on the Android or IOS platform!", "-1"));
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
        public event EventHandler<HBAdEventArgs> onAdLoadEvent;
        public event EventHandler<HBAdErrorEventArgs> onAdLoadFailureEvent;
        public event EventHandler<HBAdEventArgs> onAdShowEvent;
        public event EventHandler<HBAdErrorEventArgs> onAdShowFailureEvent;
        public event EventHandler<HBAdEventArgs> onAdCloseEvent;
        public event EventHandler<HBAdEventArgs> onAdClickEvent;
        public event EventHandler<HBAdEventArgs> onAdVideoStartEvent;
        public event EventHandler<HBAdErrorEventArgs> onAdVideoFailureEvent;
        public event EventHandler<HBAdEventArgs> onAdVideoEndEvent;

        public void loadInterstitialAd(string unitId, string mapJson){
            onAdLoadFailureEvent?.Invoke(this, new HBAdErrorEventArgs(unitId, "Must run on the Android or IOS platform!", "-1"));
       }

       public bool hasInterstitialAdReady(string unitId) { return false; }

        public string checkAdStatus(string unitId) { return ""; }

        public void showInterstitialAd(string unitId, string mapJson){}
        
       public void cleanCache(string unitId){}
    }

    class UnityNativeAdClient : IHBNativeAdClient
    {
        public event EventHandler<HBAdEventArgs> onAdLoadEvent;
        public event EventHandler<HBAdErrorEventArgs> onAdLoadFailureEvent;
        public event EventHandler<HBAdEventArgs> onAdImpressEvent;
        public event EventHandler<HBAdEventArgs> onAdClickEvent;
        public event EventHandler<HBAdEventArgs> onAdVideoStartEvent;
        public event EventHandler<HBAdEventArgs> onAdVideoEndEvent;
        public event EventHandler<HBAdProgressEventArgs> onAdVideoProgressEvent;
        public event EventHandler<HBAdEventArgs> onAdCloseEvent;

        public void loadNativeAd(string unitId, string mapJson){
            onAdLoadFailureEvent?.Invoke(this, new HBAdErrorEventArgs(unitId,  "Must run on the Android or IOS platform!", "-1"));
       }

       public bool hasAdReady(string unitId) { return false; }

       public string checkAdStatus(string unitId) { return ""; }

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
        public event EventHandler<HBAdEventArgs> onAdLoadEvent;
        public event EventHandler<HBAdErrorEventArgs> onAdLoadFailureEvent;
        public event EventHandler<HBAdEventArgs> onAdImpressEvent;
        public event EventHandler<HBAdEventArgs> onAdClickEvent;
        public event EventHandler<HBAdEventArgs> onAdAutoRefreshEvent;
        public event EventHandler<HBAdErrorEventArgs> onAdAutoRefreshFailureEvent;
        public event EventHandler<HBAdEventArgs> onAdCloseButtonClickEvent;

        public void loadAd(string unitId, string mapJson){
            onAdLoadFailureEvent?.Invoke(this, new HBAdErrorEventArgs(unitId, "Must run on the Android or IOS platform!", "-1"));
       }

       public bool adReady(string unitId) { return false; }
       
       public void showAd(string unitId, HBRect rect, Dictionary<string, string> pairs){}
        
       public void removeAd(string unitId){}
    }

    class UnityRewardedVideoAdClient : IHBRewardedVideoAdClient
    {
        public event EventHandler<HBAdEventArgs> onAdLoadEvent;
        public event EventHandler<HBAdErrorEventArgs> onAdLoadFailureEvent;
        public event EventHandler<HBAdEventArgs> onAdVideoStartEvent;
        public event EventHandler<HBAdEventArgs> onAdVideoEndEvent;
        public event EventHandler<HBAdErrorEventArgs> onAdVideoFailureEvent;
        public event EventHandler<HBAdRewardEventArgs> onAdVideoCloseEvent;
        public event EventHandler<HBAdEventArgs> onAdClickEvent;
        public event EventHandler<HBAdRewardEventArgs> onRewardEvent;

        public void loadVideoAd(string unitId, string mapJson){
            onAdLoadFailureEvent?.Invoke(this, new HBAdErrorEventArgs(unitId, "Must run on the Android or IOS platform!", "-1"));
        }

        public bool hasAdReady(string unitId) { return false; }

        public string checkAdStatus(string unitId) { return ""; }

        public void showAd(string unitId, string mapJson){}

    }
}