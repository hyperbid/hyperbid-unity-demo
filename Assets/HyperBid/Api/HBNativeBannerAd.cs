using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

using HyperBid.Common;
using HyperBid.ThirdParty.MiniJSON;


namespace HyperBid.Api
{
	public class HBNativeBannerAdShowingExtra
	{
		public static readonly string kHBNativeBannerAdShowingExtraBackgroundColor = "background_color";
		public static readonly string kHBNativeBannerAdShowingExtraAutorefreshInterval = "autorefresh_interval";
		public static readonly string kHBNativeBannerAdShowingExtraHideCloseButtonFlag = "hide_close_button_flag";
		public static readonly string kHBNativeBannerAdShowingExtraCTAButtonBackgroundColor = "cta_button_background_color";
		public static readonly string kHBNativeBannerAdShowingExtraCTATextColor = "cta_button_title_color";//of type string, example:#3e2f10
		public static readonly string kHBNativeBannerAdShowingExtraCTATextFont = "cta_text_font";//of type double
		public static readonly string kHBNativeBannerAdShowingExtraTitleColor = "title_color";
		public static readonly string kHBNativeBannerAdShowingExtraTitleFont = "title_font";
		public static readonly string kHBNativeBannerAdShowingExtraTextColor = "text_color";
		public static readonly string kHBNativeBannerAdShowingExtraTextFont = "text_font";
		public static readonly string kHBNativeBannerAdShowingExtraAdvertiserTextFont = "sponsor_text_font";
		public static readonly string kHBNativeBannerAdShowingExtraAdvertiserTextColor = "spnosor_text_color";
	}

    public class HBNativeBannerAd
    {
    	private static readonly HBNativeBannerAd instance = new HBNativeBannerAd();
		private IHBNativeBannerAdClient client;
		public HBNativeBannerAd() {
            client = GetHBNativeBannerAdClient();
		}
		
		public static HBNativeBannerAd Instance {
			get {
				return instance;
			}
		}

		public void loadAd(string placementId, Dictionary<String, String> pairs) {
			Debug.Log("HBNativeBannerAd::loadAd(" + placementId + ")");
			client.loadAd(placementId, Json.Serialize(pairs));
		}

		public bool adReady(string placementId) {
            Debug.Log("HBNativeBannerAd::adReady(" + placementId + ")");
			return client.adReady(placementId);
		}

		public void setListener(HBNativeBannerAdListener listener) {
            Debug.Log("HBNativeBannerAd::setListener");
			client.setListener(listener);
		}

		public void showAd(string placementId, HBRect rect, Dictionary<string, string> pairs) {
            Debug.Log("HBNativeBannerAd::showAd");
			client.showAd(placementId, rect, pairs);
		}

		public void removeAd(string placementId) {
            Debug.Log("HBNativeBannerAd::removeAd");
			client.removeAd(placementId);
		}

		public IHBNativeBannerAdClient GetHBNativeBannerAdClient()
        {
            return HyperBid.HBAdsClientFactory.BuildNativeBannerAdClient();
        }
    }
}
