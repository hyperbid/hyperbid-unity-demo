using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using HyperBid.Common;
using HyperBid.Api;

public class NativeAdComponent : MonoBehaviour
{
    // the placement id from the dashboard
    private static readonly string _placementId = PlacementId.NATIVE_AD;

    // the view used to render the ad
    private HBNativeAdView _adView;

    // anchor points for the ad
    private int _rootX, _rootY;
    // size of the ad
    private int _width, _height;

    // callback for ad events
    protected void OnAdLoad(object sender, HBAdEventArgs args)
    {
        Debug.Log("Native Ad - onAdLoad");
        Utils.SetText("Native Ad loaded succesfully: " + args.placementId);
    }

    protected void OnAdLoadFail(object sender, HBAdEventArgs args)
    {
        Debug.Log("Native Ad - onAdLoadFail");
        Utils.SetText(string.Format("Failed to load native ad ({0}): {1}", args.placementId, args.errorMessage));
    }

    protected void OnAdPlayVideo(object sender, HBAdEventArgs args)
    {
        Debug.Log("Native Ad - onAdPlayVideo");
        Utils.SetText("Native Ad played succesfully: " + args.placementId);
    }

    protected void OnAdVideoFail(object sender, HBAdEventArgs args)
    {
        Debug.Log("Native Ad - onAdVideoFail");
        Utils.SetText(string.Format("Failed to play native ad ({0}): {1}", args.placementId, args.errorMessage));
    }

    // Start is called before the first frame update
    void Start() { 
         #if UNITY_ANDROID
            _width  = 960;
            _height = 600;
        #elif UNITY_IOS || UNITY_IPHONE
            _width  = 320;
            _height = 250;
        #endif

        _rootX = _rootY = 100;

        // register event callbacks
        HBNativeAd.Instance.events.onAdLoadEvent        += OnAdLoad;
        HBNativeAd.Instance.events.onAdLoadFailureEvent += OnAdLoadFail;
        HBNativeAd.Instance.events.onAdVideoStartEvent  += OnAdPlayVideo;
    }

    public void LoadAd() {
        Debug.Log ("Developer load native, placementId = " + _placementId);

        // ----- since v5.6.8 -----
        Dictionary<string,object> jsonmap = new Dictionary<string,object>();

        #if UNITY_ANDROID
        HBSize nativeSize = new HBSize(_width, _height);
            jsonmap.Add(HBNativeAdLoadingExtra.kHBNativeAdLoadingExtraNativeAdSizeStruct, nativeSize);
        #elif UNITY_IOS || UNITY_IPHONE
            HBSize nativeSize = new HBSize(_width, _height, false);
            jsonmap.Add(HBNativeAdLoadingExtra.kHBNativeAdLoadingExtraNativeAdSizeStruct, nativeSize);
        #endif

        Utils.SetText("Loading native ad...");
        HBNativeAd.Instance.loadNativeAd(_placementId, jsonmap);
    }

    // displays if the ad is ready to be shown
    public void IsAdReady() {
        bool isReady = HBNativeAd.Instance.hasAdReady(_placementId);
        Utils.SetText("Native Ad Status:" + (isReady ? "ready" : "not ready"));
    }

    // displays the ad
    public void ShowAd() {
        if (HBNativeAd.Instance.hasAdReady(_placementId)) {
            Debug.Log("Developer show native....");
            HBNativeConfig conifg = new HBNativeConfig();

            string bgcolor = "#ffffff";
            string textcolor = "#000000";

            int x = _rootX, y = _rootY, width = 300 * 3, height = 200 * 3, textsize = 17;
            conifg.parentProperty = new HBNativeItemProperty(x, y, _width, _height, bgcolor, textcolor, textsize, true);

            //adlogo
            x = 0 * 3; y = 0 * 3; width = 30 * 3; height = 20 * 3; textsize = 17;
            conifg.adLogoProperty = new HBNativeItemProperty(x, y, _width, _height, bgcolor, textcolor, textsize, true);

            //adicon
            x = 0 * 3; y = 50 * 3 - 50; width = 60 * 3; height = 50 * 3; textsize = 17;
            conifg.appIconProperty = new HBNativeItemProperty(x, y, width, height, bgcolor, textcolor, textsize, true);

            //ad cta
            x = 0 * 3; y = 150 * 3; width = 300 * 3; height = 50 * 3; textsize = 17;
            conifg.ctaButtonProperty = new HBNativeItemProperty(x, y, width, height, "#ff21bcab", "#ffffff", textsize, true);

            //ad desc
            x = 60 * 3; y = 100 * 3; width = 240 * 3 - 20; height = 50 * 3 - 10; textsize = 10;
            conifg.descProperty = new HBNativeItemProperty(x, y, width, height, bgcolor, "#777777", textsize, true);

            //ad image
            x = 60 * 3; y = 0 * 3 + 20; width = 240 * 3 - 20; height = 100 * 3 - 10; textsize = 17;
            conifg.mainImageProperty = new HBNativeItemProperty(x, y, width, height, bgcolor, textcolor, textsize, true);

            //ad title
            x = 0 * 3; y = 100 * 3; width = 60 * 3; height = 50 * 3; textsize = 12;
            conifg.titleProperty = new HBNativeItemProperty(x, y, width, height, bgcolor, textcolor, textsize, true);

            _adView = new HBNativeAdView(conifg);
            Debug.Log("Developer renderAdToScene--->");
            HBNativeAd.Instance.renderAdToScene(_placementId, _adView);
        }
    }

    // removes the native ad from view
    public void RemoveAd() {
        Debug.Log ("Developer cleanView native....");
        HBNativeAd.Instance.cleanAdView(_placementId, _adView);
    }
}
