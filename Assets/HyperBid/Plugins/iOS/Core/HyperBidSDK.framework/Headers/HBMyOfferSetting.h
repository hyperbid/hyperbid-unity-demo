//
//  HBMyOfferSetting.h
//  HyperBidSDK
//
//  Created by Leo on 2019/9/23.
//  Copyright © 2019 Leo. All rights reserved.
//

#import "HBOfferSetting.h"

// v5.7.47
typedef NS_ENUM(NSInteger, HBMyOfferInterType) {
    HBMyOfferInterTypeFull = 1,
    HBMyOfferInterTypeHalf
};

@interface HBMyOfferSetting : HBOfferSetting
-(instancetype) initWithDictionary:(NSDictionary *)dictionary placementID:(NSString*)placementID;

@property(nonatomic, readwrite) NSTimeInterval resourceCacheTime;

@property(nonatomic, readwrite) HBMyOfferInterType interType;

+(instancetype) mockSetting;
@end
