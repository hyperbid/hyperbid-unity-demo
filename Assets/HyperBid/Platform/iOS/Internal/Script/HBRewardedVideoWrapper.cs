using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using AOT;
using HyperBid.ThirdParty.MiniJSON;
using HyperBid.iOS;
public class HBRewardedVideoWrapper:HBAdWrapper {
    static private Dictionary<string, HBRewardedVideoAdClient> clients;
	static private string CMessageReceiverClass = "HBRewardedVideoWrapper";

    static public void InvokeCallback(string callback, Dictionary<string, object> msgDict) {
        Debug.Log("Unity: HBRewardedVideoWrapper::InvokeCallback(" + callback + " , " + msgDict + ")");
        Dictionary<string, object> extra = new Dictionary<string, object>();
        if (msgDict.ContainsKey("extra")) { extra = msgDict["extra"] as Dictionary<string, object>; }
    	if (callback.Equals("OnRewardedVideoLoaded")) {
    		OnRewardedVideoLoaded((string)msgDict["placement_id"]);
    	} else if (callback.Equals("OnRewardedVideoLoadFailure")) {
    		Dictionary<string, object> errorDict = new Dictionary<string, object>();
            Dictionary<string, object> errorMsg = msgDict["error"] as Dictionary<string, object>;
    		if (errorMsg["code"] != null) { errorDict.Add("code", errorMsg["code"]); }
    		if (errorMsg["reason"] != null) { errorDict.Add("message", errorMsg["reason"]); }
    		OnRewardedVideoLoadFailure((string)msgDict["placement_id"], errorDict);
    	} else if (callback.Equals("OnRewardedVideoPlayFailure")) {
    		Dictionary<string, object> errorDict = new Dictionary<string, object>();
    		Dictionary<string, object> errorMsg = msgDict["error"] as Dictionary<string, object>;
            if (errorMsg.ContainsKey("code")) { errorDict.Add("code", errorMsg["code"]); }
            if (errorMsg.ContainsKey("reason")) { errorDict.Add("message", errorMsg["reason"]); }
    		OnRewardedVideoPlayFailure((string)msgDict["placement_id"], errorDict);
    	} else if (callback.Equals("OnRewardedVideoPlayStart")) {
    		OnRewardedVideoPlayStart((string)msgDict["placement_id"], Json.Serialize(extra));
    	} else if (callback.Equals("OnRewardedVideoPlayEnd")) {
    		OnRewardedVideoPlayEnd((string)msgDict["placement_id"], Json.Serialize(extra));
    	} else if (callback.Equals("OnRewardedVideoClick")) {
    		OnRewardedVideoClick((string)msgDict["placement_id"], Json.Serialize(extra));
    	} else if (callback.Equals("OnRewardedVideoClose")) {
    		OnRewardedVideoClose((string)msgDict["placement_id"], (bool)msgDict["rewarded"], Json.Serialize(extra));
        } else if (callback.Equals("OnRewardedVideoReward")) {
            OnRewardedVideoReward((string)msgDict["placement_id"], Json.Serialize(extra));
        }
    }

    //Public method(s)
    static public void setClientForPlacementID(string placementID, HBRewardedVideoAdClient client) {
        if (clients == null) clients = new Dictionary<string, HBRewardedVideoAdClient>();
        if (clients.ContainsKey(placementID)) clients.Remove(placementID);
        clients.Add(placementID, client);
    }

    static public void setExtra(Dictionary<string, object> extra) {
    	Debug.Log("Unity: HBRewardedVideoWrapper::setExtra()");
    	HBUnityCBridge.SendMessageToC(CMessageReceiverClass, "setExtra:", new object[]{extra});
    }

    static public void loadRewardedVideo(string placementID, string customData) {
    	Debug.Log("Unity: HBRewardedVideoWrapper::loadRewardedVideo(" + placementID + ")");
    	HBUnityCBridge.SendMessageToC(CMessageReceiverClass, "loadRewardedVideoWithPlacementID:customDataJSONString:callback:", new object[]{placementID, customData != null ? customData : ""}, true);
    }

