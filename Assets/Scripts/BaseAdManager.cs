using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HyperBidDemo
{
    public interface BaseAdManager
    {
        bool IsAdReady();
        bool LoadAd();
        bool ShowAd();
    }
}
