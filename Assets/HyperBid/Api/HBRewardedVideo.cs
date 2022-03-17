using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

using HyperBid.Common;
using HyperBid.ThirdParty.LitJson;


namespace HyperBid.Api
{
    public class HBRewardedVideo
    {
        private static readonly HBRewardedVideo instance = new HBRewardedVideo();
        private IHBRewardedVideoAdClient client;

        public IHBRewardedVideoEvents events { get { return client; } }

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
            client.loadVideoAd(placementId, JsonMapper.ToJson(pairs));
        }

        public bool hasAdReady(string placementId)
        {
            return client.hasAdReady(placementId);
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

        public void showAd(string placementId)
        {
            client.showAd(placementId, JsonMapper.ToJson(new Dictionary<string, string>()));
        }

        public void showAd(string placementId, Dictionary<string, string> pairs)
        {
            client.showAd(placementId, JsonMapper.ToJson(pairs));
        }
                
        public IHBRewardedVideoAdClient GetHBRewardedClient()
        {
            return HyperBid.HBAdsClientFactory.BuildRewardedVideoAdClient();
        }



    }
}