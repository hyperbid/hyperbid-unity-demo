using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HyperBid.Api;

namespace HyperBid.Common
{
    public interface IHBSDKAPIClient
    {
        void initSDK(string appId, string appKey);
        void initSDK(string appId, string appKey, HBSDKInitListener listener);
        void getUserLocation(ATGetUserLocationListener listener);
        void setGDPRLevel(int level);
        void showGDPRAuth();
        void addNetworkGDPRInfo(int networkType, string mapJson);
        void setChannel(string channel);
        void setSubChannel(string subchannel);
        void initCustomMap(string cutomMap);
        void setCustomDataForPlacementID(string customData, string placementID);
        void setLogDebug(bool isDebug);
        void setNetworkTerritory(int territory);
        int getGDPRLevel();
        bool isEUTraffic();
        void deniedUploadDeviceInfo(string deniedInfo);
    }
}
