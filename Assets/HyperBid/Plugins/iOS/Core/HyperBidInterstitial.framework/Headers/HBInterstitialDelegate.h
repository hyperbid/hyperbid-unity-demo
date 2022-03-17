//
//  HBInterstitialDelegate.h
//  HyperBidInterstitial
//
//  Created by Leo on 21/09/2018.
//  Copyright © 2018 Leo. All rights reserved.
//

#ifndef HBInterstitialDelegate_h
#define HBInterstitialDelegate_h
#import <HyperBidSDK/HyperBidSDK.h>

extern NSString *const kHBInterstitialDelegateExtraNetworkIDKey;
extern NSString *const kHBInterstitialDelegateExtraAdSourceIDKey;
extern NSString *const kHBInterstitialDelegateExtraIsHeaderBidding DEPRECATED_MSG_ATTRIBUTE("The kHBInterstitialDelegateExtraIsHeaderBidding class will be obsolete, please use kHBInterstitialDelegateExtraAdSourceIsHeaderBidding");
extern NSString *const kHBInterstitialDelegateExtraAdSourceIsHeaderBidding;
extern NSString *const kHBInterstitialDelegateExtraPrice;
extern NSString *const kHBInterstitialDelegateExtraPriority;
@protocol HBInterstitialDelegate<HBAdLoadingDelegate>

-(void) interstitialDidShowForPlacementID:(NSString*)placementID extra:(NSDictionary*)extra;
-(void) interstitialFailedToShowForPlacementID:(NSString*)placementID error:(NSError*)error extra:(NSDictionary*)extra;
-(void) interstitialDidStartPlayingVideoForPlacementID:(NSString*)placementID extra:(NSDictionary*)extra;
-(void) interstitialDidEndPlayingVideoForPlacementID:(NSString*)placementID extra:(NSDictionary*)extra;
-(void) interstitialDidFailToPlayVideoForPlacementID:(NSString*)placementID error:(NSError*)error extra:(NSDictionary*)extra;
-(void) interstitialDidCloseForPlacementID:(NSString*)placementID extra:(NSDictionary*)extra;
-(void) interstitialDidClickForPlacementID:(NSString*)placementID extra:(NSDictionary*)extra;
-(void) interstitialDeepLinkOrJumpForPlacementID:(NSString*)placementID extra:(NSDictionary*)extra result:(BOOL)success;


@end

#endif /* HBInterstitialDelegate_h */
