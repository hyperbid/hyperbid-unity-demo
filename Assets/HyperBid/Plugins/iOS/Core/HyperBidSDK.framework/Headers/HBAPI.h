//
//  HBAPI.h
//  HyperBidSDK
//
//  Created by Leo on 09/04/2018.
//  Copyright © 2018 Leo. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
extern NSString *const kHBADDelegateExtraECPMLevelKey;
extern NSString *const kHBADDelegateExtraSegmentIDKey;
extern NSString *const kHBADDelegateExtraScenarioIDKey;
extern NSString *const kHBADDelegateExtraChannelKey;
extern NSString *const kHBADDelegateExtraSubChannelKey;
extern NSString *const kHBADDelegateExtraCustomRuleKey;
extern NSString *const kHBADDelegateExtraIDKey;
extern NSString *const kHBADDelegateExtraAdunitIDKey;
extern NSString *const kHBADDelegateExtraPublisherRevenueKey;
extern NSString *const kHBADDelegateExtraCurrencyKey;
extern NSString *const kHBADDelegateExtraCountryKey;
extern NSString *const kHBADDelegateExtraFormatKey;
extern NSString *const kHBADDelegateExtraPrecisionKey;
extern NSString *const kHBADDelegateExtraNetworkTypeKey;
extern NSString *const kHBADDelegateExtraNetworkPlacementIDKey;
extern NSString *const kHBADDelegateExtraScenarioRewardNameKey;
extern NSString *const kHBADDelegateExtraScenarioRewardNumberKey;
extern NSString *const kHBADDelegateExtraPlacementRewardNameKey;
extern NSString *const kHBADDelegateExtraPlacementRewardNumberKey;
extern NSString *const kHBADDelegateExtraExtInfoKey;
extern NSString *const kHBADDelegateExtraOfferIDKey;
extern NSString *const kHBADDelegateExtraCreativeIDKey;
extern NSString *const kHBADDelegateExtraIsDeeplinkKey;
extern NSString *const kHBADDelegateExtraRVUserCustomData;

extern NSString *const HBADShowingErrorDomain;

extern NSString *const HBSDKAdLoadingErrorMsg;
extern NSString *const HBSDKAdLoadFailedErrorMsg;

extern NSString *const HBADLoadingErrorDomain;
extern NSInteger const HBADLoadingErrorCodePlacementStrategyInvalidResponse;
extern NSInteger const HBADLoadingErrorCdoePlacementStragetyNetworkError;
extern NSInteger const HBADLoadingErrorCodeADOfferLoadingFailed;
extern NSInteger const HBADLoadingErrorCodePlacementStrategyNotFound;
extern NSInteger const HBADLoadingErrorCodeADOfferNotFound;
extern NSInteger const HBADLoadingErrorCodeShowIntervalWithinPlacementPacing;
extern NSInteger const HBADLoadingErrorCodeShowTimesExceedsHourCap;
extern NSInteger const HBADLoadingErrorCodeShowTimesExceedsDayCap;
extern NSInteger const HBADLoadingErrorCodeAdapterClassNotFound;
extern NSInteger const HBADLoadingErrorCodeADOfferLoadingTimeout;
extern NSInteger const HBADLoadingErrorCodeSDKNotInitalizedProperly;
extern NSInteger const HBADLoadingErrorCodeDataConsentForbidden;
extern NSInteger const HBADLoadingErrorCodeThirdPartySDKNotImportedProperly;
extern NSInteger const HBADLoadingErrorCodeInvalidInputEncountered;
extern NSInteger const HBADLoadingErrorCodePlacementAdDeliverySwitchOff;
extern NSInteger const HBADLoadingErrorCodePreviousLoadNotFinished;
extern NSInteger const HBADLoadingErrorCodeNoUnitGroupsFoundInPlacement;
extern NSInteger const HBADLoadingErrorCodeUnitGroupsFilteredOut;
extern NSInteger const HBADLoadingErrorCodeFailureTooFrequent;
extern NSInteger const HBADLoadingErrorCodeLoadCapsExceeded;

extern NSInteger const HBADLoadingADXFailedCode;

extern NSString *const HBSDKInitErrorDomain;
extern NSInteger const HBSDKInitErrorCodeDataConsentNotSet;
extern NSInteger const HBSDKInitErrorCodeDataConsentForbidden;

