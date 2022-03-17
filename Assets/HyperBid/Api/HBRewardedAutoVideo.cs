using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

using HyperBid.Common;
using HyperBid.ThirdParty.LitJson;


namespace HyperBid.Api
{
    public class HBRewardedAutoVideo
    {
        private static readonly HBRewardedAutoVideo instance = new HBRewardedAutoVideo();
        private IHBRewardedVideoAdClient client;

        private HBRewardedAutoVideo()
        {
            client = GetHBRewardedClient();
        }

        public static HBRewardedAutoVideo Instance
        {
            get
            {
                return instance;
            }
        }

        // Auto
        public void addAutoLoadAdPlacementID(string[] placementIDList)
        {
            client.addAutoLoadAdPlacementID(placementIDList);   
        }
        
        public void removeAutoLoadAdPlacementID(string[] placementIDList)
        {
            if (placementIDList != null && placementIDList.Length > 0)
            {
                string placementIDListString = JsonMapper.ToJson(placementIDList);
                client.removeAutoLoadAdPlacementID(placementIDListString);
                Debug.Log("removeAutoLoadAdPlacementID, placementIDList === " + placementIDListString);
            }
            else
            {
                Debug.Log("removeAutoLoadAdPlacementID, placementIDList = null");
            } 
        }
        
        public bool autoLoadRewardedVideoReadyForPlacementID(string placementId)
        {
            return client.autoLoadRewardedVideoReadyForPlacementID(placementId);
        }
        public string getAutoValidAdCaches(string placementId)
        {
            return client.getAutoValidAdCaches(placementId);
        }

        public string checkAutoAdStatus(string placementId)
        {
            return client.checkAutoAdStatus(placementId);
        }
        

        public void setAutoLocalExtra(string placementId, Dictionary<string,string> pairs)
        {
            client.setAutoLocalExtra(placementId, JsonMapper.ToJson(pairs));
        }
        public void entryAutoAdScenarioWithPlacementID(string placementId, string scenarioID)
        {
            client.entryAutoAdScenarioWithPlacementID(placementId, scenarioID);
        }

        public void showAutoAd(string placementId)
        {
            client.showAutoAd(placementId, JsonMapper.ToJson(new Dictionary<string, string>()));
        }

        public void showAutoAd(string placementId, Dictionary<string, string> pairs)
        {
            client.showAutoAd(placementId, JsonMapper.ToJson(pairs));
        }        
        
        public IHBRewardedVideoAdClient GetHBRewardedClient()
        {
            return HyperBid.HBAdsClientFactory.BuildRewardedVideoAdClient();
        }



    }
}