using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HyperBid.Common;
using HyperBid.Api;

namespace HyperBid.iOS {
	public class HBBannerAdClient : IHBBannerAdClient {

        public event EventHandler<HBAdEventArgs> onAdLoadEvent;
        public event EventHandler<HBAdEventArgs> onAdLoadFailedEvent;
        public event EventHandler<HBAdEventArgs> onAdImpressEvent;
        public event EventHandler<HBAdEventArgs> onAdClickEvent;
        public event EventHandler<HBAdEventArgs> onAdAutoRefreshEvent;
        public event EventHandler<HBAdEventArgs> onAdAutoRefreshFailEvent;
        public event EventHandler<HBAdEventArgs> onAdCloseEvent;
        public event EventHandler<HBAdEventArgs> onAdCloseButtonTappedEvent;

        public void addsetting(string placementId,string json){
			//todo...
		}

	    public void loadBannerAd(string placementId, string mapJson) {
			Debug.Log("Unity: HBBannerAdClient::loadBannerAd()");
			HBBannerAdWrapper.setClientForPlacementID(placementId, this);
			HBBannerAdWrapper.loadBannerAd(placementId, mapJson);
	    }

	    public string checkAdStatus(string placementId) {
            Debug.Log("Unity: HBBannerAdClient::checkAdStatus()");
            return HBBannerAdWrapper.checkAdStatus(placementId);
        }

	    public void showBannerAd(string placementId, HBRect rect) {
			Debug.Log("Unity: HBBannerAdClient::showBannerAd()");
			HBBannerAdWrapper.showBannerAd(placementId, rect);
	    }

	    public void showBannerAd(string placementId, HBRect rect, string mapJson) {
			Debug.Log("Unity: HBBannerAdClient::showBannerAd()");
			HBBannerAdWrapper.showBannerAd(placementId, rect, mapJson);
	    }

        public void showBannerAd(string placementId, string position)
        {
            Debug.Log("Unity: HBBannerAdClient::showBannerAd()");
            HBBannerAdWrapper.showBannerAd(placementId, position);
        }

        public void showBannerAd(string placementId, string position, string mapJson)
        {
            Debug.Log("Unity: HBBannerAdClient::showBannerAd()");
            HBBannerAdWrapper.showBannerAd(placementId, position, mapJson);
        }

        public void cleanBannerAd(string placementId) {
			Debug.Log("Unity: HBBannerAdClient::cleanBannerAd()");	
			HBBannerAdWrapper.cleanBannerAd(placementId);	
	    }

	    public void hideBannerAd(string placementId) {
	    	Debug.Log("Unity: HBBannerAdClient::hideBannerAd()");	
			HBBannerAdWrapper.hideBannerAd(placementId);
	    }

	    public void showBannerAd(string placementId) {
	    	Debug.Log("Unity: HBBannerAdClient::showBannerAd()");	
			HBBannerAdWrapper.showBannerAd(placementId);
	    }

        public void cleanCache(string placementId) {
			Debug.Log("Unity: HBBannerAdClient::cleanCache()");
			HBBannerAdWrapper.clearCache();
        }

        public void OnBannerAdLoad(string placementId) {
			Debug.Log("Unity: HBBannerAdWrapper::OnBannerAdLoad()");
	        onAdLoadEvent?.Invoke(this, new HBAdEventArgs(placementId));
	    }
	    
	    public void OnBannerAdLoadFail(string placementId, string code, string message) {
			Debug.Log("Unity: HBBannerAdWrapper::OnBannerAdLoadFail()");
	        onAdLoadFailedEvent?.Invoke(this, new HBAdEventArgs(placementId, false, code, message));
	    }
	    
	    public void OnBannerAdImpress(string placementId, string callbackJson) {
			Debug.Log("Unity: HBBannerAdWrapper::OnBannerAdImpress()");
            onAdImpressEvent?.Invoke(this, new HBAdEventArgs(placementId, false, HBAdEventArgs.noValue, HBAdEventArgs.noValue, new HBCallbackInfo(callbackJson)));
	    }
	    
        public void OnBannerAdClick(string placementId, string callbackJson) {
			Debug.Log("Unity: HBBannerAdWrapper::OnBannerAdClick()");
            onAdClickEvent?.Invoke(this, new HBAdEventArgs(placementId, false, HBAdEventArgs.noValue, HBAdEventArgs.noValue, new HBCallbackInfo(callbackJson)));
	    }
	    
        public void OnBannerAdAutoRefresh(string placementId, string callbackJson) {
			Debug.Log("Unity: HBBannerAdWrapper::OnBannerAdAutoRefresh()");
            onAdAutoRefreshEvent?.Invoke(this, new HBAdEventArgs(placementId, false, HBAdEventArgs.noValue, HBAdEventArgs.noValue, callbackJson));
	    }
	    
	    public void OnBannerAdAutoRefreshFail(string placementId, string code, string message) {
			Debug.Log("Unity: HBBannerAdWrapper::OnBannerAdAutoRefreshFail()");
	        onAdAutoRefreshFailEvent?.Invoke(this, new HBAdEventArgs(placementId, false, message, code));
	    }

	    public void OnBannerAdClose(string placementId) {
			Debug.Log("Unity: HBBannerAdWrapper::OnBannerAdClose()");
	        onAdCloseEvent?.Invoke(this, new HBAdEventArgs(placementId));
	    }

	    public void OnBannerAdCloseButtonTapped(string placementId, string callbackJson) {
			Debug.Log("Unity: HBBannerAdWrapper::OnBannerAdCloseButton()");
	        onAdCloseButtonTappedEvent?.Invoke(this, new HBAdEventArgs(placementId, false, HBAdEventArgs.noValue, HBAdEventArgs.noValue, callbackJson));
	    }
	}
}