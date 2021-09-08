//
//  HBMyOfferSetting.h
//  HyperBidSDK
//
//  Created by Leo on 2019/9/23.
//  Copyright © 2019 Leo. All rights reserved.
//

#import "HBOfferSetting.h"

@interface HBMyOfferSetting : HBOfferSetting
-(instancetype) initWithDictionary:(NSDictionary *)dictionary placementID:(NSString*)placementID;

@property(nonatomic, readwrite) NSTimeInterval resourceCacheTime;

+(instancetype) mockSetting;
@end
