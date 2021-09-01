#if UNITY_ANDROID || UNITY_IOS
    #define UNITY_HYPERBID_SUPPORT
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using UnityEngine.UI;

using HyperBid.Common;
using HyperBid.Api;

#if UNITY_ANDROID
    using HyperBid.Android;
#elif UNITY_IOS
    using HyperBid.iOS;
#endif

public class HyperBidComponent : MonoBehaviour
{
    // app key and app id taken from the HyperBid dashboard
    static readonly string _appKey = "dcd2834dba9ffd7d654cc8859174070e";

    static readonly string _appid =
            #if UNITY_ANDROID
                "a60948274823ff";
            #elif UNITY_IOS
                "";
            #else
                "";
            #endif

    static public bool _isHyperbidInitialized = false;

    private readonly string _appChannel     = "testChannelUnity";
    private readonly string _appSubChannel  = "testSubChannelUnity";

    private bool _isLogDebug = true;

    private string[] _gpdrStrings = {
        "personalized",
        "non-personalized",
        "unknown"
    };

    // callback listener for getUserLocation
    // called to detect if an user is based in EU in order to create the GDPR dialog
    private class IsGDPRRequiredListener : ATGetUserLocationListener {
        public void didGetUserLocation(int location)
        {
            Debug.Log("User location:" + location);
            // if the user is in EU and no gdpr policy has been accepted yet 
            // (HBSDKAPI.UNKNOWN is the default value if no checks have been made)
            if(location == HBSDKAPI.kATUserLocationInEU && HBSDKAPI.getGDPRLevel() == HBSDKAPI.UNKNOWN)
            {
                HBSDKAPI.showGDPRAuth();
            }
        }
    }

    private class InitListener : HBSDKInitListener {
        public void initSuccess() {
            Debug.Log("HyperBid has been intialized succesfully");
            _isHyperbidInitialized = true;
        }

        public void initFail(string message) {
            Debug.LogError("Failed to initialize hyperbid: " + message);
            _isHyperbidInitialized = false;
        }
    }


    private string GetGDPRString(int level)
    {
        if(level < _gpdrStrings.Length)
            return _gpdrStrings[level];
        else
            return _gpdrStrings[HBSDKAPI.UNKNOWN];
    }

    private void InitSdk() {

        #if !UNITY_HYPERBID_SUPPORT
            Debug.LogError("HyperBid supports only Android and iOS. Please buld the proper apk/app and test directly on the device.");
            return;
        #endif
        // (Optional) Set custom map information, which can match the list of advertisers configured in the background (App latitude)
        //Note: Calling this method will clear the information set by the setChannel() and setSubChannel() methods. If this information is set, please reset it after calling this method
        HBSDKAPI.initCustomMap(new Dictionary<string, string> { { "unity3d_data", "test_data" } });

        // (Optional) Set custom map information to match the list of advertisers configured in the background (Placement latitude)
        // HBSDKAPI.setCustomDataForPlacementID(new Dictionary<string, string> { { "unity3d_data_pl", "test_data_pl" } }, _placementId);

        // (Optional) Set channel information. You can use this channel information to distinguish the advertising data of each channel in the background
        //Note: If you use the initCustomMap() method, you must call this method after the initCustomMap() method
        HBSDKAPI.setChannel(_appChannel);

        //(Optional)Set sub-channel information. You can use this channel information to distinguish the sub-channel advertising data of each channel in the background
        //Note: If you use the initCustomMap() method, you must call this method after the initCustomMap() method
        HBSDKAPI.setSubChannel(_appSubChannel);

        //Set to enable the debug log (It is strongly recommended that the test phase be opened to facilitate troubleshooting)
        HBSDKAPI.setLogDebug(_isLogDebug);

        //Check if the current network is EU
        HBSDKAPI.getUserLocation(new IsGDPRRequiredListener());
        
        // initialized the sdk with the keys provided in the dashboard
        HBSDKAPI.initSDK(_appid, _appKey, new InitListener());
    }

    public void SetDetailsText() {
        string message = "";

        #if UNITY_HYPERBID_SUPPORT
            message += "HyperBid Status: " + (_isHyperbidInitialized ? "Active" : "Inactive") + '\n';
            message += "App Id: "  + _appid + '\n';
            message += "App Key: " + _appKey + '\n';
            message += "GDPR policy: " + GetGDPRString(HBSDKAPI.getGDPRLevel()) + '\n';
            message += "Is EU user: " + HBSDKAPI.isEUTraffic() + '\n';
            message += "Debug log enabled: " + _isLogDebug;
        #else
            message = "Invalid platform, please test on Android or iOS";
        #endif

        var detailsTextUI = GameObject.Find("detailsHyperTxt/Text").GetComponent<Text>();
        
        if(detailsTextUI)
            detailsTextUI.text = message;
    }

    public void OnButtonPressed() {
        InitSdk();
    }
}
