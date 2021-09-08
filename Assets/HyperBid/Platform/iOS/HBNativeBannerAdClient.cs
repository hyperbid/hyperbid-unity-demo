using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HyperBid.Common;
using HyperBid.Api;

namespace HyperBid.iOS
{
    public class HBNativeBannerAdClient : IHBNativeBannerAdClient
    {
        public event EventHandler<HBAdEventArgs> onAdLoadEvent;
        public event EventHandler<HBAdEventArgs> onAdLoadFailureEvent;
        public event EventHandler<HBAdEventArgs> onAdImpressEvent;
        public event EventHandler<HBAdEventArgs> onAdClickEvent;
        public event EventHandler<HBAdEventArgs> onAdAutoRefreshEvent;
        public event EventHandler<HBAdEventArgs> onAdAutoRefreshFailureEvent;
        public event EventHandler<HBAdEventArgs> onAdCloseButtonClickEvent;

        public void loadAd(string placementId, string mapJson) {
    		Debug.Log("HBNativeBannerAdClient::loadAd()");
    		HBNativeBannerAdWrapper.setClientForPlacementID(placementId, this);
    		Debug.Log("HBNativeBannerAdClient::loadAd(), after set client");
    		HBNativeBannerAdWrapper.loadAd(placementId, mapJson);
    		Debug.Log("HBNativeBannerAdClient::loadAd(), after invoke load ad");
    	}
    	
		public bool adReady(string placementId) {
			Debug.Log("HBNativeBannerAdClient::adReady()");
			return HBNativeBannerAdWrapper.adReady(placementId);
		}

        public void showAd(string placementId, HBRect rect, Dictionary<string, string> pairs) {
			Debug.Log("HBNativeBannerAdClient::showAd()");
			HBNativeBannerAdWrapper.showAd(placementId, rect, pairs);
        }

        public void removeAd(string placementId) {
			Debug.Log("HBNativeBannerAdClient::removeAd()");
			HBNativeBannerAdWrapper.removeAd(placementId);
        }

        //Listener method(s)
        public void onAdLoaded(string placementId) {
        	Debug.Log("HBNativeBannerAdClient::onAdLoaded()");
        	onAdLoadEvent?.Invoke(this, new HBAdEventArgs(placementId));
        }
        
        public void onAdLoadFail(string placementId, string code, string message) {
        	Debug.Log("HBNativeBannerAdClient::onAdLoadFail()");
        	onAdLoadFailureEvent?.Invoke(this, new HBAdEventArgs(placementId, true, message, code));
        }
        
        public void onAdImpressed(string placementId, string callbackJson) {
        	Debug.Log("HBNativeBannerAdClient::onAdImpressed()");
            onAdImpressEvent?.Invoke(this, new HBAdEventArgs(placementId, false, HBAdEventArgs.noValue, HBAdEventArgs.noValue, callbackJson));
        }
        
        public void onAdClicked(string placementId, string callbackJson) {
        	Debug.Log("HBNativeBannerAdClient::onAdClicked()");
            onAdClickEvent?.Invoke(this, new HBAdEventArgs(placementId, false, HBAdEventArgs.noValue, HBAdEventArgs.noValue, callbackJson));
        }
        
        public void onAdAutoRefresh(string placementId, string callbackJson) {
        	Debug.Log("HBNativeBannerAdClient::onAdAutoRefresh()");
            onAdAutoRefreshEvent?.Invoke(this, new HBAdEventArgs(placementId, false, HBAdEventArgs.noValue, HBAdEventArgs.noValue, callbackJson));
        }
        
		public void onAdAutoRefreshFailure(string placementId, string code, string message) {
        	Debug.Log("HBNativeBannerAdClient::onAdAutoRefreshFailure()");
        	onAdAutoRefreshFailureEvent?.Invoke(this, new HBAdEventArgs(placementId, true, message, code));
        }

        public void onAdCloseButtonClicked(string placementId) {
        	Debug.Log("HBNativeBannerAdClient::onAdCloseButtonClicked()");
        	onAdCloseButtonClickEvent?.Invoke(this, new HBAdEventArgs(placementId));
        }
    }
}
