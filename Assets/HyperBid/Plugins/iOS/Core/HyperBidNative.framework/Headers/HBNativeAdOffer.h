//
//  HBNativeAdOffer.h
//  HyperBidNative
//
//  Created by HyperBid on 10/27/21.
//  Copyright © 2021 HyperBid. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "HBNativeAd.h"

NS_ASSUME_NONNULL_BEGIN

@class HBNativeADView;
@class HBNativeADConfiguration;
@interface HBNativeAdOffer : NSObject
/**
 * The native ad that is being shown.
 */
@property(nonatomic, readonly) HBNativeAd *nativeAd;
/**
 * The adOffer info of native ad.
 */
@property(nonatomic, readonly) NSDictionary *adOfferInfo;
/**
 * The HBNativeADView info of native ad.
 */
@property(nonatomic, readonly) HBNativeADView *adView;

-(__kindof UIView *) rendererWithConfiguration:(HBNativeADConfiguration*)configuration;

@end

NS_ASSUME_NONNULL_END
