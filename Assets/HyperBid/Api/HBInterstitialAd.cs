using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

using HyperBid.Common;
using HyperBid.ThirdParty.LitJson;

namespace HyperBid.Api
{
    public class HBInterstitialAdLoadingExtra
    {
        public static readonly string kHBInterstitialAdLoadingExtraInterstitialAdSize = "interstitial_ad_size";
        public static readonly string kHBInterstitialAdLoadingExtraInterstitialAdSizeStruct = "interstitial_ad_size_struct";
        public static readonly string kHBInterstitialAdSizeUsesPixelFlagKey = "uses_pixel";
    }

    public class HBInterstitialAd
	{
		private static readonly HBInterstitialAd instance = new HBInterstitialAd();
		private IHBInterstitialAdClient client;

        public IHBInterstitialAdEvents events { get { return client; } }

		private HBInterstitialAd()
		{
            client = GetHBInterstitialAdClient();
		}

		public static HBInterstitialAd Instance 
		{
			get
			{
				return instance;
			}
		}

		public void loadInterstitialAd(string placementId, Dictionary<string,object> pairs)
        {
            if (pairs != null && pairs.ContainsKey(HBInterstitialAdLoadingExtra.kHBInterstitialAdLoadingExtraInterstitialAdSizeStruct))
            {
                HBSize size = (HBSize)(pairs[HBInterstitialAdLoadingExtra.kHBInterstitialAdLoadingExtraInterstitialAdSizeStruct]);
                pairs.Add(HBInterstitialAdLoadingExtra.kHBInterstitialAdLoadingExtraInterstitialAdSize, size.width + "x" + size.height);
                pairs.Add(HBInterstitialAdLoadingExtra.kHBInterstitialAdSizeUsesPixelFlagKey, size.usesPixel);

                client.loadInterstitialAd(placementId, JsonMapper.ToJson(pairs));
            } else
            {
                client.loadInterstitialAd(placementId, JsonMapper.ToJson(pairs));
            }
        }

        public bool hasInterstitialAdReady(string placementId)
        {
            return client.hasInterstitialAdReady(placementId);
        }
        public void entryScenarioWithPlacementID(string placementId, string scenarioID)
        {
            client.entryScenarioWithPlacementID(placementId,scenarioID);
        }
        

        public string checkAdStatus(string placementId)
        {
            return client.checkAdStatus(placementId);
        }

        public string getValidAdCaches(string placementId)
        {
            return client.getValidAdCaches(placementId);
        }

        public void showInterstitialAd(string placementId)
        {
            client.showInterstitialAd(placementId, JsonMapper.ToJson(new Dictionary<string, string>()));
        }

        public void showInterstitialAd(string placementId, Dictionary<string, string> pairs)
        {
            client.showInterstitialAd(placementId, JsonMapper.ToJson(pairs));
        }

        public IHBInterstitialAdClient GetHBInterstitialAdClient()
        {
            return HyperBid.HBAdsClientFactory.BuildInterstitialAdClient();
        }

	}
}
