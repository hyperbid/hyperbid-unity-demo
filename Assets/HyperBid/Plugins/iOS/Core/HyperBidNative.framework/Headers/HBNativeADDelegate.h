//
//  HBNativeADDelegate.h
//  HyperBidSDK
//
//  Created by Leo on 08/05/2018.
//  Copyright © 2018 Leo. All rights reserved.
//

#ifndef HBNativeADDelegate_h
#define HBNativeADDelegate_h
#import <HyperBidSDK/HyperBidSDK.h>
@class HBNativeADView;
extern NSString *const kHBNativeDelegateExtraNetworkIDKey;
extern NSString *const kHBNativeDelegateExtraAdSourceIDKey;
extern NSString *const kHBNativeDelegateExtraIsHeaderBidding;
extern NSString *const kHBNativeDelegateExtraPrice;
extern NSString *const kHBNativeDelegateExtraPriority;
@protocol HBNativeADDelegate<HBAdLoadingDelegate>
-(void) didShowNativeAdInAdView:(HBNativeADView*)adView placementID:(NSString*)placementID extra:(NSDictionary *)extra;
-(void) didClickNativeAdInAdView:(HBNativeADView*)adView placementID:(NSString*)placementID extra:(NSDictionary *)extra;
-(void) didStartPlayingVideoInAdView:(HBNativeADView*)adView placementID:(NSString*)placementID extra:(NSDictionary *)extra;
-(void) didEndPlayingVideoInAdView:(HBNativeADView*)adView placementID:(NSString*)placementID extra:(NSDictionary *)extra;
-(void) didEnterFullScreenVideoInAdView:(HBNativeADView*)adView placementID:(NSString*)placementID extra:(NSDictionary *)extra;
-(void) didExitFullScreenVideoInAdView:(HBNativeADView*)adView placementID:(NSString*)placementID extra:(NSDictionary *)extra;
-(void) didTapCloseButtonInAdView:(HBNativeADView*)adView placementID:(NSString*)placementID extra:(NSDictionary *)extra;
-(void) didLoadSuccessDrawWith:(NSArray*)views placementID:(NSString*)placementID extra:(NSDictionary *)extra;
-(void) didDeepLinkOrJumpInAdView:(HBNativeADView*)adView placementID:(NSString*)placementID extra:(NSDictionary*)extra result:(BOOL)success;

// v5.7.47
-(void) didCloseDetailInAdView:(HBNativeADView*)adView placementID:(NSString*)placementID extra:(NSDictionary *)extra;
@end
#endif /* HBNativeADDelegate_h */
