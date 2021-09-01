        
using System;
using System.Collections;

namespace HyperBid.Common
{
        // event arguments for InterstitialArgs
        // use this class to implement your own callbacks
        public class HBAdEventArgs : EventArgs {
            // empty parameter
            static public readonly string noValue = null;

            //the placement id for the given ad (taken from the HyperBid dashboarf)
            public string placementId {get;}

            // the error message if any (empty on success)
            public string errorMessage {get;}

            // the code associated with the error
            public string errorCode {get;}

            // extra payload data given by the callback, different for every callback
            public string jsonMap {get;}

            // true if an error has occured
            public bool isError {get;}

            public HBAdEventArgs(string id, bool failure = false, string error = "", string errCode = "", string json = "") {
                placementId = id;
                errorMessage = error;
                errorCode = errCode;
                jsonMap = json;
                isError = failure;
            }
        }
}