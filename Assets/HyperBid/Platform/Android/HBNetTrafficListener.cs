using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HyperBid.Api;

namespace HyperBid.Android
{
    public class HBNetTrafficListener : AndroidJavaProxy
    {
        ATGetUserLocationListener mListener;
        public HBNetTrafficListener(ATGetUserLocationListener listener): base("com.hyperbid.unitybridge.sdkinit.SDKEUCallbackListener")
        {
            mListener = listener;
        }


        public void onResultCallback(bool isEu)
        {
            if (mListener != null)
            {
                if (isEu)
                {
                    mListener.didGetUserLocation(HBSDKAPI.kATUserLocationInEU);
                }
                else
                {
                    mListener.didGetUserLocation(HBSDKAPI.kATUserLocationOutOfEU);
                }
            }
        }

        public void onErrorCallback(string s)
        {
            if (mListener != null)
            {
               mListener.didGetUserLocation(HBSDKAPI.kATUserLocationUnknown);
            }
        }
    }
}
