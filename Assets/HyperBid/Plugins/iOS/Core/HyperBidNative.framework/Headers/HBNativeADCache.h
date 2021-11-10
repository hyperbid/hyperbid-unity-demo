//
//  HBNativeADCache.h
//  HyperBidSDK
//
//  Created by Leo on 17/04/2018.
//  Copyright © 2018 Leo. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <HyperBidSDK/HyperBidSDK.h>
#import "HBNativeAd.h"
//#import "HBAd.h"
#import "HBNativeADCustomEvent.h"
@class HBUnitGroupModel;
@class HBPlacementModel;
@interface HBNativeADCache : HBNativeAd<HBAd>
-(instancetype) initWithPriority:(NSInteger) priority placementModel:(HBPlacementModel*)placementModel requestID:(NSString*)requestID assets:(NSDictionary*)assets unitGroup:(HBUnitGroupModel*)unitGroup finalWaterfall:(HBWaterfall *)finalWaterfall;
@property(nonatomic) NSInteger showTimes;
/**
 Priority is calculate by the index of the unit group in the placement's unit group list; zero is the highest
 */
@property(nonatomic, readonly) NSInteger priority;
@property(nonatomic, readonly) NSInteger priorityLevel;
//@property(nonatomic, readonly) NSString *placementID;
@property(nonatomic, readonly) HBPlacementModel *placementModel;
@property(nonatomic, readonly) NSString *requestID;
@property(nonatomic, readonly) NSString *originalRequestID;
@property(nonatomic, readonly) NSDate *expireDate;
@property(nonatomic, readonly) NSDate *cacheDate;
@property(nonatomic, readonly) NSDictionary *assets;//To be removed
@property(nonatomic, readonly) HBUnitGroupModel *unitGroup;
@property(nonatomic) NSNumber *sdkTime;
@property(nonatomic, readonly) HBNativeADCustomEvent *customEvent;
/**
 * Third-party network native ad object.
 */
@property(nonatomic, readonly) id customObject;
/**
 * Third-party network unit id.
 */
@property(nonatomic, assign)NSInteger priorityIndex;

@property(nonatomic, readonly) NSString *unitID;
@property(nonatomic, readonly) NSString *iconURLString;
@property(nonatomic, readonly) NSString *imageURLString;
@property(nonatomic, readonly) NSString *appID;
@property(nonatomic) BOOL defaultPlayIfRequired;
@property(nonatomic, readonly) NSString *price;
@property(nonatomic, readonly) NSString *bidId;
@property(nonatomic, readonly) NSString *tpBidId;
@property(nonatomic, readonly, weak) HBWaterfall *finalWaterfall;
@property(nonatomic, readonly) NSInteger autoReqType;
@property(nonatomic) BOOL adReportClicked;
@property(nonatomic, copy) NSString *scene;

@end
