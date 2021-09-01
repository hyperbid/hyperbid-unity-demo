using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

using HyperBid.Common;
using HyperBid.ThirdParty.MiniJSON;


namespace HyperBid.Api
{
    public class HBRewardedVideo
    {
        private static readonly HBRewardedVideo instance = new HBRewardedVideo();
        private IHBRewardedVideoAdClient client;

        private HBRewardedVideo()
        {
            client = GetHBRewardedClient();
        }

        public static HBRewardedVideo Instance
        {
            get
            {
                return instance;
            }
        }


		/***
		 * 
		 */
        public void loadVideoAd(string placementId, Dictionary<string,string> pairs)
        {
            client.loadVideoAd(placementId, Json.Serialize(pairs));
        }

		public void setListener(HBRewardedVideoListener listener)
        {
            client.setListener(listener);
        }

        public bool hasAdReady(string placementId)
        {
            return client.hasAdReady(placementId);
        }

        public string checkAdStatus(string placementId)
        {
            return client.checkAdStatus(placementId);
        }

        public void showAd(string placementId)
        {
            client.showAd(placementId, Json.Serialize(new Dictionary<string, string>()));
        }

        public void showAd(string placementId, Dictionary<string, string> pairs)
        {
            client.showAd(placementId, Json.Serialize(pairs));
        }

        public IHBRewardedVideoAdClient GetHBRewardedClient()
        {
            return HyperBid.HBAdsClientFactory.BuildRewardedVideoAdClient();
        }

    }
}