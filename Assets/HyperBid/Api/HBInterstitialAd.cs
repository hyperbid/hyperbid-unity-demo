using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

using HyperBid.Common;
using HyperBid.ThirdParty.MiniJSON;

namespace HyperBid.Api
{
	public class HBInterstitialAd
	{
		private static readonly HBInterstitialAd instance = new HBInterstitialAd();
		private IHBInterstitialAdClient client;

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

		public void loadInterstitialAd(string placementId, Dictionary<string,string> pairs)
        {
            client.loadInterstitialAd(placementId, Json.Serialize(pairs));
        }

		public void setListener(HBInterstitialAdListener listener)
        {
            client.setListener(listener);
        }

        public bool hasInterstitialAdReady(string placementId)
        {
            return client.hasInterstitialAdReady(placementId);
        }

        public string checkAdStatus(string placementId)
        {
            return client.checkAdStatus(placementId);
        }

        public void showInterstitialAd(string placementId)
        {
            client.showInterstitialAd(placementId, Json.Serialize(new Dictionary<string, string>()));
        }

        public void showInterstitialAd(string placementId, Dictionary<string, string> pairs)
        {
            client.showInterstitialAd(placementId, Json.Serialize(pairs));
        }

        public IHBInterstitialAdClient GetHBInterstitialAdClient()
        {
            return HyperBid.HBAdsClientFactory.BuildInterstitialAdClient();
        }
	}
}
