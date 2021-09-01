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

    	public void loadAd(string placementId, string mapJson) {

    	}
    	
		public bool adReady(string placementId) {
			return false;
		}

        public void setListener(HBNativeBannerAdListener listener) {

        }

        public void showAd(string placementId, HBRect rect, Dictionary<string, string> pairs) {

        }

        public void removeAd(string placementId) {

        }
    }
}
