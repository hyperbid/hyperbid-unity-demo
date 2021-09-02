using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using AOT;
using HyperBid.ThirdParty.MiniJSON;
using HyperBid.iOS;

public class HBNativeAdWrapper:HBAdWrapper {
    static private Dictionary<string, HBNativeAdClient> clients;
    static private string CMessageReceiverClass = "HBNativeAdWrapper";

    static public void InvokeCallback(string callback, Dictionary<string, object> msgDict) {
        Debug.Log("Unity: HBNativeAdWrapper::InvokeCallback()");
        Dictionary<string, object> extra = new Dictionary<string, object>();
        if (msgDict.ContainsKey("extra")) { extra = msgDict["extra"] as Dictionary<string, object>; }
        if (callback.Equals("OnNativeAdLoaded")) {
    		OnNativeAdLoaded((string)msgDict["placement_id"]);
    	} else if (callback.Equals("OnNativeAdLoadingFailure")) {
    		Dictionary<string, object> errorDict = new Dictionary<string, object>();
            Dictionary<string, object> errorMsg = msgDict["error"] as Dictionary<string, object>;
    		if (errorMsg.ContainsKey("code")) { errorDict.Add("code", errorMsg["code"]); }
            if (errorMsg.ContainsKey("reason")) { errorDict.Add("message", errorMsg["reason"]); }
    		OnNativeAdLoadingFailure((string)msgDict["placement_id"], errorDict);
    	} else if (callback.Equals("OnNaitveAdShow")) {
    		OnNaitveAdShow((string)msgDict["placement_id"], Json.Serialize(extra));
    	} else if (callback.Equals("OnNativeAdClick")) {
    		OnNativeAdClick((string)msgDict["placement_id"], Json.Serialize(extra));
    	} else if (callback.Equals("OnNativeAdVideoStart")) {
    		OnNativeAdVideoStart((string)msgDict["placement_id"]);
    	} else if (callback.Equals("OnNativeAdVideoEnd")) {
    		OnNativeAdVideoEnd((string)msgDict["placement_id"]);
        } else if (callback.Equals("OnNativeAdCloseButtonClick")) {
            OnNativeAdCloseButtonClick((string)msgDict["placement_id"], Json.Serialize(extra));
        }
    }

    //Public method(s)
    static public void setClientForPlacementID(string placementID, HBNativeAdClient client) {
        if (clients == null) clients = new Dictionary<string, HBNativeAdClient>();
        if (clients.ContainsKey(placementID)) clients.Remove(placementID);
        clients.Add(placementID, client);
    }

    static public void loadNativeAd(string placementID, string customData) {
    	Debug.Log("Unity: HBNativeAdWrapper::loadNativeAd(" + placementID + ")");
    	HBUnityCBridge.SendMessageToC(CMessageReceiverClass, "loadNativeAdWithPlacementID:customDataJSONString:callback:", new object[]{placementID, customData != null ? customData : ""}, true);
    }

    static public bool isNativeAdReady(string placementID) {
        Debug.Log("Unity: HBNativeAdWrapper::isNativeAdReady(" + placementID + ")");
    	return HBUnityCBridge.SendMessageToC(CMessageReceiverClass, "isNativeAdReadyForPlacementID:", new object[]{placementID});
    }

    static public string checkAdStatus(string placementID) {
        Debug.Log("Unity: HBNativeAdWrapper::checkAdStatus(" + placementID + ")");
        return HBUnityCBridge.GetStringMessageFromC(CMessageReceiverClass, "checkAdStatus:", new object[]{placementID});
    }

    static public void showNativeAd(string placementID, string metrics) {
	    Debug.Log("Unity: HBNativeAdWrapper::showNativeAd(" + placementID + ")");
    	HBUnityCBridge.SendMessageToC(CMessageReceiverClass, "showNativeAdWithPlacementID:metricsJSONString:extraJsonString:", new object[]{placementID, metrics, null});
    }

    static public void showNativeAd(string placementID, string metrics, string mapJson) {
        Debug.Log("Unity: HBNativeAdWrapper::showNativeAd(" + placementID + ")");
        HBUnityCBridge.SendMessageToC(CMessageReceiverClass, "showNativeAdWithPlacementID:metricsJSONString:extraJsonString:", new object[]{placementID, metrics, mapJson});
    }

    static public void removeNativeAdView(string placementID) {
        Debug.Log("Unity: HBNativeAdWrapper::removeNativeAdView(" + placementID + ")");
        HBUnityCBridge.SendMessageToC(CMessageReceiverClass, "removeNativeAdViewWithPlacementID:", new object[]{placementID});
    }

    static public void clearCache() {
        Debug.Log("Unity: HBNativeAdWrapper::clearCache()");
    	HBUnityCBridge.SendMessageToC(CMessageReceiverClass, "clearCache", null);
    }

	//Callbacks
	static private void OnNativeAdLoaded(string placementID) {
		Debug.Log("Unity: HBNativeAdWrapper::OnNativeAdLoaded(" + placementID + ")");
        if (clients[placementID] != null) clients[placementID].onNativeAdLoaded(placementID);
	}

	static private void OnNativeAdLoadingFailure(string placementID, Dictionary<string, object> errorDict) {
		Debug.Log("Unity: HBNativeAdWrapper::OnNativeAdLoadingFailure()");
        if (clients[placementID] != null) clients[placementID].onNativeAdLoadFail(placementID, (string)errorDict["code"], (string)errorDict["message"]);
	}

    static private void OnNaitveAdShow(string placementID, string callbackJson) {
        if (clients[placementID] != null) clients[placementID].onAdImpressed(placementID, callbackJson);
		Debug.Log("Unity: HBNativeAdWrapper::OnNaitveAdShow(" + placementID + ")");
	}

    static private void OnNativeAdClick(string placementID, string callbackJson) {
        if (clients[placementID] != null) clients[placementID].onAdClicked(placementID, callbackJson);
		Debug.Log("Unity: HBNativeAdWrapper::OnNativeAdClick(" + placementID + ")");
	}

	static private void OnNativeAdVideoStart(string placementID) {
        if (clients[placementID] != null) clients[placementID].onAdVideoStart(placementID);
		Debug.Log("Unity: HBNativeAdWrapper::OnNativeAdVideoStart(" + placementID + ")");
	}

	static private void OnNativeAdVideoEnd(string placementID) {
        if (clients[placementID] != null) clients[placementID].onAdVideoEnd(placementID);
		Debug.Log("Unity: HBNativeAdWrapper::OnNativeAdVideoEnd(" + placementID + ")");
	}

    static private void OnNativeAdCloseButtonClick(string placementID, string callbackJson)
    {
        if (clients[placementID] != null) clients[placementID].onAdCloseButtonClicked(placementID, callbackJson);
        Debug.Log("Unity: HBNativeAdWrapper::OnNativeAdCloseButtonClick(" + placementID + ")");
    }
}
