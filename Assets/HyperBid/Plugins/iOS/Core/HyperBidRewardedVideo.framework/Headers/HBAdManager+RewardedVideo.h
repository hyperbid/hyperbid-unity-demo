//
//  HBAdManager+RewardedVideo.h
//  HyperBidSDK
//
//  Created by Leo on 05/07/2018.
//  Copyright © 2018 Leo. All rights reserved.
//

#import <HyperBidSDK/HyperBidSDK.h>
#import "HBRewardedVideoDelegate.h"
/*
 * Third-party extra data
 */
extern NSString *const kHBAdLoadingExtraKeywordKey;
extern NSString *const kHBAdLoadingExtraUserDataKeywordKey;
extern NSString *const kHBAdLoadingExtraUserIDKey;
extern NSString *const kHBAdLoadingExtraLocationKey;
extern NSString *const kHBAdLoadingExtraMediaExtraKey;
extern NSString *const kHBAdLoadingExtraRewardNameKey;
extern NSString *const kHBAdLoadingExtraRewardAmountKey;
extern NSString *const kHBAdLoadingExtraNetworkPlacementIDKey;

extern NSString *const kHBRewardedVideoCallbackExtraAdsourceIDKey;
extern NSString *const kHBRewardedVideoCallbackExtraNetworkIDKey;

// Klevin SDK
extern NSString *const kHBRewardedVideoKlevinRewardTimeKey;
extern NSString *const kHBRewardedVideoKlevinRewardTriggerKey;


@interface HBAdManager (RewardedVideo)
-(BOOL) isReadyRewardedVideoWithPlacementID:(NSString*)placementID;
-(HBCheckLoadModel*) checkRewardedVideoReadyAdInfo:(NSString*)placementID;
- (NSArray<NSDictionary *> *)getRewardedVideoValidAdsForPlacementID:(NSString *)placementID;
-(void) showRewardedVideoAd:(NSString*)placementID inViewController:(UIViewController*)viewController delegate:(id<HBRewardedVideoDelegate>)delegate;
-(void) showRewardedVideoAd:(NSString*)placementID scene:(NSString*)scene inViewController:(UIViewController*)viewController delegate:(id<HBRewardedVideoDelegate>)delegate;

-(void)entryRewardedVideoScenarioWithPlacementID:(NSString *)placementID scene:(NSString*)scene;

@end
