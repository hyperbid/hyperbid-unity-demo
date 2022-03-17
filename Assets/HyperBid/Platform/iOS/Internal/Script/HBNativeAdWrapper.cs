using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using AOT;
using HyperBid.ThirdParty.LitJson;
using HyperBid.iOS;

public class HBNativeAdWrapper:HBAdWrapper {
    static private Dictionary<string, HBNativeAdClient> clients;
    static private string CMessageReceiverClass = "HBNativeAdWrapper";

    static public new void InvokeCallback(JsonData jsonData) {
        Debug.Log("Unity: HBNativeAdWrapper::InvokeCallback()");
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

        if (callback.Equals("OnNativeAdLoaded")) {
    		OnNativeAdLoaded((string)msgDict["placement_id"]);
    	} else if (callback.Equals("OnNativeAdLoadingFailure")) {
    		Dictionary<string, object> errorDict = new Dictionary<string, object>();
            Dictionary<string, object> errorMsg = JsonMapper.ToObject<Dictionary<string, object>>(msgJsonData["error"].ToJson());
    		if (errorMsg.ContainsKey("code")) { errorDict.Add("code", errorMsg["code"]); }
            if (errorMsg.ContainsKey("reason")) { errorDict.Add("message", errorMsg["reason"]); }
    		OnNativeAdLoadingFailure((string)msgDict["placement_id"], errorDict);
    	} else if (callback.Equals("OnNaitveAdShow")) {
    		OnNaitveAdShow((string)msgDict["placement_id"], extraJson);
    	} else if (callback.Equals("OnNativeAdClick")) {
    		OnNativeAdClick((string)msgDict["placement_id"], extraJson);
    	} else if (callback.Equals("OnNativeAdVideoStart")) {
    		OnNativeAdVideoStart((string)msgDict["placement_id"]);
    	} else if (callback.Equals("OnNativeAdVideoEnd")) {
    		OnNativeAdVideoEnd((string)msgDict["placement_id"]);
        } else if (callback.Equals("OnNativeAdCloseButtonClick")) {
            OnNativeAdCloseButtonClick((string)msgDict["placement_id"], extraJson);
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

    static public string getValidAdCaches(string placementID)
    {
        Debug.Log("Unity: HBNativeAdWrapper::getValidAdCaches(" + placementID + ")");
        return HBUnityCBridge.GetStringMessageFromC(CMessageReceiverClass, "getValidAdCaches:", new object[] { placementID });
    }

    static public void entryScenarioWithPlacementID(string placementID, string scenarioID) 
    {
    	Debug.Log("Unity: HBNativeAdWrapper::entryScenarioWithPlacementID(" + placementID + scenarioID + ")");
    	HBUnityCBridge.SendMessageToC(CMessageReceiverClass, "entryScenarioWithPlacementID:scenarioID:", new object[]{placementID, scenarioID});
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
    // ad source callback
    static public void StartLoadingADSource(string placementID, string callbackJson)
    {
        Debug.Log("Unity: HBNativeAdWrapper::StartLoadingADSource()");
        if (clients[placementID] != null) clients[placementID].startLoadingADSource(placementID, callbackJson);
    }    
    static public void FinishLoadingADSource(string placementID, string callbackJson)
    {
        Debug.Log("Unity: HBNativeAdWrapper::FinishLoadingADSource()");
        if (clients[placementID] != null) clients[placementID].finishLoadingADSource(placementID, callbackJson);
    }

    static public void FailToLoadADSource(string placementID,string callbackJson, Dictionary<string, object> errorDict) 
    {
    	Debug.Log("Unity: HBNativeAdWrapper::FailToLoadADSource()");

        Debug.Log("placementID = " + placementID + "errorDict = " + JsonMapper.ToJson(errorDict));
        if (clients[placementID] != null) clients[placementID].failToLoadADSource(placementID,callbackJson, (string)errorDict["code"], (string)errorDict["message"]);
    }

    static public void StartBiddingADSource(string placementID, string callbackJson)
    {
        Debug.Log("Unity: HBNativeAdWrapper::StartBiddingADSource()");
        if (clients[placementID] != null) clients[placementID].startBiddingADSource(placementID, callbackJson);
    }    
    static public void FinishBiddingADSource(string placementID, string callbackJson)
    {
        Debug.Log("Unity: HBNativeAdWrapper::FinishBiddingADSource()");
        if (clients[placementID] != null) clients[placementID].finishBiddingADSource(placementID, callbackJson);
    }

    static public void FailBiddingADSource(string placementID,string callbackJson, Dictionary<string, object> errorDict) 
    {
    	Debug.Log("Unity: HBNativeAdWrapper::FailBiddingADSource()");

        Debug.Log("placementID = " + placementID + "errorDict = " + JsonMapper.ToJson(errorDict));
        if (clients[placementID] != null) clients[placementID].failBiddingADSource(placementID, callbackJson,(string)errorDict["code"], (string)errorDict["message"]);
    }
}
