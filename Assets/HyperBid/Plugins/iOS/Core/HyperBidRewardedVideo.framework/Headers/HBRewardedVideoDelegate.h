//
//  HBRewardedVideoDelegate.h
//  HyperBidSDK
//
//  Created by Leo on 05/07/2018.
//  Copyright © 2018 Leo. All rights reserved.
//

#ifndef HBRewardedVideoDelegate_h
#define HBRewardedVideoDelegate_h
#import <HyperBidSDK/HyperBidSDK.h>

extern NSString *const kHBRewardedVideoCallbackExtraAdsourceIDKey;
extern NSString *const kHBRewardedVideoCallbackExtraNetworkIDKey;
extern NSString *const kHBRewardedVideoCallbackExtraIsHeaderBidding;
extern NSString *const kHBRewardedVideoCallbackExtraPrice;
extern NSString *const kHBRewardedVideoCallbackExtraPriority;
@protocol HBRewardedVideoDelegate<HBAdLoadingDelegate>

-(void) rewardedVideoDidStartPlayingForPlacementID:(NSString*)placementID extra:(NSDictionary*)extra;
-(void) rewardedVideoDidEndPlayingForPlacementID:(NSString*)placementID extra:(NSDictionary*)extra;
-(void) rewardedVideoDidFailToPlayForPlacementID:(NSString*)placementID error:(NSError*)error extra:(NSDictionary*)extra;
-(void) rewardedVideoDidCloseForPlacementID:(NSString*)placementID rewarded:(BOOL)rewarded extra:(NSDictionary*)extra;
-(void) rewardedVideoDidClickForPlacementID:(NSString*)placementID extra:(NSDictionary*)extra;
-(void) rewardedVideoDidRewardSuccessForPlacemenID:(NSString*)placementID extra:(NSDictionary*)extra;
-(void) rewardedVideoDidDeepLinkOrJumpForPlacementID:(NSString*)placementID extra:(NSDictionary*)extra result:(BOOL)success;

@end
#endif /* HBRewardedVideoDelegate_h */
