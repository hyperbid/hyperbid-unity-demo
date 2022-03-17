using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HyperBid.Common;
using HyperBid.Api;
using HyperBid.ThirdParty.LitJson;
namespace HyperBid.Android
{
    public class HBInterstitialAdClient : AndroidJavaProxy, IHBInterstitialAdClient
    {

        private Dictionary<string, AndroidJavaObject> interstitialHelperMap = new Dictionary<string, AndroidJavaObject>();

		//private  AndroidJavaObject videoHelper;
		public event EventHandler<HBAdEventArgs>        onAdLoadEvent;
        public event EventHandler<HBAdErrorEventArgs>   onAdLoadFailureEvent;
        public event EventHandler<HBAdEventArgs>        onAdShowEvent;
        public event EventHandler<HBAdErrorEventArgs>   onAdShowFailureEvent;
        public event EventHandler<HBAdEventArgs>        onAdCloseEvent;
        public event EventHandler<HBAdEventArgs>        onAdClickEvent;
        public event EventHandler<HBAdEventArgs>        onAdVideoStartEvent;
        public event EventHandler<HBAdErrorEventArgs>   onAdVideoFailureEvent;
        public event EventHandler<HBAdEventArgs>        onAdVideoEndEvent;
        public event EventHandler<HBAdEventArgs>        onAdStartLoadSource;
        public event EventHandler<HBAdEventArgs>        onAdFinishLoadSource;
        public event EventHandler<HBAdErrorEventArgs>   onAdFailureLoadSource;
        public event EventHandler<HBAdEventArgs>        onAdStartBidding;
        public event EventHandler<HBAdEventArgs>        onAdFinishBidding;
        public event EventHandler<HBAdErrorEventArgs>   onAdFailBidding;

        private AndroidJavaObject interstitialAutoAdHelper;

        public HBInterstitialAdClient() : base("com.hyperbid.unitybridge.interstitial.InterstitialListener")
        {
            interstitialAutoAdHelper = new AndroidJavaObject("com.hyperbid.unitybridge.interstitial.InterstitialAutoAdHelper", this);
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
                    adStatusJsonString = interstitialHelperMap[placementId].Call<string>("checkAdStatus");
                }
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Exception caught: {0}", e);
                Debug.Log("HBInterstitialAdClient :  error." + e.Message);
            }