extern NSString *const kHBNetworkNameFyber;
extern NSString *const kHBNetworkNameStartApp;
extern NSString *const kHBNetworkNameFacebook;
extern NSString *const kHBNetworkNameInmobi;
extern NSString *const kHBNetworkNameAdmob;
extern NSString *const kHBNetworkNameFlurry;
extern NSString *const kHBNetworkNameMintegral;
extern NSString *const kHBNetworkNameApplovin;
extern NSString *const kHBNetworkNameGDT;
extern NSString *const kHBNetworkNameMopub;
extern NSString *const kHBNetworkNameTapjoy;
extern NSString *const kHBNetworkNameChartboost;
extern NSString *const kHBNetworkNameIronSource;
extern NSString *const kHBNetworkNameVungle;
extern NSString *const kHBNetworkNameAdColony;
extern NSString *const kHBNetworkNameUnityAds;
extern NSString *const kHBNetworkNameTT;
extern NSString *const kHBNetworkNameOneway;
extern NSString *const kHBNetworkNameAppnext;
extern NSString *const kHBNetworkNameYeahmobi;
extern NSString *const kHBNetworkNameBaidu;
extern NSString *const kHBNetworkNameMobPower;
extern NSString *const kHBNetworkNameNend;
extern NSString *const kHBNetworkNameMaio;
extern NSString *const kHBNetworkNameSigmob;
extern NSString *const kHBNetworkNameMyOffer;
extern NSString *const kHBNetworkNameKS;
extern NSString *const kHBNetworkNameOgury;
extern NSString *const kHBNetworkNameGoogleAdManager;
extern NSString *const kHBNetworkNameADX;
extern NSString *const kHBNetworkNameHelium;
extern NSString *const kHBNetworkNameMintegralOnlineApi;
extern NSString *const kHBNetworkNameGDTOnlineApi;
extern NSString *const kHBNetworkNameKidoz;
extern NSString *const kHBNetworkNameMyTarget;
extern NSString *const kHBNetworkNameMobrain;
extern NSString *const kHBNetworkNameMax;

extern NSString *const kHBInmobiGDPRStringKey;
extern NSString *const kHBInmobiConsentStringKey;

extern NSString *const kHBAdmobConsentStatusKey;
extern NSString *const kHBAdmobUnderAgeKey;

extern NSString *const kHBApplovinConscentStatusKey;
extern NSString *const kHBApplovinUnderAgeKey;

extern NSString *const kHBTapjoyConsentValueKey;
extern NSString *const kHBTapjoyGDPRSubjectionKey;

extern NSString *const kHBFlurryConsentGDPRScopeFlagKey;
extern NSString *const kHBFlurryConsentConsentStringKey;

extern NSString *const kHBAdColonyGDPRConsiderationFlagKey;
extern NSString *const kHBAdColonyGDPRConsentStringKey;

extern NSString *const kHBYeahmobiGDPRConsentValueKey;
extern NSString *const kHBYeahmobiGDPRConsentTypeKey;

extern NSString *const kHBCustomDataUserIDKey;//string
extern NSString *const kHBCustomDataAgeKey;//Integer
extern NSString *const kHBCustomDataGenderKey;//Integer
extern NSString *const kHBCustomDataNumberOfIAPKey;//Integer
extern NSString *const kHBCustomDataIAPAmountKey;//Double
extern NSString *const kHBCustomDataIAPCurrencyKey;//string
extern NSString *const kHBCustomDataChannelKey;//string
extern NSString *const kHBCustomDataSubchannelKey;//string
extern NSString *const kHBCustomDataSegmentIDKey;//int

extern NSString *const kHBDeviceDataInfoOSVersionNameKey;
extern NSString *const kHBDeviceDataInfoOSVersionCodeKey;
extern NSString *const kHBDeviceDataInfoPackageNameKey;
extern NSString *const kHBDeviceDataInfoAppVersionNameKey;
extern NSString *const kHBDeviceDataInfoAppVersionCodeKey;
extern NSString *const kHBDeviceDataInfoBrandKey;
extern NSString *const kHBDeviceDataInfoModelKey;
extern NSString *const kHBDeviceDataInfoScreenKey;
extern NSString *const kHBDeviceDataInfoNetworkTypeKey;
extern NSString *const kHBDeviceDataInfoMNCKey;
extern NSString *const kHBDeviceDataInfoMCCKey;
extern NSString *const kHBDeviceDataInfoLanguageKey;
extern NSString *const kHBDeviceDataInfoTimeZoneKey;
extern NSString *const kHBDeviceDataInfoUserAgentKey;
extern NSString *const kHBDeviceDataInfoOrientKey;
extern NSString *const kHBDeviceDataInfoIDFAKey;
extern NSString *const kHBDeviceDataInfoIDFVKey;

