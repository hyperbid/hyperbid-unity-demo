using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HyperBid.Common;
using HyperBid.Api;

namespace HyperBid.Android
{
    public class HBNativeAdClient : AndroidJavaProxy, IHBNativeAdClient
    {
        public event EventHandler<HBAdEventArgs>            onAdLoadEvent;
        public event EventHandler<HBAdErrorEventArgs>       onAdLoadFailureEvent;
        public event EventHandler<HBAdEventArgs>            onAdImpressEvent;
        public event EventHandler<HBAdEventArgs>            onAdClickEvent;
        public event EventHandler<HBAdEventArgs>            onAdVideoStartEvent;
        public event EventHandler<HBAdEventArgs>            onAdVideoEndEvent;
        public event EventHandler<HBAdProgressEventArgs>    onAdVideoProgressEvent;
        public event EventHandler<HBAdEventArgs>            onAdCloseEvent;

        private Dictionary<string, AndroidJavaObject> nativeAdHelperMap = new Dictionary<string, AndroidJavaObject>();

        public HBNativeAdClient(): base("com.hyperbid.unitybridge.nativead.NativeListener")
        {

        }

        public void loadNativeAd(string placementId, string mapJson)
        {
			Debug.Log ("loadNativeAd....jsonmap:"+mapJson);
            if(!nativeAdHelperMap.ContainsKey(placementId)){
                AndroidJavaObject nativeHelper = new AndroidJavaObject(
                    "com.hyperbid.unitybridge.nativead.NativeHelper", this);
                nativeHelper.Call("initNative", placementId);
                nativeAdHelperMap.Add(placementId, nativeHelper);
            }
			try{
                if (nativeAdHelperMap.ContainsKey(placementId)) {
                    nativeAdHelperMap[placementId].Call ("loadNative",mapJson);
				}
			}catch(System.Exception e){
				System.Console.WriteLine("Exception caught: {0}", e);
				Debug.Log ("HBNativeAdClient :  error."+e.Message);
			}
        }


        public bool hasAdReady(string placementId)
        {
			bool isready = false;
			Debug.Log ("hasAdReady....");
			try{
                if (nativeAdHelperMap.ContainsKey(placementId)) {
                    isready = nativeAdHelperMap[placementId].Call<bool> ("isAdReady");
				}
			}catch(System.Exception e){
				System.Console.WriteLine("Exception caught: {0}", e);
				Debug.Log ("HBNativeAdClient :  error."+e.Message);
			}
			return isready;   
        }

        public string checkAdStatus(string placementId)
        {
            string adStatusJsonString = "";
            Debug.Log("HBNativeAdClient : checkAdStatus....");
            try
            {
                if (nativeAdHelperMap.ContainsKey(placementId))
                {
                    adStatusJsonString = nativeAdHelperMap[placementId].Call<string>("getReadyAdInfo");
                }
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Exception caught: {0}", e);
                Debug.Log("HBNativeAdClient :  error." + e.Message);
            }

            return adStatusJsonString;
        }

		public void renderAdToScene(string placementId, HBNativeAdView anyThinkNativeAdView, string mapJson)
        {	
			string showconfig = anyThinkNativeAdView.toJSON ();
            //暂未实现 show
			Debug.Log ("renderAdToScene....showconfig >>>:"+showconfig);
			try{
                if (nativeAdHelperMap.ContainsKey(placementId)) {
                    nativeAdHelperMap[placementId].Call ("show",showconfig, mapJson);
				}
			}catch(System.Exception e){
				Debug.Log ("HBNativeAdClient :  error."+e.Message);
				System.Console.WriteLine("Exception caught: {0}", e);
			}
        }

        public void cleanAdView(string placementId, HBNativeAdView anyThinkNativeAdView)
        {
           //
			Debug.Log ("cleanAdView.... ");
			try{

				if (nativeAdHelperMap.ContainsKey(placementId)) {
					nativeAdHelperMap[placementId].Call ("cleanView");
				}

			}catch(System.Exception e){
				System.Console.WriteLine("Exception caught: {0}", e);
				Debug.Log ("HBNativeAdClient :  error."+e.Message);
			}
        }

