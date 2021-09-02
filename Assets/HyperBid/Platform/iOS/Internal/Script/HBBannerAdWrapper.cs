using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using AOT;
using HyperBid.ThirdParty.MiniJSON;
using HyperBid.iOS;
using HyperBid.Api;

public class HBBannerAdWrapper:HBAdWrapper {
	static private Dictionary<string, HBBannerAdClient> clients;
	static private string CMessaageReceiverClass = "HBBannerAdWrapper";

	static public void InvokeCallback(string callback, Dictionary<string, object> msgDict) {
        Debug.Log("Unity: HBBannerAdWrapper::InvokeCallback()");
        Dictionary<string, object> extra = new Dictionary<string, object>();
        if (msgDict.ContainsKey("extra")) { extra = msgDict["extra"] as Dictionary<string, object>; }
        if (callback.Equals("OnBannerAdLoad")) {
    		OnBannerAdLoad((string)msgDict["placement_id"]);
    	} else if (callback.Equals("OnBannerAdLoadFail")) {
            Dictionary<string, object> errorMsg = msgDict["error"] as Dictionary<string, object>;
    		OnBannerAdLoadFail((string)msgDict["placement_id"], (string)errorMsg["code"], (string)errorMsg["reason"]);
    	} else if (callback.Equals("OnBannerAdImpress")) {
    		OnBannerAdImpress((string)msgDict["placement_id"], Json.Serialize(extra));
    	} else if (callback.Equals("OnBannerAdClick")) {
    		OnBannerAdClick((string)msgDict["placement_id"], Json.Serialize(extra));
    	} else if (callback.Equals("OnBannerAdAutoRefresh")) {
    		OnBannerAdAutoRefresh((string)msgDict["placement_id"], Json.Serialize(extra));
    	} else if (callback.Equals("OnBannerAdAutoRefreshFail")) {
            Dictionary<string, object> errorMsg = msgDict["error"] as Dictionary<string, object>;
    		OnBannerAdAutoRefreshFail((string)msgDict["placement_id"], (string)errorMsg["code"], (string)errorMsg["reason"]);
    	} else if (callback.Equals("OnBannerAdClose")) {
    		OnBannerAdClose((string)msgDict["placement_id"]);
    	} else if (callback.Equals("OnBannerAdCloseButtonTapped")) {
            OnBannerAdCloseButtonTapped((string)msgDict["placement_id"], Json.Serialize(extra));
        }
    }

    static public void loadBannerAd(string placementID, string customData) {
    	Debug.Log("Unity: HBBannerAdWrapper::loadBannerAd(" + placementID + ")");
    	HBUnityCBridge.SendMessageToC(CMessaageReceiverClass, "loadBannerAdWithPlacementID:customDataJSONString:callback:", new object[]{placementID, customData != null ? customData : ""}, true);
    }

    static public string checkAdStatus(string placementID) {
        Debug.Log("Unity: HBBannerAdWrapper::checkAdStatus(" + placementID + ")");
        return HBUnityCBridge.GetStringMessageFromC(CMessaageReceiverClass, "checkAdStatus:", new object[]{placementID});
    }

    static public void hideBannerAd(string placementID) {
        Debug.Log("Unity: HBBannerAdWrapper::showBannerAd(" + placementID + ")");
        HBUnityCBridge.SendMessageToC(CMessaageReceiverClass, "hideBannerAdWithPlacementID:", new object[]{placementID}, false);
    }

    static public void showBannerAd(string placementID) {
        Debug.Log("Unity: HBBannerAdWrapper::showBannerAd(" + placementID + ")");
        HBUnityCBridge.SendMessageToC(CMessaageReceiverClass, "showBannerAdWithPlacementID:", new object[]{placementID}, false);
    }

    static public void showBannerAd(string placementID, string position)
    {
        Debug.Log("Unity: HBBannerAdWrapper::showBannerAd(" + placementID + "," + position + ")");
        Dictionary<string, object> rectDict = new Dictionary<string, object> { { "position", position } };
        HBUnityCBridge.SendMessageToC(CMessaageReceiverClass, "showBannerAdWithPlacementID:rect:extraJsonString:", new object[] { placementID, Json.Serialize(rectDict), null}, false);
    }

    static public void showBannerAd(string placementID, string position, string mapJson)
    {
        Debug.Log("Unity: HBBannerAdWrapper::showBannerAd(" + placementID + "," + position + ")");
        Dictionary<string, object> rectDict = new Dictionary<string, object> { { "position", position } };
        HBUnityCBridge.SendMessageToC(CMessaageReceiverClass, "showBannerAdWithPlacementID:rect:extraJsonString:", new object[] { placementID, Json.Serialize(rectDict), mapJson}, false);
    }

