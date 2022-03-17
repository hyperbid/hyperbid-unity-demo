using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HyperBid.Api;

namespace HyperBid.Android
{
    public class ATAreaListener : AndroidJavaProxy
    {
        ATGetAreaListener mListener;
        public ATAreaListener(ATGetAreaListener listener): base("com.hyperbid.unitybridge.sdkinit.AreaCallbackListener")
        {
            mListener = listener;
        }


        public void onResultCallback(string area)
        {
            if (mListener != null)
            {
                mListener.onArea(area);   
            }
        }

        public void onErrorCallback(string s)
        {
            if (mListener != null)
            {
               mListener.onError(s);
            }
        }
    }
}
