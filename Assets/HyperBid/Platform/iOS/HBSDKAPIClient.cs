using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HyperBid.Common;
using HyperBid.Api;
using AOT;
using System;

namespace HyperBid.iOS {
	public class HBSDKAPIClient : IHBSDKAPIClient {
        static private ATGetUserLocationListener locationListener;
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

        public void setNetworkTerritory(int territory)
        {
            HBManager.setNetworkTerritory(territory);
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
	}
}
