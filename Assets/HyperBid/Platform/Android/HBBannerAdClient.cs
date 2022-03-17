using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HyperBid.Common;
using HyperBid.Api;
namespace HyperBid.Android
{
    public class HBBannerAdClient : AndroidJavaProxy, IHBBannerAdClient
    {

        private Dictionary<string, AndroidJavaObject> bannerHelperMap = new Dictionary<string, AndroidJavaObject>();

        // triggers when a banner ad has been succesfully loaded
        public event EventHandler<HBAdEventArgs> onAdLoadEvent;

        // triggers when a banner ad has failed to load
        public event EventHandler<HBAdErrorEventArgs> onAdLoadFailureEvent;

        // triggers when a banner ad generates an impression
        public event EventHandler<HBAdEventArgs> onAdImpressEvent;

        // triggers when the user clicks a banner ad
        public event EventHandler<HBAdEventArgs> onAdClickEvent;

        // triggers when the ad refreshes
        public event EventHandler<HBAdEventArgs> onAdAutoRefreshEvent;

        // triggers when the ad fails to auto refresh
        public event EventHandler<HBAdErrorEventArgs> onAdAutoRefreshFailureEvent;

        // triggers when the banner ad is closed
        public event EventHandler<HBAdEventArgs> onAdCloseEvent;

        // triggers when the users closes the ad via the button
        public event EventHandler<HBAdEventArgs> onAdCloseButtonTappedEvent;

        public event EventHandler<HBAdEventArgs> onAdStartLoadSource;
        public event EventHandler<HBAdEventArgs> onAdFinishLoadSource;
        public event EventHandler<HBAdErrorEventArgs> onAdFailureLoadSource;
        public event EventHandler<HBAdEventArgs> onAdStartBidding;
        public event EventHandler<HBAdEventArgs> onAdFinishBidding;
        public event EventHandler<HBAdErrorEventArgs> onAdFailBidding;

        public HBBannerAdClient() : base("com.hyperbid.unitybridge.banner.BannerListener")
        {
            
        }


        public void loadBannerAd(string placementId, string mapJson)
        {

            //如果不存在则直接创建对应广告位的helper
            if(!bannerHelperMap.ContainsKey(placementId))
            {
                AndroidJavaObject bannerHelper = new AndroidJavaObject(
                    "com.hyperbid.unitybridge.banner.BannerHelper", this);
                bannerHelper.Call("initBanner", placementId);
                bannerHelperMap.Add(placementId, bannerHelper);
                Debug.Log("HBBannerAdClient : no exit helper ,create helper ");
            }

            try
            {
                Debug.Log("HBBannerAdClient : loadBannerAd ");
                bannerHelperMap[placementId].Call("loadBannerAd", mapJson);
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Exception caught: {0}", e);
                Debug.Log ("HBBannerAdClient :  error."+e.Message);
            }


        }

        public string checkAdStatus(string placementId)
        {
            string adStatusJsonString = "";
            Debug.Log("HBBannerAdClient : checkAdStatus....");
            try
            {
                if (bannerHelperMap.ContainsKey(placementId))
                {
                    adStatusJsonString = bannerHelperMap[placementId].Call<string>("checkAdStatus");
                }
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Exception caught: {0}", e);
                Debug.Log("HBBannerAdClient :  error." + e.Message);
            }

            return adStatusJsonString;
        }

        public string getValidAdCaches(string placementId)
        {
            string validAdCachesString = "";
            Debug.Log("HBBannerAdClient : getValidAdCaches....");
            try
            {
                if (bannerHelperMap.ContainsKey(placementId))
                {
                    validAdCachesString = bannerHelperMap[placementId].Call<string>("getValidAdCaches");
                }
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Exception caught: {0}", e);
                Debug.Log("HBBannerAdClient :  error." + e.Message);
            }

            return validAdCachesString;
        }


        public void showBannerAd(string placementId, string position, string mapJson)
        {
            Debug.Log("HBBannerAdClient : showBannerAd by position" );
            //todo
            try
            {
                if (bannerHelperMap.ContainsKey(placementId))
                {
                    this.bannerHelperMap[placementId].Call("showBannerAd", position, mapJson);
                }
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Exception caught: {0}", e);
                Debug.Log("HBBannerAdClient :  error." + e.Message);
            }

        }
       

		
        public void showBannerAd(string placementId, HBRect rect, string mapJson)
        {
            Debug.Log("HBBannerAdClient : showBannerAd " );

			try{
                if (bannerHelperMap.ContainsKey(placementId)) {
                    this.bannerHelperMap[placementId].Call ("showBannerAd", rect.x, rect.y, rect.width, rect.height, mapJson);
				}
			}catch(System.Exception e){
				System.Console.WriteLine("Exception caught: {0}", e);
                Debug.Log ("HBBannerAdClient :  error."+e.Message);

			}
        }

