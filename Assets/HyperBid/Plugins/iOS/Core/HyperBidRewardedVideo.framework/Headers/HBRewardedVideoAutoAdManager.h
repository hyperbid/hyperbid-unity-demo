//
//  HBRewardedVideoAutoAdManager.h
//  HyperBidRewardedVideo
//
//  Created by Jean on 2021/12/31.
//  Copyright © 2021 HyperBid. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#import "HBRewardedVideoDelegate.h"

NS_ASSUME_NONNULL_BEGIN

@interface HBRewardedVideoAutoAdManager : NSObject

@property(nonatomic, weak) id<HBAdLoadingDelegate> delegate;

+ (instancetype)sharedInstance;

- (void)addAutoLoadAdPlacementIDArray:(NSArray <NSString *> *)placementIDArray;
- (void)removeAutoLoadAdPlacementIDArray:(NSArray<NSString *> *)placementIDArray;


- (void)setLocalExtra:(NSDictionary *)extra placementID:(NSString *)placementID;
- (BOOL)autoLoadRewardedVideoReadyForPlacementID:(NSString *)placementID;
- (NSArray<NSDictionary *> *)checkValidAdCachesWithPlacementID:(NSString *)placementID;
- (HBCheckLoadModel *)checkRewardedVideoReadyAdInfo:(NSString *)placementID;

- (void)showAutoLoadRewardedVideoWithPlacementID:(NSString*)placementID inViewController:(UIViewController*)viewController delegate:(id<HBRewardedVideoDelegate>)delegate;
- (void)showAutoLoadRewardedVideoWithPlacementID:(NSString*)placementID scene:( NSString* _Nullable )scene inViewController:(UIViewController*)viewController delegate:(id<HBRewardedVideoDelegate>)delegate;

- (void)entryAdScenarioWithPlacementID:(NSString *)placementID scenarioID:(NSString *)scenarioID;

@end

NS_ASSUME_NONNULL_END
