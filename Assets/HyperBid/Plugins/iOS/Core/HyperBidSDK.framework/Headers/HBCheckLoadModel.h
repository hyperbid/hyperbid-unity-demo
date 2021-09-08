//
//  HBCheckLoadModel.h
//  HyperBidSDK
//
//  Created by HyperBid on 9/29/20.
//  Copyright © 2020 HyperBid. All rights reserved.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

@interface HBCheckLoadModel : NSObject

@property(nonatomic) BOOL isLoading;
@property(nonatomic) BOOL isReady;
@property(nonatomic, readwrite) NSDictionary *adOfferInfo;

@end

NS_ASSUME_NONNULL_END