        public void cleanBannerAd(string placementId)
        {
			
            Debug.Log("HBBannerAdClient : cleanBannerAd" );

			try{
                if (bannerHelperMap.ContainsKey(placementId)) {
                    this.bannerHelperMap[placementId].Call ("cleanBannerAd");
				}
			}catch(System.Exception e){
				System.Console.WriteLine("Exception caught: {0}", e);
                Debug.Log ("HBBannerAdClient :  error."+e.Message);
			}
        }

        public void hideBannerAd(string placementId) 
        {
            Debug.Log("HBBannerAdClient : hideBannerAd");

            try
            {
                if (bannerHelperMap.ContainsKey(placementId))
                {
                    this.bannerHelperMap[placementId].Call("hideBannerAd");
                }
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Exception caught: {0}", e);
                Debug.Log("HBBannerAdClient :  error." + e.Message);
            }
        }

        //针对已有的进行展示，没有就调用该方法无效
        public void showBannerAd(string placementId)
        {
            Debug.Log("HBBannerAdClient : showBannerAd ");

            try
            {
                if (bannerHelperMap.ContainsKey(placementId))
                {
                    this.bannerHelperMap[placementId].Call("showBannerAd");
                }
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Exception caught: {0}", e);
                Debug.Log("HBBannerAdClient :  error." + e.Message);

            }
        }

        public void cleanCache(string placementId)
        {
            
        }

       
        //广告加载成功
        public void onBannerLoaded(string placementId)
        {
            Debug.Log("onBannerLoaded...unity3d.");
            onAdLoadEvent?.Invoke(this, new HBAdEventArgs(placementId));
        }

        //广告加载失败
        public void onBannerFailed(string placementId,string code, string error)
        {
            Debug.Log("onBannerFailed...unity3d.");
            onAdLoadFailureEvent?.Invoke(this, new HBAdErrorEventArgs(placementId, error, code));
        }

        //广告点击
        public void onBannerClicked(string placementId, string callbackJson)
        {
            Debug.Log("onBannerClicked...unity3d.");
            onAdClickEvent?.Invoke(this, new HBAdEventArgs(placementId, callbackJson));
            
        }

        //广告展示
        public void onBannerShow(string placementId, string callbackJson)
        {
            Debug.Log("onBannerShow...unity3d.");
            onAdImpressEvent?.Invoke(this, new HBAdEventArgs(placementId, callbackJson));
        }

        //广告关闭
        public void onBannerClose(string placementId, string callbackJson)
        {
            Debug.Log("onBannerClose...unity3d.");
            onAdCloseEvent?.Invoke(this, new HBAdEventArgs(placementId, callbackJson));
        }
        //广告关闭
        public void onBannerAutoRefreshed(string placementId, string callbackJson)
        {
            Debug.Log("onBannerAutoRefreshed...unity3d.");
            onAdAutoRefreshEvent?.Invoke(this, new HBAdEventArgs(placementId, callbackJson));
        }
        //广告自动刷新失败
        public void onBannerAutoRefreshFail(string placementId, string code, string msg)
        {
            Debug.Log("onBannerAutoRefreshFail...unity3d.");
            onAdAutoRefreshFailureEvent?.Invoke(this, new HBAdErrorEventArgs(placementId, msg, code));
        }

        // Adsource Listener
        public void startLoadingADSource(string placementId, string callbackJson)
        {
            Debug.Log("Unity: HBBannerAdWrapper::startLoadingADSource()");
            onAdStartLoadSource?.Invoke(this, new HBAdEventArgs(placementId, callbackJson));
        }
        public void finishLoadingADSource(string placementId, string callbackJson)
        {
            Debug.Log("Unity: HBBannerAdWrapper::finishLoadingADSource()");
            onAdFinishLoadSource?.Invoke(this, new HBAdEventArgs(placementId, callbackJson));
        }
        public void failToLoadADSource(string placementId, string callbackJson, string code, string error)
        {
            Debug.Log("Unity: HBBannerAdWrapper::failToLoadADSource()");
            onAdFailureLoadSource?.Invoke(this, new HBAdErrorEventArgs(placementId, callbackJson, code, error));
        }
        public void startBiddingADSource(string placementId, string callbackJson)
        {
            Debug.Log("Unity: HBBannerAdWrapper::startBiddingADSource()");
            onAdStartBidding?.Invoke(this, new HBAdEventArgs(placementId, callbackJson));
        }
        public void finishBiddingADSource(string placementId, string callbackJson)
        {
            Debug.Log("Unity: HBBannerAdWrapper::finishBiddingADSource()");
            onAdFinishBidding?.Invoke(this, new HBAdEventArgs(placementId, callbackJson));
        }
        public void failBiddingADSource(string placementId, string callbackJson, string code, string error)
        {
            Debug.Log("Unity: HBBannerAdWrapper::failBiddingADSource()");
            onAdFailBidding?.Invoke(this, new HBAdErrorEventArgs(placementId, callbackJson, code, error));
        }

    }
}
