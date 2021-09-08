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
extern NSString *const kHBSplashDelegateExtraIsHeaderBidding;
extern NSString *const kHBSplashDelegateExtraPrice;
extern NSString *const kHBSplashDelegateExtraPriority;

@protocol HBSplashDelegate<HBAdLoadingDelegate>

-(void)splashDidShowForPlacementID:(NSString*)placementID extra:(NSDictionary *) extra;
-(void)splashDidClickForPlacementID:(NSString*)placementID extra:(NSDictionary *) extra;
-(void)splashDidCloseForPlacementID:(NSString*)placementID extra:(NSDictionary *) extra;
-(void)splashDeepLinkOrJumpForPlacementID:(NSString*)placementID extra:(NSDictionary*)extra result:(BOOL)success;

-(void)splashZoomOutViewDidClickForPlacementID:(NSString*)placementID extra:(NSDictionary *) extra;
-(void)splashZoomOutViewDidCloseForPlacementID:(NSString*)placementID extra:(NSDictionary *) extra;
@end
#endif /* HBSplashDelegate_h */
