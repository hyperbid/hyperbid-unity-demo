using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using AOT;
using HyperBid.ThirdParty.MiniJSON;
using HyperBid.iOS;

public class HBInterstitialAdWrapper:HBAdWrapper {
	static private Dictionary<string, HBInterstitialAdClient> clients;
	static private string CMessaageReceiverClass = "HBInterstitialAdWrapper";

	static public void InvokeCallback(string callback, Dictionary<string, object> msgDict) {
        Debug.Log("Unity: HBInterstitialAdWrapper::InvokeCallback()");
        Dictionary<string, object> extra = new Dictionary<string, object>();
        if (msgDict.ContainsKey("extra")) { extra = msgDict["extra"] as Dictionary<string, object>; }
        if (callback.Equals("OnInterstitialAdLoaded")) {
    		OnInterstitialAdLoaded((string)msgDict["placement_id"]);
    	} else if (callback.Equals("OnInterstitialAdLoadFailure")) {
    		Dictionary<string, object> errorDict = new Dictionary<string, object>();
            Dictionary<string, object> errorMsg = msgDict["error"] as Dictionary<string, object>;
    		if (errorMsg.ContainsKey("code")) { errorDict.Add("code", errorMsg["code"]); }
            if (errorMsg.ContainsKey("reason")) { errorDict.Add("message", errorMsg["reason"]); }
    		OnInterstitialAdLoadFailure((string)msgDict["placement_id"], errorDict);
    	} else if (callback.Equals("OnInterstitialAdVideoPlayFailure")) {
    		Dictionary<string, object> errorDict = new Dictionary<string, object>();
    		Dictionary<string, object> errorMsg = msgDict["error"] as Dictionary<string, object>;
            if (errorMsg.ContainsKey("code")) { errorDict.Add("code", errorMsg["code"]); }
            if (errorMsg.ContainsKey("reason")) { errorDict.Add("message", errorMsg["reason"]); }
    		OnInterstitialAdVideoPlayFailure((string)msgDict["placement_id"], errorDict);
    	} else if (callback.Equals("OnInterstitialAdVideoPlayStart")) {
    		OnInterstitialAdVideoPlayStart((string)msgDict["placement_id"], Json.Serialize(extra));
    	} else if (callback.Equals("OnInterstitialAdVideoPlayEnd")) {
    		OnInterstitialAdVideoPlayEnd((string)msgDict["placement_id"], Json.Serialize(extra));
    	} else if (callback.Equals("OnInterstitialAdShow")) {
    		OnInterstitialAdShow((string)msgDict["placement_id"], Json.Serialize(extra));
    	} else if (callback.Equals("OnInterstitialAdClick")) {
    		OnInterstitialAdClick((string)msgDict["placement_id"], Json.Serialize(extra));
    	} else if (callback.Equals("OnInterstitialAdClose")) {
            OnInterstitialAdClose((string)msgDict["placement_id"], Json.Serialize(extra));
        } else if (callback.Equals("OnInterstitialAdFailedToShow")) {
            OnInterstitialAdFailedToShow((string)msgDict["placement_id"]);
        }
    }

	static public void setClientForPlacementID(string placementID, HBInterstitialAdClient client) {
        if (clients == null) clients = new Dictionary<string, HBInterstitialAdClient>();
        if (clients.ContainsKey(placementID)) clients.Remove(placementID);
        clients.Add(placementID, client);
	}

	static public void loadInterstitialAd(string placementID, string customData) {
    	Debug.Log("Unity: HBInterstitialAdWrapper::loadInterstitialAd(" + placementID + ")");
    	HBUnityCBridge.SendMessageToC(CMessaageReceiverClass, "loadInterstitialAdWithPlacementID:customDataJSONString:callback:", new object[]{placementID, customData != null ? customData : ""}, true);
    }

    static public bool hasInterstitialAdReady(string placementID) {
        Debug.Log("Unity: HBInterstitialAdWrapper::isInterstitialAdReady(" + placementID + ")");
    	return HBUnityCBridge.SendMessageToC(CMessaageReceiverClass, "interstitialAdReadyForPlacementID:", new object[]{placementID});
    }

