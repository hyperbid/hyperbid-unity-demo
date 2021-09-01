using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_ANDROID
    using HyperBid.Android;
#elif UNITY_IOS
    using HyperBid.iOS;
#endif

using HyperBid.Common;
using HyperBid.Api;
using HyperBidDemo;

public class InterstitialAdComponent : MonoBehaviour
{
    static protected readonly string _placementId = "b60ac576327734";
    protected HBInterstitialAdClient _adClient;
    protected string _jsonMap = "";

    public void Start() {
        _adClient = new HBInterstitialAdClient();
    }

    public void LoadAd() {
        if(!string.IsNullOrEmpty(_placementId)) {
            _adClient.loadInterstitialAd(_placementId, _jsonMap);
        }

    }
    
    public bool IsAdReady() {
        return _adClient.hasInterstitialAdReady(_placementId);
    }

    public void ShowAd() {
        if(IsAdReady()) {
            _adClient.showInterstitialAd(_placementId, _jsonMap);
        }
    }
}
