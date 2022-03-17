using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HyperBid.Common;
using HyperBid.Api;
using HyperBid.ThirdParty.LitJson;


namespace HyperBid.iOS {

    public class HBInterstitialAdClient : IHBInterstitialAdClient
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
        public event EventHandler<HBAdEventArgs> onAdStartLoadSource;
        public event EventHandler<HBAdEventArgs> onAdFinishLoadSource;
        public event EventHandler<HBAdErrorEventArgs> onAdFailureLoadSource;
        public event EventHandler<HBAdEventArgs> onAdStartBidding;
        public event EventHandler<HBAdEventArgs> onAdFinishBidding;
        public event EventHandler<HBAdErrorEventArgs> onAdFailBidding;

        public void addsetting(string placementId,string json){
			//todo...
		}

	    public void loadInterstitialAd(string placementId, string mapJson) {
			Debug.Log("Unity: HBInterstitialAdClient::loadInterstitialAd()");
            HBInterstitialAdWrapper.setClientForPlacementID(placementId, this);
			HBInterstitialAdWrapper.loadInterstitialAd(placementId, mapJson);
		}

		public bool hasInterstitialAdReady(string placementId) {
			Debug.Log("Unity: HBInterstitialAdClient::hasInterstitialAdReady()");
			return HBInterstitialAdWrapper.hasInterstitialAdReady(placementId);
		}

		public void showInterstitialAd(string placementId, string mapJson) {
			Debug.Log("Unity: HBInterstitialAdClient::showInterstitialAd()");
			HBInterstitialAdWrapper.showInterstitialAd(placementId, mapJson);
		}

		public void cleanCache(string placementId) {
			Debug.Log("Unity: HBInterstitialAdClient::cleanCache()");
			HBInterstitialAdWrapper.clearCache(placementId);
		}

		public string checkAdStatus(string placementId) {
			Debug.Log("Unity: HBInterstitialAdClient::checkAdStatus()");
			return HBInterstitialAdWrapper.checkAdStatus(placementId);
		}

		public string getValidAdCaches(string placementId)
		{
			Debug.Log("Unity: HBInterstitialAdClient::getValidAdCaches()");
			return HBInterstitialAdWrapper.getValidAdCaches(placementId);
		}

		public void entryScenarioWithPlacementID(string placementId, string scenarioID){
            Debug.Log("Unity: HBInterstitialAdClient::entryScenarioWithPlacementID()");
			HBInterstitialAdWrapper.entryScenarioWithPlacementID(placementId,scenarioID);
		}


		//Callbacks
		public void OnInterstitialAdLoaded(string placementID) {
	    	Debug.Log("Unity: HBInterstitialAdClient::OnInterstitialAdLoaded()");
	        onAdLoadEvent?.Invoke(this, new HBAdEventArgs(placementID));
	    }

	    public void OnInterstitialAdLoadFailure(string placementID, string code, string error) {
	    	Debug.Log("Unity: HBInterstitialAdClient::OnInterstitialAdLoadFailure()");
	        onAdLoadFailureEvent?.Invoke(this, new HBAdErrorEventArgs(placementID, error, code));
	    }

	     public void OnInterstitialAdVideoPlayFailure(string placementID, string code, string error) {
	    	Debug.Log("Unity: HBInterstitialAdClient::OnInterstitialAdVideoPlayFailure()");
	        onAdVideoFailureEvent?.Invoke(this, new HBAdErrorEventArgs(placementID, error, code));
	    }

	    public void OnInterstitialAdVideoPlayStart(string placementID, string callbackJson) {
	    	Debug.Log("Unity: HBInterstitialAdClient::OnInterstitialAdPlayStart()");
	        onAdVideoStartEvent?.Invoke(this, new HBAdEventArgs(placementID, callbackJson));
	    }

	    public void OnInterstitialAdVideoPlayEnd(string placementID, string callbackJson) {
	    	Debug.Log("Unity: HBInterstitialAdClient::OnInterstitialAdVideoPlayEnd()");
	        onAdVideoEndEvent?.Invoke(this, new HBAdEventArgs(placementID, callbackJson));
	    }

        public void OnInterstitialAdShow(string placementID, string callbackJson) {
	    	Debug.Log("Unity: HBInterstitialAdClient::OnInterstitialAdShow()");
            onAdShowEvent?.Invoke(this, new HBAdEventArgs(placementID, callbackJson));
	    }

        public void OnInterstitialAdFailedToShow(string placementID) {
	    	Debug.Log("Unity: HBInterstitialAdClient::OnInterstitialAdFailedToShow()");
	        onAdShowFailureEvent?.Invoke(this, new HBAdErrorEventArgs(placementID, "Failed to show video ad", "-1"));
	    }

        public void OnInterstitialAdClick(string placementID, string callbackJson) {
	    	Debug.Log("Unity: HBInterstitialAdClient::OnInterstitialAdClick()");
            onAdClickEvent?.Invoke(this, new HBAdEventArgs(placementID, callbackJson));
	    }

