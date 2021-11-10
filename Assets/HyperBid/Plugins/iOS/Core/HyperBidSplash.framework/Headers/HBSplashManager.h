//
//  HBSplashManager.h
//  HyperBidSplash
//
//  Created by Leo on 2018/12/20.
//  Copyright © 2018 Leo. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <HyperBidSDK/HyperBidSDK.h>

extern NSString *const kHBSplashExtraContainerViewKey;
extern NSString *const kHBSplashExtraWindowKey;
extern NSString *const kHBSplashExtraWindowSceneKey;
extern NSString *const kHBSplashExtraLoadingStartDateKey;
extern NSString *const kHBSplashExtraBackgroundImageViewKey;

extern NSString *const kHBSplashExtraHeaderBiddingKey;


@class HBSplash;
@interface HBSplashManager : NSObject<HBAdManagement>
+(instancetype) sharedManager;

- (void)saveAdWithoutPlacementSetting:(HBSplash *)splash extra:(NSDictionary *)extra placementID:(NSString *)placementID;
//- (HBSplash *)splashReadyWithPlacementID:(NSString *)placementID;
- (HBSplash *)splashForPlacementID:(NSString*)placementID invalidateStatus:(BOOL)invalidateStatus extra:(NSDictionary* __autoreleasing*)extra;
- (NSArray<HBSplash *> *)validCachedAdsForPlacementID:(NSString*)placementID;

- (void)removeAdWithSplashAD:(HBSplash *)splash;
- (void)ckearDefaultSplash;

@end
