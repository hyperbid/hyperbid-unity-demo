using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

using HyperBid.Common;
using HyperBid.ThirdParty.LitJson;

namespace HyperBid.Api
{
   
    public class HBInterstitialAutoAd
	{
		private static readonly HBInterstitialAutoAd instance = new HBInterstitialAutoAd();
		private IHBInterstitialAdClient client;

		private HBInterstitialAutoAd()
		{
            client = GetHBInterstitialAdClient();
		}

		public static HBInterstitialAutoAd Instance 
		{
			get
			{
				return instance;
			}
		}

	    public IHBInterstitialAdClient GetHBInterstitialAdClient()
        {
            return HyperBid.HBAdsClientFactory.BuildInterstitialAdClient();
        }

        // auto

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

        public string checkAutoAdStatus(string placementId)
        {
            return client.checkAutoAdStatus(placementId);
        }

        public bool autoLoadInterstitialAdReadyForPlacementID(string placementId)
        {
            return client.autoLoadInterstitialAdReadyForPlacementID(placementId);
        }
        public string getAutoValidAdCaches(string placementId)
        {
            return client.getAutoValidAdCaches(placementId);
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






	}
}