    static public void showInterstitialAd(string placementID, string mapJson) {
	    Debug.Log("Unity: HBInterstitialAdWrapper::showInterstitialAd(" + placementID + ")");
    	HBUnityCBridge.SendMessageToC(CMessaageReceiverClass, "showInterstitialAdWithPlacementID:extraJsonString:", new object[]{placementID, mapJson});
    }

    static public void clearCache(string placementID) {
        Debug.Log("Unity: HBInterstitialAdWrapper::clearCache()");
    	HBUnityCBridge.SendMessageToC(CMessaageReceiverClass, "clearCache", null);
    }

    static public string checkAdStatus(string placementID) {
        Debug.Log("Unity: HBInterstitialAdWrapper::checkAdStatus(" + placementID + ")");
        return HBUnityCBridge.GetStringMessageFromC(CMessaageReceiverClass, "checkAdStatus:", new object[]{placementID});
    }

    //Callbacks
    static private void OnInterstitialAdLoaded(string placementID) {
    	Debug.Log("Unity: HBInterstitialAdWrapper::OnInterstitialAdLoaded()");
        if (clients[placementID] != null) clients[placementID].OnInterstitialAdLoaded(placementID);
    }

    static private void OnInterstitialAdLoadFailure(string placementID, Dictionary<string, object> errorDict) {
    	Debug.Log("Unity: HBInterstitialAdWrapper::OnInterstitialAdLoadFailure()");
        Debug.Log("placementID = " + placementID + "errorDict = " + Json.Serialize(errorDict));
        if (clients[placementID] != null) clients[placementID].OnInterstitialAdLoadFailure(placementID, (string)errorDict["code"], (string)errorDict["message"]);
    }

     static private void OnInterstitialAdVideoPlayFailure(string placementID, Dictionary<string, object> errorDict) {
    	Debug.Log("Unity: HBInterstitialAdWrapper::OnInterstitialAdVideoPlayFailure()");
        if (clients[placementID] != null) clients[placementID].OnInterstitialAdVideoPlayFailure(placementID, (string)errorDict["code"], (string)errorDict["message"]);
    }

    static private void OnInterstitialAdVideoPlayStart(string placementID, string callbackJson) {
    	Debug.Log("Unity: HBInterstitialAdWrapper::OnInterstitialAdPlayStart()");
        if (clients[placementID] != null) clients[placementID].OnInterstitialAdVideoPlayStart(placementID, callbackJson);
    }

    static private void OnInterstitialAdVideoPlayEnd(string placementID, string callbackJson) {
    	Debug.Log("Unity: HBInterstitialAdWrapper::OnInterstitialAdVideoPlayEnd()");
        if (clients[placementID] != null) clients[placementID].OnInterstitialAdVideoPlayEnd(placementID, callbackJson);
    }

    static private void OnInterstitialAdShow(string placementID, string callbackJson) {
    	Debug.Log("Unity: HBInterstitialAdWrapper::OnInterstitialAdShow()");
        if (clients[placementID] != null) clients[placementID].OnInterstitialAdShow(placementID, callbackJson);
    }

    static private void OnInterstitialAdFailedToShow(string placementID) {
        Debug.Log("Unity: HBInterstitialAdWrapper::OnInterstitialAdFailedToShow()");
        if (clients[placementID] != null) clients[placementID].OnInterstitialAdFailedToShow(placementID);
    }

    static private void OnInterstitialAdClick(string placementID, string callbackJson) {
    	Debug.Log("Unity: HBInterstitialAdWrapper::OnInterstitialAdClick()");
        if (clients[placementID] != null) clients[placementID].OnInterstitialAdClick(placementID, callbackJson);
    }

    static private void OnInterstitialAdClose(string placementID, string callbackJson) {
    	Debug.Log("Unity: HBInterstitialAdWrapper::OnInterstitialAdClose()");
        if (clients[placementID] != null) clients[placementID].OnInterstitialAdClose(placementID, callbackJson);
    }
}



