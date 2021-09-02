using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HyperBid.Common;
using HyperBid.Api;
using HyperBid.iOS;
using HyperBid.ThirdParty.MiniJSON;

namespace HyperBid.iOS {
	public class HBNativeAdClient : IHBNativeAdClient {

        public event EventHandler<HBAdEventArgs> onAdLoadEvent;
        public event EventHandler<HBAdEventArgs> onAdLoadFailEvent;
        public event EventHandler<HBAdEventArgs> onAdImpressEvent;
        public event EventHandler<HBAdEventArgs> onAdClickedEvent;
        public event EventHandler<HBAdEventArgs> onAdVideoStartEvent;
        public event EventHandler<HBAdEventArgs> onAdVideoEndEvent;
        public event EventHandler<HBAdEventArgs> onAdVideoProgressEvent;
        public event EventHandler<HBAdEventArgs> onAdClosedEvent;

        public void loadNativeAd(string placementId, string mapJson) {
            Debug.Log("Unity:HBNativeAdClient::loadNativeAd()");
            HBNativeAdWrapper.setClientForPlacementID(placementId, this);
            HBNativeAdWrapper.loadNativeAd(placementId, mapJson);
        }

		public void setLocalExtra (string placementId,string localExtra){
			
		}

        public bool hasAdReady(string placementId) {
            Debug.Log("Unity:HBNativeAdClient::hasAdReady()");
			return HBNativeAdWrapper.isNativeAdReady(placementId);
        }

        public string checkAdStatus(string placementId) {
            Debug.Log("Unity: HBNativeAdClient::checkAdStatus()");
            return HBNativeAdWrapper.checkAdStatus(placementId);
        }

		public void renderAdToScene(string placementId, HBNativeAdView anyThinkNativeAdView) {	
            Debug.Log("Unity:HBNativeAdClient::renderAdToScene()");
            HBNativeAdWrapper.showNativeAd(placementId, anyThinkNativeAdView.toJSON());
        }

        public void renderAdToScene(string placementId, HBNativeAdView anyThinkNativeAdView, string mapJson) {  
            Debug.Log("Unity:HBNativeAdClient::renderAdToScene()");
            HBNativeAdWrapper.showNativeAd(placementId, anyThinkNativeAdView.toJSON(), mapJson);
        }

        public void cleanAdView(string placementId, HBNativeAdView anyThinkNativeAdView) {
			Debug.Log("Unity:HBNativeAdClient::cleanAdView()");
            HBNativeAdWrapper.removeNativeAdView(placementId);
        }

        public void onApplicationForces(string placementId, HBNativeAdView anyThinkNativeAdView) {
			Debug.Log("Unity:HBNativeAdClient::onApplicationForces()");
        }

        public void onApplicationPasue(string placementId, HBNativeAdView anyThinkNativeAdView) {
			Debug.Log("Unity:HBNativeAdClient::onApplicationPasue()");
        }

        public void cleanCache(string placementId) {
			Debug.Log("Unity:HBNativeAdClient::cleanCache()");
            HBNativeAdWrapper.clearCache();
        }

        //Callbacks
        public void onAdImpressed(string placementId, string callbackJson) {
            Debug.Log("Unity:HBNativeAdClient::onAdImpressed...unity3d.");
            onAdImpressEvent?.Invoke(this, new HBAdEventArgs(placementId, false, HBAdEventArgs.noValue, HBAdEventArgs.noValue, callbackJson));
        }

        public void onAdClicked(string placementId, string callbackJson) {
            Debug.Log("Unity:HBNativeAdClient::onAdClicked...unity3d.");
            onAdClickedEvent?.Invoke(this, new HBAdEventArgs(placementId, false, HBAdEventArgs.noValue, HBAdEventArgs.noValue, callbackJson));
        }

        public void onAdCloseButtonClicked(string placementId, string callbackJson)
        {
            Debug.Log("Unity:HBNativeAdClient::onAdCloseButtonClicked...unity3d.");
            onAdClosedEvent?.Invoke(this, new HBAdEventArgs(placementId, false, HBAdEventArgs.noValue, HBAdEventArgs.noValue, callbackJson));
        }

        public void onAdVideoStart(string placementId) {
            Debug.Log("Unity:HBNativeAdClient::onAdVideoStart...unity3d.");
            onAdVideoStartEvent?.Invoke(this, new HBAdEventArgs(placementId));
        }

        public void onAdVideoEnd(string placementId) {
            Debug.Log("Unity:HBNativeAdClient::onAdVideoEnd...unity3d.");
            onAdVideoEndEvent?.Invoke(this, new HBAdEventArgs(placementId));
        }

        public void onAdVideoProgress(string placementId,int progress) {
            Debug.Log("Unity:HBNativeAdClient::onAdVideoProgress...progress[" + progress + "]");
            onAdVideoProgressEvent?.Invoke(this, new HBAdEventArgs(placementId, false, HBAdEventArgs.noValue, HBAdEventArgs.noValue, HBAdEventArgs.noValue, false, progress));
        }

        public void onNativeAdLoaded(string placementId) {
            Debug.Log("Unity:HBNativeAdClient::onNativeAdLoaded...unity3d.");
            onAdLoadEvent?.Invoke(this, new HBAdEventArgs(placementId));
        }

        public void onNativeAdLoadFail(string placementId,string code, string msg) {
            Debug.Log("Unity:HBNativeAdClient::onNativeAdLoadFail...unity3d. code:" + code + " msg:" + msg);
            onAdLoadFailEvent?.Invoke(this, new HBAdEventArgs(placementId, false, msg, code));
        }
	}
}