    static public bool isRewardedVideoReady(string placementID) {
        Debug.Log("Unity: HBRewardedVideoWrapper::isRewardedVideoReady(" + placementID + ")");
    	return HBUnityCBridge.SendMessageToC(CMessageReceiverClass, "rewardedVideoReadyForPlacementID:", new object[]{placementID});
    }

    static public void showRewardedVideo(string placementID, string mapJson) {
	    Debug.Log("Unity: HBRewardedVideoWrapper::showRewardedVideo(" + placementID + ")");
    	HBUnityCBridge.SendMessageToC(CMessageReceiverClass, "showRewardedVideoWithPlacementID:extraJsonString:", new object[]{placementID, mapJson});
    }

    static public void clearCache() {
        Debug.Log("Unity: HBRewardedVideoWrapper::clearCache()");
    	HBUnityCBridge.SendMessageToC(CMessageReceiverClass, "clearCache", null);
    }

    static public string checkAdStatus(string placementID) {
        Debug.Log("Unity: HBRewardedVideoWrapper::checkAdStatus(" + placementID + ")");
        return HBUnityCBridge.GetStringMessageFromC(CMessageReceiverClass, "checkAdStatus:", new object[]{placementID});
    }

    //Callbacks
    static public void OnRewardedVideoLoaded(string placementID) {
    	Debug.Log("Unity: HBRewardedVideoWrapper::OnRewardedVideoLoaded()");
        if (clients[placementID] != null) clients[placementID].onRewardedVideoAdLoaded(placementID);
    }

    static public void OnRewardedVideoLoadFailure(string placementID, Dictionary<string, object> errorDict) {
    	Debug.Log("Unity: HBRewardedVideoWrapper::OnRewardedVideoLoadFailure()");
        Debug.Log("placementID = " + placementID + "errorDict = " + Json.Serialize(errorDict));
        if (clients[placementID] != null) clients[placementID].onRewardedVideoAdFailed(placementID, (string)errorDict["code"], (string)errorDict["message"]);
    }

     static public void OnRewardedVideoPlayFailure(string placementID, Dictionary<string, object> errorDict) {
    	Debug.Log("Unity: HBRewardedVideoWrapper::OnRewardedVideoPlayFailure()");
        if (clients[placementID] != null) clients[placementID].onRewardedVideoAdPlayFailed(placementID, (string)errorDict["code"], (string)errorDict["message"]);

    }

    static public void OnRewardedVideoPlayStart(string placementID, string callbackJson) {
    	Debug.Log("Unity: HBRewardedVideoWrapper::OnRewardedVideoPlayStart()");
        if (clients[placementID] != null) clients[placementID].onRewardedVideoAdPlayStart(placementID, callbackJson);
    }

    static public void OnRewardedVideoPlayEnd(string placementID, string callbackJson) {
    	Debug.Log("Unity: HBRewardedVideoWrapper::OnRewardedVideoPlayEnd()");
        if (clients[placementID] != null) clients[placementID].onRewardedVideoAdPlayEnd(placementID, callbackJson);
    }

    static public void OnRewardedVideoClick(string placementID, string callbackJson) {
    	Debug.Log("Unity: HBRewardedVideoWrapper::OnRewardedVideoClick()");
        if (clients[placementID] != null) clients[placementID].onRewardedVideoAdPlayClicked(placementID, callbackJson);
    }

    static public void OnRewardedVideoClose(string placementID, bool rewarded, string callbackJson) {
    	Debug.Log("Unity: HBRewardedVideoWrapper::OnRewardedVideoClose()");
        if (clients[placementID] != null) clients[placementID].onRewardedVideoAdClosed(placementID, rewarded, callbackJson);
    }
    static public void OnRewardedVideoReward(string placementID, string callbackJson)
    {
        Debug.Log("Unity: HBRewardedVideoWrapper::OnRewardedVideoReward()");
        if (clients[placementID] != null) clients[placementID].onRewardedVideoReward(placementID, callbackJson);
    }

}


