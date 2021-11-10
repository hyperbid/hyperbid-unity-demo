//
//  HBAd.h
//  HyperBidSDK
//
//  Created by Leo on 28/06/2018.
//  Copyright © 2018 Leo. All rights reserved.
//

#ifndef HBAd_h
#define HBAd_h
#import "HBPlacementModel.h"
#import "HBUnitGroupModel.h"
@class HBWaterfall;
@protocol HBAd<NSObject>
-(instancetype) initWithPriority:(NSInteger) priority placementModel:(HBPlacementModel*)placementModel requestID:(NSString*)requestID assets:(NSDictionary*)assets unitGroup:(HBUnitGroupModel*)unitGroup finalWaterfall:(HBWaterfall*)finalWaterfall;
-(void) renewAdWithPriority:(NSInteger)priority placementModel:(HBPlacementModel*)placementModel unitGroup:(HBUnitGroupModel*)unitGroup requestID:(NSString*)requestID;
@property(nonatomic, readonly) HBPlacementModel *placementModel;
@property(nonatomic, readonly) HBUnitGroupModel *unitGroup;
/**
 Priority is calculate by the index of the unit group in the placement's unit group list; zero is the highest
 */
@property(nonatomic, readonly) NSInteger priority;
@property(nonatomic, readonly) NSInteger priorityLevel;//the order in witch this ad has been requested
@property(nonatomic, readonly) NSString *requestID;

@property(nonatomic, readonly) NSString *originalRequestID;
@property(nonatomic) NSInteger showTimes;
@property(nonatomic, readonly) NSDate *expireDate;
@property(nonatomic, readonly) NSDate *cacheDate;
/**
 * Third-party network native ad object.
 */
@property(nonatomic, readonly) id customObject;
@property(nonatomic, readonly) NSString *unitID;
@property(nonatomic, readonly) NSString *appID;
- (NSString *)ecpm;

@optional
-(BOOL) expired;
-(BOOL) ready;
@property(nonatomic, readonly) BOOL filledByReady;
@property(nonatomic, readonly) BOOL filledByAutoloadOnClose;
@property(nonatomic, readonly) BOOL fillByAutorefresh;
@property(nonatomic) BOOL defaultPlayIfRequired;
@property(nonatomic, readonly) BOOL renewed;
@property(nonatomic, readonly) NSString *price;
@property(nonatomic, readonly) NSString *bidId;
@property(nonatomic, readonly) NSString *tpBidId;
@property(nonatomic, readonly, weak) HBWaterfall *finalWaterfall;
@property(nonatomic, readonly) NSInteger autoReqType;
@property(nonatomic) BOOL adReportClicked;
@end

#endif /* HBAd_h */
