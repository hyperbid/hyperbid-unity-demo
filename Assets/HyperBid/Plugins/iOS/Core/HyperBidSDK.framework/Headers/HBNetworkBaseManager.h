//
//  HBNetworkBaseManager.h
//  HyperBidSDK
//
//  Created by HyperBid on 11/11/20.
//  Copyright © 2020 HyperBid. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "HBAPI+Internal.h"

NS_ASSUME_NONNULL_BEGIN

@interface HBNetworkBaseManager : NSObject
+(void) initWithCustomInfo:(NSDictionary*)serverInfo localInfo:(NSDictionary*)localInfo;
@end

NS_ASSUME_NONNULL_END
