using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HyperBid.Common;
using HyperBid.Api;
using AOT;
using System;
using HyperBid.ThirdParty.LitJson;

namespace HyperBid.iOS {
	public class HBSDKAPIClient : IHBSDKAPIClient {
        static private ATGetUserLocationListener locationListener;
        static private ATGetAreaListener areaListener;

        public HBSDKAPIClient () {
            Debug.Log("Unity:HBSDKAPIClient::HBSDKAPIClient()");
		}
		public void initSDK(string appId, string appKey) {
			Debug.Log("Unity:HBSDKAPIClient::initSDK(string, string)");
			initSDK(appId, appKey, null);
	    }

	    public void initSDK(string appId, string appKey, HBSDKInitListener listener) {
	    	Debug.Log("Unity:HBSDKAPIClient::initSDK(string, string, HBSDKInitListener)");
	    	bool started = HBManager.StartSDK(appId, appKey);
            if (listener != null)
            {
                if (started)
                {
                    listener.initSuccess();
                }
                else
                {
                    listener.initFail("Failed to init.");
                }
            }
	    }

        [MonoPInvokeCallback(typeof(Func<string, int>))]
       static public int DidGetUserLocation(string location)
        {
            if (locationListener != null) { locationListener.didGetUserLocation(Int32.Parse(location)); }
            return 0;
        }

        [MonoPInvokeCallback(typeof(Func<string, int>))]
        static public int GetAreaInfo(string msg)
        {
            Debug.Log("Unity:HBSDKAPIClient::GetAreaInfo(" + msg + ")");
            if (areaListener != null) 
            { 
                JsonData msgJsonData = JsonMapper.ToObject(msg);
                IDictionary idic = (System.Collections.IDictionary)msgJsonData;

                if (idic.Contains("areaCode")) {
                    string areaCode = (string)msgJsonData["areaCode"];
                    Debug.Log("Unity:HBSDKAPIClient::GetAreaInfo::areaCode(" + areaCode + ")");
                    areaListener.onArea(areaCode);
                }
                
                if (idic.Contains("errorMsg")) { 
                    string errorMsg = (string)msgJsonData["errorMsg"];
                    Debug.Log("Unity:HBSDKAPIClient::GetAreaInfo::errorMsg(" + errorMsg + ")");
                    areaListener.onError(errorMsg);
                }
            }
            return 0;
        }

        public void getUserLocation(ATGetUserLocationListener listener)
        {
            Debug.Log("Unity:HBSDKAPIClient::getUserLocation()");
            HBSDKAPIClient.locationListener = listener;
            HBManager.getUserLocation(DidGetUserLocation);
        }

        public void setGDPRLevel(int level) {
	    	Debug.Log("Unity:HBSDKAPIClient::setGDPRLevel()");
	    	HBManager.SetDataConsent(level);
	    }

	    public void showGDPRAuth() {
	    	Debug.Log("Unity:HBSDKAPIClient::showGDPRAuth()");
	    	HBManager.ShowGDPRAuthDialog();
	    }

	    public void setPurchaseFlag() {
			HBManager.setPurchaseFlag();
		}

		public void clearPurchaseFlag() {
			HBManager.clearPurchaseFlag();
		}

		public bool purchaseFlag() {
			return HBManager.purchaseFlag();
		}

	    public void addNetworkGDPRInfo(int networkType, string mapJson) {
	    	Debug.Log("Unity:HBSDKAPIClient::addNetworkGDPRInfo()");
	    	HBManager.SetNetworkGDPRInfo(networkType, mapJson);
	    }

        public void setChannel(string channel)
        {
            HBManager.setChannel(channel);
        }

        public void setSubChannel(string subchannel)
        {
            HBManager.setSubChannel(subchannel);
        }

        public void initCustomMap(string jsonMap)
        {
            HBManager.setCustomMap(jsonMap);
        }

        public void setCustomDataForPlacementID(string customData, string placementID)
        {
            HBManager.setCustomDataForPlacementID(customData, placementID);
        }

        public void setLogDebug(bool isDebug)
        {
            HBManager.setLogDebug(isDebug);
        }

        public int getGDPRLevel()
        {
            return HBManager.GetDataConsent();
        }

        public bool isEUTraffic()
        {
            return HBManager.isEUTraffic();
        }

        public void deniedUploadDeviceInfo(string deniedInfo)
        {
            HBManager.deniedUploadDeviceInfo(deniedInfo);
        }

        public void setExcludeBundleIdArray(string bundleIds)
        {
            Debug.Log("Unity:HBSDKAPIClient::setExcludeBundleIdArray()");
            HBManager.setExcludeBundleIdArray(bundleIds);
        }

        public void setExcludeAdSourceIdArrayForPlacementID(string placementID, string adSourceIds) 
        {
            Debug.Log("Unity:HBSDKAPIClient::setExcludeAdSourceIdArrayForPlacementID()");
            HBManager.setExcludeAdSourceIdArrayForPlacementID(placementID, adSourceIds);
        }
        
        public void setSDKArea(int area)
        {
            Debug.Log("Unity:HBSDKAPIClient::setSDKArea()");
            HBManager.setSDKArea(area);
        }
        
        public void getArea(ATGetAreaListener listener)
        {
            Debug.Log("Unity:HBSDKAPIClient::getArea()");
            HBSDKAPIClient.areaListener = listener;
            HBManager.getArea(GetAreaInfo);
        }
        
        public void setWXStatus(bool install)
        {
            Debug.Log("Unity:HBSDKAPIClient::setWXStatus()");
            HBManager.setWXStatus(install);
        }
        
        public void setLocation(double longitude, double latitude)
        {
            Debug.Log("Unity:HBSDKAPIClient::setLocation()");
            HBManager.setLocation(longitude, latitude);
        }
	}
}
