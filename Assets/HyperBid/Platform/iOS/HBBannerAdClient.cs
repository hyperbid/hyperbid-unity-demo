using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HyperBid.Common;
using HyperBid.Api;

namespace HyperBid.iOS {
	public class HBBannerAdClient : IHBBannerAdClient {

        public event EventHandler<HBAdEventArgs> onAdLoadEvent;
        public event EventHandler<HBAdErrorEventArgs> onAdLoadFailureEvent;
        public event EventHandler<HBAdEventArgs> onAdImpressEvent;
        public event EventHandler<HBAdEventArgs> onAdClickEvent;
        public event EventHandler<HBAdEventArgs> onAdAutoRefreshEvent;
        public event EventHandler<HBAdErrorEventArgs> onAdAutoRefreshFailureEvent;
        public event EventHandler<HBAdEventArgs> onAdCloseEvent;
        public event EventHandler<HBAdEventArgs> onAdCloseButtonTappedEvent;
        public event EventHandler<HBAdEventArgs> onAdStartLoadSource;
        public event EventHandler<HBAdEventArgs> onAdFinishLoadSource;
        public event EventHandler<HBAdErrorEventArgs> onAdFailureLoadSource;
        public event EventHandler<HBAdEventArgs> onAdStartBidding;
        public event EventHandler<HBAdEventArgs> onAdFinishBidding;
        public event EventHandler<HBAdErrorEventArgs> onAdFailBidding;

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

		public string getValidAdCaches(string placementId)
		{
			Debug.Log("Unity: HBBannerAdClient::getValidAdCaches()");
			return HBBannerAdWrapper.getValidAdCaches(placementId);
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
	        onAdLoadFailureEvent?.Invoke(this, new HBAdErrorEventArgs(placementId, message, code));
	    }
	    
	    public void OnBannerAdImpress(string placementId, string callbackJson) {
			Debug.Log("Unity: HBBannerAdWrapper::OnBannerAdImpress()");
            onAdImpressEvent?.Invoke(this, new HBAdEventArgs(placementId, callbackJson));
	    }
	    
        public void OnBannerAdClick(string placementId, string callbackJson) {
			Debug.Log("Unity: HBBannerAdWrapper::OnBannerAdClick()");
            onAdClickEvent?.Invoke(this, new HBAdEventArgs(placementId, callbackJson));
	    }
	    
        public void OnBannerAdAutoRefresh(string placementId, string callbackJson) {
			Debug.Log("Unity: HBBannerAdWrapper::OnBannerAdAutoRefresh()");
            onAdAutoRefreshEvent?.Invoke(this, new HBAdEventArgs(placementId, callbackJson));
	    }
	    
	    public void OnBannerAdAutoRefreshFail(string placementId, string code, string message) {
			Debug.Log("Unity: HBBannerAdWrapper::OnBannerAdAutoRefreshFail()");
	        onAdAutoRefreshFailureEvent?.Invoke(this, new HBAdErrorEventArgs(placementId, message, code));
	    }

	    public void OnBannerAdClose(string placementId) {
			Debug.Log("Unity: HBBannerAdWrapper::OnBannerAdClose()");
	        onAdCloseEvent?.Invoke(this, new HBAdEventArgs(placementId));
	    }

	    public void OnBannerAdCloseButtonTapped(string placementId, string callbackJson) {
			Debug.Log("Unity: HBBannerAdWrapper::OnBannerAdCloseButton()");
	        onAdCloseButtonTappedEvent?.Invoke(this, new HBAdEventArgs(placementId, callbackJson));
	    }
		//auto callbacks
	    public void startLoadingADSource(string placementId, string callbackJson) 
		{
	        Debug.Log("Unity: HBBannerAdWrapper::startLoadingADSource()");
            onAdStartLoadSource?.Invoke(this, new HBAdEventArgs(placementId, callbackJson));
	    }
	    public void finishLoadingADSource(string placementId, string callbackJson) 
		{
	        Debug.Log("Unity: HBBannerAdWrapper::finishLoadingADSource()");
            onAdFinishLoadSource?.Invoke(this, new HBAdEventArgs(placementId, callbackJson));
	    }	
	    public void failToLoadADSource(string placementId,string callbackJson, string code, string error) 
		{
	        Debug.Log("Unity: HBBannerAdWrapper::failToLoadADSource()");
	        onAdFailureLoadSource?.Invoke(this, new HBAdErrorEventArgs(placementId, callbackJson, code, error));
	    }
		public void startBiddingADSource(string placementId, string callbackJson) 
		{
	        Debug.Log("Unity: HBBannerAdWrapper::startBiddingADSource()");
            onAdStartBidding?.Invoke(this, new HBAdEventArgs(placementId, callbackJson));
	    }
	    public void finishBiddingADSource(string placementId, string callbackJson) 
		{
	        Debug.Log("Unity: HBBannerAdWrapper::finishBiddingADSource()");
            onAdFinishBidding?.Invoke(this, new HBAdEventArgs(placementId, callbackJson));
	    }	
	    public void failBiddingADSource(string placementId, string callbackJson,string code, string error) 
		{
	        Debug.Log("Unity: HBBannerAdWrapper::failBiddingADSource()");
	        onAdFailBidding?.Invoke(this, new HBAdErrorEventArgs(placementId, callbackJson, code, error));
	    }
	}
}