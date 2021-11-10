//
//  HBAdManager+Splash.h
//  HyperBidSplash
//
//  Created by Leo on 2018/12/20.
//  Copyright © 2018 Leo. All rights reserved.
//

#import <HyperBidSDK/HyperBidSDK.h>
extern NSString *const kHBSplashExtraCountdownKey;
extern NSString *const kHBSplashExtraTolerateTimeoutKey;
extern NSString *const kHBSplashExtraHideSkipButtonFlagKey;
extern NSString *const kHBSplashExtraBackgroundImageKey;
extern NSString *const kHBSplashExtraBackgroundColorKey;
extern NSString *const kHBSplashExtraSkipButtonCenterKey;
extern NSString *const kHBSplashExtraCustomSkipButtonKey;
extern NSString *const kHBSplashExtraCanClickFlagKey;
extern NSString *const kHBSplashExtraShowDirectionKey;//Supported by KS Splash, defaults to Vertical, 1 to Horizontal
// 5.7.61+
extern NSString *const kHBSplashExtraCountdownIntervalKey;

extern NSString *const kHBSplashExtraPlacementIDKey;
extern NSString *const kHBSplashExtraNetworkFirmID;
extern NSString *const kHBSplashExtraAdSourceIDKey;
#pragma mark - Mintegral
extern NSString *const kHBSplashExtraMintegralAppKey;
extern NSString *const kHBSplashExtraMintegralAppID;
extern NSString *const kHBSplashExtraMintegralPlacementID;
extern NSString *const kHBSplashExtraMintegralUnitID;
#pragma mark - GDT
extern NSString *const kHBSplashExtraGDTAppID;
extern NSString *const kHBSplashExtraGDTUnitID;
#pragma mark - TT
extern NSString *const kHBSplashExtraAppID;
extern NSString *const kHBSplashExtraSlotID;
extern NSString *const kHBSplashExtraPersonalizedTemplateFlag;
extern NSString *const kHBSplashExtraZoomOutKey;
#pragma mark - Baidu
extern NSString *const kHBSplashExtraBaiduAppID;
extern NSString *const kHBSplashExtraBaiduAdPlaceID;
#pragma mark - Sigmob
extern NSString *const kHBSplashExtraSigmobAppKey;
extern NSString *const kHBSplashExtraSigmobAppID;
extern NSString *const kHBSplashExtraSigmobPlacementID;
#pragma mark - Admob
extern NSString *const kHBSplashExtraAdmobAppID;
extern NSString *const kHBSplashExtraAdmobUnitID;
extern NSString *const kHBSplashExtraAdmobOrientation;
#pragma mark - KuaiShou
extern NSString *const kHBSplashExtraKSAppID ;
extern NSString *const kHBSplashExtraKSPosID;

extern NSString *const kHBAdLoadingExtraSplashAdSizeKey;
extern NSString *const kHBSplashExtraRootViewControllerKey;

#pragma mark - Klevin
extern NSString *const kHBSplashExtraKlevinPosID;
extern NSString *const kHBSplashExtraKlevinAppID;


@protocol HBSplashDelegate;
@interface HBAdManager (Splash)
- (void)loadAd:(NSString *)placementID extra:(NSDictionary *)extra delegate:(id<HBSplashDelegate>)delegate containerView:(UIView *)containerView;
- (void)requestSettings:(NSString*)placementID;
- (void)showSplashWithPlacementID:(NSString*)placementID window:(UIWindow*)window delegate:(id<HBSplashDelegate>)delegate;

// v5.7.61+
- (void)showSplashWithPlacementID:(NSString*)placementID window:(UIWindow*)window extra:(NSDictionary *)extra delegate:(id<HBSplashDelegate>)delegate;

- (void)showSplashWithPlacementID:(NSString*)placementID window:(UIWindow*)window windowScene:(UIWindowScene *)windowScene delegate:(id<HBSplashDelegate>)delegate API_AVAILABLE(ios(13.0));
- (BOOL)splashReadyForPlacementID:(NSString *)placementID;
- (BOOL)splashReadyForPlacementID:(NSString *)placementID sendTK:(BOOL)send;

- (HBCheckLoadModel*)checkSplashLoadStatusForPlacementID:(NSString *)placementID;
- (NSArray<NSDictionary *> *)getSplashValidAdsForPlacementID:(NSString *)placementID;

@end