            return adStatusJsonString;
        }
        
        public void entryScenarioWithPlacementID(string placementId, string scenarioID){
            Debug.Log("HBInterstitialAdClient : entryScenarioWithPlacementID....");
            try
            {
                if (interstitialHelperMap.ContainsKey(placementId))
                {
                    interstitialHelperMap[placementId].Call("entryAdScenario", scenarioID);
                }
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Exception caught: {0}", e);
                Debug.Log("HBInterstitialAdClient entryScenarioWithPlacementID:  error." + e.Message);
            }


        }


        public string getValidAdCaches(string placementId)
        {
            string validAdCachesString = "";
            Debug.Log("HBNativeAdClient : getValidAdCaches....");
            try
            {
                if (interstitialHelperMap.ContainsKey(placementId))
                {
                    validAdCachesString = interstitialHelperMap[placementId].Call<string>("getValidAdCaches");
                }
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Exception caught: {0}", e);
                Debug.Log("HBNativeAdClient :  error." + e.Message);
            }

            return validAdCachesString;
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
            onAdLoadEvent?.Invoke(this, new HBAdEventArgs(placementId));
        }

        // the ad has failed to load
        // @param0: placement id used by your ad 
        public void onInterstitialAdLoadFail(string placementId,string code, string error)
        {
            Debug.Log("onInterstitialAdFailed...unity3d.");
            onAdLoadFailureEvent?.Invoke(this, new HBAdErrorEventArgs(placementId, error, code));
        }

        //开始播放
        public void onInterstitialAdVideoStart(string placementId, string callbackJson)
        {
            Debug.Log("onInterstitialAdPlayStart...unity3d.");
            onAdVideoStartEvent?.Invoke(this, new HBAdEventArgs(placementId, callbackJson));
        }

        //结束播放
        public void onInterstitialAdVideoEnd(string placementId, string callbackJson)
        {
            Debug.Log("onInterstitialAdPlayEnd...unity3d.");
            onAdVideoEndEvent?.Invoke(this, new HBAdEventArgs(placementId, callbackJson));
        }

        //播放失败
        public void onInterstitialAdVideoError(string placementId,string code, string error)
        {
            Debug.Log("onInterstitialAdPlayFailed...unity3d.");
            onAdVideoFailureEvent?.Invoke(this, new HBAdErrorEventArgs(placementId, error, code));
        }
        //广告关闭
        public void onInterstitialAdClose(string placementId, string callbackJson)
        {
            Debug.Log("onInterstitialAdClosed...unity3d.");
            onAdCloseEvent?.Invoke(this, new HBAdEventArgs(placementId, callbackJson));
        }
        //广告点击
        public void onInterstitialAdClicked(string placementId, string callbackJson)
        {
            Debug.Log("onInterstitialAdClicked...unity3d.");
            onAdClickEvent?.Invoke(this, new HBAdEventArgs(placementId, callbackJson));
        }

        public void onInterstitialAdShow(string placementId, string callbackJson){
            Debug.Log("onInterstitialAdShow...unity3d.");
            onAdShowEvent?.Invoke(this, new HBAdEventArgs(placementId, callbackJson));
        }

        // Adsource Listener
        //auto callbacks
        public void startLoadingADSource(string placementId, string callbackJson)
        {
            Debug.Log("Unity: HBInterstitialAdClient::startLoadingADSource()");
            onAdStartLoadSource?.Invoke(this, new HBAdEventArgs(placementId, callbackJson));
        }
        public void finishLoadingADSource(string placementId, string callbackJson)
        {
            Debug.Log("Unity: HBInterstitialAdClient::finishLoadingADSource()");
            onAdFinishLoadSource?.Invoke(this, new HBAdEventArgs(placementId, callbackJson));
        }
        public void failToLoadADSource(string placementId, string callbackJson, string code, string error)
        {
            Debug.Log("Unity: HBInterstitialAdClient::failToLoadADSource()");
            onAdFailureLoadSource?.Invoke(this, new HBAdErrorEventArgs(placementId, callbackJson, code, error));
        }
        public void startBiddingADSource(string placementId, string callbackJson)
        {
            Debug.Log("Unity: HBInterstitialAdClient::startBiddingADSource()");
            onAdStartBidding?.Invoke(this, new HBAdEventArgs(placementId, callbackJson));
        }
        public void finishBiddingADSource(string placementId, string callbackJson)
        {
            Debug.Log("Unity: HBInterstitialAdClient::finishBiddingADSource()");
            onAdFinishBidding?.Invoke(this, new HBAdEventArgs(placementId, callbackJson));
        }
        public void failBiddingADSource(string placementId, string callbackJson, string code, string error)
        {
            Debug.Log("Unity: HBInterstitialAdClient::failBiddingADSource()");
            onAdFailBidding?.Invoke(this, new HBAdErrorEventArgs(placementId, callbackJson, code, error));
        }

        // Auto
        public void addAutoLoadAdPlacementID(string[] placementIDList){
            Debug.Log("Unity: HBInterstitialAdClient:addAutoLoadAdPlacementID()" + JsonMapper.ToJson(placementIDList));
            try
            {
                interstitialAutoAdHelper.Call("addPlacementIds", JsonMapper.ToJson(placementIDList));
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Exception caught: {0}", e);
                Debug.Log("Unity: HBInterstitialAdClient addAutoLoadAdPlacementID:  error." + e.Message);
            }
        }

		public void removeAutoLoadAdPlacementID(string placementId) 
		{
            Debug.Log("Unity: HBInterstitialAdClient:removeAutoLoadAdPlacementID()");
            try
            {
                interstitialAutoAdHelper.Call("removePlacementIds", placementId);
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Exception caught: {0}", e);
                Debug.Log("Unity: HBInterstitialAdClient removeAutoLoadAdPlacementID:  error." + e.Message);
            }
        }

		public bool autoLoadInterstitialAdReadyForPlacementID(string placementId) 
		{
			Debug.Log("Unity: HBInterstitialAdClient:autoLoadInterstitialAdReadyForPlacementID()");
            bool isready = false;
            try
            {
                isready = interstitialAutoAdHelper.Call<bool>("isAdReady", placementId);
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Exception caught: {0}", e);
                Debug.Log("HBInterstitialAdClient:autoLoadInterstitialAdReadyForPlacementID( :  error." + e.Message);
            }
            return isready;
        }
		public string getAutoValidAdCaches(string placementId)
		{
			Debug.Log("Unity: HBInterstitialAdClient:getAutoValidAdCaches()");
            string adStatusJsonString = "";
            try
            {
                adStatusJsonString = interstitialAutoAdHelper.Call<string>("getValidAdCaches", placementId);
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Exception caught: {0}", e);
                Debug.Log("HBInterstitialAdClient:getAutoValidAdCaches() :  error." + e.Message);
            }

            return adStatusJsonString;
        }

        public void setAutoLocalExtra(string placementId, string mapJson)
        {
            Debug.Log("Unity: HBInterstitialAdClient:setAutoLocalExtra()");
            try
            {
                interstitialAutoAdHelper.Call("setAdExtraData", placementId, mapJson);
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Exception caught: {0}", e);
                Debug.Log("HBInterstitialAdClient:setAutoLocalExtra() :  error." + e.Message);
            }
        }

        public void entryAutoAdScenarioWithPlacementID(string placementId, string scenarioID) 
		{
			Debug.Log("Unity: HBInterstitialAdClient:entryAutoAdScenarioWithPlacementID()");
            try
            {
                interstitialAutoAdHelper.Call("entryAdScenario", placementId, scenarioID);
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Exception caught: {0}", e);
                Debug.Log("HBInterstitialAdClient:entryAutoAdScenarioWithPlacementID() :  error." + e.Message);
            }
        }

		public void showAutoAd(string placementId, string mapJson) 
		{
	    	Debug.Log("Unity: HBInterstitialAdClient::showAutoAd()");
            try
            {
                interstitialAutoAdHelper.Call("show", placementId, mapJson);
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Exception caught: {0}", e);
                Debug.Log("Unity: HBInterstitialAdClient:showAutoAd() :  error." + e.Message);
            }
        }
        
        public string checkAutoAdStatus(string placementId)
        {
            Debug.Log("Unity: HBInterstitialAdClient:checkAutoAdStatus() : checkAutoAdStatus....");
            string adStatusJsonString = "";
            try
            {
                adStatusJsonString = interstitialAutoAdHelper.Call<string>("checkAdStatus", placementId);
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Exception caught: {0}", e);
                Debug.Log("Unity: HBInterstitialAdClient:checkAutoAdStatus() :  error." + e.Message);
            }

            return adStatusJsonString;
        }
       
    }
}
