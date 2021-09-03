using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HyperBid.Common;
using HyperBid.Api;

namespace HyperBid.iOS {
	public class HBRewardedVideoAdClient : IHBRewardedVideoAdClient {

        public event EventHandler<HBAdEventArgs> onAdLoadEvent;
        public event EventHandler<HBAdEventArgs> onAdLoadFailEvent;
        public event EventHandler<HBAdEventArgs> onAdVideoStartEvent;
        public event EventHandler<HBAdEventArgs> onAdVideoEndEvent;
        public event EventHandler<HBAdEventArgs> onAdVideoFailEvent;
        public event EventHandler<HBAdEventArgs> onAdVideoClosedEvent;
        public event EventHandler<HBAdEventArgs> onAdClickedEvent;
        public event EventHandler<HBAdEventArgs> onRewardEvent;

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

		//Callbacks
	    public void onRewardedVideoAdLoaded(string placementId) {
	        Debug.Log("Unity: HBRewardedVideoAdClient::onRewardedVideoAdLoaded()");
	        onAdLoadEvent?.Invoke(this, new HBAdEventArgs(placementId));
	    }

	    public void onRewardedVideoAdFailed(string placementId, string code, string error) {
	        Debug.Log("Unity: HBRewardedVideoAdClient::onRewardedVideoAdFailed()");
	        onAdLoadEvent?.Invoke(this, new HBAdEventArgs(placementId, true, error, code));
	    }

        public void onRewardedVideoAdPlayStart(string placementId, string callbackJson) {
	        Debug.Log("Unity: HBRewardedVideoAdClient::onRewardedVideoAdPlayStart()");
            onAdVideoStartEvent?.Invoke(this, new HBAdEventArgs(placementId, false, HBAdEventArgs.noValue, HBAdEventArgs.noValue, callbackJson));
	    }

        public void onRewardedVideoAdPlayEnd(string placementId, string callbackJson) {
	        Debug.Log("Unity: HBRewardedVideoAdClient::onRewardedVideoAdPlayEnd()");
            onAdVideoEndEvent?.Invoke(this, new HBAdEventArgs(placementId, false, HBAdEventArgs.noValue, HBAdEventArgs.noValue, callbackJson));
	    }

	    public void onRewardedVideoAdPlayFailed(string placementId, string code, string error) {
	        Debug.Log("Unity: HBRewardedVideoAdClient::onRewardedVideoAdPlayFailed()");
	        onAdVideoFailEvent?.Invoke(this, new HBAdEventArgs(placementId, false, error, code));
	    }

        public void onRewardedVideoAdClosed(string placementId, bool isRewarded, string callbackJson) {
	        Debug.Log("Unity: HBRewardedVideoAdClient::onRewardedVideoAdClosed()");
            onAdVideoClosedEvent?.Invoke(this, new HBAdEventArgs(placementId, false, HBAdEventArgs.noValue, HBAdEventArgs.noValue, callbackJson, isRewarded));
	    }

        public void onRewardedVideoAdPlayClicked(string placementId, string callbackJson) {
	        Debug.Log("Unity: HBRewardedVideoAdClient::onRewardedVideoAdPlayClicked()");
            onAdClickedEvent?.Invoke(this, new HBAdEventArgs(placementId, false, HBAdEventArgs.noValue, HBAdEventArgs.noValue, callbackJson));
	    }

        public void onRewardedVideoReward(string placementId, string callbackJson) {
            Debug.Log("Unity: HBRewardedVideoAdClient::onRewardedVideoReward()");
            onRewardEvent?.Invoke(this, new HBAdEventArgs(placementId, false, HBAdEventArgs.noValue, HBAdEventArgs.noValue, callbackJson));
        }
	}
}
