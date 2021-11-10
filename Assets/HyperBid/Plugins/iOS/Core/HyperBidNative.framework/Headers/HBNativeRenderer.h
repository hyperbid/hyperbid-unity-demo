//
//  HBNativeRenderer.h
//  HyperBidSDK
//
//  Created by Leo on 25/04/2018.
//  Copyright © 2018 Leo. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <HyperBidSDK/HyperBidSDK.h>
#import "HBNativeADRenderer.h"
//#import "HBAPI.h"
@protocol HBNativeADRenderer;
@class HBNativeADCache;
@interface HBNativeRenderer : NSObject<HBNativeADRenderer>
//This method has been added specifically for Mopub; renderers of other networks don't implement it.
+(id) retrieveRendererWithOffer:(HBNativeADCache*)offer;
-(UIView*)retriveADView;
- (void)recordCustomPlayerStatus:(HBPlayerStatus)status currentTime:(NSTimeInterval)time;
-(instancetype) initWithConfiguraton:(HBNativeADConfiguration*)configuration adView:(HBNativeADView*)adView;
-(__kindof UIView*)createMediaView;
@property(nonatomic, weak) HBNativeADView *ADView;
@property(nonatomic, readonly) HBNativeADConfiguration *configuration;
@end
