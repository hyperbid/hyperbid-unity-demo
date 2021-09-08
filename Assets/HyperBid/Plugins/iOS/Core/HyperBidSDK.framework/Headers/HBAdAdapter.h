//
//  HBAdAdapter.h
//  HyperBidSDK
//
//  Created by Leo on 05/07/2018.
//  Copyright © 2018 Leo. All rights reserved.
//

#ifndef HBAdAdapter_h
#define HBAdAdapter_h
extern NSString *const kHBADapterCustomInfoStatisticsInfoKey;
extern NSString *const kHBAdapterCustomInfoPlacementModelKey;
extern NSString *const kHBAdapterCustomInfoUnitGroupModelKey;
extern NSString *const kHBAdapterCustomInfoRequestIDKey;
extern NSString *const kHBAdapterCustomInfoExtraKey;
@protocol HBAd;
@class HBPlacementModel;
@class HBUnitGroupModel;
@class HBMyOfferOfferModel;
@class HBBidInfo;
@class HBWaterfall;
@protocol HBAdAdapter<NSObject>
@property (nonatomic,copy) void (^metaDataDidLoadedBlock)(void);
/*
 * Create a rewarded instance for download event and FOR DOWNLOAD EVENT ONLY.
 */
//+(id<HBAd>) placeholderAdWithPlacementModel:(HBPlacementModel*)placementModel requestID:(NSString*)requestID unitGroup:(HBUnitGroupModel*)unitGroup finalWaterfall:(HBWaterfall*)finalWaterfall;
+(id<HBAd>) readyFilledAdWithPlacementModel:(HBPlacementModel*)placementModel requestID:(NSString*)requestID priority:(NSInteger)priority unitGroup:(HBUnitGroupModel*)unitGroup finalWaterfall:(HBWaterfall*)finalWaterfall;
+(HBMyOfferOfferModel*) resourceReadyMyOfferForPlacementModel:(HBPlacementModel*)placementModel unitGroupModel:(HBUnitGroupModel*)unitGroupModel info:(NSDictionary*)info;
+(BOOL) adReadyForInfo:(NSDictionary*)info;
-(instancetype) initWithNetworkCustomInfo:(NSDictionary *)serverInfo localInfo:(NSDictionary *)localInfo;
-(void) loadADWithInfo:(NSDictionary *)serverInfo localInfo:(NSDictionary *)localInfo completion:(void (^)(NSArray<NSDictionary*> *assets, NSError *error))completion;
+(void) bidRequestWithPlacementModel:(HBPlacementModel*)placementModel unitGroupModel:(HBUnitGroupModel*)unitGroupModel info:(NSDictionary*)info completion:(void(^)(HBBidInfo *bidInfo, NSError *error))completion;
@optional
+(NSDictionary*)headerBiddingParametersWithUnitGroupModel:(HBUnitGroupModel*)model extra:(NSDictionary *)extra;
+(NSString*) adsourceRemoteKeyWithContent:(NSDictionary*)content unitGroupModel:(HBUnitGroupModel *)unitGroupModel;
@end
#endif /* HBAdAdapter_h */
