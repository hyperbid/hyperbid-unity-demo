//
//  HBNativeADConfiguration.h
//  HyperBidSDK
//
//  Created by Leo on 20/04/2018.
//  Copyright © 2018 Leo. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
extern NSString *const kHBNativeAdConfigurationContextAdOptionsViewFrameKey;//CGRect wrapped as NSValue, supported by facebook
extern NSString *const kHBNativeAdConfigurationContextAdLogoViewFrameKey;//CGRect wrapped as NSValue, supported by gdt&baidu, defaults to top-left corner
extern NSString *const kHBNativeAdConfigurationContextNetworkLogoViewFrameKey;//CGRect wrapped as NSValue, support by baidu, defaults to bottom-right corner
@protocol HBNativeADDelegate;
@interface HBNativeADConfiguration : NSObject
/**
 You can store some extra network specific context info in this dictionary.
 */
@property(nonatomic) NSDictionary *context;
@property(nonatomic) Class renderingViewClass;
@property(nonatomic) CGRect ADFrame;
@property(nonatomic) CGRect mediaViewFrame;
@property(nonatomic, weak) UIViewController *rootViewController;
@property(nonatomic, weak) id<HBNativeADDelegate> delegate;
@property(nonatomic) BOOL sizeToFit;

@end
