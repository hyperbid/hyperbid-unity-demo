using System;

public class PlacementId
{
#if UNITY_ANDROID

    public static readonly string AD_BANNER        = "b60ac5774d0b51";
    public static readonly string INTERSTITIAL     = "b60ac576327734";
    public static readonly string REWARDED_VIDEO   = "b609482b1a6f29";
    public static readonly string NATIVE_AD        = "b6125ed179ee63";
    public static readonly string NATIVE_BANNER    = "b6125eb849fd18";

#elif UNITY_IOS || UNITY_IPHONE

    public static readonly string AD_BANNER        = "b609cb47492e07";
    public static readonly string INTERSTITIAL     = "b60947896916dc";
    public static readonly string REWARDED_VIDEO   = "b6094785ed662c";
    public static readonly string NATIVE_AD        = "";
    public static readonly string NATIVE_BANNER    = "";

#else

    public static readonly string AD_BANNER        = "";
    public static readonly string INTERSTITIAL     = "";
    public static readonly string REWARDED_VIDEO   = "";
    public static readonly string NATIVE_AD        = "";
    public static readonly string NATIVE_BANNER    = "";

#endif
}

