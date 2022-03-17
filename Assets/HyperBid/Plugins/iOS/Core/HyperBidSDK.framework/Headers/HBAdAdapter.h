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
extern NSString *const kHBAdapterCustomInfoBuyeruIdKey;
extern NSString *const kHBAdapterCustomInfoBidInfoKey;
extern NSString *const kHBAdapterCustomInfoBidResultKey;
extern NSString *const kHBAdapterCustomInfoBidPlacementIDKey;

extern NSString *const kHBHeaderBiddingParametersUnitIdKey;
extern NSString *const kHBHeaderBiddingParametersNetworkFirmIdKey;
extern NSString *const kHBHeaderBiddingParametersAdFormatKey;
extern NSString *const kHBHeaderBiddingParametersAdSourceIdKey;
extern NSString *const kHBHeaderBiddingParametersEcpofferKey;
extern NSString *const kHBHeaderBiddingParametersGetOfferKey;
extern NSString *const kHBHeaderBiddingParametersDisplayManagerVersionKey;
extern NSString *const kHBHeaderBiddingParametersTplVersionKey;
extern NSString *const kHBHeaderBiddingParametersAppIdKey;
extern NSString *const kHBHeaderBiddingParametersAccountIdKey;
extern NSString *const kHBHeaderBiddingParametersBuyeruIdKey;
extern NSString *const kHBHeaderBiddingParametersBidTokenKey;
extern NSString *const kHBHeaderBiddingParametersAdWidthKey;
extern NSString *const kHBHeaderBiddingParametersAdHeightKey;

extern NSString *const kHBHeaderBiddingParametersDirectOfferKey;

extern NSString *const kHBHeaderBiddingParametersDirectOfferSizeKey;


extern NSString *const kHBHeaderBiddingParametersBidFormatKey;
extern NSString *const kHBHeaderBiddingParametersBidderTypeKey;
extern NSString *const kHBHeaderBiddingParametersUnitGroupKey;

extern NSString *const kHBHeaderBiddingParametersSDKInfoKey;

@protocol HBAd;
@class HBPlacementModel;
@class HBUnitGroupModel;
@class HBMyOfferOfferModel;
@class HBBidInfo;
@class HBWaterfall;
@class HBInHouseBidRequest;

typedef NS_ENUM(NSInteger, HBBiddingLossType) {
    HBBiddingLossWithLowPriceInNormal = 103,
    HBBiddingLossWithLowPriceInHB = 102,
    HBBiddingLossWithBiddingTimeOut = 2,
    HBBiddingLossWithExpire = 5
};

@protocol HBAdAdapter<NSObject>
@property (nonatomic,copy) void (^metaDataDidLoadedBlock)(void);
/*
 * Create a rewarded instance for download event and FOR DOWNLOAD EVENT ONLY.
 */
//+(id<HBAd>) placeholderAdWithPlacementModel:(HBPlacementModel*)placementModel requestID:(NSString*)requestID unitGroup:(HBUnitGroupModel*)unitGroup finalWaterfall:(HBWaterfall*)finalWaterfall;
+(id<HBAd>) readyFilledAdWithPlacementModel:(HBPlacementModel*)placementModel requestID:(NSString*)requestID priority:(NSInteger)priority unitGroup:(HBUnitGroupModel*)unitGroup finalWaterfall:(HBWaterfall*)finalWaterfall localInfo:(NSDictionary *)localInfo;
+(HBMyOfferOfferModel*) resourceReadyMyOfferForPlacementModel:(HBPlacementModel*)placementModel unitGroupModel:(HBUnitGroupModel*)unitGroupModel info:(NSDictionary*)info;
+(BOOL) adReadyForInfo:(NSDictionary*)info;
-(instancetype) initWithNetworkCustomInfo:(NSDictionary *)serverInfo localInfo:(NSDictionary *)localInfo;
-(void) loadADWithInfo:(NSDictionary *)serverInfo localInfo:(NSDictionary *)localInfo completion:(void (^)(NSArray<NSDictionary*> *assets, NSError *error))completion;
+(void) bidRequestWithPlacementModel:(HBPlacementModel*)placementModel unitGroupModel:(HBUnitGroupModel*)unitGroupModel info:(NSDictionary*)info completion:(void(^)(HBBidInfo *bidInfo, NSError *error))completion;
@optional
//+(void) inhouseRequestInfoWithCompletion:(void(^)(NSDictionary *bidResult))completion;

+(void)headerBiddingParametersWithUnitGroupModel:(HBUnitGroupModel*)model extra:(NSDictionary *)extra completion:(void(^)(NSDictionary *headerBiddingParams))completion;
//+(HBInHouseBidRequest*)inHouseBiddingRequestWithPlacementModel:(HBPlacementModel*)placementModel unitGroupModel:(HBUnitGroupModel*)unitGroupModel extra:(NSDictionary *)extra;

+(void) sendWinnerNotifyWithCustomObject:(id)customObject secondPrice:(NSString*)price;
+(void) sendLossNotifyWithCustomObject:(id)customObject lossType:(HBBiddingLossType)lossType;

@end
#endif /* HBAdAdapter_h */
