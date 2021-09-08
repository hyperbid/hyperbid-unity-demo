//
//  HBAdManager+Native.h
//  HyperBidNative
//
//  Created by Leo on 07/07/2018.
//  Copyright © 2018 Leo. All rights reserved.
//

#import <HyperBidSDK/HyperBidSDK.h>

//Currently only GDT supports these two keys.
extern NSString *const kHBExtraInfoNativeAdSizeKey;//the value has to be an NSValue wrapped CGSize object.
extern NSString *const kHBExtraInfoNativeAdTypeKey;//The value is requried for GDT native ad and has to be an NSNumber warpped HBGDTNativeAdType(NSInteger); Pass @(HBGDTNativeAdTypeTemplate)(@1) for template ads and @(HBGDTNativeAdTypeSelfRendering)(@2) for self rendering ads.
//Following keys are supported by nend only
extern NSString *const kHBExtraInfoNativeAdUserIDKey;
extern NSString *const kHBExtraInfoNativeAdMediationNameKey;
extern NSString *const kHBExtraInfoNaitveAdUserFeatureKey;
extern NSString *const kHBExtraInfoNativeAdLocationEnabledFlagKey;
typedef NS_ENUM(NSInteger, HBGDTNativeAdType) {
    HBGDTNativeAdTypeTemplate = 1,
    HBGDTNativeAdTypeSelfRendering = 2
};
@class HBNativeADView;
@class HBNativeADConfiguration;
@interface HBAdManager (Native)
-(BOOL) isReadyNativeAdWithPlacementID:(NSString*)placementID;
/**
 * This method uses the renderingViewClass you specify in the configuration to create an instance and:
 1) returns it(for networks Facebook, Inmobi, Mintegral, Admob, Flurry, Applovin) or
 2) adds it to a superView and returns the super view instead(for network Mopub).
 * To retrieve the instance of the class you specify as the rendering view class, cast the returned view to HBNativeADView and call its embededAdView method(the view returned might not be of class HBNativeADView).
 */
- (__kindof UIView*) retriveAdViewWithPlacementID:(NSString*)placementID configuration:(HBNativeADConfiguration*)configuration;
- (__kindof UIView*) retriveAdViewWithPlacementID:(NSString*)placementID configuration:(HBNativeADConfiguration*)configuration scene:(NSString *)scene;

- (HBCheckLoadModel*)checkNativeLoadStatusForPlacementID:(NSString*)placementID;

@end
