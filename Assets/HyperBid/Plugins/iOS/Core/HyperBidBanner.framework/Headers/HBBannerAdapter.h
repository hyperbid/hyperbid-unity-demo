//
//  HBBannerAdapter.h
//  HyperBidBanner
//
//  Created by Leo on 2018/10/8.
//  Copyright © 2018 Leo. All rights reserved.
//

#ifndef HBBannerAdapter_h
#define HBBannerAdapter_h
@class HBBanner;
@protocol HBBannerAdapter<HBAdAdapter>
+(void) showBanner:(HBBanner*)banner inView:(UIView*)view presentingViewController:(UIViewController*)viewController;
@end
#endif /* HBBannerAdapter_h */
