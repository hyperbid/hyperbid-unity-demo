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

extern NSString *const kHBNativeAdSizeToFitKey;
typedef NS_ENUM(NSInteger, HBGDTNativeAdType) {
    HBGDTNativeAdTypeTemplate = 1,
    HBGDTNativeAdTypeSelfRendering = 2
};
@class HBNativeADView;
@class HBNativeADConfiguration;

@protocol HBBaiduTemplateRenderingAttributeDelegate <NSObject>

@optional
//logo配置
@property (nonatomic, strong) NSString *iconWidth;
@property (nonatomic, strong) NSString *iconHeight;
@property (nonatomic, strong) NSString *iconLeft;
@property (nonatomic, strong) NSString *iconTop;
@property (nonatomic, strong) NSString *iconRight;
@property (nonatomic, strong) NSString *iconBottom;

//标题配置
@property (nonatomic, strong) NSString *titleLeft;
@property (nonatomic, strong) NSString *titleTop;
@property (nonatomic, strong) NSString *titleWidth;
@property (nonatomic, strong) NSString *titleHeight;
@property (nonatomic, strong) NSString *titleRight;
@property (nonatomic, strong) NSString *titleBottom;
@property (nonatomic, strong) NSString *titleFontSize;//系统默认字体
@property (nonatomic, strong) UIFont *titleFont;
@property (nonatomic, strong) UIColor *titleColor;

//主素材：大图、视频、三图首图
@property (nonatomic, strong) NSString *mainMaterialLeft;
@property (nonatomic, strong) NSString *mainMaterialTop;
@property (nonatomic, strong) NSString *mainMaterialWidth;
@property (nonatomic, strong) NSString *mainMaterialHeight;
@property (nonatomic, strong) NSString *mainMaterialRight;
@property (nonatomic, strong) NSString *mainMaterialBottom;

//三图的中图
@property (nonatomic, strong) NSString *centerPicLeft;
@property (nonatomic, strong) NSString *centerPicTop;
@property (nonatomic, strong) NSString *centerPicWidth;
@property (nonatomic, strong) NSString *centerPicHeight;
@property (nonatomic, strong) NSString *centerPicRight;
@property (nonatomic, strong) NSString *centerPicBottom;

//三图的右图
@property (nonatomic, strong) NSString *lastPicLeft;
@property (nonatomic, strong) NSString *lastPicTop;
@property (nonatomic, strong) NSString *lastPicWidth;
@property (nonatomic, strong) NSString *lastPicHeight;
@property (nonatomic, strong) NSString *lastPicRight;
@property (nonatomic, strong) NSString *lastPicBottom;

//底部行为按钮
@property (nonatomic, strong) NSString *buttonLeft;
@property (nonatomic, strong) NSString *buttonRight;
@property (nonatomic, strong) NSString *buttonTop;
@property (nonatomic, strong) NSString *buttonBottom;
@property (nonatomic, strong) NSString *buttonWidth;
@property (nonatomic, strong) NSString *buttonHeight;
@property (nonatomic, strong) UIFont *buttonFont;
@property (nonatomic, strong) NSString *buttonCornerRadius;
@property (nonatomic, strong) UIColor *buttonTitleColor;
@property (nonatomic, strong) UIColor *buttonBackgroundColor;

//底部负反馈按钮
@property (nonatomic, strong) NSString *dislikeBtnLeft;
@property (nonatomic, strong) NSString *dislikeBtnRight;
@property (nonatomic, strong) NSString *dislikeBtnTop;
@property (nonatomic, strong) NSString *dislikeBtnBottom;
@property (nonatomic, strong) NSString *dislikeBtnHeigth;
@property (nonatomic, strong) NSString *dislikeBtnWidth;
@property (nonatomic, strong) UIImage *dislikeBtnImage;

//底部品牌字样,建议不更改
@property (nonatomic, strong) NSString *brandLeft;
@property (nonatomic, strong) NSString *brandWidth;
@property (nonatomic, strong) NSString *brandHeight;
@property (nonatomic, strong) NSString *brandBottom;
@property (nonatomic, strong) NSString *brandFontSize;
@property (nonatomic, strong) UIFont *brandFont;
@property (nonatomic, strong) UIColor *brandColor;
@end

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

// v5.7.53+
- (NSArray<NSDictionary *> *)getNativeValidAdsForPlacementID:(NSString *)placementID;


- (void) setBaiduTemplateRenderingAttribute:(id<HBBaiduTemplateRenderingAttributeDelegate> )baiduTemplateRenderingAttribute;


@end
