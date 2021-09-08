//
//  HBRewardedVideoCustomEvent.h
//  HyperBidSDK
//
//  Created by Leo on 05/07/2018.
//  Copyright © 2018 Leo. All rights reserved.
//

#import <HyperBidSDK/HyperBidSDK.h>
//#import "HBAdCustomEvent.h"
#import "HBRewardedVideoDelegate.h"
#import "HBRewardedVideo.h"

@interface HBRewardedVideoCustomEvent : HBAdCustomEvent
-(void) trackRewardedVideoAdPlayEventWithError:(NSError*)error;
-(void) trackRewardedVideoAdCloseRewarded:(BOOL)rewarded;
-(void) trackRewardedVideoAdVideoStart;
-(void) trackRewardedVideoAdVideoEnd;
-(void) trackRewardedVideoAdClick;
-(void) trackRewardedVideoAdShow;
-(void) trackRewardedVideoAdLoadFailed:(NSError*)error;
-(void) trackRewardedVideoAdLoaded:(id)adObject adExtra:(NSDictionary *)adExtra;
-(void) trackRewardedVideoAdRewarded;
-(void) trackRewardedVideoAdDeeplinkOrJumpResult:(BOOL)success;

-(NSDictionary*)delegateExtra;

-(instancetype) initWithInfo:(NSDictionary*)serverInfo localInfo:(NSDictionary *)localInfo;
@property(nonatomic, weak) id<HBRewardedVideoDelegate> delegate;
@property(nonatomic, weak) HBRewardedVideo *rewardedVideo;
@property(nonatomic, readonly) NSString *unitID;
@property(nonatomic) NSString *userID;
@property(nonatomic, assign) NSInteger priorityIndex;
@end
