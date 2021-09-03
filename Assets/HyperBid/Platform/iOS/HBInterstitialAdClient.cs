using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HyperBid.Common;
using HyperBid.Api;

namespace HyperBid.iOS {
	public class HBInterstitialAdClient : IHBInterstitialAdClient {

        public event EventHandler<HBAdEventArgs> onAdLoadEvent;
        public event EventHandler<HBAdEventArgs> onAdLoadFailedEvent;
        public event EventHandler<HBAdEventArgs> onAdShowEvent;
        public event EventHandler<HBAdEventArgs> onAdShowFailedEvent;
        public event EventHandler<HBAdEventArgs> onAdCloseEvent;
        public event EventHandler<HBAdEventArgs> onAdClickEvent;
        public event EventHandler<HBAdEventArgs> onAdPlayVideoEvent;
        public event EventHandler<HBAdEventArgs> onAdPlayVideoFailedEvent;
        public event EventHandler<HBAdEventArgs> onAdEndVideoEvent;

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
	        onAdLoadFailedEvent?.Invoke(this, new HBAdEventArgs(placementID, true , error, code));
	    }

	     public void OnInterstitialAdVideoPlayFailure(string placementID, string code, string error) {
	    	Debug.Log("Unity: HBInterstitialAdClient::OnInterstitialAdVideoPlayFailure()");
	        onAdPlayVideoFailedEvent?.Invoke(this, new HBAdEventArgs(placementID, false, error, code));
	    }

	    public void OnInterstitialAdVideoPlayStart(string placementID, string callbackJson) {
	    	Debug.Log("Unity: HBInterstitialAdClient::OnInterstitialAdPlayStart()");
	        onAdPlayVideoEvent?.Invoke(this, new HBAdEventArgs(placementID, false, HBAdEventArgs.noValue, HBAdEventArgs.noValue, callbackJson));
	    }

	    public void OnInterstitialAdVideoPlayEnd(string placementID, string callbackJson) {
	    	Debug.Log("Unity: HBInterstitialAdClient::OnInterstitialAdVideoPlayEnd()");
	        onAdEndVideoEvent?.Invoke(this, new HBAdEventArgs(placementID, false, HBAdEventArgs.noValue, HBAdEventArgs.noValue, callbackJson));
	    }

        public void OnInterstitialAdShow(string placementID, string callbackJson) {
	    	Debug.Log("Unity: HBInterstitialAdClient::OnInterstitialAdShow()");
            onAdShowEvent?.Invoke(this, new HBAdEventArgs(placementID, false, HBAdEventArgs.noValue, HBAdEventArgs.noValue, callbackJson));
	    }

        public void OnInterstitialAdFailedToShow(string placementID) {
	    	Debug.Log("Unity: HBInterstitialAdClient::OnInterstitialAdFailedToShow()");
	        onAdShowFailedEvent?.Invoke(this, new HBAdEventArgs(placementID, true));
	    }

        public void OnInterstitialAdClick(string placementID, string callbackJson) {
	    	Debug.Log("Unity: HBInterstitialAdClient::OnInterstitialAdClick()");
            onAdClickEvent?.Invoke(this, new HBAdEventArgs(placementID, false, HBAdEventArgs.noValue, HBAdEventArgs.noValue, callbackJson));
	    }

        public void OnInterstitialAdClose(string placementID, string callbackJson) {
	    	Debug.Log("Unity: HBInterstitialAdClient::OnInterstitialAdClose()");
            onAdCloseEvent?.Invoke(this, new HBAdEventArgs(placementID, false, HBAdEventArgs.noValue, HBAdEventArgs.noValue, callbackJson));
	    }
	}
}
