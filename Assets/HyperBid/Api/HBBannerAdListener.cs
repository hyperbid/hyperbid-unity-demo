using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HyperBid.Api
{
    public interface HBBannerAdListener
    {
		/***
		 * 广告请求成功（注意：对于Android来说，所有回调方法均不在Unity的主线程）
		 */
        void onAdLoad(string placementId);
		/***
		 * 广告请求失败（注意：对于Android来说，所有回调方法均不在Unity的主线程）
		 */ 
        void onAdLoadFail(string placementId, string code, string message);
		/***
		 * 广告展示（注意：对于Android来说，所有回调方法均不在Unity的主线程）
		 */ 
        void onAdImpress(string placementId, HBCallbackInfo callbackInfo);
		/**
		 * 广告点击（注意：对于Android来说，所有回调方法均不在Unity的主线程）
		 */ 
        void onAdClick(string placementId, HBCallbackInfo callbackInfo);
		/**
		 * 广告自动刷新（注意：对于Android来说，所有回调方法均不在Unity的主线程）
		 */
        void onAdAutoRefresh(string placementId, HBCallbackInfo callbackInfo);
        /**
        *广告自动刷新失败（注意：对于Android来说，所有回调方法均不在Unity的主线程）
        */
        void onAdAutoRefreshFail(string placementId, string code, string message);
        /**
        *广告关闭；某些厂商不支持（注意：对于Android来说，所有回调方法均不在Unity的主线程）
        */
        void onAdClose(string placementId);
        /**
        *广告关闭；某些厂商不支持（注意：对于Android来说，所有回调方法均不在Unity的主线程）
        */
        void onAdCloseButtonTapped(string placementId, HBCallbackInfo callbackInfo);
    }
}
