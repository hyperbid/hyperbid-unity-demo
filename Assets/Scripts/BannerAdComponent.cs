using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using HyperBid.Common;
using HyperBid.Api;

public class BannerAdComponent : MonoBehaviour
{
    static readonly protected string _placementId = PlacementId.AD_BANNER;
    
    protected int  _width;
    protected int  _height;
    protected bool _isHidden;

    // event callback for when the ad is loaded
    protected void OnAdLoad(object sender, HBAdEventArgs args)
    {
        Debug.Log("BannerAd - OnAdLoad");
        Utils.SetText("Banner ad has been loaded succesfully");
    }

    // event callback if the ad has failed to load
    protected void OnAdLoadFail(object sender, HBAdErrorEventArgs args) {
        Debug.Log("BannerAd - OnAdLoadFail");
        Utils.SetText("Failed to load banner ad: " + args.errorMessage);
    }

    // event callback if the ad has been clicked
    protected void OnAdClicked(object sender, HBAdEventArgs args) {
        Debug.Log("Banner Ad - OnAdClicked");
        Utils.SetText("Banner Ad has been clicked");
    }

    // event callback if the ad has been closed
    protected void OnAdClosed(object sender, HBAdEventArgs args) {
        Debug.Log("Banner Ad - OnAdClosed");
        Utils.SetText("Banner Ad has been closed");
    }

    // Start is called before the first frame update
    void Start()
    {
        //var adPanel = GameObject.Find("").GetComponent<RectTransform>();

        // set the dimensions of the banner
        _width  = Screen.width;
        _height = 100;

        // register the needed callbacks
        HBBannerAd.Instance.events.onAdLoadEvent         += OnAdLoad;
        HBBannerAd.Instance.events.onAdLoadFailureEvent  += OnAdLoadFail;
        HBBannerAd.Instance.events.onAdClickEvent        += OnAdClicked;
        HBBannerAd.Instance.events.onAdCloseEvent        += OnAdClosed;
    }

    // Update is called once per frame
    public void LoadAd() {
        Dictionary<string, object> jsonmap = new Dictionary<string, object>();

        // Configure the width and height of the banner to be displayed, whether to use pixel as the unit(Only valid for iOS, Android uses pixel as the unit)
        HBSize bannerSize = new HBSize(_width, _height);
        
        jsonmap.Add(HBBannerAdLoadingExtra.kHBBannerAdLoadingExtraBannerAdSizeStruct, bannerSize);

        //since v5.6.5, only for Admob inline adaptive banner
        jsonmap.Add(HBBannerAdLoadingExtra.kHBBannerAdLoadingExtraInlineAdaptiveWidth, bannerSize.width);
        jsonmap.Add(HBBannerAdLoadingExtra.kHBBannerAdLoadingExtraInlineAdaptiveOrientation, HBBannerAdLoadingExtra.kHBBannerAdLoadingExtraInlineAdaptiveOrientationCurrent);

        Utils.SetText("Loading banner ad...");
        HBBannerAd.Instance.loadBannerAd(_placementId, jsonmap);
    }

    protected bool CheckAdStatus()
    {
        var json = JsonUtility.FromJson<Dictionary<string, string>>(HBBannerAd.Instance.checkAdStatus(_placementId));
        if (json.ContainsKey("isReady"))
            return json["isReady"].ToLower() == "true";

        return false;
    }

    // displays if the ad is ready
    public void IsAdReady() {
        string message = CheckAdStatus() ? "Ad status: ready" : "Ad status: not ready";
        Utils.SetText(message);
    }

    // shows the ad on the screen
    public void ShowAd() {
        //if (CheckAdStatus()) {
            HBBannerAd.Instance.showBannerAd(_placementId, HBBannerAdLoadingExtra.kHBBannerAdShowingPositionBottom);
        //}
    }

    // closes the ad
    public void CloseAd() {
        HBBannerAd.Instance.cleanBannerAd(_placementId);
    }

    // hides the banner
    void HideAd() {
        HBBannerAd.Instance.hideBannerAd(_placementId);
    }
}
