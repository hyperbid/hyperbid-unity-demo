//
//  HBAdManager.h
//  HyperBidSDK
//
//  Created by Leo on 04/05/2018.
//  Copyright © 2018 Leo. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>

extern NSString *const kHBAdAssetsCustomEventKey;
extern NSString *const kHBAdAssetsCustomObjectKey;

//Banner's ad size, the value is a NSValue wrapped CGSize; for banner ad loading, we only support (320.0f, 50.0f) now, so the value you pass via this key will be ignored.
extern NSString *const kHBExtraInfoAdSizeKey;
extern NSString *const kHBAdLoadingExtraGDTEnableDefaultAudioSessionKey;
@protocol HBAdLoadingDelegate;
@interface HBAdManager : NSObject
+(instancetype) sharedManager;
@property(nonatomic, strong) NSDictionary *extra;
/*
 We mark this method as deprecated because we move customData into HBAPI's shared instance. customData passed via this method will just be ignored.
 */
-(void) loadAd:(NSString*)placementID extra:(NSDictionary*)extra customData:(NSDictionary*)customData delegate:(id<HBAdLoadingDelegate>)delegate DEPRECATED_ATTRIBUTE;
-(void) loadAd:(NSString*)placementID extra:(NSDictionary*)extra delegate:(id<HBAdLoadingDelegate>)delegate;
-(void) clearCache DEPRECATED_ATTRIBUTE;
@end