typedef NS_ENUM(NSInteger, HBUserLocation) {
    HBUserLocationUnknown = 0,
    HBUserLocationInEU = 1,
    HBUserLocationOutOfEU = 2
};

typedef NS_ENUM(NSInteger, HBDataConsentSet) {
    //Let it default to forbidden if not set
    HBDataConsentSetUnknown = 0,
    HBDataConsentSetPersonalized = 1,
    HBDataConsentSetNonpersonalized = 2
};

typedef NS_ENUM(NSUInteger, HBNetworkTerritory) {
    HBNetworkTerritory_CN = 1,
    HBNetworkTerritory_NO_CN,
};

@interface HBAPI : NSObject

+(NSDictionary<NSNumber*, NSString*>*)networkNameMap;
+(void) setDebugLog:(BOOL)logEnabled;
+(void) initCheck;
+(instancetype)sharedInstance;
+(BOOL) getMPisInit;
+(void) setMPisInit:(BOOL)MPisInit;
/*
 only for adx，onlineApi，MyOffer  banner&splash adLogo，NO by default
 */
+(void) setAdLogoVisible:(BOOL)adLogoVisible;
/*
 only for pangle，Territory_CN by default
 */
+ (void)setNetworkTerritory:(HBNetworkTerritory)territory;
/**
 * Inspect the error parameter to see what's the matter.
 */
-(BOOL) initWithAppID:(NSString*)appID appKey:(NSString*)appKey error:(NSError**)error;

/**
 * consentString might be nil.
 * This method is thread-safe.
 */
-(void) setDataConsentSet:(HBDataConsentSet)dataConsentSet consentString:(NSDictionary<NSString*, NSString*>*)consentString;
/**
 * Whether the device is located in data protected area.
 */
-(BOOL)inDataProtectionArea;

-(void) getGDPRWithCallback:(void(^)(HBUserLocation location))callback;

-(NSString*)psID;
    

/**
 * Show the data consent dialog using the specified constroller as the presenting view controller.
 * viewController might be nil, for which the root view controller of the window will be used instead.
 */
-(void) showGDPRPageInViewController:(UIViewController*)viewController dismissalCallback:(void(^)(void))dismissCallback;
-(void) showGDPRPageInViewController:(UIViewController*)viewController loadingFailureCallback:(void(^)(NSError *error))loadingFailureCallback dismissalCallback:(void(^)(void))dismissCallback;
/**
 * Defaults to forbidden;
 * Thread-safe.
 */

