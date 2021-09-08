//
//  HBInterstitialAdapter.h
//  HyperBidInterstitial
//
//  Created by Leo on 21/09/2018.
//  Copyright © 2018 Leo. All rights reserved.
//

#ifndef HBInterstitialAdapter_h
#define HBInterstitialAdapter_h
#import <HyperBidSDK/HyperBidSDK.h>
#import "HBInterstitialDelegate.h"
#import "HBInterstitial.h"
#import <UIKit/UIKit.h>

@protocol HBInterstitialAdapter<HBAdAdapter>
@optional
+(BOOL) adReadyWithCustomObject:(id)customObject info:(NSDictionary*)info;
+(void) showInterstitial:(HBInterstitial*)interstitial inViewController:(UIViewController*)viewController delegate:(id<HBInterstitialDelegate>)delegate;
@end

#endif /* HBInterstitialAdapter_h */
