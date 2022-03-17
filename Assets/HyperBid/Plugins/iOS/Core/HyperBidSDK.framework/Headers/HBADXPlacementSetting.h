//
//  HBADXPlacementSetting.h
//  HyperBidSDK
//
//  Created by Leo on 20/8/2020.
//  Copyright © 2020 HyperBid. All rights reserved.
//

#import "HBOfferSetting.h"

typedef NS_ENUM(NSInteger, HBDirectOfferInterType) {
    HBDirectOfferInterTypeFull = 1,
    HBDirectOfferInterTypeHalf
};

typedef NS_ENUM(NSInteger, HBDirectOfferUnitType) {
    HBDirectOfferUnitPictureType = 0,
    HBDirectOfferUnitVideoType = 1,
};

@interface HBADXPlacementSetting : HBOfferSetting
-(instancetype) initWithPlacementDictionary:(NSDictionary *)placementDictionary infoDictionary:(NSDictionary *)infoDictionary  placementID:(NSString*)placementID;

+(instancetype) mockSetting;

@property(nonatomic, assign) HBDirectOfferInterType interType;

@property(nonatomic, assign) HBDirectOfferUnitType unitType;



@end
