//
//  HBWaterfallManager.h
//  HyperBidSDK
//
//  Created by Leo on 2020/4/28.
//  Copyright © 2020 HyperBid. All rights reserved.
//

#import <Foundation/Foundation.h>
@class HBUnitGroupModel;
@class HBPlacementModel;
typedef NS_ENUM(NSInteger, HBWaterfallType) {
    HBWaterfallTypeWaterfall = 0,
    HBWaterfallTypeHeaderBidding = 1,
    HBWaterfallTypeFinal = 2
};

typedef NS_ENUM(NSInteger, HBUnitGroupFinishType) {
    HBUnitGroupFinishTypeFinished = 0,
    HBUnitGroupFinishTypeTimeout = 1,
    HBUnitGroupFinishTypeFailed = 2
};

@interface HBWaterfallWrapper:NSObject
-(void) finish;
-(void) fill;
-(HBUnitGroupModel*) filledUnitGroupWithMaximumPrice;
@property(nonatomic) NSInteger numberOfCachedOffers;
@property(nonatomic, readonly, getter=isFilled) BOOL filled;
@property(nonatomic) BOOL headerBiddingFired;
@property(nonatomic) BOOL headerBiddingFailed;
@end

@interface HBWaterfall:NSObject
-(instancetype) initWithUnitGroups:(NSArray<HBUnitGroupModel*>*)unitGroups placementID:(NSString*)placementID requestID:(NSString*)requestID;
-(void) requestUnitGroup:(HBUnitGroupModel*)unitGroup;
-(void) finishUnitGroup:(HBUnitGroupModel*)unitGroup withType:(HBUnitGroupFinishType)type;
-(void) addUnitGroup:(HBUnitGroupModel*)unitGroup;
-(void) insertUnitGroup:(HBUnitGroupModel*)unitGroup price:(NSString *)price;
-(HBUnitGroupModel*) firstPendingNonHBUnitGroupWithNetworkFirmID:(NSInteger)nwFirmID;
-(HBUnitGroupModel*) unitGroupWithUnitID:(NSString*)unitID;
-(HBUnitGroupModel*) unitGroupWithMaximumPrice;
-(HBUnitGroupModel*) unitGroupWithMinimumPrice;
-(BOOL)canContinueLoading:(BOOL)waitForSentRequests;
-(void) enumerateTimeoutUnitGroupWithBlock:(void(^)(HBUnitGroupModel*unitGroup))block;
@property(nonatomic, strong) NSMutableArray<HBUnitGroupModel*>* unitGroups;
@property(nonatomic, readonly) NSUInteger numberOfTimeoutRequests;
@property(nonatomic, readonly) HBWaterfallType type;
@property(nonatomic, readonly, getter=isLoading) BOOL loading;
@end

@interface HBWaterfallManager : NSObject
+(instancetype) sharedManager;
-(BOOL) loadingAdForPlacementID:(NSString*)placementID;
-(void) attachWaterfall:(HBWaterfall*)waterfall completion:(void(^)(HBWaterfallWrapper *waterfallWrapper, HBWaterfall *waterfall, HBWaterfall *headerBiddingWaterfall, HBWaterfall *finalWaterfall, BOOL finished, NSDate *loadStartDate))completion;
-(void) accessWaterfallForPlacementID:(NSString*)placementID requestID:(NSString*)requestID withBlock:(void(^)(HBWaterfallWrapper *waterfallWrapper, HBWaterfall *waterfall, HBWaterfall *headerBiddingWaterfall, HBWaterfall *finalWaterfall, BOOL finished, NSDate *loadStartDate))block;
@end
