//
//  HBSplashDelegate.h
//  HyperBidSplash
//
//  Created by Leo on 2018/12/20.
//  Copyright © 2018 Leo. All rights reserved.
//

#ifndef HBSplashDelegate_h
#define HBSplashDelegate_h
#import <HyperBidSDK/HyperBidSDK.h>

extern NSString *const kHBSplashDelegateExtraNetworkIDKey;
extern NSString *const kHBSplashDelegateExtraAdSourceIDKey;
extern NSString *const kHBSplashDelegateExtraIsHeaderBidding DEPRECATED_MSG_ATTRIBUTE("The kHBSplashDelegateExtraIsHeaderBidding class will be obsolete, please use kHBSplashDelegateExtraAdSourceIsHeaderBidding");
extern NSString *const kHBSplashDelegateExtraAdSourceIsHeaderBidding;
extern NSString *const kHBSplashDelegateExtraPrice;
extern NSString *const kHBSplashDelegateExtraPriority;

@protocol HBSplashDelegate<HBAdLoadingDelegate>

-(void)splashDidShowForPlacementID:(NSString*)placementID extra:(NSDictionary *) extra;
-(void)splashDidClickForPlacementID:(NSString*)placementID extra:(NSDictionary *) extra;
-(void)splashDidCloseForPlacementID:(NSString*)placementID extra:(NSDictionary *) extra;
-(void)splashDeepLinkOrJumpForPlacementID:(NSString*)placementID extra:(NSDictionary*)extra result:(BOOL)success;

-(void)splashZoomOutViewDidClickForPlacementID:(NSString*)placementID extra:(NSDictionary *) extra;
-(void)splashZoomOutViewDidCloseForPlacementID:(NSString*)placementID extra:(NSDictionary *) extra;

// 5.7.53+
- (void)splashDetailDidClosedForPlacementID:(NSString*)placementID extra:(NSDictionary *) extra;
- (void)splashDidShowFailedForPlacementID:(NSString*)placementID error:(NSError *)error extra:(NSDictionary *)extra;

// 5.7.61+ This callback is triggered when the skip button is customized.
- (void)splashCountdownTime:(NSInteger)countdown forPlacementID:(NSString*)placementID extra:(NSDictionary *) extra;;

@end
#endif /* HBSplashDelegate_h */
