using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HyperBid.Common;
using HyperBid.Api;

using HyperBid.ThirdParty.LitJson;

namespace HyperBid.Android
{
    public class HBRewardedVideoAdClient : AndroidJavaProxy,IHBRewardedVideoAdClient
    {

        private Dictionary<string, AndroidJavaObject> videoHelperMap = new Dictionary<string, AndroidJavaObject>();

		private AndroidJavaObject videoAutoAdHelper;

        public event EventHandler<HBAdEventArgs>        onAdLoadEvent;
        public event EventHandler<HBAdErrorEventArgs>   onAdLoadFailureEvent;
        public event EventHandler<HBAdEventArgs>        onAdVideoStartEvent;
        public event EventHandler<HBAdEventArgs>        onAdVideoEndEvent;
        public event EventHandler<HBAdErrorEventArgs>   onAdVideoFailureEvent;
        public event EventHandler<HBAdRewardEventArgs>  onAdVideoCloseEvent;
        public event EventHandler<HBAdEventArgs>        onAdClickEvent;
        public event EventHandler<HBAdRewardEventArgs>  onRewardEvent;
        public event EventHandler<HBAdEventArgs>        onAdStartLoadSource;
        public event EventHandler<HBAdEventArgs>        onAdFinishLoadSource;
        public event EventHandler<HBAdErrorEventArgs>   onAdFailureLoadSource;
        public event EventHandler<HBAdEventArgs>        onAdStartBidding;
        public event EventHandler<HBAdEventArgs>        onAdFinishBidding;
        public event EventHandler<HBAdErrorEventArgs>   onAdFailBidding;
        public event EventHandler<HBAdEventArgs>        onPlayAgainStart;
        public event EventHandler<HBAdEventArgs>        onPlayAgainEnd;
        public event EventHandler<HBAdErrorEventArgs>   onPlayAgainFailure;
        public event EventHandler<HBAdEventArgs>        onPlayAgainClick;
        public event EventHandler<HBAdEventArgs>        onPlayAgainReward;

        public HBRewardedVideoAdClient() : base("com.hyperbid.unitybridge.videoad.VideoListener")
        {
            videoAutoAdHelper = new AndroidJavaObject("com.hyperbid.unitybridge.videoad.VideoAutoAdHelper", this);
        }


        public void loadVideoAd(string placementId, string mapJson)
        {

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
                    adStatusJsonString = videoHelperMap[placementId].Call<string>("checkAdStatus");
                }
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Exception caught: {0}", e);
                Debug.Log("HBRewardedVideoAdClient :  error." + e.Message);
            }

