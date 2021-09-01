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

        
        public event EventHandler<HBAdEventArgs> onAdLoad 
            {add {client.onAdLoad += value;} remove {client.onAdLoad -= value;}}

        public event EventHandler<HBAdEventArgs> onAdLoadFailed 
            {add {client.onAdLoadFailed += value;} remove {client.onAdLoadFailed -= value;}}

        public event EventHandler<HBAdEventArgs> onAdShow 
            {add {client.onAdShow += value;} remove {client.onAdShow -= value;}}

        public event EventHandler<HBAdEventArgs> onAdShowFailed 
            {add {client.onAdShowFailed += value;} remove {client.onAdShowFailed -= value;}}

        public event EventHandler<HBAdEventArgs> onAdClose 
            {add {client.onAdClose += value;} remove {client.onAdClose -= value;}}

        public event EventHandler<HBAdEventArgs> onAdClick 
            {add {client.onAdClick += value;} remove {client.onAdClick -= value;}}

        public event EventHandler<HBAdEventArgs> onAdPlayVideo 
            {add {client.onAdPlayVideo += value;} remove {client.onAdPlayVideo -= value;}}

        public event EventHandler<HBAdEventArgs> onAdPlayVideoFailed 
            {add {client.onAdPlayVideoFailed += value;} remove {client.onAdPlayVideoFailed -= value;}}
            
        public event EventHandler<HBAdEventArgs> onAdEndVideo 
            {add {client.onAdEndVideo += value;} remove {client.onAdEndVideo -= value;}}

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
