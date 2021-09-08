//
//  HBADXPlacementSetting.h
//  HyperBidSDK
//
//  Created by Leo on 20/8/2020.
//  Copyright © 2020 HyperBid. All rights reserved.
//

#import "HBOfferSetting.h"

@interface HBADXPlacementSetting : HBOfferSetting
-(instancetype) initWithPlacementDictionary:(NSDictionary *)placementDictionary infoDictionary:(NSDictionary *)infoDictionary  placementID:(NSString*)placementID;

+(instancetype) mockSetting;
@end
