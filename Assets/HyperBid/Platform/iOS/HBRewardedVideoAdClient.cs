using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HyperBid.Common;
using HyperBid.Api;

namespace HyperBid.iOS {
	public class HBRewardedVideoAdClient : IHBRewardedVideoAdClient {
		public event EventHandler<HBAdEventArgs> onVideoAdLoaded;       // triggers when a rewarded video has been loaded
        public event EventHandler<HBAdEventArgs> onVideoAdLoadFail;     // triggers when a rewarded video has failed to load (or none have been returned)
        public event EventHandler<HBAdEventArgs> onVideoAdPlayStart;    // triggers on video start
        public event EventHandler<HBAdEventArgs> onVideoAdPlayEnd;      // triggers on video end
        public event EventHandler<HBAdEventArgs> onVideoAdPlayFail;     // triggers if the video fails to play
        public event EventHandler<HBAdEventArgs> onVideoAdPlayClosed;   // triggers when the user has closed the ad
        public event EventHandler<HBAdEventArgs> onVideoAdPlayClicked;  // triggers when the user has clicked the ad
        public event EventHandler<HBAdEventArgs> onGiveReward;          // triggers when the user has finsihed watching the ad and should be rewarded



		public void addsetting (string placementId,string json){
			//todo...
		}
		public void setListener(HBRewardedVideoListener listener) {
			Debug.Log("Unity: HBRewardedVideoAdClient::setListener()");
	        anyThinkListener = listener;
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
	        onVideoAdLoaded?.Invoke(this, new HBAdEventArgs(placementId));
	    }

	    public void onRewardedVideoAdFailed(string placementId, string code, string error) {
	        Debug.Log("Unity: HBRewardedVideoAdClient::onRewardedVideoAdFailed()");
	        onVideoAdLoadFail?.Invoke(this, new HBAdEventArgs(placementId, true, error, code));
	    }

        public void onRewardedVideoAdPlayStart(string placementId, string callbackJson) {
	        Debug.Log("Unity: HBRewardedVideoAdClient::onRewardedVideoAdPlayStart()");
            onVideoAdPlayStart?.Invoke(this, new HBAdEventArgs(placementId, false, HBAdEventArgs.noValue, HBAdEventArgs.noValue, callbackJson));
	    }

        public void onRewardedVideoAdPlayEnd(string placementId, string callbackJson) {
	        Debug.Log("Unity: HBRewardedVideoAdClient::onRewardedVideoAdPlayEnd()");
            onVideoAdPlayEnd?.Invoke(this, new HBAdEventArgs(placementId, false, HBAdEventArgs.noValue, HBAdEventArgs.noValue, callbackJson));
	    }

	    public void onRewardedVideoAdPlayFailed(string placementId, string code, string error) {
	        Debug.Log("Unity: HBRewardedVideoAdClient::onRewardedVideoAdPlayFailed()");
	        onVideoAdPlayFail?.Invoke(this, new HBAdEventArgs(placementId, false, error, code));
	    }

        public void onRewardedVideoAdClosed(string placementId, bool isRewarded, string callbackJson) {
	        Debug.Log("Unity: HBRewardedVideoAdClient::onRewardedVideoAdClosed()");
            onVideoAdPlayClosed?.Invoke(this, new HBAdEventArgs(placementId, false, HBAdEventArgs.noValue, HBAdEventArgs.noValue, callbackJson, isRewarded));
	    }

        public void onRewardedVideoAdPlayClicked(string placementId, string callbackJson) {
	        Debug.Log("Unity: HBRewardedVideoAdClient::onRewardedVideoAdPlayClicked()");
            onVideoAdPlayClicked?.Invoke(this, new HBAdEventArgs(placementId, false, HBAdEventArgs.noValue, HBAdEventArgs.noValue, callbackJson));
	    }

        public void onRewardedVideoReward(string placementId, string callbackJson) {
            Debug.Log("Unity: HBRewardedVideoAdClient::onRewardedVideoReward()");
            onGiveReward?.Invoke(this, new HBAdEventArgs(placementId, false, HBAdEventArgs.noValue, HBAdEventArgs.noValue, callbackJson));
        }
	}
}
