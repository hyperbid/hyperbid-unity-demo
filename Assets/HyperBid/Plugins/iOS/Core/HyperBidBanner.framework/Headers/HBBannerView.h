//
//  HBBannerView.h
//  HyperBidBanner
//
//  Created by Leo on 18/09/2018.
//  Copyright © 2018 Leo. All rights reserved.
//

#import <UIKit/UIKit.h>

@protocol HBBannerDelegate;
@interface HBBannerView : UIView
//to be move into a internal category
@property(nonatomic, weak) id<HBBannerDelegate> delegate;
@property(nonatomic, weak) UIViewController *presentingViewController;

- (void)sendImpressionTracking;
@end
