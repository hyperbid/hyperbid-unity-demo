        
using System;
using System.Collections;
using HyperBid.Api;

namespace HyperBid.Api
{
    public interface IHBBannerEvents
    {
        // triggers when a banner ad has been succesfully loaded
        event EventHandler<HBAdEventArgs> onAdLoadEvent;

        // triggers when a banner ad has failed to load
        event EventHandler<HBAdEventArgs> onAdLoadFailureEvent;

        // triggers when a banner ad generates an impression
        event EventHandler<HBAdEventArgs> onAdImpressEvent;

        // triggers when the user clicks a banner ad
        event EventHandler<HBAdEventArgs> onAdClickEvent;

        // triggers when the ad refreshes
        event EventHandler<HBAdEventArgs> onAdAutoRefreshEvent;

        // triggers when the ad fails to auto refresh
        event EventHandler<HBAdEventArgs> onAdAutoRefreshFailEvent;

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
        event EventHandler<HBAdEventArgs> onAdLoadFailureEvent;

        // called when the ad is shown
        event EventHandler<HBAdEventArgs> onAdShowEvent;

        // called if the ad has failed to be shown
        event EventHandler<HBAdEventArgs> onAdShowFailureEvent;

        // called when the ad is closed
        event EventHandler<HBAdEventArgs> onAdCloseEvent;

        // called when an user has clicked an ad
        event EventHandler<HBAdEventArgs> onAdClickEvent;

        // called when a video ad has started playing
        event EventHandler<HBAdEventArgs> onAdVideoStartEvent;

        // called if an ad video has failed to be displayed
        event EventHandler<HBAdEventArgs> onAdVideoFailureEvent;

        // called when ad video has finished
        event EventHandler<HBAdEventArgs> onAdVideoEndEvent;          
    }

    public interface IHBNativeAdEvents
    {
        // triggers when the native ad is loaded
        event EventHandler<HBAdEventArgs> onAdLoadEvent;

        // triggers in the case the ad fails to load
        event EventHandler<HBAdEventArgs> onAdLoadFailureEvent;

        // triggers when the ad generates an impression
        event EventHandler<HBAdEventArgs> onAdImpressEvent;

        // triggers when the user clicks the ad
        event EventHandler<HBAdEventArgs> onAdClickEvent;

        // triggers when the ad video starts
        event EventHandler<HBAdEventArgs> onAdVideoStartEvent;

        // triggers when the ad video ends
        event EventHandler<HBAdEventArgs> onAdVideoEndEvent;

        // triggers if the ad progresses
        event EventHandler<HBAdEventArgs> onAdVideoProgressEvent;

        // triggers when the ad is closed
        event EventHandler<HBAdEventArgs> onAdCloseEvent;           
    }

    public interface IHBNativeBannerEvents
    {
        // triggers when the native banner is loaded
        event EventHandler<HBAdEventArgs> onAdLoadEvent;

        // triggers in the case the ad fails to load
        event EventHandler<HBAdEventArgs> onAdLoadFailureEvent;

        // triggers if an impression is registered
        event EventHandler<HBAdEventArgs> onAdImpressEvent;

        // triggers if the banner has been clicked
        event EventHandler<HBAdEventArgs> onAdClickEvent;

        // triggers when the ad refreshes
        event EventHandler<HBAdEventArgs> onAdAutoRefreshEvent;

        // triggers on refresh failure
        event EventHandler<HBAdEventArgs> onAdAutoRefreshFailureEvent;

        // triggers when the user closes the ad
        event EventHandler<HBAdEventArgs> onAdCloseButtonClickEvent; 
    }

    public interface IHBRewardedVideoEvents
    {
        // triggers when a rewarded video has been loaded
        event EventHandler<HBAdEventArgs> onAdLoadEvent;

        // triggers when a rewarded video has failed to load (or none have been returned)
        event EventHandler<HBAdEventArgs> onAdLoadFailureEvent;

        // triggers on video start
        event EventHandler<HBAdEventArgs> onAdVideoStartEvent;

        // triggers on video end
        event EventHandler<HBAdEventArgs> onAdVideoEndEvent;

        // triggers if the video fails to play
        event EventHandler<HBAdEventArgs> onAdVideoFailureEvent;

        // triggers when the user has closed the ad
        event EventHandler<HBAdEventArgs> onAdVideoCloseEvent;

        // triggers when the user has clicked the ad
        event EventHandler<HBAdEventArgs> onAdClickEvent;

        // triggers when the user has finsihed watching the ad and should be rewarded
        event EventHandler<HBAdEventArgs> onRewardEvent;           
    }


    /*
     * event arguments passed by all ads, required as a paramter for creating callbacks
     * sample usage: public void onAdLoadCallback(object sender, HBAdEventArgs args) {Debug.Log("Id:" + args.placementId);}
     */
    public class HBAdEventArgs : EventArgs {
            // empty parameter
            static public readonly string noValue = null;

            // the placement id for the given ad (taken from the HyperBid dashboarf)
            public string placementId {get;}

            // the error message if any (empty on success)
            public string errorMessage {get;}

            // the code associated with the error
            public string errorCode {get;}

            // info from the callback json
            public HBCallbackInfo callbackInfo {get;}

            // true if an error has occured
            public bool isError {get;}

            // true if the user should be rewarded for watching the ad (for rewarded video only)
            public bool isRewarded {get;}

            // the current progress of the ad (for native ads only)
            public int adProgress {get;}

            public HBAdEventArgs(string id, bool isFailure = false, string error = "", string errCode = "", string json = "", bool doReward = false, int progress = -1) {
                placementId = id;
                errorMessage = error;
                errorCode = errCode;
                callbackInfo = new HBCallbackInfo(json);
                isError = isFailure;
                isRewarded = doReward;
                adProgress = progress;
            }
        }
}