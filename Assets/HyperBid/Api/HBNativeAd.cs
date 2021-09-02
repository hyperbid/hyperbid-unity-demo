using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

using HyperBid.Common;
using HyperBid.ThirdParty.MiniJSON;


namespace HyperBid.Api
{
    public class HBNativeAdLoadingExtra
    {
        public static readonly string kHBNativeAdLoadingExtraNativeAdSizeStruct = "native_ad_size_struct";
        public static readonly string kHBNativeAdLoadingExtraNativeAdSize = "native_ad_size";
        public static readonly string kHBNativeAdSizeUsesPixelFlagKey = "uses_pixel";
    }

    public class HBNativeAd
    {
        private static readonly HBNativeAd instance = new HBNativeAd();
        private IHBNativeAdClient client;

        public IHBNativeAdEvents events { get { return client; } }

        public HBNativeAd(){
            client = GetHBNativeAdClient();
        }

        public static HBNativeAd Instance
        {
            get
            {
                return instance;
            }
        }


        public void loadNativeAd(string placementId, Dictionary<String,object> pairs){
            if (pairs != null && pairs.ContainsKey(HBNativeAdLoadingExtra.kHBNativeAdLoadingExtraNativeAdSizeStruct))
            {
                HBSize size = (HBSize)(pairs[HBNativeAdLoadingExtra.kHBNativeAdLoadingExtraNativeAdSizeStruct]);
                pairs.Add(HBNativeAdLoadingExtra.kHBNativeAdLoadingExtraNativeAdSize, size.width + "x" + size.height);
                pairs.Add(HBNativeAdLoadingExtra.kHBNativeAdSizeUsesPixelFlagKey, size.usesPixel);
            }
            client.loadNativeAd(placementId,Json.Serialize(pairs));
        }

        public bool hasAdReady(string placementId){
            return client.hasAdReady(placementId);
        }

       public string checkAdStatus(string placementId)
        {
            return client.checkAdStatus(placementId);
        }

        public void renderAdToScene(string placementId, HBNativeAdView anyThinkNativeAdView){
            client.renderAdToScene(placementId, anyThinkNativeAdView, "");
        }

        public void renderAdToScene(string placementId, HBNativeAdView anyThinkNativeAdView, Dictionary<string,string> pairs){
            client.renderAdToScene(placementId, anyThinkNativeAdView, Json.Serialize(pairs));
        }

        public void cleanAdView(string placementId, HBNativeAdView anyThinkNativeAdView){
            client.cleanAdView(placementId, anyThinkNativeAdView);
        }

        public void onApplicationForces(string placementId, HBNativeAdView anyThinkNativeAdView){
            client.onApplicationForces(placementId, anyThinkNativeAdView);
        }

        public void onApplicationPasue(string placementId, HBNativeAdView anyThinkNativeAdView){
            client.onApplicationPasue(placementId, anyThinkNativeAdView);
        }

        public void cleanCache(string placementId){
            client.cleanCache(placementId);
        }



        public IHBNativeAdClient GetHBNativeAdClient()
        {
            return HyperBid.HBAdsClientFactory.BuildNativeAdClient();
        }

    }
}
