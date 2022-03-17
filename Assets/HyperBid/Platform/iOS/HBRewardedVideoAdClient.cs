using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using HyperBid.Common;
using HyperBid.Api;
using HyperBid.ThirdParty.LitJson;

namespace HyperBid.iOS {
	public class HBRewardedVideoAdClient : IHBRewardedVideoAdClient {

        public event EventHandler<HBAdEventArgs>		onAdLoadEvent;
        public event EventHandler<HBAdErrorEventArgs>	onAdLoadFailureEvent;
        public event EventHandler<HBAdEventArgs>		onAdVideoStartEvent;
        public event EventHandler<HBAdEventArgs>		onAdVideoEndEvent;
        public event EventHandler<HBAdErrorEventArgs>	onAdVideoFailureEvent;
        public event EventHandler<HBAdRewardEventArgs>	onAdVideoCloseEvent;
        public event EventHandler<HBAdEventArgs>		onAdClickEvent;
        public event EventHandler<HBAdRewardEventArgs>	onRewardEvent;
        public event EventHandler<HBAdEventArgs>		onAdStartLoadSource;
        public event EventHandler<HBAdEventArgs>		onAdFinishLoadSource;
        public event EventHandler<HBAdErrorEventArgs>	onAdFailureLoadSource;
        public event EventHandler<HBAdEventArgs>		onAdStartBidding;
        public event EventHandler<HBAdEventArgs>		onAdFinishBidding;
        public event EventHandler<HBAdErrorEventArgs>	onAdFailBidding;
        public event EventHandler<HBAdEventArgs>		onPlayAgainStart;
        public event EventHandler<HBAdEventArgs>		onPlayAgainEnd;
        public event EventHandler<HBAdErrorEventArgs>	onPlayAgainFailure;
        public event EventHandler<HBAdEventArgs>		onPlayAgainClick;
        public event EventHandler<HBAdEventArgs>		onPlayAgainReward;

        public void addsetting (string placementId,string json){
			//todo...
		}

		public void loadVideoAd(string placementId, string mapJson) {
			Debug.Log("Unity: HBRewardedVideoAdClient::loadVideoAd()");
			HBRewardedVideoWrapper.setClientForPlacementID(placementId, this);
			HBRewardedVideoWrapper.loadRewardedVideo(placementId, mapJson);
		}

		public bool hasAdReady(string placementId) {
			Debug.Log("Unity: HBRewardedVideoAdClient::hasAdReady()");
			return HBRewardedVideoWrapper.isRewardedVideoReady(placementId);
		}

		//To be implemented
		public void setUserData(string placementId, string userId, string customData) {
			Debug.Log("Unity: HBRewardedVideoAdClient::setUserData()");
	    }

	    public void showAd(string placementId, string mapJson) {
	    	Debug.Log("Unity: HBRewardedVideoAdClient::showAd()");
	    	HBRewardedVideoWrapper.showRewardedVideo(placementId, mapJson);
	    }

	    public void cleanAd(string placementId) {
	    	Debug.Log("Unity: HBRewardedVideoAdClient::cleanAd()");
	    	HBRewardedVideoWrapper.clearCache();
	    }

	    public void onApplicationForces(string placementId) {
			Debug.Log("Unity: HBRewardedVideoAdClient::onApplicationForces()");
	    }

	    public void onApplicationPasue(string placementId) {
			Debug.Log("Unity: HBRewardedVideoAdClient::onApplicationPasue()");
	    }

	    public string checkAdStatus(string placementId) {
	    	Debug.Log("Unity: HBRewardedVideoAdClient::checkAdStatus()");
	    	return HBRewardedVideoWrapper.checkAdStatus(placementId);
	    }

		public void entryScenarioWithPlacementID(string placementId, string scenarioID){
            Debug.Log("Unity: HBRewardedVideoAdClient::entryScenarioWithPlacementID()");
			HBRewardedVideoWrapper.entryScenarioWithPlacementID(placementId,scenarioID);
		}

		public string getValidAdCaches(string placementId)
		{
			Debug.Log("Unity: HBRewardedVideoAdClient::getValidAdCaches()");
			return HBRewardedVideoWrapper.getValidAdCaches(placementId);
		}

        // Auto
		public void addAutoLoadAdPlacementID(string[] placementIDList)
		{
			Debug.Log("Unity: HBRewardedVideoAdClient:addAutoLoadAdPlacementID()");

	     	if (placementIDList != null && placementIDList.Length > 0)
            {
				foreach (string placementID in placementIDList)
        		{
					HBRewardedVideoWrapper.setClientForPlacementID(placementID, this);
				}

                string placementIDListString = JsonMapper.ToJson(placementIDList);
				HBRewardedVideoWrapper.addAutoLoadAdPlacementID(placementIDListString);
                Debug.Log("addAutoLoadAdPlacementID, placementIDList === " + placementIDListString);
            }
            else
            {
                Debug.Log("addAutoLoadAdPlacementID, placementIDList = null");
            } 			
		}

