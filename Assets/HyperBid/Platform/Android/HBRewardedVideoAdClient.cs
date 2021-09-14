﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HyperBid.Common;
using HyperBid.Api;
namespace HyperBid.Android
{
    public class HBRewardedVideoAdClient : AndroidJavaProxy,IHBRewardedVideoAdClient
    {
        private Dictionary<string, AndroidJavaObject> videoHelperMap = new Dictionary<string, AndroidJavaObject>();

        public event EventHandler<HBAdEventArgs>        onAdLoadEvent;
        public event EventHandler<HBAdErrorEventArgs>   onAdLoadFailureEvent;
        public event EventHandler<HBAdEventArgs>        onAdVideoStartEvent;
        public event EventHandler<HBAdEventArgs>        onAdVideoEndEvent;
        public event EventHandler<HBAdErrorEventArgs>   onAdVideoFailureEvent;
        public event EventHandler<HBAdRewardEventArgs>  onAdVideoCloseEvent;
        public event EventHandler<HBAdEventArgs>        onAdClickEvent;
        public event EventHandler<HBAdRewardEventArgs>  onRewardEvent;

        public HBRewardedVideoAdClient() : base("com.hyperbid.unitybridge.videoad.VideoListener")
        {
            
        }

        public void loadVideoAd(string placementId, string mapJson)
        {
            //如果不存在则直接创建对应广告位的helper
            if(!videoHelperMap.ContainsKey(placementId))
            {
                AndroidJavaObject videoHelper = new AndroidJavaObject(
                    "com.hyperbid.unitybridge.videoad.VideoHelper", this);
                videoHelper.Call("initVideo", placementId);
                videoHelperMap.Add(placementId, videoHelper);
                Debug.Log("HBRewardedVideoAdClient : no exit helper ,create helper ");
            }

            try
            {
                Debug.Log("HBRewardedVideoAdClient : loadVideoAd ");
                videoHelperMap[placementId].Call("fillVideo", mapJson);
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Exception caught: {0}", e);
				Debug.Log ("HBRewardedVideoAdClient :  error."+e.Message);
            }

        }

        public bool hasAdReady(string placementId)
        {
			bool isready = false;
			Debug.Log ("HBRewardedVideoAdClient : hasAdReady....");
			try{
                if (videoHelperMap.ContainsKey(placementId)) {
                    isready = videoHelperMap[placementId].Call<bool> ("isAdReady");
				}
			}catch(System.Exception e){
				System.Console.WriteLine("Exception caught: {0}", e);
				Debug.Log ("HBRewardedVideoAdClient :  error."+e.Message);
			}
			return isready; 
        }

        public string checkAdStatus(string placementId)
        {
            string adStatusJsonString = "";
            Debug.Log("HBRewardedVideoAdClient : checkAdStatus....");
            try
            {
                if (videoHelperMap.ContainsKey(placementId))
                {
                    adStatusJsonString = videoHelperMap[placementId].Call<string>("getReadyAdInfo");
                }
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Exception caught: {0}", e);
                Debug.Log("HBRewardedVideoAdClient :  error." + e.Message);
            }

            return adStatusJsonString;
        }

        public void showAd(string placementId, string scenario)
        {
			Debug.Log("HBRewardedVideoAdClient : showAd " );

			try{
                if (videoHelperMap.ContainsKey(placementId)) {
                    this.videoHelperMap[placementId].Call ("showVideo", scenario);
				}
			}catch(System.Exception e){
				System.Console.WriteLine("Exception caught: {0}", e);
				Debug.Log ("HBRewardedVideoAdClient :  error."+e.Message);

			}
        }

        //广告加载成功
        public void onRewardedVideoAdLoaded(string placementId)
        {
            Debug.Log("onRewardedVideoAdLoaded...unity3d.");
            onAdLoadEvent?.Invoke(this, new HBAdEventArgs(placementId));
            
        }

        //广告加载失败
        public void onRewardedVideoAdFailed(string placementId,string code, string error)
        {
            Debug.Log("onRewardedVideoAdFailed...unity3d.");
            onAdLoadFailureEvent?.Invoke(this, new HBAdErrorEventArgs(placementId, error, code));
        }

        //开始播放
        public void onRewardedVideoAdPlayStart(string placementId, string callbackJson)
        {
            Debug.Log("onRewardedVideoAdPlayStart...unity3d.");
            onAdVideoStartEvent?.Invoke(this, new HBAdEventArgs(placementId, callbackJson));
        }

        //结束播放
        public void onRewardedVideoAdPlayEnd(string placementId, string callbackJson)
        {
            Debug.Log("onRewardedVideoAdPlayEnd...unity3d.");
            onAdVideoEndEvent?.Invoke(this, new HBAdEventArgs(placementId, callbackJson));
        }

        //播放失败
        public void onRewardedVideoAdPlayFailed(string placementId,string code, string error)
        {
            Debug.Log("onRewardedVideoAdPlayFailed...unity3d.");
            onAdVideoFailureEvent?.Invoke(this, new HBAdErrorEventArgs(placementId, error, code));
        }
        //广告关闭
        public void onRewardedVideoAdClosed(string placementId,bool isRewarded, string callbackJson)
        {
            Debug.Log("onRewardedVideoAdClosed...unity3d.");
            onAdVideoCloseEvent?.Invoke(this, new HBAdRewardEventArgs(placementId, callbackJson, isRewarded));
        }
        //广告点击
        public void onRewardedVideoAdPlayClicked(string placementId, string callbackJson)
        {
            Debug.Log("onRewardedVideoAdPlayClicked...unity3d.");
            onAdClickEvent?.Invoke(this, new HBAdEventArgs(placementId, callbackJson));
        }

        //广告激励下发
        public void onReward(string placementId, string callbackJson)
        {
            Debug.Log("onReward...unity3d.");
            onRewardEvent?.Invoke(this, new HBAdRewardEventArgs(placementId, callbackJson, true));
        }
       
    }
}
