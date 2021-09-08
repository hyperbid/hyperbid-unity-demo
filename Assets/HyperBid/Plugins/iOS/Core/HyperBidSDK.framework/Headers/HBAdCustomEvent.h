//
//  HBAdCustomEvent.h
//  HyperBidSDK
//
//  Created by Leo on 05/07/2018.
//  Copyright © 2018 Leo. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "HBAd.h"
#import "HBTracker.h"
#import "HBBidInfo.h"

extern NSString *const kHBSDKFailedToLoadSplashADMsg;
extern NSString *const kHBSDKFailedToLoadBannerADMsg;
extern NSString *const kHBSDKFailedToLoadInterstitialADMsg;
extern NSString *const kHBSDKFailedToLoadNativeADMsg;
extern NSString *const kHBSDKFailedToLoadRewardedVideoADMsg;
extern NSString *const kHBSDKSplashADTooLongToLoadPlacementSettingMsg;
extern NSString *const kHBSDKImportIssueErrorReason;
extern NSString *const kHBAdAssetsAppIDKey;
@interface HBAdCustomEvent : NSObject
+(NSDictionary*)customInfoWithUnitGroupModel:(HBUnitGroupModel*)unitGroupModel extra:(NSDictionary*)extra;
-(instancetype) initWithUnitID:(NSString*)unitID serverInfo:(NSDictionary*)serverInfo localInfo:(NSDictionary*)localInfo;
-(void) handleAssets:(NSDictionary*)assets;
-(void) handleLoadingFailure:(NSError*)error;
-(void) handleClose;
-(void) trackShow;
-(void) trackClick;
-(HBNativeADSourceType) adSourceType;
@property(nonatomic, weak) id<HBAd> ad;
@property(nonatomic) NSNumber *sdkTime;
@property(nonatomic, copy) void(^requestCompletionBlock)(NSArray<NSDictionary*> *assets, NSError *error);
@property(nonatomic) NSInteger requestNumber;
@property (nonatomic,copy) void (^customEventMetaDataDidLoadedBlock)(void);
/**
 * Failed or successful, a request's considered finished.
 */
@property(nonatomic) NSInteger numberOfFinishedRequests;
@property(nonatomic, readonly) NSMutableArray<NSDictionary*>* assets;
@property(nonatomic, readonly) NSDictionary *serverInfo;
@property(nonatomic, readonly) NSDictionary *localInfo;
@property(nonatomic) BOOL rewardGranted;

-(void) saveShowAPIContext;
@property(nonatomic, readonly) NSDate *showDate;
@property(nonatomic, readonly) NSString *psIDOnShow;

+(NSInteger) calculateAdPriority:(id<HBAd>)ad;

@property (nonatomic, assign) NSString *networkUnitId;
@property (nonatomic) NSDictionary *networkCustomInfo;
@property (nonatomic) NSDictionary *headerBiddingExtra;
-(void)assetsHeaderBiddingTrackingExtraWithBidInfo:(HBBidInfo*)bidInfo;

@end