		public void removeAutoLoadAdPlacementID(string placementId) 
		{
			Debug.Log("Unity: HBRewardedVideoAdClient:removeAutoLoadAdPlacementID()");
			HBRewardedVideoWrapper.removeAutoLoadAdPlacementID(placementId);
		}

		public bool autoLoadRewardedVideoReadyForPlacementID(string placementId) 
		{
			Debug.Log("Unity: HBRewardedVideoAdClient:autoLoadRewardedVideoReadyForPlacementID()");
			return HBRewardedVideoWrapper.autoLoadRewardedVideoReadyForPlacementID(placementId);
		}
		public string getAutoValidAdCaches(string placementId)
		{
			Debug.Log("Unity: HBRewardedVideoAdClient:getAutoValidAdCaches()");
			return HBRewardedVideoWrapper.getAutoValidAdCaches(placementId);
		}
		public string checkAutoAdStatus(string placementId) {
	    	Debug.Log("Unity: HBRewardedVideoAdClient::checkAutoAdStatus()");
	    	return HBRewardedVideoWrapper.checkAutoAdStatus(placementId);
	    }

		public void setAutoLocalExtra(string placementId, string mapJson) 
		{
			Debug.Log("Unity: HBRewardedVideoAdClient:setAutoLocalExtra()");
			HBRewardedVideoWrapper.setAutoLocalExtra(placementId, mapJson);
		}
		public void entryAutoAdScenarioWithPlacementID(string placementId, string scenarioID) 
		{
			Debug.Log("Unity: HBRewardedVideoAdClient:entryAutoAdScenarioWithPlacementID()");
			HBRewardedVideoWrapper.entryAutoAdScenarioWithPlacementID(placementId, scenarioID);
		}
		public void showAutoAd(string placementId, string mapJson) 
		{
	    	Debug.Log("Unity: HBRewardedVideoAdClient::showAutoAd()");
	    	HBRewardedVideoWrapper.showAutoRewardedVideo(placementId, mapJson);
	    }

		//Callbacks
		public void onRewardedVideoAdLoaded(string placementId) {
	        Debug.Log("Unity: HBRewardedVideoAdClient::onRewardedVideoAdLoaded()");
	        onAdLoadEvent?.Invoke(this, new HBAdEventArgs(placementId));
	    }

	    public void onRewardedVideoAdFailed(string placementId, string code, string error) {
	        Debug.Log("Unity: HBRewardedVideoAdClient::onRewardedVideoAdFailed()");
	        onAdLoadEvent?.Invoke(this, new HBAdErrorEventArgs(placementId, error, code));
	    }

        public void onRewardedVideoAdPlayStart(string placementId, string callbackJson) {
	        Debug.Log("Unity: HBRewardedVideoAdClient::onRewardedVideoAdPlayStart()");
            onAdVideoStartEvent?.Invoke(this, new HBAdEventArgs(placementId, callbackJson));
	    }

        public void onRewardedVideoAdPlayEnd(string placementId, string callbackJson) {
	        Debug.Log("Unity: HBRewardedVideoAdClient::onRewardedVideoAdPlayEnd()");
            onAdVideoEndEvent?.Invoke(this, new HBAdEventArgs(placementId, callbackJson));
	    }

	    public void onRewardedVideoAdPlayFailed(string placementId, string code, string error) {
	        Debug.Log("Unity: HBRewardedVideoAdClient::onRewardedVideoAdPlayFailed()");
	        onAdVideoFailureEvent?.Invoke(this, new HBAdErrorEventArgs(placementId, error, code));
	    }

        public void onRewardedVideoAdClosed(string placementId, bool isRewarded, string callbackJson) {
	        Debug.Log("Unity: HBRewardedVideoAdClient::onRewardedVideoAdClosed()");
            onAdVideoCloseEvent?.Invoke(this, new HBAdRewardEventArgs(placementId, callbackJson, isRewarded));
	    }

        public void onRewardedVideoAdPlayClicked(string placementId, string callbackJson) {
	        Debug.Log("Unity: HBRewardedVideoAdClient::onRewardedVideoAdPlayClicked()");
            onAdClickEvent?.Invoke(this, new HBAdEventArgs(placementId, callbackJson));
	    }

        public void onRewardedVideoReward(string placementId, string callbackJson) {
            Debug.Log("Unity: HBRewardedVideoAdClient::onRewardedVideoReward()");
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

	}
}
