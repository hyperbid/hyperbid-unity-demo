//
//  HBNativeADRenderer.h
//  HyperBidSDK
//
//  Created by Leo on 20/04/2018.
//  Copyright © 2018 Leo. All rights reserved.
//

#ifndef HBNativeADRenderer_h
#define HBNativeADRenderer_h
#import <UIKit/UIKit.h>
@class HBNativeADView;
@class HBNativeADConfiguration;
@class HBNativeADCache;
@protocol HBNativeADRenderer<NSObject>
/**
 Might return nil.
 */
-(__kindof UIView*)createMediaView;

/**
 Render the assets onto the the associated AD view. Adopter implements this method in a network-specific way.
 */
-(void) renderOffer:(HBNativeADCache*)offer;

/**
 * Whether the ad being rendered is video ad.
 */
-(BOOL)isVideoContents;

/**
 This reference to the associated AD view should be kept as a weak one, for an AD view strongly keeps its render.
 This property is added so that the renderAssets: method can access it directly.
 */
@property(nonatomic, weak) HBNativeADView *ADView;

@end

#endif /* HBNativeADRenderer_h */
