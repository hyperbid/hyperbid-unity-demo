//
//  HBNativeADCustomEvent.h
//  HyperBidSDK
//
//  Created by Leo on 25/04/2018.
//  Copyright © 2018 Leo. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <HyperBidSDK/HyperBidSDK.h>

#import "HBNativeADView.h"
//#import "HBTracker.h"
#import "HBNativeADDelegate.h"
//#import "HBAdCustomEvent.h"

@class HBNativeADCache;
@interface HBNativeADCustomEvent : HBAdCustomEvent
-(void) trackNativeAdLoaded:(NSDictionary*)assets;
-(void) trackNativeAdLoadFailed:(NSError*)error;
-(void) didAttachMediaView;
-(void) willDetachOffer:(HBNativeADCache*)offer fromAdView:(HBNativeADView*)adView;
/**
 *@para refresh: whether the show is trigered by a ad refresh.
 */
- (void)trackNativeAdShow:(BOOL)refresh;
- (void)trackNativeAdClick;
- (void)trackNativeAdVideoStart;
- (void)trackNativeAdVideoEnd;
- (void)trackNativeAdClosed;
- (void)trackNativeAdImpression;
-(void) trackNativeAdDeeplinkOrJumpResult:(BOOL)success;

/// If it returns YES, then when sending the embedding points of "show", the embedding points of "impression" will be sent together. Otherwise, it will not be sent. Same for the banner ads (HBBannerCustomEvent.h).
- (BOOL)sendImpressionTrackingIfNeed;

-(NSDictionary*)delegateExtra;
- (NSDictionary *)delegateExtraWithNativeAD:(HBNativeADCache *)cache;

-(HBNativeADSourceType) sourceType;
@property(nonatomic, copy) void(^requestCompletionBlock)(NSArray<NSDictionary*> *assets, NSError *error);
@property(nonatomic, weak) HBNativeADView *adView;
@property(nonatomic) NSInteger requestNumber;
/**
 * Failed or successful, a request's considered finished.
 */
@property(nonatomic) NSInteger numberOfFinishedRequests;
@property(nonatomic, readonly) NSMutableArray<NSDictionary*>* assets;
@property(nonatomic) NSDictionary *requestExtra;
@property(nonatomic) NSDictionary *serverExtra;
@end

@interface HBNativeADView(Event)
-(void) notifyNativeAdClick;
-(void) notifyVideoStart;
-(void) notifyVideoEnd;
-(void) notifyVideoEnterFullScreen;
-(void) notifyVideoExitFullScreen;
-(void) notifyCloseButtonTapped;
-(void) notifyDeeplinkOrJumpResult:(BOOL)success;

@end
