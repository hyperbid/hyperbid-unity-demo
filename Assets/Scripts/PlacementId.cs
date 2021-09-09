using System;

/*
 * Placement ids for all the ad types
 * in order to make requests for ads it is required to 
 * create the proper placement ids & mediation in the
 * hyperbid dashboard
 * in the unity sdk you need to supply different
 * placements for android & iOS as they are
 * treated as a different application
 */
public class PlacementId
{
#if UNITY_ANDROID

    public static readonly string AD_BANNER        = "b60ac5774d0b51";
    public static readonly string INTERSTITIAL     = "b60ac576327734";
    public static readonly string REWARDED_VIDEO   = "b609482b1a6f29";
    public static readonly string NATIVE_AD        = "b6137609524740";

#elif UNITY_IOS || UNITY_IPHONE

    public static readonly string AD_BANNER        = "b609cb47492e07";
    public static readonly string INTERSTITIAL     = "b60947896916dc";
    public static readonly string REWARDED_VIDEO   = "b6094785ed662c";
    public static readonly string NATIVE_AD        = "b613766e6ceae9";

#else

    public static readonly string AD_BANNER        = "";
    public static readonly string INTERSTITIAL     = "";
    public static readonly string REWARDED_VIDEO   = "";
    public static readonly string NATIVE_AD        = "";
    public static readonly string NATIVE_BANNER    = "";

#endif
}

