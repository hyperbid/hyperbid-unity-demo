using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using AOT;
using HyperBid.ThirdParty.LitJson;
using HyperBid.iOS;

public class HBInterstitialAdWrapper:HBAdWrapper {
	static private Dictionary<string, HBInterstitialAdClient> clients;
	static private string CMessaageReceiverClass = "HBInterstitialAdWrapper";

	static public new void InvokeCallback(JsonData jsonData) {
        Debug.Log("Unity: HBInterstitialAdWrapper::InvokeCallback()");
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
        
        if (callback.Equals("OnInterstitialAdLoaded")) {
    		OnInterstitialAdLoaded((string)msgDict["placement_id"]);
    	} else if (callback.Equals("OnInterstitialAdLoadFailure")) {
    		Dictionary<string, object> errorDict = new Dictionary<string, object>();
            Dictionary<string, object> errorMsg = JsonMapper.ToObject<Dictionary<string, object>>(msgJsonData["error"].ToJson());
    		if (errorMsg.ContainsKey("code")) { errorDict.Add("code", errorMsg["code"]); }
            if (errorMsg.ContainsKey("reason")) { errorDict.Add("message", errorMsg["reason"]); }
    		OnInterstitialAdLoadFailure((string)msgDict["placement_id"], errorDict);
    	} else if (callback.Equals("OnInterstitialAdVideoPlayFailure")) {
    		Dictionary<string, object> errorDict = new Dictionary<string, object>();
    		Dictionary<string, object> errorMsg = JsonMapper.ToObject<Dictionary<string, object>>(msgJsonData["error"].ToJson());
            if (errorMsg.ContainsKey("code")) { errorDict.Add("code", errorMsg["code"]); }
            if (errorMsg.ContainsKey("reason")) { errorDict.Add("message", errorMsg["reason"]); }
    		OnInterstitialAdVideoPlayFailure((string)msgDict["placement_id"], errorDict);
    	} else if (callback.Equals("OnInterstitialAdVideoPlayStart")) {
    		OnInterstitialAdVideoPlayStart((string)msgDict["placement_id"], extraJson);
    	} else if (callback.Equals("OnInterstitialAdVideoPlayEnd")) {
    		OnInterstitialAdVideoPlayEnd((string)msgDict["placement_id"], extraJson);
    	} else if (callback.Equals("OnInterstitialAdShow")) {
    		OnInterstitialAdShow((string)msgDict["placement_id"], extraJson);
    	} else if (callback.Equals("OnInterstitialAdClick")) {
    		OnInterstitialAdClick((string)msgDict["placement_id"], extraJson);
    	} else if (callback.Equals("OnInterstitialAdClose")) {
            OnInterstitialAdClose((string)msgDict["placement_id"], extraJson);
        } else if (callback.Equals("OnInterstitialAdFailedToShow")) {
            OnInterstitialAdFailedToShow((string)msgDict["placement_id"]);
        }else if (callback.Equals("startLoadingADSource")) {
            StartLoadingADSource((string)msgDict["placement_id"], extraJson);
        }else if (callback.Equals("finishLoadingADSource")) {
            FinishLoadingADSource((string)msgDict["placement_id"], extraJson);
        }else if (callback.Equals("failToLoadADSource")) {

    		Dictionary<string, object> errorDict = new Dictionary<string, object>();
            Dictionary<string, object> errorMsg = JsonMapper.ToObject<Dictionary<string, object>>(msgJsonData["error"].ToJson());
    		if (errorMsg["code"] != null) { errorDict.Add("code", errorMsg["code"]); }
    		if (errorMsg["reason"] != null) { errorDict.Add("message", errorMsg["reason"]); }
    		FailToLoadADSource((string)msgDict["placement_id"],extraJson, errorDict);  

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

    static public string getValidAdCaches(string placementID)
    {
        Debug.Log("Unity: HBInterstitialAdWrapper::checkAdStatus(" + placementID + ")");
        return HBUnityCBridge.GetStringMessageFromC(CMessaageReceiverClass, "getValidAdCaches:", new object[] { placementID });
    }
  
    static public void entryScenarioWithPlacementID(string placementID, string scenarioID) 
    {
    	Debug.Log("Unity: HBInterstitialAdWrapper::entryScenarioWithPlacementID(" + placementID + scenarioID + ")");
    	HBUnityCBridge.SendMessageToC(CMessaageReceiverClass, "entryScenarioWithPlacementID:scenarioID:", new object[]{placementID, scenarioID});
    }

    //Callbacks
    static private void OnInterstitialAdLoaded(string placementID) {
    	Debug.Log("Unity: HBInterstitialAdWrapper::OnInterstitialAdLoaded()");
        if (clients[placementID] != null) clients[placementID].OnInterstitialAdLoaded(placementID);
    }

    static private void OnInterstitialAdLoadFailure(string placementID, Dictionary<string, object> errorDict) {
    	Debug.Log("Unity: HBInterstitialAdWrapper::OnInterstitialAdLoadFailure()");
        Debug.Log("placementID = " + placementID + "errorDict = " + JsonMapper.ToJson(errorDict));
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
    // ad source callback
    static public void StartLoadingADSource(string placementID, string callbackJson)
    {
        Debug.Log("Unity: HBInterstitialAdWrapper::StartLoadingADSource()");
        if (clients[placementID] != null) clients[placementID].startLoadingADSource(placementID, callbackJson);
    }    
    static public void FinishLoadingADSource(string placementID, string callbackJson)
    {
        Debug.Log("Unity: HBInterstitialAdWrapper::FinishLoadingADSource()");
        if (clients[placementID] != null) clients[placementID].finishLoadingADSource(placementID, callbackJson);
    }

    static public void FailToLoadADSource(string placementID,string callbackJson, Dictionary<string, object> errorDict) 
    {
    	Debug.Log("Unity: HBInterstitialAdWrapper::FailToLoadADSource()");

        Debug.Log("placementID = " + placementID + "errorDict = " + JsonMapper.ToJson(errorDict));
        if (clients[placementID] != null) clients[placementID].failToLoadADSource(placementID,callbackJson,(string)errorDict["code"], (string)errorDict["message"]);
    }

    static public void StartBiddingADSource(string placementID, string callbackJson)
    {
        Debug.Log("Unity: HBInterstitialAdWrapper::StartBiddingADSource()");
        if (clients[placementID] != null) clients[placementID].startBiddingADSource(placementID, callbackJson);
    }    
    static public void FinishBiddingADSource(string placementID, string callbackJson)
    {
        Debug.Log("Unity: HBInterstitialAdWrapper::FinishBiddingADSource()");
        if (clients[placementID] != null) clients[placementID].finishBiddingADSource(placementID, callbackJson);
    }

    static public void FailBiddingADSource(string placementID, string callbackJson,Dictionary<string, object> errorDict) 
    {
    	Debug.Log("Unity: HBInterstitialAdWrapper::FailBiddingADSource()");

        Debug.Log("placementID = " + placementID + "errorDict = " + JsonMapper.ToJson(errorDict));
        if (clients[placementID] != null) clients[placementID].failBiddingADSource(placementID,callbackJson,(string)errorDict["code"], (string)errorDict["message"]);
    }

 // Auto
     static public void addAutoLoadAdPlacementID(string placementID) 
     {
    	Debug.Log("Unity: HBInterstitialAdWrapper::addAutoLoadAdPlacementID(" + placementID + ")");
    	HBUnityCBridge.SendMessageToC(CMessaageReceiverClass, "addAutoLoadAdPlacementID:callback:", new object[]{placementID}, true);
    }

    static public void removeAutoLoadAdPlacementID(string placementID) 
    {
    	Debug.Log("Unity: HBInterstitialAdWrapper::removeAutoLoadAdPlacementID(" + placementID + ")");
    	HBUnityCBridge.SendMessageToC(CMessaageReceiverClass, "removeAutoLoadAdPlacementID:", new object[]{placementID});
    }
    static public bool autoLoadInterstitialAdReadyForPlacementID(string placementID) 
    {
        Debug.Log("Unity: HBInterstitialAdWrapper::autoLoadInterstitialAdReadyForPlacementID(" + placementID + ")");
        
    	return HBUnityCBridge.SendMessageToC(CMessaageReceiverClass, "autoLoadInterstitialAdReadyForPlacementID:", new object[]{placementID});
    }    
    static public string getAutoValidAdCaches(string placementID)
    {
        Debug.Log("Unity: HBInterstitialAdWrapper::getAutoValidAdCaches");
        return HBUnityCBridge.GetStringMessageFromC(CMessaageReceiverClass, "getAutoValidAdCaches:", new object[]{placementID});
    }

    static public string checkAutoAdStatus(string placementID) {
        Debug.Log("Unity: HBInterstitialAdWrapper::checkAutoAdStatus(" + placementID + ")");
        return HBUnityCBridge.GetStringMessageFromC(CMessaageReceiverClass, "checkAutoAdStatus:", new object[]{placementID});
    }

    static public void setAutoLocalExtra(string placementID, string customData) 
    {

    	Debug.Log("Unity: HBInterstitialAdWrapper::setAutoLocalExtra(" + placementID + customData + ")");
    	HBUnityCBridge.SendMessageToC(CMessaageReceiverClass, "setAutoLocalExtra:customDataJSONString:", new object[] {placementID, customData != null ? customData : ""});
    }

    static public void entryAutoAdScenarioWithPlacementID(string placementID, string scenarioID) 
    {
    	Debug.Log("Unity: HBInterstitialAdWrapper::entryAutoAdScenarioWithPlacementID(" + placementID + scenarioID + ")");
    	HBUnityCBridge.SendMessageToC(CMessaageReceiverClass, "entryAutoAdScenarioWithPlacementID:scenarioID:", new object[]{placementID, scenarioID});
    }

    static public void showAutoInterstitialAd(string placementID, string mapJson) {
	    Debug.Log("Unity: HBInterstitialAdWrapper::showAutoInterstitialAd(" + placementID + ")");
    	HBUnityCBridge.SendMessageToC(CMessaageReceiverClass, "showAutoInterstitialAd:extraJsonString:", new object[]{placementID, mapJson});
    }



}



