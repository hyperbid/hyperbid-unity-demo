//
//  HBInterstitialAutoAdManager.h
//  HyperBidInterstitial
//
//  Created by Jean on 2021/12/31.
//  Copyright © 2021 HyperBid. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#import "HBInterstitialDelegate.h"

NS_ASSUME_NONNULL_BEGIN

@interface HBInterstitialAutoAdManager : NSObject

@property(nonatomic, weak) id<HBAdLoadingDelegate> delegate;

+ (instancetype)sharedInstance;

- (void)addAutoLoadAdPlacementIDArray:(NSArray <NSString *> *)placementIDArray;
- (void)removeAutoLoadAdPlacementIDArray:(NSArray<NSString *> *)placementIDArray;

- (void)setLocalExtra:(NSDictionary * _Nullable)extra placementID:(NSString *)placementID;
- (BOOL)autoLoadInterstitialReadyForPlacementID:(NSString *)placementID;
- (NSArray<NSDictionary *> *)checkValidAdCachesWithPlacementID:(NSString *)placementID;
- (HBCheckLoadModel *)checkInterstitialReadyAdInfo:(NSString *)placementID;

- (void)showAutoLoadInterstitialWithPlacementID:(NSString*)placementID inViewController:(UIViewController*)viewController delegate:(id<HBInterstitialDelegate>)delegate;
- (void)showAutoLoadInterstitialWithPlacementID:(NSString*)placementID scene:( NSString* _Nullable )scene inViewController:(UIViewController*)viewController delegate:(id<HBInterstitialDelegate>)delegate;

- (void)entryAdScenarioWithPlacementID:(NSString *)placementID scenarioID:(NSString *)scenarioID;

@end

NS_ASSUME_NONNULL_END