        public void onApplicationForces(string placementId, HBNativeAdView anyThinkNativeAdView)
        {
			Debug.Log ("onApplicationForces.... ");
			try{

				if (nativeAdHelperMap.ContainsKey(placementId)) {
					nativeAdHelperMap[placementId].Call ("onResume");
				}

			}catch(System.Exception e){
				System.Console.WriteLine("Exception caught: {0}", e);
				Debug.Log ("HBNativeAdClient :  error."+e.Message);
			}
        }


        public void onApplicationPasue(string placementId, HBNativeAdView anyThinkNativeAdView)
        {

			Debug.Log ("onApplicationPasue.... ");
			try{
				if (nativeAdHelperMap.ContainsKey(placementId)) {
					nativeAdHelperMap[placementId].Call ("onPause");
				}
			}catch(System.Exception e){
				System.Console.WriteLine("Exception caught: {0}", e);
				Debug.Log ("HBNativeAdClient :  error."+e.Message);
			}
        }

        public void cleanCache(string placementId)
        {
			Debug.Log ("cleanCache....");
			try{
                if (nativeAdHelperMap.ContainsKey(placementId)) {
                    nativeAdHelperMap[placementId].Call ("clean");
				}
			}catch(System.Exception e){
				System.Console.WriteLine("Exception caught: {0}", e);
				Debug.Log ("HBNativeAdClient :  error."+e.Message);
			}
        }

        /**
     * 广告展示回调
     *
     * @param view
     */
        public void onAdImpressed(string placementId, string callbackJson)
        {
            Debug.Log("onAdImpressed...unity3d.");
            onAdImpressEvent?.Invoke(this, new HBAdEventArgs(placementId, callbackJson));
        }

        /**
     * 广告点击回调
     *
     * @param view
     */
        public void onAdClicked(string placementId, string callbackJson)
        {
            Debug.Log("onAdClicked...unity3d.");
            onAdClickEvent?.Invoke(this, new HBAdEventArgs(placementId, callbackJson));
        }

        /**
     * 广告视频开始回调
     *
     * @param view
     */
        public void onAdVideoStart(string placementId)
        {
            Debug.Log("onAdVideoStart...unity3d.");
            onAdVideoStartEvent?.Invoke(this, new HBAdEventArgs(placementId));
        }

        /**
     * 广告视频结束回调
     *
     * @param view
     */
        public void onAdVideoEnd(string placementId)
        {
            Debug.Log("onAdVideoEnd...unity3d.");
            onAdVideoEndEvent?.Invoke(this, new HBAdEventArgs(placementId));
        }

        /**
     * 广告视频进度回调
     *
     * @param view
     */
        public void onAdVideoProgress(string placementId,int progress)
        {
            Debug.Log("onAdVideoProgress...progress[" + progress + "]");
            onAdVideoProgressEvent?.Invoke(this, new HBAdProgressEventArgs(placementId, "", progress));
        }

        /**
   * 广告视频进度回调
   *
   * @param view
   */
        public void onAdCloseButtonClicked(string placementId, string callbackJson)
        {
            Debug.Log("onAdCloseButtonClicked...unity3d");
            onAdCloseEvent?.Invoke(this, new HBAdEventArgs(placementId, callbackJson));
        }


        /**
     * 广告加载成功
     */
        public void onAdLoaded(string placementId)
        {
            Debug.Log("onNativeAdLoaded...unity3d.");
            onAdLoadEvent?.Invoke(this, new HBAdEventArgs(placementId));
        }

        /**
     * 广告加载失败
     */
        public void onNativeAdLoadFail(string placementId,string code, string msg)
        {
            Debug.Log("onNativeAdLoadFail...unity3d. code:" + code + " msg:" + msg);
            onAdLoadFailureEvent?.Invoke(this, new HBAdErrorEventArgs(placementId, msg, code));
        }

    }
}
