//
//  HBRewardedVideoAdapter.h
//  HyperBidSDK
//
//  Created by Leo on 05/07/2018.
//  Copyright © 2018 Leo. All rights reserved.
//

#ifndef HBRewardedVideoAdapter_h
#define HBRewardedVideoAdapter_h
#import <HyperBidSDK/HyperBidSDK.h>
//#import "HBAdAdapter.h"
#import "HBRewardedVideoDelegate.h"
#import "HBRewardedVideo.h"
#import <UIKit/UIKit.h>

@protocol HBRewardedVideoAdapter<HBAdAdapter>
@optional
+(BOOL) adReadyWithCustomObject:(id)customObject info:(NSDictionary*)info;
+(void) showRewardedVideo:(HBRewardedVideo*)rewardedVideo inViewController:(UIViewController*)viewController delegate:(id<HBRewardedVideoDelegate>)delegate;
@end
#endif /* HBRewardedVideoAdapter_h */
