//
//  HBBanner+Internal.h
//  HyperBidBanner
//
//  Created by Leo on 28/09/2018.
//  Copyright © 2018 Leo. All rights reserved.
//

#ifndef HBBanner_Internal_h
#define HBBanner_Internal_h
#import "HBBannerView.h"
@class HBBanner;
@interface HBBannerView(Internal)
-(instancetype) initWithFrame:(CGRect)frame banner:(HBBanner*)banner;
-(void) loadNextWithoutRefresh;
@property(nonatomic) HBBanner *banner;
@end
#endif /* HBBanner_Internal_h */
