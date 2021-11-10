//
//  HBBidInfoManager.h
//  HyperBidSDK
//
//  Created by Leo on 2020/4/28.
//  Copyright © 2020 HyperBid. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "HBBidInfo.h"
@class HBUnitGroupModel;
@class HBPlacementModel;
@class HBBidWaterFallModel;
@class HBBidNotifSendModel;
@interface HBBidInfoManager : NSObject
+(instancetype) sharedManager;
/**
 Used for renew bidinfo
 */
-(void) saveRequestID:(NSString*)requestID forPlacementID:(NSString*)placementID;
-(NSString*)requestForPlacementID:(NSString*)placementID;
-(void) renewBidInfoForPlacementID:(NSString*)placementID fromRequestID:(NSString*)requestID toRequestID:(NSString*)newRequestID unitGroups:(NSArray<HBUnitGroupModel*>*)unitGroups;

/**
 Used for send hb loss notification
 */
-(void) saveWithBidNotifSendModel:(HBBidNotifSendModel*)bidNotifSendModel forRequestID:(NSString*)requestID;
-(HBBidNotifSendModel*)getBidNotifSendModelForRequestID:(NSString*)requestID;

-(void) saveNoPriceCacheWitBidWaterFallModel:(HBBidWaterFallModel*)bidWaterfallModel;
-(void) removeNoPriceCacheWithTpBidId:(NSString*)tpBidId unitId:(NSString*)unitId;
-(HBBidWaterFallModel *)getBidWaterFallModelWithTpBidId:(NSString*)tpBidId unitId:(NSString*)unitId;


/**
 send hb win、loss、display notification
 */
-(void)sendHBWinnerNotificationForBidInfo:(HBBidInfo *)bidInfo;
-(void)sendHBLossNotificationForPlacementID:(NSString*)placementID requestID:(NSString*)requestID unitGroups:(NSArray<HBUnitGroupModel*>*)unitGroups headerBiddingUnitGroups:(NSArray<HBUnitGroupModel*>*)headerBiddingUnitGroups;
-(void)sendNotifyDisplayForPlacementID:(NSString*)placementID unitGroup:(HBUnitGroupModel*)unitGroup winner:(BOOL)isWinner headerBidding:(BOOL)headerBidding price:(NSString *)price;

-(void)sendHBLossNotificationForBidInfo:(HBBidInfo *)bidInfo price:(NSString*)price headerBidding:(BOOL)headerBidding requestID:(NSString*)requestID;

-(void) saveBidInfo:(HBBidInfo*)bidInfo forRequestID:(NSString*)requestID;
-(void) removeDiskBidInfo:(HBBidInfo*)bidInfo;
-(void) hasBeenSendNotifBidInfoForPlacementID:(NSString*)placementID unitGroupModel:(HBUnitGroupModel*)unitGroupModel requestID:(NSString*)requestID;
-(void) invalidateBidInfoForPlacementID:(NSString*)placementID unitGroupModel:(HBUnitGroupModel*)unitGroupModel requestID:(NSString*)requestID;
-(HBBidInfo*) bidInfoForPlacementID:(NSString*)placementID unitGroupModel:(HBUnitGroupModel*)unitGroupModel requestID:(NSString*)requestID;
-(NSArray<HBUnitGroupModel*>*) unitGroupWithHistoryBidInfoAvailableForPlacementID:(NSString*)placementID unitGroups:(NSArray<HBUnitGroupModel*>*)unitGroupsToInspect inhouseUnitGroups:(NSArray<HBUnitGroupModel*>*)inhouseUnitGroupsToInspect s2sUnitGroups:(NSArray<HBUnitGroupModel*>*)s2sUnitGroupsToInspect newRequestID:(NSString*)newRequestID;
+(NSString *) priceForUnitGroup:(HBUnitGroupModel*)unitGroupModel;
+(NSString *) priceForUnitGroup:(HBUnitGroupModel*)unitGroupModel placementID:(NSString*)placementID requestID:(NSString*)requestID;
+(NSString *) getPriceToSendHBNotifiForUnitGroup:(HBUnitGroupModel*)unitGroupModel;

-(BOOL) checkAdxBidInfoExpireForUnitGroupModel:(HBUnitGroupModel*)unitGroupModel;
-(HBBidInfo*) getBidInfoCachedForPlacementID:(NSString*)placementID unitGroup:(HBUnitGroupModel*)unitGroup;
-(void) invalidateBidInfoForPlacementID:(NSString*)placementID unitGroupModel:(HBUnitGroupModel*)unitGroupModel;
@end