            return adStatusJsonString;
        }

        public void entryScenarioWithPlacementID(string placementId, string scenarioID){
            Debug.Log("HBRewardedVideoAdClient : entryScenarioWithPlacementID....");
            try
            {
                if (videoHelperMap.ContainsKey(placementId))
                {
                    videoHelperMap[placementId].Call("entryAdScenario", scenarioID);
                }
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Exception caught: {0}", e);
                Debug.Log("HBRewardedVideoAdClient entryScenarioWithPlacementID:  error." + e.Message);
            }

        }
    
        

        public string getValidAdCaches(string placementId)
        {
            string validAdCachesString = "";
            Debug.Log("HBNativeAdClient : getValidAdCaches....");
            try
            {
                if (videoHelperMap.ContainsKey(placementId))
                {
                    validAdCachesString = videoHelperMap[placementId].Call<string>("getValidAdCaches");
                }
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Exception caught: {0}", e);
                Debug.Log("HBNativeAdClient :  error." + e.Message);
            }

            return validAdCachesString;
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



        public void onRewardedVideoAdLoaded(string placementId)
        {
            Debug.Log("onRewardedVideoAdLoaded...unity3d.");
            onAdLoadEvent?.Invoke(this, new HBAdEventArgs(placementId));
            
        }


        public void onRewardedVideoAdFailed(string placementId,string code, string error)
        {
            Debug.Log("onRewardedVideoAdFailed...unity3d.");
            onAdLoadFailureEvent?.Invoke(this, new HBAdErrorEventArgs(placementId, error, code));
        }


        public void onRewardedVideoAdPlayStart(string placementId, string callbackJson)
        {
            Debug.Log("onRewardedVideoAdPlayStart...unity3d.");
            onAdVideoStartEvent?.Invoke(this, new HBAdEventArgs(placementId, callbackJson));
        }


        public void onRewardedVideoAdPlayEnd(string placementId, string callbackJson)
        {
            Debug.Log("onRewardedVideoAdPlayEnd...unity3d.");
            onAdVideoEndEvent?.Invoke(this, new HBAdEventArgs(placementId, callbackJson));
        }


        public void onRewardedVideoAdPlayFailed(string placementId,string code, string error)
        {
            Debug.Log("onRewardedVideoAdPlayFailed...unity3d.");
            onAdVideoFailureEvent?.Invoke(this, new HBAdErrorEventArgs(placementId, error, code));
        }

        public void onRewardedVideoAdClosed(string placementId,bool isRewarded, string callbackJson)
        {
            Debug.Log("onRewardedVideoAdClosed...unity3d.");
            onAdVideoCloseEvent?.Invoke(this, new HBAdRewardEventArgs(placementId, callbackJson, isRewarded));
        }

        public void onRewardedVideoAdPlayClicked(string placementId, string callbackJson)
        {
            Debug.Log("onRewardedVideoAdPlayClicked...unity3d.");
            onAdClickEvent?.Invoke(this, new HBAdEventArgs(placementId, callbackJson));
        }


        public void onReward(string placementId, string callbackJson)
        {
            Debug.Log("onReward...unity3d.");
            onRewardEvent?.Invoke(this, new HBAdRewardEventArgs(placementId, callbackJson, true));
        }


        public void onRewardedVideoAdAgainPlayStart(string placementId, string callbackJson)
        {
            Debug.Log("onRewardedVideoAdAgainPlayStart...unity3d.");
            onPlayAgainStart?.Invoke(this, new HBAdEventArgs(placementId, callbackJson));
        }

        public void onRewardedVideoAdAgainPlayEnd(string placementId, string callbackJson)
        {
            Debug.Log("onRewardedVideoAdAgainPlayEnd...unity3d.");
            onPlayAgainEnd?.Invoke(this, new HBAdEventArgs(placementId, callbackJson));
        }


        public void onRewardedVideoAdAgainPlayFailed(string placementId, string code, string error)
        {
            Debug.Log("onRewardedVideoAdAgainPlayFailed...unity3d.");
            onPlayAgainFailure?.Invoke(this, new HBAdErrorEventArgs(placementId, error, code));
        }


        public void onRewardedVideoAdAgainPlayClicked(string placementId, string callbackJson)
        {
            Debug.Log("onRewardedVideoAdAgainPlayClicked...unity3d.");
            onPlayAgainClick?.Invoke(this, new HBAdEventArgs(placementId, callbackJson));
        }


        public void onAgainReward(string placementId, string callbackJson)
        {
            Debug.Log("onAgainReward...unity3d.");
            onPlayAgainReward?.Invoke(this, new HBAdEventArgs(placementId, callbackJson));
        }

        //auto callbacks
        public void startLoadingADSource(string placementId, string callbackJson)
        {
            Debug.Log("Unity: HBRewardedVideoAdClient::startLoadingADSource()");
            onAdStartLoadSource?.Invoke(this, new HBAdEventArgs(placementId, callbackJson));
        }
        public void finishLoadingADSource(string placementId, string callbackJson)
        {
            Debug.Log("Unity: HBRewardedVideoAdClient::finishLoadingADSource()");
            onAdFinishLoadSource?.Invoke(this, new HBAdEventArgs(placementId, callbackJson));
        }
        public void failToLoadADSource(string placementId, string callbackJson, string code, string error)
        {
            Debug.Log("Unity: HBRewardedVideoAdClient::failToLoadADSource()");
            onAdFailureLoadSource?.Invoke(this, new HBAdErrorEventArgs(placementId, callbackJson, code, error));
        }
        public void startBiddingADSource(string placementId, string callbackJson)
        {
            Debug.Log("Unity: HBRewardedVideoAdClient::startBiddingADSource()");
            onAdStartBidding?.Invoke(this, new HBAdEventArgs(placementId, callbackJson));
        }
        public void finishBiddingADSource(string placementId, string callbackJson)
        {
            Debug.Log("Unity: HBRewardedVideoAdClient::finishBiddingADSource()");
            onAdFinishBidding?.Invoke(this, new HBAdEventArgs(placementId, callbackJson));
        }
        public void failBiddingADSource(string placementId, string callbackJson, string code, string error)
        {
            Debug.Log("Unity: HBRewardedVideoAdClient::failBiddingADSource()");
            onAdFailBidding?.Invoke(this, new HBAdErrorEventArgs(placementId, callbackJson, code, error));
        }


        // Auto
        public void addAutoLoadAdPlacementID(string[] placementIDList){
            Debug.Log("Unity: HBRewardedVideoAdClient:addAutoLoadAdPlacementID()" + JsonMapper.ToJson(placementIDList));
            try
            {
                videoAutoAdHelper.Call("addPlacementIds", JsonMapper.ToJson(placementIDList));
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Exception caught: {0}", e);
                Debug.Log("Unity: HBRewardedVideoAdClient addAutoLoadAdPlacementID:  error." + e.Message);
            }
        }

        public void removeAutoLoadAdPlacementID(string placementId) {
			Debug.Log("Unity: HBRewardedVideoAdClient:removeAutoLoadAdPlacementID()");
            try
            {
                videoAutoAdHelper.Call("removePlacementIds", placementId);
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Exception caught: {0}", e);
                Debug.Log("Unity: HBRewardedVideoAdClient removeAutoLoadAdPlacementID:  error." + e.Message);
            }
        }

		public bool autoLoadRewardedVideoReadyForPlacementID(string placementId) 
        {
			Debug.Log("Unity: HBRewardedVideoAdClient:autoLoadRewardedVideoReadyForPlacementID()");
            bool isready = false;
            try
            {
                 isready = videoAutoAdHelper.Call<bool>("isAdReady", placementId);
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Exception caught: {0}", e);
                Debug.Log("HBRewardedVideoAdClient:autoLoadRewardedVideoReadyForPlacementID( :  error." + e.Message);
            }
            return isready;
		}

		public string getAutoValidAdCaches(string placementId)
		{
            Debug.Log("Unity: HBRewardedVideoAdClient:getAutoValidAdCaches()");
            string adStatusJsonString = "";
            try
            {
                adStatusJsonString = videoAutoAdHelper.Call<string>("getValidAdCaches", placementId);
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Exception caught: {0}", e);
                Debug.Log("HBRewardedVideoAdClient:getAutoValidAdCaches() :  error." + e.Message);
            }

            return adStatusJsonString;
		}

		public void setAutoLocalExtra(string placementId, string mapJson) 
        {
            Debug.Log("Unity: HBRewardedVideoAdClient:setAutoLocalExtra()");
            try
            {
                videoAutoAdHelper.Call("setAdExtraData", placementId, mapJson);
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Exception caught: {0}", e);
                Debug.Log("HBRewardedVideoAdClient:setAutoLocalExtra() :  error." + e.Message);
            }
        }

        public void entryAutoAdScenarioWithPlacementID(string placementId, string scenarioID) 
        {
            Debug.Log("Unity: HBRewardedVideoAdClient:entryAutoAdScenarioWithPlacementID()");
            try
            {
                videoAutoAdHelper.Call("entryAdScenario", placementId, scenarioID);
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Exception caught: {0}", e);
                Debug.Log("HBRewardedVideoAdClient:entryAutoAdScenarioWithPlacementID() :  error." + e.Message);
            }
        }
		public void showAutoAd(string placementId, string mapJson) {
            Debug.Log("Unity: HBRewardedVideoAdClient:showAutoAd()");
            try
            {
                videoAutoAdHelper.Call("show", placementId, mapJson);
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Exception caught: {0}", e);
                Debug.Log("Unity: HBRewardedVideoAdClient:showAutoAd() :  error." + e.Message);
            }
        }

        public string checkAutoAdStatus(string placementId)
        {
            Debug.Log("Unity: HBRewardedVideoAdClient:checkAutoAdStatus() : checkAutoAdStatus....");
            string adStatusJsonString = "";
            try
            {
                adStatusJsonString = videoAutoAdHelper.Call<string>("checkAdStatus", placementId);
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Exception caught: {0}", e);
                Debug.Log("Unity: HBRewardedVideoAdClient:checkAutoAdStatus() :  error." + e.Message);
            }

            return adStatusJsonString;
        }



    }
}
