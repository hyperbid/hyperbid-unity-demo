//
//  HBAdManager+Interstitial.h
//  HyperBidInterstitial
//
//  Created by Leo on 21/09/2018.
//  Copyright © 2018 Leo. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "HBInterstitialDelegate.h"
extern NSString *const kHBInterstitialExtraMediationNameKey;
extern NSString *const kHBInterstitialExtraUserIDKey;
extern NSString *const kHBInterstitialExtraUserFeatureKey;
extern NSString *const kHBInterstitialExtraLocationEnabledFlagKey;
extern NSString *const kHBInterstitialExtraMuteStartPlayingFlagKey;
extern NSString *const kHBInterstitialExtraFallbackFullboardBackgroundColorKey;
extern NSString *const kHBInterstitialExtraAdSizeKey;//Supported by TT interstitial, defaults to 600 X 600
extern NSString *const kHBInterstitialExtraUsesRewardedVideo;

extern NSString *const kHBInterstitialExtraAdSize600_400;
extern NSString *const kHBInterstitialExtraAdSize600_600;
extern NSString *const kHBInterstitialExtraAdSize600_900;
@interface HBAdManager (Interstitial)
-(BOOL) isReadyInterstitialWithPlacementID:(NSString*)placementID;
-(HBCheckLoadModel*) checkInterstitialReadyAdInfo:(NSString*)placementID;

// v5.7.53+
- (NSArray<NSDictionary *> *)getInterstitialValidAdsForPlacementID:(NSString *)placementID;

-(void) showInterstitialAd:(NSString*)placementID inViewController:(UIViewController*)viewController delegate:(id<HBInterstitialDelegate>)delegate;
-(void) showInterstitialAd:(NSString*)placementID scene:(NSString*)scene inViewController:(UIViewController*)viewController delegate:(id<HBInterstitialDelegate>)delegate;

- (void)entryInterstitialScenarioWithPlacementID:(NSString *)placementID scene:(NSString *)scene;
@end
