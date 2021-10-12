using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HyperBid.Common;
using HyperBid.Api;
using HyperBidDemo;

using UnityEngine.UI;

public class InterstitialAdComponent : MonoBehaviour
{
    static protected readonly string _placementId = PlacementId.INTERSTITIAL;

    // callback called when the ad is ready
    protected void OnAdLoad(object sender, HBAdEventArgs args) {
        Debug.Log("InterstitialAd - OnAdLoad");
        Utils.SetText("Interstitial Ad has been succesfully loaded");
    }

    // callback called if the ad failed to load
    protected void OnAdLoadFail(object sender, HBAdErrorEventArgs args) {
        Debug.Log("InterstitialAd - OnAdLoad");
        Utils.SetText("Interstitial Ad has failed to load:" + args.errorMessage);
    }

    // callback called when the video starts playing
    protected void OnAdPlayVideo(object sender, HBAdEventArgs args) {
        Debug.Log("InterstitialAd - OnAdPlayVideo");
        Utils.SetText("Interstitial Ad Video is playing");
    }

    // callback called if the video has failed to play
    protected void OnAdPlayVideoFailed(object sender, HBAdEventArgs args) {
        Debug.Log("InterstitialAd - OnAdPlayVideoFailed");
        Utils.SetText("Interstitial Ad has failed to play");

    }

    public void Start() {
        // register event callbacks
        HBInterstitialAd.Instance.events.onAdLoadEvent           += OnAdLoad;
        HBInterstitialAd.Instance.events.onAdLoadFailureEvent    += OnAdLoadFail;
        HBInterstitialAd.Instance.events.onAdVideoStartEvent     += OnAdPlayVideo;
        HBInterstitialAd.Instance.events.onAdVideoFailureEvent   += OnAdPlayVideoFailed;
    }

    public void LoadAd() {
        Utils.SetText("Loading interstitial ad...");
        HBInterstitialAd.Instance.loadInterstitialAd(_placementId, new Dictionary<string, string>());
    }

    public void IsAdReady() {
        bool isReady = HBInterstitialAd.Instance.hasInterstitialAdReady(_placementId);
        Utils.SetText("Ad status:" + (isReady ? "ready" : "not ready"));
    }
     
    public void ShowAd() {
        if(HBInterstitialAd.Instance.hasInterstitialAdReady(_placementId)) {
            HBInterstitialAd.Instance.showInterstitialAd(_placementId);
        }
    }
}
