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
    	public event EventHandler<HBAdEventArgs> onNativeAdLoad;              // triggers when the native banner is loaded
        public event EventHandler<HBAdEventArgs> onNativeAdLoadFail;          // triggers in case an error occured while loading the banner
        public event EventHandler<HBAdEventArgs> onNativeAdImpressed;         // triggers if an impression is registered
        public event EventHandler<HBAdEventArgs> onNativeAdClicked;           // triggers if the banner has been clicked
        public event EventHandler<HBAdEventArgs> onNativeAdAutoRefresh;        // triggers when the ad refreshes
        public event EventHandler<HBAdEventArgs> onNativeAdAutoRefreshFailure; // triggers on refresh failure
        public event EventHandler<HBAdEventArgs> onNativeAdCloseButtonClicked; // triggers when the user closes the ad

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

        public void setListener(HBNativeBannerAdListener listener) {
			Debug.Log("HBNativeBannerAdClient::setListener()");
			this.listener = listener;
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
        	onAdLoad?.Invoke(placementId);
        }
        
        public void onAdLoadFail(string placementId, string code, string message) {
        	Debug.Log("HBNativeBannerAdClient::onAdLoadFail()");
        	if(listener != null) listener.onAdLoadFail(placementId, code, message);
        }
        
        public void onAdImpressed(string placementId, string callbackJson) {
        	Debug.Log("HBNativeBannerAdClient::onAdImpressed()");
            if(listener != null) listener.onAdImpressed(placementId, new HBCallbackInfo(callbackJson));
        }
        
        public void onAdClicked(string placementId, string callbackJson) {
        	Debug.Log("HBNativeBannerAdClient::onAdClicked()");
            if(listener != null) listener.onAdClicked(placementId, new HBCallbackInfo(callbackJson));
        }
        
        public void onAdAutoRefresh(string placementId, string callbackJson) {
        	Debug.Log("HBNativeBannerAdClient::onAdAutoRefresh()");
            if(listener != null) listener.onAdAutoRefresh(placementId, new HBCallbackInfo(callbackJson));
        }
        
		public void onAdAutoRefreshFailure(string placementId, string code, string message) {
        	Debug.Log("HBNativeBannerAdClient::onAdAutoRefreshFailure()");
        	if(listener != null) listener.onAdAutoRefreshFailure(placementId, code, message);
        }

        public void onAdCloseButtonClicked(string placementId) {
        	Debug.Log("HBNativeBannerAdClient::onAdCloseButtonClicked()");
        	if(listener != null) listener.onAdCloseButtonClicked(placementId);
        }
    }
}
