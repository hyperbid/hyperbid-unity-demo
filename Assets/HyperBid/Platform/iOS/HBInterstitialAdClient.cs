using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HyperBid.Common;
using HyperBid.Api;

namespace HyperBid.iOS {
	public class HBInterstitialAdClient : IHBInterstitialAdClient {

        public event EventHandler<HBAdEventArgs> onAdLoadEvent;
        public event EventHandler<HBAdErrorEventArgs> onAdLoadFailureEvent;
        public event EventHandler<HBAdEventArgs> onAdShowEvent;
        public event EventHandler<HBAdErrorEventArgs> onAdShowFailureEvent;
        public event EventHandler<HBAdEventArgs> onAdCloseEvent;
        public event EventHandler<HBAdEventArgs> onAdClickEvent;
        public event EventHandler<HBAdEventArgs> onAdVideoStartEvent;
        public event EventHandler<HBAdErrorEventArgs> onAdVideoFailureEvent;
        public event EventHandler<HBAdEventArgs> onAdVideoEndEvent;

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
	}
}
