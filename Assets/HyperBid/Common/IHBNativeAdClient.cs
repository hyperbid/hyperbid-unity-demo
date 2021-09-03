﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HyperBid.Api;

namespace HyperBid.Common
{
	public interface IHBNativeAdClient : IHBNativeAdEvents
	{
		/***
		 * 请求广告  
		 * @param placementId  广告位id
		 * @parm mapJson 各平台的私有属性 一般可以不调用
		 */
        void loadNativeAd(string placementId, string mapJson);
		/***
		 * 判断是否有广告存在
		 * 可以在显示广告之前调用
		 * @param placementId  广告位id
		 */
        bool hasAdReady(string placementId);
         /**
         * 获取广告状态信息（是否正在加载、是否存在可以展示广告、广告缓存详细信息）
         * @param unityid
         *
         */
        string checkAdStatus(string placementId);
		/***
		 * 
		 * 展示广告,
		 * @param placementId 
		 * @param anyThinkNativeAdView  这里的属性是显示区域坐标等配置,需要自行设置
         * @parm mapJson
		 */
        void renderAdToScene(string placementId, HBNativeAdView anyThinkNativeAdView, string mapJson);

		/***
		 * 
		 * 清理广告
		 * @param placementId 
		 * @param anyThinkNativeAdView  这里的属性是显示区域坐标等配置,需要自行设置
		 */
        void cleanAdView(string placementId, HBNativeAdView anyThinkNativeAdView);
		/***
		 * 页面显示
		 */
        void onApplicationForces(string placementId, HBNativeAdView anyThinkNativeAdView);
		/***
		 * 页面隐藏
		 */ 
        void onApplicationPasue(string placementId, HBNativeAdView anyThinkNativeAdView);
		/***
		 * 清理缓存
		 */ 
        void cleanCache(string placementId);

    }
}
