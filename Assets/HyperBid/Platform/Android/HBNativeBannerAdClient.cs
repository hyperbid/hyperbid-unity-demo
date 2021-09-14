using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HyperBid.Common;
using HyperBid.Api;

namespace HyperBid.Android
{
    public class HBNativeBannerAdClient :IHBNativeBannerAdClient
    {

        public HBNativeBannerAdClient() {

        }

        public event EventHandler<HBAdEventArgs>        onAdLoadEvent;
        public event EventHandler<HBAdErrorEventArgs>   onAdLoadFailureEvent;
        public event EventHandler<HBAdEventArgs>        onAdImpressEvent;
        public event EventHandler<HBAdEventArgs>        onAdClickEvent;
        public event EventHandler<HBAdEventArgs>        onAdAutoRefreshEvent;
        public event EventHandler<HBAdErrorEventArgs>   onAdAutoRefreshFailureEvent;
        public event EventHandler<HBAdEventArgs>        onAdCloseButtonClickEvent;

        public void loadAd(string placementId, string mapJson) {

    	}
    	
		public bool adReady(string placementId) {
			return false;
		}

        public void showAd(string placementId, HBRect rect, Dictionary<string, string> pairs) {

        }

        public void removeAd(string placementId) {

        }
    }
}
