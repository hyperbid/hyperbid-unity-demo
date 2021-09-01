using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HyperBid.Common;
using HyperBid.Api;
using HyperBidDemo;

using UnityEngine.UI;

public class InterstitialAdComponent : MonoBehaviour
{
    static protected readonly string _placementId = "b60ac576327734";
    protected Text _messageText;

    protected void OnAdLoad(object sender, HBAdEventArgs args) {
        _messageText.text = "On callback";
    }

    public void Start() {
        HBInterstitialAd.Instance.onAdLoad += OnAdLoad;
        _messageText = GameObject.Find("detailsHyperTxt/Text").GetComponent<Text>();
    }

    public void LoadAd() {
        if(!string.IsNullOrEmpty(_placementId)) {
            HBInterstitialAd.Instance.loadInterstitialAd(_placementId, new Dictionary<string, string>());
        }
    }

    public void IsAdReady() {
        bool isReady = HBInterstitialAd.Instance.hasInterstitialAdReady(_placementId);
        _messageText.text = isReady ? "Ad is ready" : "Ad is not ready";
    }

    public void ShowAd() {
        if(HBInterstitialAd.Instance.hasInterstitialAdReady(_placementId)) {
            HBInterstitialAd.Instance.showInterstitialAd(_placementId);
        }
    }
}
