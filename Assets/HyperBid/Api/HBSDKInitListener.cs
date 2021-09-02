using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HyperBid.Api
{
    public interface HBSDKInitListener
    {

        void initSuccess();
        void initFail(string message);
    }
}
