using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using HyperBid.Common;
using HyperBid.Api;

public class BannerAdComponent : MonoBehaviour
{
    static readonly protected string _placementId = PlacementId.AD_BANNER;
    
    protected int _width;
    protected int _height;
    
    protected void OnAdLoadFail(object sender, HBAdEventArgs args) {
        var txt = GameObject.Find("detailsHyperTxt/Text").GetComponent<Text>();
        txt.text = args.errorMessage;
    }

    // Start is called before the first frame update
    void Start()
    {
        _width  = Screen.width;
        _height = 100;
        HBBannerAd.Instance.events.onAdLoadFailedEvent += OnAdLoadFail;
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

        HBBannerAd.Instance.loadBannerAd(_placementId, jsonmap);
    }

    public void IsAdReady() {
        var detailsText = GameObject.Find("detailsHyperTxt/Text").GetComponent<Text>();
        detailsText.text = HBBannerAd.Instance.checkAdStatus(_placementId);
    }

    public void ShowAd() {
        HBBannerAd.Instance.showBannerAd(_placementId, HBBannerAdLoadingExtra.kHBBannerAdShowingPisitionBottom);
    }

    public void HideAd() {
        HBBannerAd.Instance.hideBannerAd(_placementId);
    }

    void ClearAd() {

    }
}