/**
 * Set network consent info individually; according to the network specifications, types for the info you should provide for the networks should be as follows:
 * Mintegral: dictionary, in which you can either set @YES/@NO for key @0 to allow/prevent all three types of data collection(example, @{@0:@YES}), or you can set @YES/@NO each for @1, @2, @3 keys respectively(example, @{@1:@YES, @2:@NO, @3:@YES});for more detailed infomation, please refer to its official website.
 * Inmobi: A dictionary containing the follow keys and values:
    1) An string, @"0" means user not being in GDPR area, @"1" otherwise, with key kHBInmobiGDPRStringKey
    2) An string, @"true" means user having granted consent, @"false" otherwise with key kHBInmobiConsentStringKey
 * Mopub: BOOL wrapped as an NSNumber
 * Admob: A dictionary containing the follow keys and values:
 *       1) An NSInteger wrapped as an NSNumber specifying the consent status(0=unknown, 1=non personalized or 2=personalized), under the key kHBAdmobConsentStatusKey
 *       2) A BOOL wrapped as an NSNumber indicating whether you as a developer are aware that the user is under the age of conscent, under the key kHBAdmobUnderAgeKey
 * Applovin: A dictionary containing the following keys and values:
 *       1) A BOOL wrapped as an NSNumber indicating if user has provided consent for information sharing with AppLovin, under the key
 *       2) A BOOL wrapped as an NSNumber indicating if the user is age restricted
 * Flurry: An NSDictionary containing the following value:
        1) A BOOL wrapped as an NSNumber indicating if the user is whihin GDPR scrope,
        2) An NSDictionary
    Please refer to Flurry's develper website for more detailed info: https://developer.yahoo.com/flurry/docs/integrateflurry/ios/.
 * Tapjoy: A dictionary containing the following key-value pairs (refer to Tapjoy developer website for more details):
 *       1) An string value, "0" (User has not provided consent), "1" (User has provided consent) or a daisybit string as suggested in IAB's Transparency and Consent Framework
 *       2) An BOOL wrapped as an NSNumber, the value should be set to YES when User (Subject) is applicable to GDPR regulations
 *          and NO when User is not applicable to GDPR regulations. In the absence of this call, Tapjoy server makes the
 *          determination of GDPR applicability.
 * Chartboost: A BOOL wrapped as an NSNumber to restrict Chartboost's ability to collect personal data from the device. When this is set to YES. IDFA               and ip address will not
 *   be collected by the SDK or the server. Use this to communicate an EEU Data Subject's preference regarding data collection.
 * Vungle: A NSInteger wrapped as an NSNumber, symentics as follows:
 *         1 (recommended): Publisher controls the GDPR consent process at the user level, then communicates the user’s choice to Vungle. To do this,   developers can collect the user’s consent using their own mechanism, and then use Vungle APIs to update or query the user’s consent status.
 *         2: Allow Vungle to handle the requirements. Vungle will display a consent dialog before playing an ad for a European user, and will remember the user’s consent or rejection for subsequent ads.
 * IronSource: A BOOL wrapped as an NSNumber, refer to IronSource's offical website for its symentics:https://developers.ironsrc.com/ironsource-mobile/ios/advanced-settings/#step-1.
 * AdColony: A dictionary containing the following entries:
 *        1) A BOOL warpped as NSNumber, which is to inform the AdColony service if GDPR should be considered for the user based on if they are they EU citizens or from EU territories. Default is FALSE. This is for GDPR compliance, see https://www.adcolony.com/gdpr/
 *        2) A NSString, which defines end user's consent for information collected from the user. The IAB Europe Transparency and Consent framework defines standard APIs and formats for communicating between Consent Management Platforms (CMPs) collecting consents from end users and vendors embedded on a website or in a mobile application. It provides a unified interface for a seamless integration where CMPs and vendors do not have to integrate manually with hundreds of partners. This is for GDPR compliance through IAB, see https://github.com/InteractiveAdvertisingBureau/GDPR-Transparency-and-Consent-Framework/blob/master/v1.1%20Implementation%20Guidelines.md#vendors
 * UnityAds: A BOOL wrapped as an NSNumber.

 * Example: {
            kHBNetworkNameMintegral:@{@1:@YES, @2:@YES, @3:@NO},
            kHBNetworkNameInmobi:@YES,
            kHBNetworkNameMopub:@NO,
            kHBNetworkNameAdmob:@{kHBAdmobConsentStatusKey:@1, kHBAdmobUnderAgeKey:@NO},
            kHBNetworkNameApplovin:@{kHBApplovinConscentStatusKey:@YES, kHBApplovinUnderAgeKey:@NO},
            kHBNetworkNameTapjoy:@{kHBTapjoyConsentValueKey:@"0",kHBTapjoyGDPRSubjectionKey:@NO},
            kHBNetworkNameChartboost:@NO,
            kHBNetworkNameVungle:@1,
            kHBNetworkNameIronSource:NO,
            kHBNetworkNameAdColony:@{kHBAdColonyGDPRConsentStringKey:@"0",kHBAdColonyGDPRConsiderationFlagKey:@NO},
            kHBNetworkNameUnityAds:@YES
 *          }
 */
@property(nonatomic) NSDictionary *networkConsentInfo;
@property(nonatomic, readonly) HBDataConsentSet dataConsentSet;
@property(nonatomic, readonly) NSDictionary<NSString*, NSString*>* consentStrings;
@property(nonatomic, readonly) NSString *appID;
@property(nonatomic, readonly) NSString *appKey;

/*
 channel & customData has to be set before init
 */
@property(nonatomic) NSString *channel;
@property(nonatomic) NSString *subchannel;
@property(nonatomic) NSDictionary *customData;

@property(nonatomic, readonly, class) NSDate *firstLaunchDate;
-(void) setCustomData:(NSDictionary *)customData forPlacementID:(NSString*)placementID;
-(NSDictionary*) customDataForPlacementID:(NSString*)placementID;
-(NSString*)version;
/*
set exlude appleid list for sdk to filter offers
*/
-(void) setExludeAppleIds:(NSArray *)appleIdArray;
-(NSArray*) exludeAppleIdArray;

/*
set denied Upload Info list for sdk to Control report
*/
-(void) setDeniedUploadInfos:(NSArray *)uploadInfoArray;
-(NSArray*) deniedUploadInfoArray;
-(BOOL) isContainsForDeniedUploadInfoArray:(NSString *)key;

@end
