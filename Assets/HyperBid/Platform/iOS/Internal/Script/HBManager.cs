using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HBManager {
	private static bool SDKStarted;
	public static bool StartSDK(string appID, string appKey) {
		Debug.Log("Unity: HBManager::StartSDK(" + appID + "," + appKey + ")");
		if (!SDKStarted) {
			Debug.Log("Has not been started before, will starting SDK");
			SDKStarted = true;
			return HBUnityCBridge.SendMessageToC("HBUnityManager", "startSDKWithAppID:appKey:", new object[]{appID, appKey});
		} else {
			Debug.Log("SDK has been started already, ignore this call");
            return false;
		}
	}

	public static void setPurchaseFlag() {
		HBUnityCBridge.SendMessageToC("HBUnityManager", "setPurchaseFlag", null);
	}

	public static void clearPurchaseFlag() {
		HBUnityCBridge.SendMessageToC("HBUnityManager", "clearPurchaseFlag", null);
	}

	public static bool purchaseFlag() {
		return HBUnityCBridge.SendMessageToC("HBUnityManager", "clearPurchaseFlag", null);
	}

	public static bool isEUTraffic() {
		return HBUnityCBridge.SendMessageToC("HBUnityManager", "inDataProtectionArea", null);
	}

    public static void getUserLocation(Func<string, int> callback)
    {
        Debug.Log("Unity:HBManager::getUserLocation()");
        HBUnityCBridge.SendMessageToCWithCallBack("HBUnityManager", "getUserLocation:", new object[] { }, callback);
    }

	public static void ShowGDPRAuthDialog() {
		HBUnityCBridge.SendMessageToC("HBUnityManager", "presentDataConsentDialog", null);
	}

	public static int GetDataConsent() {
		return HBUnityCBridge.GetMessageFromC("HBUnityManager", "getDataConsent", null);
	}

	public static void SetDataConsent(int consent) {
		HBUnityCBridge.SendMessageToC("HBUnityManager", "setDataConsent:", new object[]{consent});
	}

	public static void SetNetworkGDPRInfo(int network, string mapJson) {
		HBUnityCBridge.SendMessageToC("HBUnityManager", "setDataConsent:network:", new object[]{mapJson, network});
	}

    public static void setChannel(string channel)
    {
        HBUnityCBridge.SendMessageToC("HBUnityManager", "setChannel:", new object[] {channel});
    }

    public static void setSubChannel(string subchannel)
    {
        HBUnityCBridge.SendMessageToC("HBUnityManager", "setSubChannel:", new object[] {subchannel});
    }

    public static void setCustomMap(string jsonMap)
    {
        HBUnityCBridge.SendMessageToC("HBUnityManager", "setCustomData:", new object[] { jsonMap });
    }

    public static void setCustomDataForPlacementID(string customData, string placementID)
    {
        HBUnityCBridge.SendMessageToC("HBUnityManager", "setCustomData:forPlacementID:", new object[] {customData, placementID});
    }

    public static void setLogDebug(bool isDebug)
    {
        HBUnityCBridge.SendMessageToC("HBUnityManager", "setDebugLog:", new object[] { isDebug ? "true" : "false" });
    }

    public static void deniedUploadDeviceInfo(string deniedInfo)
    {
        HBUnityCBridge.SendMessageToC("HBUnityManager", "deniedUploadDeviceInfo:", new object[] {deniedInfo});
    }

    public static void setExcludeBundleIdArray(string bundleIds)
    {
        Debug.Log("Unity:HBManager::setExcludeBundleIdArray()");
        HBUnityCBridge.SendMessageToC("HBUnityManager", "setExcludeBundleIdArray:", new object[] {bundleIds});
    }

    public static void setExcludeAdSourceIdArrayForPlacementID(string placementID, string adSourceIds) 
    {
        Debug.Log("Unity:HBManager::setExcludeAdSourceIdArrayForPlacementID()");
        HBUnityCBridge.SendMessageToC("HBUnityManager", "setExludePlacementid:unitIDArray:", new object[] {placementID, adSourceIds});
    }
    
    public static void setSDKArea(int area)
    {
        Debug.Log("Unity:HBManager::setSDKArea()");
        HBUnityCBridge.SendMessageToC("HBUnityManager", "setSDKArea:", new object[] {area});
    }
    
    public static void getArea(Func<string, int> callback)
    {
        Debug.Log("Unity:HBManager::getArea()");
        HBUnityCBridge.SendMessageToCWithCallBack("HBUnityManager", "getArea:", new object[] { }, callback);
    }
    
    public static void setWXStatus(bool install)
    {
        Debug.Log("Unity:HBManager::setWXStatus()");
    	HBUnityCBridge.SendMessageToC("HBUnityManager", "setWXStatus:", new object[] {install});
    }
    
    public static void setLocation(double longitude, double latitude)
    {
        Debug.Log("Unity:HBManager::setLocation()");
    	HBUnityCBridge.SendMessageToC("HBUnityManager", "setLocationLongitude:dimension:", new object[] {longitude, latitude});
    }
}