    static public void showBannerAd(string placementID, HBRect rect) {
        Debug.Log("Unity: HBBannerAdWrapper::showBannerAd(" + placementID + ")");
        Dictionary<string, object> rectDict = new Dictionary<string, object>{ {"x", rect.x},  {"y", rect.y}, {"width", rect.width}, {"height", rect.height}, {"uses_pixel", rect.usesPixel}};
        HBUnityCBridge.SendMessageToC(CMessaageReceiverClass, "showBannerAdWithPlacementID:rect:extraJsonString:", new object[]{placementID, Json.Serialize(rectDict), null}, false);
    }

    static public void showBannerAd(string placementID, HBRect rect, string mapJson) {
    	Debug.Log("Unity: HBBannerAdWrapper::showBannerAd(" + placementID + ")");
        Dictionary<string, object> rectDict = new Dictionary<string, object>{ {"x", rect.x},  {"y", rect.y}, {"width", rect.width}, {"height", rect.height}, {"uses_pixel", rect.usesPixel}};
    	HBUnityCBridge.SendMessageToC(CMessaageReceiverClass, "showBannerAdWithPlacementID:rect:extraJsonString:", new object[]{placementID, Json.Serialize(rectDict), mapJson}, false);
    }

    static public void cleanBannerAd(string placementID) {
    	Debug.Log("Unity: HBBannerAdWrapper::cleanBannerAd(" + placementID + ")");
    	HBUnityCBridge.SendMessageToC(CMessaageReceiverClass, "removeBannerAdWithPlacementID:", new object[]{placementID}, false);
    }

    static public void clearCache() {
        Debug.Log("Unity: HBBannerAdWrapper::clearCache()");
    	HBUnityCBridge.SendMessageToC(CMessaageReceiverClass, "clearCache", null);
    }

    static public void setClientForPlacementID(string placementID, HBBannerAdClient client) {
        if (clients == null) clients = new Dictionary<string, HBBannerAdClient>();
        if (clients.ContainsKey(placementID)) clients.Remove(placementID);
        clients.Add(placementID, client);
	}

	static private void OnBannerAdLoad(string placementID) {
		Debug.Log("Unity: HBBannerAdWrapper::OnBannerAdLoad()");
        if (clients[placementID] != null) clients[placementID].OnBannerAdLoad(placementID);
    }
    
    static private void OnBannerAdLoadFail(string placementID, string code, string message) {
		Debug.Log("Unity: HBBannerAdWrapper::OnBannerAdLoadFail()");
        if (clients[placementID] != null) clients[placementID].OnBannerAdLoadFail(placementID, code, message);
    }
    
    static private void OnBannerAdImpress(string placementID, string callbackJson) {
		Debug.Log("Unity: HBBannerAdWrapper::OnBannerAdImpress()");
        if (clients[placementID] != null) clients[placementID].OnBannerAdImpress(placementID, callbackJson);
    }
    
    static private void OnBannerAdClick(string placementID, string callbackJson) {
		Debug.Log("Unity: HBBannerAdWrapper::OnBannerAdClick()");
        if (clients[placementID] != null) clients[placementID].OnBannerAdClick(placementID, callbackJson);
    }
    
    static private void OnBannerAdAutoRefresh(string placementID, string callbackJson) {
		Debug.Log("Unity: HBBannerAdWrapper::OnBannerAdAutoRefresh()");
        if (clients[placementID] != null) clients[placementID].OnBannerAdAutoRefresh(placementID, callbackJson);
    }
    
    static private void OnBannerAdAutoRefreshFail(string placementID, string code, string message) {
		Debug.Log("Unity: HBBannerAdWrapper::OnBannerAdAutoRefreshFail()");
        if (clients[placementID] != null) clients[placementID].OnBannerAdAutoRefreshFail(placementID, code, message);
    }

    static private void OnBannerAdCloseButtonTapped(string placementID, string callbackJson) {
        Debug.Log("Unity: HBBannerAdWrapper::onAdCloseButtonTapped()");
        if (clients[placementID] != null) clients[placementID].OnBannerAdCloseButtonTapped(placementID, callbackJson);
    }

    static private void OnBannerAdClose(string placementID) {
		Debug.Log("Unity: HBBannerAdWrapper::OnBannerAdClose()");
        if (clients[placementID] != null) clients[placementID].OnBannerAdClose(placementID);
    }
}
