//
//  HBBannerCustomEvent.h
//  HyperBidBanner
//
//  Created by Leo on 18/09/2018.
//  Copyright © 2018 Leo. All rights reserved.
//

#import <HyperBidSDK/HyperBidSDK.h>
//#import "HBAdCustomEvent.h"
#import "HBBannerDelegate.h"
//#import "HBAdAdapter.h"
//#import "HBPlacementModel.h"
#import "HBBanner.h"
#import "HBBannerView.h"
@interface HBBannerCustomEvent : HBAdCustomEvent
-(void) trackBannerAdClick;
-(void) trackBannerAdImpression;
-(void) trackBannerAdClosed;
-(void) trackBannerAdLoaded:(id)bannerView adExtra:(NSDictionary *)adExtra;
//-(void) trackBannerAdShow;
-(void) trackBannerAdLoadFailed:(NSError*)error;
-(void) trackBannerAdDeeplinkOrJumpResult:(BOOL)success;

-(NSDictionary*)delegateExtra;
-(instancetype) initWithInfo:(NSDictionary*)serverInfo localInfo:(NSDictionary*)localInfo;
-(void) cleanup;
-(void) removedFromWindow;
/// Some ad SDKs do not call back after ads were displayed. Override it and return 'YES', a impression tracking will be sent. Same for the native ads (HBNativeADCustomEvent).
- (BOOL)sendImpressionTrackingIfNeed;

- (void)invalidateBidInfo;

@property(nonatomic, weak) id<HBBannerDelegate> delegate;
@property(nonatomic, weak) HBBanner *banner;
@property(nonatomic, weak) HBBannerView *bannerView;
@property(nonatomic, readonly) NSString *unitID;
@property(nonatomic, readonly) CGSize size;
@property(nonatomic, strong) NSValue *admobAdSizeValue;//For admob
@property(nonatomic, assign) NSInteger admobAdSizeFlags;//For admob
@property(nonatomic) NSDictionary *loadingParameters;//For nend
@property(nonatomic) BOOL adjustAdSize;//For nend
@property(nonatomic, assign) NSInteger priorityIndex;

+(UIViewController*)rootViewControllerWithPlacementID:(NSString*)placementID requestID:(NSString*)requestID;
@end
