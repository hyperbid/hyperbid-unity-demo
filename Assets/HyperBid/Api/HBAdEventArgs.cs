        
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

        public HBAdErrorEventArgs(String placementId, String callbackJson, String message, String code)
            : base(placementId, callbackJson)
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


    public interface IHCommonEvents
    {
        // triggers when the ad has been succesfully loaded
        event EventHandler<HBAdEventArgs> onAdLoadEvent;

        // triggers when the ad has failed to load
        event EventHandler<HBAdErrorEventArgs> onAdLoadFailureEvent;

        // triggers when a the ad has started to load
        event EventHandler<HBAdEventArgs> onAdStartLoadSource;

        // triggers when a the ad has finished to load
        event EventHandler<HBAdEventArgs> onAdFinishLoadSource;

        // triggers when a the ad has started to load
        event EventHandler<HBAdErrorEventArgs> onAdFailureLoadSource;

        // triggers when a the ad has started to load
        event EventHandler<HBAdEventArgs> onAdStartBidding;

        // triggers when a the ad has started to load
        event EventHandler<HBAdEventArgs> onAdFinishBidding;

        // triggers when a the ad has started to load
        event EventHandler<HBAdErrorEventArgs> onAdFailBidding;
    }


    public interface IHBBannerEvents: IHCommonEvents
    {
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
    
    public interface IHBInterstitialAdEvents : IHCommonEvents
    {
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

    public interface IHBNativeAdEvents : IHCommonEvents
    {
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

    public interface IHBRewardedVideoEvents : IHCommonEvents
    {
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

        event EventHandler<HBAdEventArgs> onPlayAgainStart;

        event EventHandler<HBAdEventArgs> onPlayAgainEnd;

        event EventHandler<HBAdErrorEventArgs> onPlayAgainFailure;

        event EventHandler<HBAdEventArgs> onPlayAgainClick;

        event EventHandler<HBAdEventArgs> onPlayAgainReward;
    }
}