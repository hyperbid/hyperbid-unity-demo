//
//  HBAdManagement.h
//  HyperBidSDK
//
//  Created by Leo on 04/07/2018.
//  Copyright © 2018 Leo. All rights reserved.
//

#ifndef HBAdManagement_h
#define HBAdManagement_h
@protocol HBAd;
@class HBPlacementModel;
@class HBUnitGroupModel;
@class HBWaterfall;
extern NSString *const kHBAdAssetsCustomEventKey;
extern NSString *const kHBAdAssetsCustomObjectKey;
extern NSString *const kHBAdAssetsUnitIDKey;
extern NSString *const kHBAdAssetsPriceKey;
extern NSString *const kHBAdAssetsBidIDKey;
extern NSString *const kHBAdAssetsTpBidIDKey;

typedef NS_ENUM(NSInteger, HBAdSourceStatus) {
    HBAdSourceStatusInvalid = 0,//pacing & cap_by_hour/cap_by_day
    HBAdSourceStatusNoOffer = 1,
    HBAdSourceStatusOfferNotReady = 2,
    HBAdSourceStatusOfferExpired = 3,
    HBAdSourceStatusReady = 4
};

@protocol HBAdManagement<NSObject>
-(void) addAdWithADAssets:(NSDictionary*)assets withPlacementSetting:(HBPlacementModel*)placementModel unitGroup:(HBUnitGroupModel*)unitGroup finalWaterfall:(HBWaterfall*)finalWaterfall requestID:(NSString*)requestID;
-(BOOL) inspectAdSourceStatusWithPlacementModel:(HBPlacementModel*)placementModel unitGroup:(HBUnitGroupModel*)unitGroup finalWaterfall:(HBWaterfall*)finalWaterfall requestID:(NSString*)requestID extraInfo:(NSArray<NSDictionary*>*__autoreleasing*)extraInfo;
-(void) updateAdSourceOffersWithPlacementModel:(HBPlacementModel*)placementModel finalWaterfall:(HBWaterfall*)finalWaterfall requestID:(NSString*)requestID;
-(void) invalidateStatusForAd:(id<HBAd>)ad;
-(BOOL) adSourceStatusInPlacementModel:(HBPlacementModel*)placementModel unitGroup:(HBUnitGroupModel*)unitGroup;

/**
 
 */
-(NSArray<id<HBAd>>*) adsWithPlacementID:(NSString*)placementID;

-(id<HBAd>) adWithPlacementModel:(HBPlacementModel*)placementModel unitGroup:(HBUnitGroupModel*)unitGroup;

-(NSArray<id<HBAd>>*) availableStatusAdsWithPlacementModel:(HBPlacementModel*)placementModel;

/**
 * Clear all cached offer
 */
-(void) clearCache;

/*
 *Return the following structure, indicating the status of the ad-sources within the placement:
 {
     @0:@(HBAdSourceStatusInvalid),
     @1:@(HBAdSourceStatusOfferNotReady),
     @2:@(HBAdSourceStatusOfferExpired),
     @3:@(HBAdSourceStatusReady)
 }
 */
-(NSDictionary<NSNumber*, NSNumber*>*)placementStatusWithPlacementID:(NSString*)placementID;
-(void) clearCahceForPlacementID:(NSString*)placementID;
-(void) removeAdForPlacementID:(NSString*)placementID unitID:(NSString*)unitID;
-(NSInteger) highestPriorityOfShownAdInPlacementID:(NSString*)placementID requestID:(NSString*)requestID;
@end

#endif /* HBAdManagement_h */
