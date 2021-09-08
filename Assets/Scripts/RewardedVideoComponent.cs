using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HyperBid.Common;
using HyperBid.Api;
using HyperBidDemo;

using UnityEngine.UI;

public class RewardedVideoComponent : MonoBehaviour
{
    static readonly protected string _placementId = PlacementId.REWARDED_VIDEO;

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
        HBRewardedVideo.Instance.events.onAdLoadEvent           += OnAdLoaded;
        HBRewardedVideo.Instance.events.onAdLoadFailureEvent    += OnAdLoadFailed;
        HBRewardedVideo.Instance.events.onAdVideoStartEvent     += OnAdVideoPlay;
        HBRewardedVideo.Instance.events.onAdVideoFailureEvent   += OnAdVideoPlayFailed;
    }

    public void LoadAd() {
        HBRewardedVideo.Instance.loadVideoAd(_placementId, new Dictionary<string, string>());
    }

    public void IsAdReady() {
        HBRewardedVideo.Instance.hasAdReady(_placementId);

    }
    public void ShowAd() {
        if (HBRewardedVideo.Instance.hasAdReady(_placementId)) {
            HBRewardedVideo.Instance.showAd(_placementId);
        }
    }
}
