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
//-(void) callback;
-(HBUnitGroupModel*) filledUnitGroupWithMaximumPrice;
- (HBUnitGroupModel *)requestingUnitGroupMaxPriceWithFilteredUnitID:(NSString *)unitID;
@property(nonatomic) NSInteger numberOfCachedOffers;
@property(nonatomic, readonly, getter=isFilled) BOOL filled;
//@property(nonatomic, readonly, getter=isCallbacked) BOOL callbacked;
@property(nonatomic) BOOL headerBiddingFired;
@property(nonatomic) BOOL headerBiddingFailed;
@property(nonatomic, readonly) dispatch_queue_t access_queue;

@end

@interface HBWaterfall:NSObject
-(instancetype) initWithUnitGroups:(NSArray<HBUnitGroupModel*>*)unitGroups placementID:(NSString*)placementID requestID:(NSString*)requestID;
-(void) requestUnitGroup:(HBUnitGroupModel*)unitGroup;
-(void) finishUnitGroup:(HBUnitGroupModel*)unitGroup withType:(HBUnitGroupFinishType)type;
-(void) addUnitGroup:(HBUnitGroupModel*)unitGroup;
-(void) insertUnitGroup:(HBUnitGroupModel*)unitGroup price:(NSString *)price;
-(void) insertUnitGroup:(HBUnitGroupModel*)unitGroup price:(NSString *)price filtered:(BOOL)filtered;

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

- (NSMutableArray<HBUnitGroupModel *> *)getWaterfallUnitGroups;
@end

@interface HBWaterfallManager : NSObject
+(instancetype) sharedManager;

-(void) removeWaterfallWrappers:(NSString *)placementID;

-(BOOL) loadingAdForPlacementID:(NSString*)placementID;

// just for api: check ad loading status
-(BOOL) loadingAdForPlacementID:(NSString*)placementID skipSettingLoadingStatus:(BOOL)skip;

-(void) attachWaterfall:(HBWaterfall*)waterfall completion:(void(^)(HBWaterfallWrapper *waterfallWrapper, HBWaterfall *waterfall, HBWaterfall *headerBiddingWaterfall, HBWaterfall *finalWaterfall, BOOL finished, NSDate *loadStartDate))completion;

-(void) attachDefaultWaterfall:(HBWaterfall*)defaultWaterfall completion:(void(^)(HBWaterfallWrapper *waterfallWrapper, HBWaterfall *waterfall, HBWaterfall *headerBiddingWaterfall, HBWaterfall *finalWaterfall, HBWaterfall *defaultWaterfall, BOOL finished, NSDate *loadStartDate))completion;
-(void) accessWaterfallForPlacementID:(NSString*)placementID requestID:(NSString*)requestID withBlock:(void(^)(HBWaterfallWrapper *waterfallWrapper, HBWaterfall *waterfall, HBWaterfall *headerBiddingWaterfall, HBWaterfall *defaultWaterfall, HBWaterfall *finalWaterfall, BOOL finished, NSDate *loadStartDate))block;

@end
