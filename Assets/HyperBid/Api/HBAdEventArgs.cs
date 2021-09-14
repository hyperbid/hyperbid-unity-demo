        
using System;
using System.Collections;
using HyperBid.Api;

namespace HyperBid.Api
{
    
    public class HBAdEventArgs
    {
        public String placementId { get; }
        public HBCallbackInfo callbackInfo { get; }

        public HBAdEventArgs(String id)
        {
            placementId = id;
            callbackInfo = new HBCallbackInfo("");
        }

        public HBAdEventArgs(String id, String callbackJson)
        {
            placementId = id;
            callbackInfo = new HBCallbackInfo(callbackJson);
        }
    }

    public class HBAdErrorEventArgs : HBAdEventArgs
    {
        public String errorMessage { get; }
        public String errorCode { get; }

        public HBAdErrorEventArgs(String placementId, String message, String code)
            : base(placementId)
        {
            errorMessage = message;
            errorCode = code;
        }
    }

    public class HBAdProgressEventArgs : HBAdEventArgs
    {
        public int adProgress { get; }

        public HBAdProgressEventArgs(String placementId, String callbackJson, int progress)
            : base(placementId, callbackJson)
        {
            adProgress = progress;
        }
    }

    public class HBAdRewardEventArgs : HBAdEventArgs
    {
        public bool isRewarded { get; }

        public HBAdRewardEventArgs(String placementId, String callbackJson, bool doReward)
            : base(placementId, callbackJson)
        {
            isRewarded = doReward;
        }
    }


    public interface IHBBannerEvents
    {
        // triggers when a banner ad has been succesfully loaded
        event EventHandler<HBAdEventArgs> onAdLoadEvent;

        // triggers when a banner ad has failed to load
        event EventHandler<HBAdErrorEventArgs> onAdLoadFailureEvent;

        // triggers when a banner ad generates an impression
        event EventHandler<HBAdEventArgs> onAdImpressEvent;

        // triggers when the user clicks a banner ad
        event EventHandler<HBAdEventArgs> onAdClickEvent;

        // triggers when the ad refreshes
        event EventHandler<HBAdEventArgs> onAdAutoRefreshEvent;

        // triggers when the ad fails to auto refresh
        event EventHandler<HBAdErrorEventArgs> onAdAutoRefreshFailureEvent;

        // triggers when the banner ad is closed
        event EventHandler<HBAdEventArgs> onAdCloseEvent;

        // triggers when the users closes the ad via the button
        event EventHandler<HBAdEventArgs> onAdCloseButtonTappedEvent;
    }
    
    public interface IHBInterstitialAdEvents
    {
        // called when the interstitial ad is loaded from the provider
        event EventHandler<HBAdEventArgs> onAdLoadEvent;

        // if no ad has been returned or a network error has occured
        event EventHandler<HBAdErrorEventArgs> onAdLoadFailureEvent;

        // called when the ad is shown
        event EventHandler<HBAdEventArgs> onAdShowEvent;

        // called if the ad has failed to be shown
        event EventHandler<HBAdErrorEventArgs> onAdShowFailureEvent;

        // called when the ad is closed
        event EventHandler<HBAdEventArgs> onAdCloseEvent;

        // called when an user has clicked an ad
        event EventHandler<HBAdEventArgs> onAdClickEvent;

        // called when a video ad has started playing
        event EventHandler<HBAdEventArgs> onAdVideoStartEvent;

        // called if an ad video has failed to be displayed
        event EventHandler<HBAdErrorEventArgs> onAdVideoFailureEvent;

        // called when ad video has finished
        event EventHandler<HBAdEventArgs> onAdVideoEndEvent;          
    }

    public interface IHBNativeAdEvents
    {
        // triggers when the native ad is loaded
        event EventHandler<HBAdEventArgs> onAdLoadEvent;

        // triggers in the case the ad fails to load
        event EventHandler<HBAdErrorEventArgs> onAdLoadFailureEvent;

        // triggers when the ad generates an impression
        event EventHandler<HBAdEventArgs> onAdImpressEvent;

        // triggers when the user clicks the ad
        event EventHandler<HBAdEventArgs> onAdClickEvent;

        // triggers when the ad video starts
        event EventHandler<HBAdEventArgs> onAdVideoStartEvent;

        // triggers when the ad video ends
        event EventHandler<HBAdEventArgs> onAdVideoEndEvent;

        // triggers if the ad progresses
        event EventHandler<HBAdProgressEventArgs> onAdVideoProgressEvent;

        // triggers when the ad is closed
        event EventHandler<HBAdEventArgs> onAdCloseEvent;           
    }

    public interface IHBNativeBannerEvents
    {
        // triggers when the native banner is loaded
        event EventHandler<HBAdEventArgs> onAdLoadEvent;

        // triggers in the case the ad fails to load
        event EventHandler<HBAdErrorEventArgs> onAdLoadFailureEvent;

        // triggers if an impression is registered
        event EventHandler<HBAdEventArgs> onAdImpressEvent;

        // triggers if the banner has been clicked
        event EventHandler<HBAdEventArgs> onAdClickEvent;

        // triggers when the ad refreshes
        event EventHandler<HBAdEventArgs> onAdAutoRefreshEvent;

        // triggers on refresh failure
        event EventHandler<HBAdErrorEventArgs> onAdAutoRefreshFailureEvent;

        // triggers when the user closes the ad
        event EventHandler<HBAdEventArgs> onAdCloseButtonClickEvent; 
    }

    public interface IHBRewardedVideoEvents
    {
        // triggers when a rewarded video has been loaded
        event EventHandler<HBAdEventArgs> onAdLoadEvent;

        // triggers when a rewarded video has failed to load (or none have been returned)
        event EventHandler<HBAdErrorEventArgs> onAdLoadFailureEvent;

        // triggers on video start
        event EventHandler<HBAdEventArgs> onAdVideoStartEvent;

        // triggers on video end
        event EventHandler<HBAdEventArgs> onAdVideoEndEvent;

        // triggers if the video fails to play
        event EventHandler<HBAdErrorEventArgs> onAdVideoFailureEvent;

        // triggers when the user has closed the ad
        event EventHandler<HBAdRewardEventArgs> onAdVideoCloseEvent;

        // triggers when the user has clicked the ad
        event EventHandler<HBAdEventArgs> onAdClickEvent;

        // triggers when the user has finsihed watching the ad and should be rewarded
        event EventHandler<HBAdRewardEventArgs> onRewardEvent;           
    }
}