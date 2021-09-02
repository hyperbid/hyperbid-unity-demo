using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using AOT;
using HyperBid.ThirdParty.MiniJSON;
using HyperBid.iOS;
using HyperBid.Api;

public class HBNativeBannerAdWrapper : HBAdWrapper {
	static private Dictionary<string, HBNativeBannerAdClient> clients;
    static private string CMessageReceiverClass = "HBNativeBannerAdWrapper";

    static public void InvokeCallback(string callback, Dictionary<string, object> msgDict) {
        Debug.Log("Unity: HBNativeBannerAdWrapper::InvokeCallback()");
        Dictionary<string, object> extra = new Dictionary<string, object>();
        if (msgDict.ContainsKey("extra")) { extra = msgDict["extra"] as Dictionary<string, object>; }
        if (callback.Equals("OnNativeBannerAdLoaded")) {
    		OnNativeBannerAdLoaded((string)msgDict["placement_id"]);
    	} else if (callback.Equals("OnNativeBannerAdLoadingFailure")) {
    		Dictionary<string, object> errorDict = new Dictionary<string, object>();
            Dictionary<string, object> errorMsg = msgDict["error"] as Dictionary<string, object>;
    		if (errorMsg.ContainsKey("code")) { errorDict.Add("code", errorMsg["code"]); }
            if (errorMsg.ContainsKey("reason")) { errorDict.Add("message", errorMsg["reason"]); }
    		OnNativeBannerAdLoadingFailure((string)msgDict["placement_id"], errorDict);
    	} else if (callback.Equals("OnNaitveBannerAdShow")) {
    		OnNaitveBannerAdShow((string)msgDict["placement_id"], Json.Serialize(extra));
    	} else if (callback.Equals("OnNativeBannerAdClick")) {
    		OnNativeBannerAdClick((string)msgDict["placement_id"], Json.Serialize(extra));
    	} else if (callback.Equals("OnNativeBannerAdAutorefresh")) {
    		OnNativeBannerAdAutorefresh((string)msgDict["placement_id"], Json.Serialize(extra));
    	} else if (callback.Equals("OnNativeBannerAdAutorefreshFailed")) {
    		Dictionary<string, object> errorDict = new Dictionary<string, object>();
            Dictionary<string, object> errorMsg = msgDict["error"] as Dictionary<string, object>;
    		if (errorMsg.ContainsKey("code")) { errorDict.Add("code", errorMsg["code"]); }
            if (errorMsg.ContainsKey("reason")) { errorDict.Add("message", errorMsg["reason"]); }
    		OnNativeBannerAdAutorefreshFailed((string)msgDict["placement_id"], errorDict);
    	} else if (callback.Equals("OnNativeBannerAdCloseButtonClicked")) {
    		OnNativeBannerAdCloseButtonClicked((string)msgDict["placement_id"]);
    	} else if (callback.Equals("PauseAudio")) {
            Debug.Log("c# : callback, PauseAudio");
            PauseAudio();
        } else if (callback.Equals("ResumeAudio")) {
            Debug.Log("c# : callback, ResumeAudio");
            ResumeAudio();
        }
    }

    static public void PauseAudio() {
        Debug.Log("HBNativeBannerAdWrapper::PauseAudio()");
    }

    static public void ResumeAudio() {
        Debug.Log("HBNativeBannerAdWrapper::ResumeAudio()");
    }

    static public void setClientForPlacementID(string placementID, HBNativeBannerAdClient client) {
    	Debug.Log("HBNativeBannerAdWrapper::setClientForPlacementID()");
        if (clients == null) clients = new Dictionary<string, HBNativeBannerAdClient>();
        if (clients.ContainsKey(placementID)) clients.Remove(placementID);
        clients.Add(placementID, client);
	}

    static public void loadAd(string placementID, string customData) {
		Debug.Log("HBNativeBannerAdWrapper::loadAd()");
		HBUnityCBridge.SendMessageToC(CMessageReceiverClass, "loadNativeBannerAdWithPlacementID:customDataJSONString:callback:", new object[]{placementID, customData != null ? customData : ""}, true);
	}
	
	static public bool adReady(string placementID) {
		Debug.Log("HBNativeBannerAdWrapper::adReady()");
		return HBUnityCBridge.SendMessageToC(CMessageReceiverClass, "isNativeBannerAdReadyForPlacementID:", new object[]{placementID});
	}

    static public void showAd(string placementID, HBRect rect, Dictionary<string, string> pairs) {
		Debug.Log("HBNativeBannerAdWrapper::showAd()");
		Dictionary<string, object> rectDict = new Dictionary<string, object>{ {"x", rect.x},  {"y", rect.y}, {"width", rect.width}, {"height", rect.height} };
    	HBUnityCBridge.SendMessageToC(CMessageReceiverClass, "showNativeBannerAdWithPlacementID:rect:extra:", new object[]{placementID, Json.Serialize(rectDict), Json.Serialize(pairs != null ? pairs : new Dictionary<string, string>())}, false);
    }

    static public void removeAd(string placementID) {
		Debug.Log("HBNativeBannerAdWrapper::removeAd()");
		HBUnityCBridge.SendMessageToC(CMessageReceiverClass, "removeNativeBannerAdWithPlacementID:", new object[]{placementID}, false);
    }

    //Callbacks
    static private void OnNativeBannerAdLoaded(string placementID) {
		Debug.Log("Unity: HBNativeBannerAdWrapper::OnNativeBannerAdLoaded(" + placementID + ")");
        if (clients[placementID] != null) clients[placementID].onAdLoaded(placementID);
	}

	static private void OnNativeBannerAdLoadingFailure(string placementID, Dictionary<string, object> errorDict) {
		Debug.Log("Unity: HBNativeBannerAdWrapper::OnNativeBannerAdLoadingFailure(" + placementID + ")");
        if (clients[placementID] != null) clients[placementID].onAdLoadFail(placementID, (string)errorDict["code"], (string)errorDict["message"]);
	}

	static private void OnNaitveBannerAdShow(string placementID, string callbackJson) {
		Debug.Log("Unity: HBNativeBannerAdWrapper::OnNaitveBannerAdShow(" + placementID + ")");
        if (clients[placementID] != null) clients[placementID].onAdImpressed(placementID, callbackJson);
	}

    static private void OnNativeBannerAdClick(string placementID, string callbackJson) {
		Debug.Log("Unity: HBNativeBannerAdWrapper::OnNativeBannerAdClick(" + placementID + ")");
        if (clients[placementID] != null) clients[placementID].onAdClicked(placementID, callbackJson);
	}

    static private void OnNativeBannerAdAutorefresh(string placementID, string callbackJson) {
		Debug.Log("Unity: HBNativeBannerAdWrapper::OnNativeBannerAdAutorefresh(" + placementID + ")");
        if (clients[placementID] != null) clients[placementID].onAdAutoRefresh(placementID, callbackJson);
	}

	static private void OnNativeBannerAdAutorefreshFailed(string placementID, Dictionary<string, object> errorDict) {
		Debug.Log("Unity: HBNativeBannerAdWrapper::OnNativeBannerAdAutorefreshFailed(" + placementID + ")");
        if (clients[placementID] != null) clients[placementID].onAdAutoRefreshFailure(placementID, (string)errorDict["code"], (string)errorDict["message"]);
	}

	static private void OnNativeBannerAdCloseButtonClicked(string placementID) {
		Debug.Log("Unity: HBNativeBannerAdWrapper::OnNativeBannerAdCloseButtonClicked(" + placementID + ")");
        if (clients[placementID] != null) clients[placementID].onAdCloseButtonClicked(placementID);
	}
}
