//
//  HBAdManager+Banner.h
//  HyperBidBanner
//
//  Created by Leo on 18/09/2018.
//  Copyright © 2018 Leo. All rights reserved.
//

#import <HyperBidSDK/HyperBidSDK.h>
//Supported by Nend banner only
extern NSString *const kHBBannerLoadingExtraParameters;
extern NSString *const kHBAdLoadingExtraBannerAdSizeKey;//defaults to 320 * 50
extern NSString *const kHBAdLoadingExtraBannerSizeAdjustKey;//Currently supported by Nend
extern NSString *const kHBAdLoadingExtraBannerSizeUsesFilledKey;//Currently supported by Pangle, defaults to YES for filled

extern NSString *const kHBAdLoadingExtraAdmobBannerSizeKey;//Admob Adaptive width
extern NSString *const kHBAdLoadingExtraAdmobAdSizeFlagsKey;//Admob AdSize flags

@class HBBannerView;
@interface HBAdManager (Banner)
-(BOOL) isReadyBannerAdWithPlacementID:(NSString*)placementID;
-(BOOL) isReadyBannerAdWithPlacementID:(NSString*)placementID sendTK:(BOOL)send;
/*
 nil will be returned if you try to show banner ad for the placementID if it's not ready.
 */
- (nullable HBBannerView*)getBannerViewWithPlacementID:(NSString*)placementID;
- (nullable HBBannerView*)getBannerViewWithPlacementID:(NSString*)placementID extra:(NSDictionary *)extra DEPRECATED_ATTRIBUTE;
- (nullable HBBannerView*)getBannerViewWithPlacementID:(NSString*)placementID scene:(NSString *)scene;

- (HBCheckLoadModel*)checkBannerLoadStatusForPlacementID:(NSString*)placementID;
@end