        public void OnInterstitialAdClose(string placementID, string callbackJson) {
	    	Debug.Log("Unity: HBInterstitialAdClient::OnInterstitialAdClose()");
            onAdCloseEvent?.Invoke(this, new HBAdEventArgs(placementID, callbackJson));
	    }
		//auto callbacks
		public void startLoadingADSource(string placementId, string callbackJson)
		{
			Debug.Log("Unity: HInterstitialAdClient::startLoadingADSource()");
			onAdStartLoadSource?.Invoke(this, new HBAdEventArgs(placementId, callbackJson));
		}
		public void finishLoadingADSource(string placementId, string callbackJson)
		{
			Debug.Log("Unity: HInterstitialAdClient::finishLoadingADSource()");
			onAdFinishLoadSource?.Invoke(this, new HBAdEventArgs(placementId, callbackJson));
		}
		public void failToLoadADSource(string placementId, string callbackJson, string code, string error)
		{
			Debug.Log("Unity: HInterstitialAdClient::failToLoadADSource()");
			onAdFailureLoadSource?.Invoke(this, new HBAdErrorEventArgs(placementId, callbackJson, code, error));
		}
		public void startBiddingADSource(string placementId, string callbackJson)
		{
			Debug.Log("Unity: HInterstitialAdClient::startBiddingADSource()");
			onAdStartBidding?.Invoke(this, new HBAdEventArgs(placementId, callbackJson));
		}
		public void finishBiddingADSource(string placementId, string callbackJson)
		{
			Debug.Log("Unity: HInterstitialAdClient::finishBiddingADSource()");
			onAdFinishBidding?.Invoke(this, new HBAdEventArgs(placementId, callbackJson));
		}
		public void failBiddingADSource(string placementId, string callbackJson, string code, string error)
		{
			Debug.Log("Unity: HInterstitialAdClient::failBiddingADSource()");
			onAdFailBidding?.Invoke(this, new HBAdErrorEventArgs(placementId, callbackJson, code, error));
		}

		// Auto
		public void addAutoLoadAdPlacementID(string[] placementIDList) 
		{
			Debug.Log("Unity: HBInterstitialAdClient:addAutoLoadAdPlacementID()");

		

	     	if (placementIDList != null && placementIDList.Length > 0)
            {
				foreach (string placementID in placementIDList)
        		{
					HBInterstitialAdWrapper.setClientForPlacementID(placementID, this);
				}

                string placementIDListString = JsonMapper.ToJson(placementIDList);
				HBInterstitialAdWrapper.addAutoLoadAdPlacementID(placementIDListString);
                Debug.Log("addAutoLoadAdPlacementID, placementIDList === " + placementIDListString);
            }
            else
            {
                Debug.Log("addAutoLoadAdPlacementID, placementIDList = null");
            } 		

		}

		public void removeAutoLoadAdPlacementID(string placementId) 
		{
			Debug.Log("Unity: HBInterstitialAdClient:removeAutoLoadAdPlacementID()");
			HBInterstitialAdWrapper.removeAutoLoadAdPlacementID(placementId);
		}

		public bool autoLoadInterstitialAdReadyForPlacementID(string placementId) 
		{
			Debug.Log("Unity: HBInterstitialAdClient:autoLoadInterstitialAdReadyForPlacementID()");
			return HBInterstitialAdWrapper.autoLoadInterstitialAdReadyForPlacementID(placementId);
		}
		public string getAutoValidAdCaches(string placementId)
		{
			Debug.Log("Unity: HBInterstitialAdClient:getAutoValidAdCaches()");
			return HBInterstitialAdWrapper.getAutoValidAdCaches(placementId);
		}

		public string checkAutoAdStatus(string placementId) {
			Debug.Log("Unity: HBInterstitialAdClient::checkAutoAdStatus()");
			return HBInterstitialAdWrapper.checkAutoAdStatus(placementId);
		}	


		public void setAutoLocalExtra(string placementId, string mapJson) 
		{
			Debug.Log("Unity: HBInterstitialAdClient:setAutoLocalExtra()");
			HBInterstitialAdWrapper.setAutoLocalExtra(placementId, mapJson);
		}
		public void entryAutoAdScenarioWithPlacementID(string placementId, string scenarioID) 
		{
			Debug.Log("Unity: HBInterstitialAdClient:entryAutoAdScenarioWithPlacementID()");
			HBInterstitialAdWrapper.entryAutoAdScenarioWithPlacementID(placementId, scenarioID);
		}
		public void showAutoAd(string placementId, string mapJson) 
		{
	    	Debug.Log("Unity: HBInterstitialAdClient::showAutoAd()");
	    	HBInterstitialAdWrapper.showAutoInterstitialAd(placementId, mapJson);
	    }
	}
}
