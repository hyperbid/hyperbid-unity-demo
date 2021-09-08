//
//  HBTracker.h
//  HyperBidSDK
//
//  Created by Leo on 19/04/2018.
//  Copyright © 2018 Leo. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "HBAd.h"
extern NSString *const kHBTrackerExtraErrorKey;
extern NSString *const kHBTrackerExtraAutoloadFlagKey;
extern NSString *const kHBTrackerExtraSDKCalledFlagKey;
extern NSString *const kHBTrackerExtraSDKNotCalledReasonKey;
extern NSString *const kHBTrackerExtraLoadFailureReasonKey;
extern NSString *const kHBTrackerExtraASIDKey;
extern NSString *const kHBTrackerExtraStatusKey;
extern NSString *const kHBTrackerExtraShownNetworkPriorityInfoKey;
extern NSString *const kHBTrackerExtraHeaderBiddingInfoKey;
extern NSString *const kHBTrackerExtraResourceTypeKey;
extern NSString *const kHBTrackerExtraUnitIDKey;//Ad source id
extern NSString *const kHBTrackerExtraNetworkFirmIDKey;
extern NSString *const kHBTrackerExtraRefreshFlagKey;//for banner&native banner refresh
extern NSString *const kHBTrackerExtraDefaultLoadFlagKey;
extern NSString *const kHBTrackerExtraFilledWithinNetworkTimeoutFlagKey;
extern NSString *const kHBTrackerExtraFillRequestFlagKey;
extern NSString *const kHBTrackerExtraFillTimeKey;
extern NSString *const kHBTrackerExtraASResultKey;
extern NSString *const kHBTrackerExtraAppIDKey;
extern NSString *const kHBTrackerExtraLastestRequestIDKey;
extern NSString *const kHBTrackerExtraLastestRequestIDMatchFlagKey;
extern NSString *const kHBTrackerExtraAdFilledByReadyFlagKey;
extern NSString *const kHBTrackerExtraAutoloadOnCloseFlagKey;
extern NSString *const kHBTrackerExtraLoadTimeKey;
extern NSString *const kHBTrackerExtraClickAddressKey;
extern NSString *const kHBTrackerExtraMyOfferDefaultFalgKey;
extern NSString *const kHBTrackerExtraOfferLoadedByAdSourceStatusFlagKey;
extern NSString *const kHBTrackerExtraCustomObjectKey;
extern NSString *const kHBTrackerExtraAdObjectKey;
extern NSString *const kHBTrackerExtraAdShowSceneKey;
extern NSString *const kHBTrackerExtraAdShowSDKTimeKey;
extern NSString *const kHBTrackerExtraTrafficGroupIDKey;
extern NSString *const kHBTrackerExtraUGUnitIDKey;
extern NSString *const kHBTrackerExtraASIDKey;
extern NSString *const kHBTrackerExtraFormatKey;
extern NSString *const kHBTrackerExtraRequestExpectedOfferNumberFlagKey;

// ofm
extern NSString *const kHBTrackerExtraOFMTrafficIDKey;
extern NSString *const kHBTrackerExtraOFMSystemKey;
extern NSString *const kHBTrackerExtraOFMPreECPMKey;
extern NSString *const kHBTrackerExtraOFMKey;

typedef NS_ENUM(NSInteger, HBNativeADTrackType) {
    HBNativeADTrackTypeADRequest = 1,
    //Send when the ad's been successfully downloaded.
    HBNativeADTrackTypeADRecalledSuccessfully = 2,
    //Send when the ad's failed to be downloaded.
    HBNativeADTrackTypeADRecallFailed = 3,
    HBNativeADTrackTypeADShow = 4, // impression
    HBNativeADTrackTypeADRefreshShow = 5,
    HBNativeADTrackTypeADClicked = 6,
    HBNativeADTrackTypeVideoPlayed = 7,
    HBNativeAdTrackTypeVideoStart = 8,
    HBNativeAdTrackTypeVideoEnd = 9,
    HBNativeAdTrackTypeLoad = 10,
    HBNativeAdTrackTypeBidSort = 11,
    HBNativeAdTrackTypeLoadResult = 12,//currently sent when loading succeeds
    HBNativeAdTrackTypeShowAPICall = 13,
    HBNativeADTrackTypeRankAndShuffle = 15
};

typedef NS_ENUM(NSInteger, HBNativeADSourceType) {
    HBNativeADSourceTypeUnknown = 0,
    HBNativeADSourceTypeVideo = 1,
    HBNativeADSourceTypeImage = 2
};
@interface HBTracker : NSObject
+(instancetype)sharedTracker;
-(void) trackWithPlacementID:(NSString*)placementID requestID:(NSString*)requestID trackType:(HBNativeADTrackType)trackType extra:(nullable NSDictionary*)extra;
-(void) trackClickWithAd:(nonnull id<HBAd>)ad extra:(nullable NSDictionary*)extra;
/*
 * For header bidding
 */
+(nullable NSDictionary*)headerBiddingTrackingExtraWithAd:(id<HBAd>)ad requestID:(NSString*)requestID;

+(nullable NSDictionary*)dataElementWithPlacementID:(NSString*)placementID requestID:(NSString*)requestID trackType:(HBNativeADTrackType)trackType extra:(NSDictionary*)extra;
+(nullable NSDictionary*)commonParameters;
@end
