//
//  HBNativeBannerWrapper.h
//  HyperBidSDKDemo
//
//  Created by Leo on 2019/4/10.
//  Copyright © 2019 Leo. All rights reserved.
//

#import <UIKit/UIKit.h>
#import <HyperBidNative/HyperBidNative.h>
@class HBNativeBannerView;
@protocol HBNativeBannerDelegate<NSObject>
-(void) didFinishLoadingNativeBannerAdWithPlacementID:(NSString *)placementID;
-(void) didFailToLoadNativeBannerAdWithPlacementID:(NSString*)placementID error:(NSError*)error;
-(void) didShowNativeBannerAdInView:(HBNativeBannerView*)bannerView placementID:(NSString*)placementID DEPRECATED_ATTRIBUTE;
-(void) didClickNativeBannerAdInView:(HBNativeBannerView*)bannerView placementID:(NSString*)placementID DEPRECATED_ATTRIBUTE;
-(void) didClickCloseButtonInNativeBannerAdView:(HBNativeBannerView*)bannerView placementID:(NSString*)placementID DEPRECATED_ATTRIBUTE;
-(void) didAutorefreshNativeBannerAdInView:(HBNativeBannerView*)bannerView placementID:(NSString*)placementID DEPRECATED_ATTRIBUTE;
-(void) didFailToAutorefreshNativeBannerAdInView:(HBNativeBannerView*)bannerView placementID:(NSString*)placementID error:(NSError*)error DEPRECATED_ATTRIBUTE;

-(void) didShowNativeBannerAdInView:(HBNativeBannerView*)bannerView placementID:(NSString*)placementID extra:(NSDictionary *)extra;
-(void) didClickNativeBannerAdInView:(HBNativeBannerView*)bannerView placementID:(NSString*)placementID extra:(NSDictionary *)extra;
-(void) didClickCloseButtonInNativeBannerAdView:(HBNativeBannerView*)bannerView placementID:(NSString*)placementID extra:(NSDictionary *)extra;
-(void) didAutorefreshNativeBannerAdInView:(HBNativeBannerView*)bannerView placementID:(NSString*)placementID extra:(NSDictionary *)extra;
-(void) didFailToAutorefreshNativeBannerAdInView:(HBNativeBannerView*)bannerView placementID:(NSString*)placementID extra:(NSDictionary *)extra error:(NSError*)error;
- (void)didNativeBannerDeeplinkOrJumpInView:(HBNativeBannerView*)bannerView placementID:(NSString*)placementID extra:(NSDictionary *)extra result:(BOOL)success;

@end

@interface HBNativeBannerView:UIView
@property(nonatomic, weak) id<HBNativeBannerDelegate> delegate;
@end

extern NSString *const kHBNativeBannerAdShowingExtraBackgroundColorKey;
extern NSString *const kHBNativeBannerAdShowingExtraAdSizeKey;
extern NSString *const kHBNativeBannerAdShowingExtraAutorefreshIntervalKey;
extern NSString *const kHBNativeBannerAdShowingExtraHideCloseButtonFlagKey;
extern NSString *const kHBNativeBannerAdShowingExtraCTAButtonBackgroundColorKey;
extern NSString *const kHBNativeBannerAdShowingExtraCTAButtonTitleFontKey;
extern NSString *const kHBNativeBannerAdShowingExtraCTAButtonTitleColorKey;
extern NSString *const kHBNativeBannerAdShowingExtraTitleFontKey;
extern NSString *const kHBNativeBannerAdShowingExtraTitleColorKey;
extern NSString *const kHBNativeBannerAdShowingExtraTextFontKey;
extern NSString *const kHBNativeBannerAdShowingExtraTextColorKey;
extern NSString *const kHBNativeBannerAdShowingExtraAdvertiserTextFontKey;
extern NSString *const kHBNativeBannerAdShowingExtraAdvertiserTextColorKey;
@interface HBNativeBannerWrapper:NSObject
+(instancetype) sharedWrapper;
+(void) loadNativeBannerAdWithPlacementID:(NSString*)placementID extra:(NSDictionary*)extra customData:(NSDictionary*)customData delegate:(id<HBNativeBannerDelegate>)delegate;
+(HBNativeBannerView*) retrieveNativeBannerAdViewWithPlacementID:(NSString*)placementID extra:(NSDictionary*)extra delegate:(id<HBNativeBannerDelegate>)delegate;
+(BOOL) nativeBannerAdReadyForPlacementID:(NSString*)placementID;
@end
