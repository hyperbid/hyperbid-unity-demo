 //
//  HBNativeSplashWrapper.h
//  HyperBidSDKDemo
//
//  Created by Leo on 2019/3/19.
//  Copyright © 2019 Leo. All rights reserved.
//

#import <UIKit/UIKit.h>
extern NSString *const kHBNativeSplashShowingExtraRecommendTitleKey;
extern NSString *const kHBNativeSplashShowingExtraCTAButtonBackgroundColorKey;
extern NSString *const kHBNativeSplashShowingExtraCTAButtonTitleColorKey;
extern NSString *const kHBNativeSplashShowingExtraContainerViewKey;
extern NSString *const kHBNativeSplashShowingExtraCountdownIntervalKey;
extern NSString *const kHBNatievSplashShowingExtraStyleKey;

extern NSString *const kHBNativeSplashShowingExtraStylePortrait;
extern NSString *const kHBNativeSplashShowingExtraStyleLandscape;

@protocol HBNativeSplashDelegate<NSObject>
-(void) finishLoadingNativeSplashAdForPlacementID:(NSString*)placementID;
-(void) failedToLoadNativeSplashAdForPlacementID:(NSString*)placementID error:(NSError*)error;
-(void) didShowNativeSplashAdForPlacementID:(NSString*)placementID DEPRECATED_ATTRIBUTE;
-(void) didClickNaitveSplashAdForPlacementID:(NSString*)placementID DEPRECATED_ATTRIBUTE;
-(void) didCloseNativeSplashAdForPlacementID:(NSString*)placementID DEPRECATED_ATTRIBUTE;

-(void) didShowNativeSplashAdForPlacementID:(NSString*)placementID extra:(NSDictionary *)extra;
-(void) didClickNaitveSplashAdForPlacementID:(NSString*)placementID extra:(NSDictionary *)extra;
-(void) didCloseNativeSplashAdForPlacementID:(NSString*)placementID extra:(NSDictionary *)extra;
- (void)didNativeSplashDeeplinkOrJumpForPlacementID:(NSString*)placementID extra:(NSDictionary *)extra result:(BOOL)success;

@end

@interface HBNativeSplashWrapper : NSObject
+(instancetype) sharedWrapper;
+(void) loadNativeSplashAdWithPlacementID:(NSString*)placementID extra:(NSDictionary*)extra customData:(NSDictionary*)customData delegate:(id<HBNativeSplashDelegate>)delegate;
+(void) showNativeSplashAdWithPlacementID:(NSString*)placementID extra:(NSDictionary*)extra delegate:(id<HBNativeSplashDelegate>)delegate;
+(BOOL) splashNativeAdReadyForPlacementID:(NSString*)placementID;
@end
