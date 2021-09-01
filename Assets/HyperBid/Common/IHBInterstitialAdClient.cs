using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HyperBid.Api;
using HyperBid.Common;

namespace HyperBid.Common
{
    public interface IHBInterstitialAdClient 
    {
		/* 
		 * events used for callbacks
         * in order to monitor the state of your ads it is advised to subscribe to the following events
         * e.g: myInterstitialAd.onAdLoadFailed += myErrorCallback;
         */
        event EventHandler<HBAdEventArgs> onAdLoad; 	// called when the interstitial ad is loaded from the provider
        event EventHandler<HBAdEventArgs> onAdLoadFailed;  // if no ad has been returned or a network error has occured
        event EventHandler<HBAdEventArgs> onAdShow;  // called when the ad is shown
        event EventHandler<HBAdEventArgs> onAdShowFailed;  // called if the ad has failed to be shown
        event EventHandler<HBAdEventArgs> onAdClose;  // called when the ad is closed
        event EventHandler<HBAdEventArgs> onAdClick;  // called when an user has clicked an ad
        event EventHandler<HBAdEventArgs> onAdPlayVideo;  // called when a video ad has started playing
        event EventHandler<HBAdEventArgs> onAdPlayVideoFailed;  // called if a video as has failed to be displayed
        event EventHandler<HBAdEventArgs> onAdEndVideo;  // called when ad video has finished


		/***
		 * 请求广告  
		 * @param placementId  广告位id
		 * @parm mapJson 各平台的私有属性 一般可以不调用
		 */
        void loadInterstitialAd(string placementId, string mapJson);
		/***
		 * 
		 * 设置监听回调接口
		 * 
		 * @param listener  
		 */
        bool hasInterstitialAdReady(string placementId);
        /**
        * 获取广告状态信息（是否正在加载、是否存在可以展示广告、广告缓存详细信息）
        * @param unityid
        * 
        */
        string checkAdStatus(string placementId);
        /***
		 * 显示广告
		 */
        void showInterstitialAd(string placementId, string mapJson);
    }
}
