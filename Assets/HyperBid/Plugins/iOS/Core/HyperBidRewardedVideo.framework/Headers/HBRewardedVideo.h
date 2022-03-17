//
//  HBRewardedVideo.h
//  HyperBidSDK
//
//  Created by Leo on 28/06/2018.
//  Copyright © 2018 Leo. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <HyperBidSDK/HyperBidSDK.h>
//#import "HBAd.h"
@class HBRewardedVideoCustomEvent;
@interface HBRewardedVideo : NSObject<HBAd>
-(instancetype) initWithPriority:(NSInteger) priority placementModel:(HBPlacementModel*)placementModel requestID:(NSString*)requestID assets:(NSDictionary*)assets unitGroup:(HBUnitGroupModel*)unitGroup finalWaterfall:(HBWaterfall *)finalWaterfall;
@property(nonatomic) NSInteger showTimes;
/**
 Priority is calculate by the index of the unit group in the placement's unit group list; zero is the highest
 */
@property(nonatomic, readonly) NSInteger priority;
@property(nonatomic, readonly) NSInteger priorityLevel;
@property(nonatomic, readonly) HBPlacementModel *placementModel;
@property(nonatomic, readonly) NSString *requestID;
@property(nonatomic, readonly) NSString *originalRequestID;
@property(nonatomic, readonly) NSDate *cacheDate;
@property(nonatomic, readonly) NSDate *expireDate;
@property(nonatomic, readonly) HBUnitGroupModel *unitGroup;
@property(nonatomic) NSMutableArray<NSDictionary*> *unitGroupInfos;
@property(nonatomic, readonly) NSString *unitID;
/**
 * Third-party network native ad object.
 */
@property(nonatomic, readonly) id customObject;

@property(nonatomic) HBRewardedVideoCustomEvent *customEvent;
@property(nonatomic, readonly) NSString *appID;
@property(nonatomic) BOOL defaultPlayIfRequired;
@property(nonatomic, copy) NSString *scene;
@property(nonatomic, readonly) NSString *price;
@property(nonatomic, readonly) NSString *bidId;
@property(nonatomic, readonly) NSString *tpBidId;
@property(nonatomic, readonly, weak) HBWaterfall *finalWaterfall;
@property(nonatomic, readonly) NSInteger autoReqType;
@property(nonatomic) BOOL adReportClicked;
@end
