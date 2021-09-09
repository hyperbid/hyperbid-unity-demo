using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HyperBid.Common;
using HyperBid.Api;
using HyperBidDemo;

using UnityEngine.UI;

public class RewardedVideoComponent : MonoBehaviour
{
    // the placement id from the dashboard
    static readonly protected string _placementId = PlacementId.REWARDED_VIDEO;

    // callbacks for ad actions
    protected void OnAdLoaded(object sender, HBAdEventArgs args) {
        Debug.Log("RewardedVideo - OnAdLoad");
        Utils.SetText("The Rewarded Video has been loaded succesfully");
    }

    protected void OnAdLoadFailed(object sender, HBAdEventArgs args) {
        Debug.Log("RewardedVideo - OnAdLoadFailed");
        Utils.SetText("The Rewarded Video has failed to load: " + args.errorMessage);
    }

    protected void OnAdVideoPlay(object sender, HBAdEventArgs args) {
        Debug.Log("RewardedVideo - OnAdLoadFailed");
        Utils.SetText("The Rewarded Video is currently playing." );
    }

    protected void OnAdVideoPlayFailed(object sender, HBAdEventArgs args) {
        Debug.Log("RewardedVideo - OnAdVideoPlayFailed");
        Utils.SetText("The Rewarded Video has failed to play: " + args.errorMessage);
    }

    protected void OnAdVideoReward(object sender, HBAdEventArgs args) {
        Debug.Log("RewardedVideo - OnAdVideoReward");
        Utils.SetText("Rewarded video watched. User should be rewarded.");
    }


    // Start is called before the first frame update
    void Start() {
        // register event callbacks
        HBRewardedVideo.Instance.events.onAdLoadEvent           += OnAdLoaded;
        HBRewardedVideo.Instance.events.onAdLoadFailureEvent    += OnAdLoadFailed;
        HBRewardedVideo.Instance.events.onAdVideoStartEvent     += OnAdVideoPlay;
        HBRewardedVideo.Instance.events.onAdVideoFailureEvent   += OnAdVideoPlayFailed;
    }

    // loads the ad from the server
    public void LoadAd() {
        HBRewardedVideo.Instance.loadVideoAd(_placementId, new Dictionary<string, string>());
        Utils.SetText("Rewarded video is loading...");
    }

    // displays the ad's status (if it is ready to be shown or not) 
    public void IsAdReady() {
        bool isReady = HBRewardedVideo.Instance.hasAdReady(_placementId);
        Utils.SetText("Rewarded Video Status:" + (isReady ? "ready" : "not ready"));
    }

    // shows the ad if ready
    public void ShowAd() {
        if (HBRewardedVideo.Instance.hasAdReady(_placementId)) {
            HBRewardedVideo.Instance.showAd(_placementId);
        }
    }
}
