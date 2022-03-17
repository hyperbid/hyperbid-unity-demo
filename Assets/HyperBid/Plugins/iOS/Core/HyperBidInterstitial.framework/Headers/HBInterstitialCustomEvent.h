//
//  HBInterstitialCustomEvent.h
//  HyperBidInterstitial
//
//  Created by Leo on 21/09/2018.
//  Copyright © 2018 Leo. All rights reserved.
//

#import "HBInterstitial.h"
#import "HBInterstitialDelegate.h"
#import <HyperBidSDK/HyperBidSDK.h>
@interface HBInterstitialCustomEvent : HBAdCustomEvent
-(void) trackInterstitialAdLoaded:(id)interstitialAd adExtra:(NSDictionary *)adExtra;
-(void) trackInterstitialAdLoadFailed:(NSError*)error;
-(void) trackInterstitialAdShow;
-(void) trackInterstitialAdShowFailed:(NSError*)error;
-(void) trackInterstitialAdVideoStart;
-(void) trackInterstitialAdVideoEnd;
-(void) trackInterstitialAdDidFailToPlayVideo:(NSError*)error;
-(void) trackInterstitialAdClick;
-(void) trackInterstitialAdClose;
-(void) trackInterstitialAdDeeplinkOrJumpResult:(BOOL)success;

-(void) trackdInterstitialAdVideoRewarded;

-(NSDictionary*)delegateExtra;
-(HBNativeADSourceType) adSourceType;
-(instancetype) initWithInfo:(NSDictionary*)serverInfo localInfo:(NSDictionary*)localInfo ;
@property(nonatomic, weak) id<HBInterstitialDelegate> delegate;
@property(nonatomic, strong) HBInterstitial *interstitial;
@property(nonatomic, readonly) NSString *unitID;
@property(nonatomic, assign) NSInteger priorityIndex;
@end
