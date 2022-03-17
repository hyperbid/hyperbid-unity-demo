using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using AOT;
using HyperBid.ThirdParty.LitJson;
using HyperBid.iOS;
public class HBRewardedVideoWrapper:HBAdWrapper {
    static private Dictionary<string, HBRewardedVideoAdClient> clients;
	static private string CMessageReceiverClass = "HBRewardedVideoWrapper";

    static public new void InvokeCallback(JsonData jsonData) {
        Debug.Log("Unity: HBRewardedVideoWrapper::InvokeCallback()");
        string extraJson = "";
        string callback = (string)jsonData["callback"];
        Dictionary<string, object> msgDict = JsonMapper.ToObject<Dictionary<string, object>>(jsonData["msg"].ToJson());
        JsonData msgJsonData = jsonData["msg"];
        IDictionary idic = (System.Collections.IDictionary)msgJsonData;

        if (idic.Contains("extra")) { 
            JsonData extraJsonDate = msgJsonData["extra"];
            if (extraJsonDate != null) {
                extraJson = msgJsonData["extra"].ToJson();
            }
        }
        
    	if (callback.Equals("OnRewardedVideoLoaded")) {
    		OnRewardedVideoLoaded((string)msgDict["placement_id"]);
    	} else if (callback.Equals("OnRewardedVideoLoadFailure")) {

    		Dictionary<string, object> errorDict = new Dictionary<string, object>();
            Dictionary<string, object> errorMsg = JsonMapper.ToObject<Dictionary<string, object>>(msgJsonData["error"].ToJson());
    		if (errorMsg["code"] != null) { errorDict.Add("code", errorMsg["code"]); }
    		if (errorMsg["reason"] != null) { errorDict.Add("message", errorMsg["reason"]); }
    		OnRewardedVideoLoadFailure((string)msgDict["placement_id"], errorDict);

    	} else if (callback.Equals("OnRewardedVideoPlayFailure")) {
    		Dictionary<string, object> errorDict = new Dictionary<string, object>();
    		Dictionary<string, object> errorMsg = JsonMapper.ToObject<Dictionary<string, object>>(msgJsonData["error"].ToJson());
            if (errorMsg.ContainsKey("code")) { errorDict.Add("code", errorMsg["code"]); }
            if (errorMsg.ContainsKey("reason")) { errorDict.Add("message", errorMsg["reason"]); }
    		OnRewardedVideoPlayFailure((string)msgDict["placement_id"], errorDict);
    	} else if (callback.Equals("OnRewardedVideoPlayStart")) {
    		OnRewardedVideoPlayStart((string)msgDict["placement_id"], extraJson);
    	} else if (callback.Equals("OnRewardedVideoPlayEnd")) {
    		OnRewardedVideoPlayEnd((string)msgDict["placement_id"], extraJson);
    	} else if (callback.Equals("OnRewardedVideoClick")) {
    		OnRewardedVideoClick((string)msgDict["placement_id"], extraJson);
    	} else if (callback.Equals("OnRewardedVideoClose")) {
    		OnRewardedVideoClose((string)msgDict["placement_id"], (bool)msgDict["rewarded"], extraJson);
        } else if (callback.Equals("OnRewardedVideoReward")) {
            OnRewardedVideoReward((string)msgDict["placement_id"], extraJson);
        } else if (callback.Equals("OnRewardedVideoAdAgainPlayStart")) {
            OnRewardedVideoAdAgainPlayStart((string)msgDict["placement_id"], extraJson);
        } else if (callback.Equals("OnRewardedVideoAdAgainPlayEnd")) {
            OnRewardedVideoAdAgainPlayEnd((string)msgDict["placement_id"], extraJson);
        } else if (callback.Equals("OnRewardedVideoAdAgainPlayFailed")) {
            Dictionary<string, object> errorDict = new Dictionary<string, object>();
            Dictionary<string, object> errorMsg = JsonMapper.ToObject<Dictionary<string, object>>(msgJsonData["error"].ToJson());
            if (errorMsg.ContainsKey("code")) { errorDict.Add("code", errorMsg["code"]); }
            if (errorMsg.ContainsKey("reason")) { errorDict.Add("message", errorMsg["reason"]); }
            OnRewardedVideoAdAgainPlayFailed((string)msgDict["placement_id"], errorDict);
        } else if (callback.Equals("OnRewardedVideoAdAgainPlayClicked")) {
            OnRewardedVideoAdAgainPlayClicked((string)msgDict["placement_id"], extraJson);
        }else if (callback.Equals("OnAgainReward")) {
            OnAgainReward((string)msgDict["placement_id"], extraJson);
        }else if (callback.Equals("startLoadingADSource")) {
            StartLoadingADSource((string)msgDict["placement_id"], extraJson);
        }else if (callback.Equals("finishLoadingADSource")) {
            FinishLoadingADSource((string)msgDict["placement_id"], extraJson);
        }else if (callback.Equals("failToLoadADSource")) {
    		Dictionary<string, object> errorDict = new Dictionary<string, object>();
            Dictionary<string, object> errorMsg = JsonMapper.ToObject<Dictionary<string, object>>(msgJsonData["error"].ToJson());
    		if (errorMsg["code"] != null) { errorDict.Add("code", errorMsg["code"]); }
    		if (errorMsg["reason"] != null) { errorDict.Add("message", errorMsg["reason"]); }

    		FailToLoadADSource((string)msgDict["placement_id"],extraJson,errorDict);            
        }else if (callback.Equals("startBiddingADSource")) {
            StartBiddingADSource((string)msgDict["placement_id"], extraJson);
           
        }else if (callback.Equals("finishBiddingADSource")) {
            FinishBiddingADSource((string)msgDict["placement_id"], extraJson);
  
        }else if (callback.Equals("failBiddingADSource")) {
        	Dictionary<string, object> errorDict = new Dictionary<string, object>();
            Dictionary<string, object> errorMsg = JsonMapper.ToObject<Dictionary<string, object>>(msgJsonData["error"].ToJson());
    		if (errorMsg["code"] != null) { errorDict.Add("code", errorMsg["code"]); }
    		if (errorMsg["reason"] != null) { errorDict.Add("message", errorMsg["reason"]); }
    		FailBiddingADSource((string)msgDict["placement_id"],extraJson, errorDict);
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

    static public string getValidAdCaches(string placementID)
    {
        Debug.Log("Unity: HBRewardedVideoWrapper::getValidAdCaches(" + placementID + ")");
        return HBUnityCBridge.GetStringMessageFromC(CMessageReceiverClass, "getValidAdCaches:", new object[] { placementID });
    }

    static public void entryScenarioWithPlacementID(string placementID, string scenarioID) 
    {
    	Debug.Log("Unity: HBRewardedVideoWrapper::entryScenarioWithPlacementID(" + placementID + scenarioID + ")");
    	HBUnityCBridge.SendMessageToC(CMessageReceiverClass, "entryScenarioWithPlacementID:scenarioID:", new object[]{placementID, scenarioID});
    }

    //Callbacks
    static public void OnRewardedVideoLoaded(string placementID) {
    	Debug.Log("Unity: HBRewardedVideoWrapper::OnRewardedVideoLoaded()");
        if (clients[placementID] != null) clients[placementID].onRewardedVideoAdLoaded(placementID);
    }

    static public void OnRewardedVideoLoadFailure(string placementID, Dictionary<string, object> errorDict) {
    	Debug.Log("Unity: HBRewardedVideoWrapper::OnRewardedVideoLoadFailure()");
        Debug.Log("placementID = " + placementID + "errorDict = " + JsonMapper.ToJson(errorDict));
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

    //------again callback
    static public void OnRewardedVideoAdAgainPlayStart(string placementID, string callbackJson)
    {
        Debug.Log("Unity: HBRewardedVideoWrapper::onRewardedVideoAdAgainPlayStart()");
        if (clients[placementID] != null) clients[placementID].onRewardedVideoAdAgainPlayStart(placementID, callbackJson);
    }


    static public void OnRewardedVideoAdAgainPlayEnd(string placementID, string callbackJson)
    {
        Debug.Log("Unity: HBRewardedVideoWrapper::onRewardedVideoAdAgainPlayEnd()");
        if (clients[placementID] != null) clients[placementID].onRewardedVideoAdAgainPlayEnd(placementID, callbackJson);
    }


    static public void OnRewardedVideoAdAgainPlayFailed(string placementID, Dictionary<string, object> errorDict)
    {
        Debug.Log("Unity: HBRewardedVideoWrapper::onRewardedVideoAdAgainPlayFailed()");
        if (clients[placementID] != null) clients[placementID].onRewardedVideoAdAgainPlayFailed(placementID, (string)errorDict["code"], (string)errorDict["message"]);
    }


    static public void OnRewardedVideoAdAgainPlayClicked(string placementID, string callbackJson)
    {
        Debug.Log("Unity: HBRewardedVideoWrapper::onRewardedVideoAdAgainPlayClicked()");
        if (clients[placementID] != null) clients[placementID].onRewardedVideoAdAgainPlayClicked(placementID, callbackJson);
    }


    static public void OnAgainReward(string placementID, string callbackJson)
    {
        Debug.Log("Unity: HBRewardedVideoWrapper::onAgainReward()");
        if (clients[placementID] != null) clients[placementID].onAgainReward(placementID, callbackJson);
    }

    // Auto
     static public void addAutoLoadAdPlacementID(string placementID) 
     {
    	Debug.Log("Unity: HBRewardedVideoWrapper::addAutoLoadAdPlacementID(" + placementID + ")");
    	HBUnityCBridge.SendMessageToC(CMessageReceiverClass, "addAutoLoadAdPlacementID:callback:", new object[]{placementID}, true);
    }

    static public void removeAutoLoadAdPlacementID(string placementID) 
    {
    	Debug.Log("Unity: HBRewardedVideoWrapper::removeAutoLoadAdPlacementID(" + placementID + ")");
    	HBUnityCBridge.SendMessageToC(CMessageReceiverClass, "removeAutoLoadAdPlacementID:", new object[]{placementID});
    }
    static public bool autoLoadRewardedVideoReadyForPlacementID(string placementID) 
    {
        Debug.Log("Unity: HBRewardedVideoWrapper::autoLoadRewardedVideoReadyForPlacementID(" + placementID + ")");
    	return HBUnityCBridge.SendMessageToC(CMessageReceiverClass, "autoLoadRewardedVideoReadyForPlacementID:", new object[]{placementID});
    }    
    static public string getAutoValidAdCaches(string placementID)
    {
        Debug.Log("Unity: HBRewardedVideoWrapper::getAutoValidAdCaches");
        return HBUnityCBridge.GetStringMessageFromC(CMessageReceiverClass, "getAutoValidAdCaches:", new object[]{placementID});
    }

    static public string checkAutoAdStatus(string placementID) {
        Debug.Log("Unity: HBRewardedVideoWrapper::checkAutoAdStatus(" + placementID + ")");
        return HBUnityCBridge.GetStringMessageFromC(CMessageReceiverClass, "checkAutoAdStatus:", new object[]{placementID});
    }

    static public void setAutoLocalExtra(string placementID, string customData) 
    {

    	Debug.Log("Unity: HBRewardedVideoWrapper::setAutoLocalExtra(" + placementID + customData + ")");
    	HBUnityCBridge.SendMessageToC(CMessageReceiverClass, "setAutoLocalExtra:customDataJSONString:", new object[] {placementID, customData != null ? customData : ""});
    }

    static public void entryAutoAdScenarioWithPlacementID(string placementID, string scenarioID) 
    {
    	Debug.Log("Unity: HBRewardedVideoWrapper::entryAutoAdScenarioWithPlacementID(" + placementID + scenarioID + ")");
    	HBUnityCBridge.SendMessageToC(CMessageReceiverClass, "entryAutoAdScenarioWithPlacementID:scenarioID:", new object[]{placementID, scenarioID});
    }

    static public void showAutoRewardedVideo(string placementID, string mapJson) {
	    Debug.Log("Unity: HBRewardedVideoWrapper::showAutoRewardedVideo(" + placementID + ")");
    	HBUnityCBridge.SendMessageToC(CMessageReceiverClass, "showAutoRewardedVideoWithPlacementID:extraJsonString:", new object[]{placementID, mapJson});
    }

    // ad source callback
    static public void StartLoadingADSource(string placementID, string callbackJson)
    {
        Debug.Log("Unity: HBRewardedVideoWrapper::StartLoadingADSource()");
        if (clients[placementID] != null) clients[placementID].startLoadingADSource(placementID, callbackJson);
    }    
    static public void FinishLoadingADSource(string placementID, string callbackJson)
    {
        Debug.Log("Unity: HBRewardedVideoWrapper::FinishLoadingADSource()");
        if (clients[placementID] != null) clients[placementID].finishLoadingADSource(placementID, callbackJson);
    }

    static public void FailToLoadADSource(string placementID, string callbackJson, Dictionary<string, object> errorDict) 
    {
    	Debug.Log("Unity: HBRewardedVideoWrapper::FailToLoadADSource()");

        Debug.Log("placementID = " + placementID + "errorDict = " + JsonMapper.ToJson(errorDict));
        if (clients[placementID] != null) clients[placementID].failToLoadADSource(placementID, callbackJson,(string)errorDict["code"], (string)errorDict["message"]);
    }

    static public void StartBiddingADSource(string placementID, string callbackJson)
    {
        Debug.Log("Unity: HBRewardedVideoWrapper::StartBiddingADSource()");
        if (clients[placementID] != null) clients[placementID].startBiddingADSource(placementID, callbackJson);
    }    
    static public void FinishBiddingADSource(string placementID, string callbackJson)
    {
        Debug.Log("Unity: HBRewardedVideoWrapper::FinishBiddingADSource()");
        if (clients[placementID] != null) clients[placementID].finishBiddingADSource(placementID, callbackJson);
    }

    static public void FailBiddingADSource(string placementID, string callbackJson,Dictionary<string, object> errorDict) 
    {
    	Debug.Log("Unity: HBRewardedVideoWrapper::FailBiddingADSource()");

        Debug.Log("placementID = " + placementID + "errorDict = " + JsonMapper.ToJson(errorDict));
        if (clients[placementID] != null) clients[placementID].failBiddingADSource(placementID,callbackJson,(string)errorDict["code"], (string)errorDict["message"]);
    }   




}


