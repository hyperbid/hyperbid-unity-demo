//
//  HBBannerDelegate.h
//  HyperBidSDK
//
//  Created by Leo on 18/09/2018.
//  Copyright © 2018 Leo. All rights reserved.
//

#ifndef HBBannerDelegate_h
#define HBBannerDelegate_h
#import <HyperBidSDK/HyperBidSDK.h>
@class HBBannerView;

extern NSString *const kHBBannerDelegateExtraNetworkIDKey;
extern NSString *const kHBBannerDelegateExtraAdSourceIDKey;
extern NSString *const kHBBannerDelegateExtraIsHeaderBidding DEPRECATED_MSG_ATTRIBUTE("The kHBBannerDelegateExtraIsHeaderBidding class will be obsolete, please use kHBBannerDelegateExtraAdSourceIsHeaderBidding");
extern NSString *const kHBBannerDelegateExtraAdSourceIsHeaderBidding;
extern NSString *const kHBBannerDelegateExtraPrice;
extern NSString *const kHBBannerDelegateExtraPriority;

@protocol HBBannerDelegate<HBAdLoadingDelegate>
-(void) bannerView:(HBBannerView*)bannerView failedToAutoRefreshWithPlacementID:(NSString*)placementID error:(NSError*)error;
-(void) bannerView:(HBBannerView*)bannerView didShowAdWithPlacementID:(NSString*)placementID extra:(NSDictionary *)extra;
-(void) bannerView:(HBBannerView*)bannerView didClickWithPlacementID:(NSString*)placementID extra:(NSDictionary *)extra;
-(void) bannerView:(HBBannerView*)bannerView didCloseWithPlacementID:(NSString*)placementID extra:(NSDictionary *)extra DEPRECATED_ATTRIBUTE;
-(void) bannerView:(HBBannerView*)bannerView didAutoRefreshWithPlacement:(NSString*)placementID extra:(NSDictionary *)extra;
-(void) bannerView:(HBBannerView*)bannerView didTapCloseButtonWithPlacementID:(NSString*)placementID extra:(NSDictionary*)extra;
-(void)  bannerView:(HBBannerView*)bannerView didDeepLinkOrJumpForPlacementID:(NSString*)placementID extra:(NSDictionary*)extra result:(BOOL)success;

@end
#endif /* HBBannerDelegate_h */
