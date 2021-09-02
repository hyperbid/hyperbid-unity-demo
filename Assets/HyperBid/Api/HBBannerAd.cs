using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

using HyperBid.Common;
using HyperBid.ThirdParty.MiniJSON;

namespace HyperBid.Api
{
    public class HBBannerAdLoadingExtra
    {
        public static readonly string kHBBannerAdLoadingExtraBannerAdSize = "banner_ad_size";
        public static readonly string kHBBannerAdLoadingExtraBannerAdSizeStruct = "banner_ad_size_struct";
        public static readonly string kHBBannerAdSizeUsesPixelFlagKey = "uses_pixel";
        public static readonly string kHBBannerAdShowingPisitionTop = "top";
        public static readonly string kHBBannerAdShowingPisitionBottom = "bottom";

        //Deprecated in v5.7.3
        public static readonly string kHBBannerAdLoadingExtraInlineAdaptiveWidth = "inline_adaptive_width";
        public static readonly string kHBBannerAdLoadingExtraInlineAdaptiveOrientation = "inline_adaptive_orientation";
        public static readonly int kHBBannerAdLoadingExtraInlineAdaptiveOrientationCurrent = 0;
        public static readonly int kHBBannerAdLoadingExtraInlineAdaptiveOrientationPortrait = 1;
        public static readonly int kHBBannerAdLoadingExtraInlineAdaptiveOrientationLandscape = 2;
        //Deprecated in v5.7.3

        public static readonly string kHBBannerAdLoadingExtraAdaptiveWidth = "adaptive_width";
        public static readonly string kHBBannerAdLoadingExtraAdaptiveOrientation = "adaptive_orientation";
        public static readonly int kHBBannerAdLoadingExtraAdaptiveOrientationCurrent = 0;
        public static readonly int kHBBannerAdLoadingExtraAdaptiveOrientationPortrait = 1;
        public static readonly int kHBBannerAdLoadingExtraAdaptiveOrientationLandscape = 2;

    }
    public class HBBannerAd
	{
		private static readonly HBBannerAd instance = new HBBannerAd();
		private IHBBannerAdClient client;

        public IHBBannerEvents events { get { return client; } }

		private HBBannerAd()
		{
            client = GetHBBannerAdClient();
		}

		public static HBBannerAd Instance
		{
			get
			{
				return instance;
			}
		}

		/**
		API
		*/
		public void loadBannerAd(string placementId, Dictionary<string,object> pairs)
		{
            if (pairs != null && pairs.ContainsKey(HBBannerAdLoadingExtra.kHBBannerAdLoadingExtraBannerAdSize))
            {
                client.loadBannerAd(placementId, Json.Serialize(pairs));
            }
            else if (pairs != null && pairs.ContainsKey(HBBannerAdLoadingExtra.kHBBannerAdLoadingExtraBannerAdSizeStruct))
            {
                HBSize size = (HBSize)(pairs[HBBannerAdLoadingExtra.kHBBannerAdLoadingExtraBannerAdSizeStruct]);
                pairs.Add(HBBannerAdLoadingExtra.kHBBannerAdLoadingExtraBannerAdSize, size.width + "x" + size.height);
                pairs.Add(HBBannerAdLoadingExtra.kHBBannerAdSizeUsesPixelFlagKey, size.usesPixel);

                //Dictionary<string, object> newPaires = new Dictionary<string, object> { { HBBannerAdLoadingExtra.kHBBannerAdLoadingExtraBannerAdSize, size.width + "x" + size.height }, { HBBannerAdLoadingExtra.kHBBannerAdSizeUsesPixelFlagKey, size.usesPixel } };
                client.loadBannerAd(placementId, Json.Serialize(pairs));
            }
            else
            {
                client.loadBannerAd(placementId, Json.Serialize(pairs));
            }

		}

        public string checkAdStatus(string placementId)
        {
            return client.checkAdStatus(placementId);
        }

        public void showBannerAd(string placementId, HBRect rect)
        {
            client.showBannerAd(placementId, rect, "");
        }

        public void showBannerAd(string placementId, HBRect rect, Dictionary<string,string> pairs)
        {
            client.showBannerAd(placementId, rect, Json.Serialize(pairs));
        }

        public void showBannerAd(string placementId, string position)
        {
            client.showBannerAd(placementId, position, "");
        }

        public void showBannerAd(string placementId, string position, Dictionary<string,string> pairs)
        {
            client.showBannerAd(placementId, position, Json.Serialize(pairs));
        }

        public void showBannerAd(string placementId)
        {
            client.showBannerAd(placementId);
        }

        public void hideBannerAd(string placementId)
        {
            client.hideBannerAd(placementId);
        }

        public void cleanBannerAd(string placementId)
        {
            client.cleanBannerAd(placementId);
        }

        public IHBBannerAdClient GetHBBannerAdClient()
        {
            return HyperBid.HBAdsClientFactory.BuildBannerAdClient();
        }
	}
}
