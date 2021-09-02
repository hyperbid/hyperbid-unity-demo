//
//  HBNativeAdWrapper.m
//  UnityContainer
//
//  Created by Martin Lau on 27/07/2018.
//  Copyright © 2018 Martin Lau. All rights reserved.
//

#import "HBNativeAdWrapper.h"
#import "HBUnityUtilities.h"
#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#import <HyperBidNative/HBAdManager+Native.h>
#import <HyperBidNative/HBNativeAdConfiguration.h>
#import <HyperBidNative/HBNativeADView.h>
#import "MTAutolayoutCategories.h"
#import "HBUnityManager.h"

static NSString *const kParsedPropertiesFrameKey = @"frame";
static NSString *const kParsedPropertiesBackgroundColorKey = @"background_color";
static NSString *const kParsedPropertiesTextColorKey = @"text_color";
static NSString *const kParsedPropertiesTextSizeKey = @"text_size";

static NSString *const kNativeAssetAdvertiser = @"advertiser_label";
static NSString *const kNativeAssetText = @"text";
static NSString *const kNativeAssetTitle = @"title";
static NSString *const kNativeAssetCta = @"cta";
static NSString *const kNativeAssetRating = @"rating";
static NSString *const kNativeAssetIcon = @"icon";
static NSString *const kNativeAssetMainImage = @"main_image";
static NSString *const kNativeAssetSponsorImage = @"sponsor_image";
static NSString *const kNativeAssetDislike = @"dislike_button";
static NSString *const kNativeAssetMedia = @"media";

static NSString *kHBAdLoadingExtraNativeAdSizeKey = @"native_ad_size";
static NSString *kHBNativeAdSizeUsesPixelFlagKey = @"uses_pixel";

NSDictionary* parseUnityProperties(NSDictionary *properties) {
    NSMutableDictionary *result = NSMutableDictionary.dictionary;
    CGFloat scale = [properties[@"usesPixel"] boolValue] ? [UIScreen mainScreen].nativeScale : 1.0f;
    result[kParsedPropertiesFrameKey] = [NSString stringWithFormat:@"{{%@, %@}, {%@, %@}}", @([properties[@"x"] doubleValue] / scale), @([properties[@"y"] doubleValue] / scale), @([properties[@"width"] doubleValue] / scale), @([properties[@"height"] doubleValue] / scale)];
    result[kParsedPropertiesBackgroundColorKey] = properties[@"backgroundColor"];
    result[kParsedPropertiesTextColorKey] = properties[@"textColor"];
    result[kParsedPropertiesTextSizeKey] = properties[@"textSize"];
    
    return result;
}

NSDictionary* parseUnityMetrics(NSDictionary* metrics) {
    NSMutableDictionary *result = NSMutableDictionary.dictionary;
    NSDictionary *keysMap = @{@"appIcon":kNativeAssetIcon, @"mainImage":kNativeAssetMainImage, @"title":kNativeAssetTitle, @"desc":kNativeAssetText, @"adLogo":kNativeAssetSponsorImage, @"cta":kNativeAssetCta, @"dislike":kNativeAssetDislike};
    [keysMap enumerateKeysAndObjectsUsingBlock:^(id  _Nonnull key, id  _Nonnull obj, BOOL * _Nonnull stop) { result[keysMap[key]] = parseUnityProperties(metrics[key]); }];
    return result;
}

@interface HBUnityNativeAdView:HBNativeADView
@property(nonatomic, readonly) UILabel *advertiserLabel;
@property(nonatomic, readonly) UILabel *textLabel;
@property(nonatomic, readonly) UILabel *titleLabel;
@property(nonatomic, readonly) UILabel *ctaLabel;
@property(nonatomic, readonly) UILabel *ratingLabel;
@property(nonatomic, readonly) UIImageView *iconImageView;
@property(nonatomic, readonly) UIImageView *mainImageView;
@property(nonatomic, readonly) UIImageView *sponsorImageView;
@property(nonatomic, readonly) UIButton *dislikeButton;
-(void) configureMetrics:(NSDictionary<NSString*, NSString*>*)metrics;
@end

@implementation HBUnityNativeAdView
-(void) initSubviews {
    [super initSubviews];
    _advertiserLabel = [UILabel autolayoutLabelFont:[UIFont boldSystemFontOfSize:15.0f] textColor:[UIColor blackColor] textAlignment:NSTextAlignmentLeft];
    [self addSubview:_advertiserLabel];
    
    _titleLabel = [UILabel autolayoutLabelFont:[UIFont boldSystemFontOfSize:18.0f] textColor:[UIColor blackColor] textAlignment:NSTextAlignmentLeft];
    [self addSubview:_titleLabel];
    
    _textLabel = [UILabel autolayoutLabelFont:[UIFont systemFontOfSize:12.0f] textColor:[UIColor blackColor]];
    _textLabel.numberOfLines = 2;
    [self addSubview:_textLabel];
    
    _ctaLabel = [UILabel autolayoutLabelFont:[UIFont systemFontOfSize:15.0f] textColor:[UIColor blackColor]];
    _ctaLabel.textAlignment = NSTextAlignmentCenter;
    [self addSubview:_ctaLabel];
    
    _ratingLabel = [UILabel autolayoutLabelFont:[UIFont systemFontOfSize:15.0f] textColor:[UIColor blackColor]];
    [self addSubview:_ratingLabel];
    
    _iconImageView = [UIImageView autolayoutView];
    _iconImageView.layer.cornerRadius = 4.0f;
    _iconImageView.layer.masksToBounds = YES;
    _iconImageView.contentMode = UIViewContentModeScaleAspectFit;
    [self addSubview:_iconImageView];
    
    _mainImageView = [UIImageView autolayoutView];
    _mainImageView.contentMode = UIViewContentModeScaleAspectFit;
    [self addSubview:_mainImageView];
    
    _sponsorImageView = [UIImageView autolayoutView];
    _sponsorImageView.contentMode = UIViewContentModeScaleAspectFit;
    [self addSubview:_sponsorImageView];
    
    _dislikeButton = [UIButton buttonWithType:UIButtonTypeCustom];
    _dislikeButton.translatesAutoresizingMaskIntoConstraints = NO;
    UIImage *closeImg = [UIImage imageNamed:@"icon_webview_close" inBundle:[NSBundle bundleWithPath:[[NSBundle mainBundle] pathForResource:@"HyperBidSDK" ofType:@"bundle"]] compatibleWithTraitCollection:nil];
    [_dislikeButton setImage:closeImg forState:0];
    [self addSubview:_dislikeButton];
}

-(NSArray<UIView*>*)clickableViews {
    NSMutableArray *clickableViews = [NSMutableArray arrayWithObjects:_iconImageView, _ctaLabel, nil];
    if (self.mediaView != nil) { [clickableViews addObject:self.mediaView]; }
    return clickableViews;
}

-(void) configureMetrics:(NSDictionary *)metrics {
    NSDictionary<NSString*, UIView*> *views = @{kNativeAssetTitle:_titleLabel, kNativeAssetText:_textLabel, kNativeAssetCta:_ctaLabel, kNativeAssetRating:_ratingLabel, kNativeAssetAdvertiser:_advertiserLabel, kNativeAssetIcon:_iconImageView, kNativeAssetMainImage:_mainImageView, kNativeAssetSponsorImage:_sponsorImageView, kNativeAssetDislike:_dislikeButton};
    [views enumerateKeysAndObjectsUsingBlock:^(id  _Nonnull key, id  _Nonnull obj, BOOL * _Nonnull stop) {
        CGRect frame = CGRectFromString(metrics[key][kParsedPropertiesFrameKey]);
        [self addConstraintsWithVisualFormat:[NSString stringWithFormat:@"|-x-[%@(w)]", key] options:0 metrics:@{@"x":@(frame.origin.x), @"w":@(frame.size.width)} views:views];
        [self addConstraintsWithVisualFormat:[NSString stringWithFormat:@"V:|-y-[%@(h)]", key] options:0 metrics:@{@"y":@(frame.origin.y), @"h":@(frame.size.height)} views:views];
        if ([obj respondsToSelector:@selector(setBackgroundColor:)] && [metrics[key] containsObjectForKey:@"background_color"]) [obj setBackgroundColor:[metrics[key][@"background_color"] hasPrefix:@"#"] ? [UIColor colorWithHexString:metrics[key][@"background_color"]] : [UIColor clearColor]];
        if ([obj respondsToSelector:@selector(setTextColor:)] && [metrics[key] containsObjectForKey:@"text_color"]) [obj setTextColor:[UIColor colorWithHexString:metrics[key][@"text_color"]]];
        if ([obj respondsToSelector:@selector(setFont:)] && [metrics[key] containsObjectForKey:@"text_size"] && [metrics[key][@"text_size"] respondsToSelector:@selector(doubleValue)]) [obj setFont:[UIFont systemFontOfSize:[metrics[key][@"text_size"] doubleValue]]];
    }];
    if ([metrics containsObjectForKey:kNativeAssetMedia]) self.mediaView.frame = CGRectFromString(metrics[kNativeAssetMedia][kParsedPropertiesFrameKey]);
    else self.mediaView.frame = CGRectFromString(metrics[kNativeAssetMainImage][kParsedPropertiesFrameKey]);
}
@end

#define CS_HBNativeAdWrapper "HBNativeAdWrapper"
@interface HBNativeAdWrapper()
@property(nonatomic, readonly) NSMutableDictionary<NSString*, UIView*> *viewsStorage;
@end
@implementation HBNativeAdWrapper
+(instancetype)sharedInstance {
    static HBNativeAdWrapper *sharedInstance = nil;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        sharedInstance = [[HBNativeAdWrapper alloc] init];
    });
    return sharedInstance;
}

-(instancetype) init {
    self = [super init];
    if (self != nil) _viewsStorage = [NSMutableDictionary<NSString*, UIView*> dictionary];
    return self;
}

- (id)selWrapperClassWithDict:(NSDictionary *)dict callback:(void(*)(const char*, const char*))callback {
    NSString *selector = dict[@"selector"];
    NSArray<NSString*>* arguments = dict[@"arguments"];
    NSString *firstObject = @"";
    NSString *secondObject = @"";
    NSString *lastObject = @"";
    if (![HBUnityUtilities isEmpty:arguments]) {
        for (int i = 0; i < arguments.count; i++) {
            if (i == 0) { firstObject = arguments[i]; }
            else if (i == 1) { secondObject = arguments[i]; }
            else { lastObject = arguments[i]; }
        }
    }
    
    if ([selector isEqualToString:@"loadNativeAdWithPlacementID:customDataJSONString:callback:"]) {
        [self loadNativeAdWithPlacementID:firstObject customDataJSONString:secondObject callback:callback];
    } else if ([selector isEqualToString:@"isNativeAdReadyForPlacementID:"]) {
        return [NSNumber numberWithBool:[self isNativeAdReadyForPlacementID:firstObject]];
    } else if ([selector isEqualToString:@"showNativeAdWithPlacementID:metricsJSONString:extraJsonString:"]) {
        [self showNativeAdWithPlacementID:firstObject metricsJSONString:secondObject extraJsonString:lastObject];
    } else if ([selector isEqualToString:@"removeNativeAdViewWithPlacementID:"]) {
        [self removeNativeAdViewWithPlacementID:firstObject];
    } else if ([selector isEqualToString:@"checkAdStatus:"]) {
        return [self checkAdStatus:firstObject];
    }  else if ([selector isEqualToString:@"clearCache"]) {
        [self clearCache];
    }
    return nil;
}

-(void) loadNativeAdWithPlacementID:(NSString*)placementID customDataJSONString:(NSString*)customDataJSONString callback:(void(*)(const char*, const char *))callback {
    [self setCallBack:callback forKey:placementID];
    NSMutableDictionary *extra = [NSMutableDictionary dictionaryWithDictionary:@{kHBExtraInfoNativeAdTypeKey:@(HBGDTNativeAdTypeSelfRendering), kHBExtraNativeImageSizeKey:kHBExtraNativeImageSize690_388}];
    if ([customDataJSONString isKindOfClass:[NSString class]] && [customDataJSONString length] > 0) {
        NSDictionary *extraDict = [NSJSONSerialization JSONObjectWithData:[customDataJSONString dataUsingEncoding:NSUTF8StringEncoding] options:NSJSONReadingAllowFragments error:nil];
        NSLog(@"extraDict = %@", extraDict);
        CGFloat scale = [extraDict[kHBNativeAdSizeUsesPixelFlagKey] boolValue] ? [UIScreen mainScreen].nativeScale : 1.0f;
        if ([extraDict[kHBAdLoadingExtraNativeAdSizeKey] isKindOfClass:[NSString class]] && [[extraDict[kHBAdLoadingExtraNativeAdSizeKey] componentsSeparatedByString:@"x"] count] == 2) {
            NSArray<NSString*>* com = [extraDict[kHBAdLoadingExtraNativeAdSizeKey] componentsSeparatedByString:@"x"];
            extra[kHBExtraInfoNativeAdSizeKey] = [NSValue valueWithCGSize:CGSizeMake([com[0] doubleValue] / scale, [com[1] doubleValue] / scale)];
        }
    }
    NSLog(@"extra = %@", extra);
    [[HBAdManager sharedManager] loadAd:placementID extra:extra delegate:self];
}

-(BOOL) isNativeAdReadyForPlacementID:(NSString*)placementID {
    return [[HBAdManager sharedManager] isReadyNativeAdWithPlacementID:placementID];
}

-(NSString*) checkAdStatus:(NSString *)placementID {
    HBCheckLoadModel *checkLoadModel = [[HBAdManager sharedManager] checkNativeLoadStatusForPlacementID:placementID];
    NSMutableDictionary *statusDict = [NSMutableDictionary dictionary];
    statusDict[@"isLoading"] = @(checkLoadModel.isLoading);
    statusDict[@"isReady"] = @(checkLoadModel.isReady);
    statusDict[@"adInfo"] = checkLoadModel.adOfferInfo;
    NSLog(@"HBNativeAdWrapper::statusDict = %@", statusDict);
    return statusDict.jsonString;
}

-(void) showNativeAdWithPlacementID:(NSString*)placementID metricsJSONString:(NSString*)metricsJSONString extraJsonString:(NSString*)extraJsonString {
    if ([self isNativeAdReadyForPlacementID:placementID]) {
        NSDictionary *metrics = [NSJSONSerialization JSONObjectWithData:[metricsJSONString dataUsingEncoding:NSUTF8StringEncoding] options:NSJSONReadingAllowFragments error:nil];
        NSDictionary *extraDict = ([extraJsonString isKindOfClass:[NSString class]] && [extraJsonString dataUsingEncoding:NSUTF8StringEncoding] != nil) ? [NSJSONSerialization JSONObjectWithData:[extraJsonString dataUsingEncoding:NSUTF8StringEncoding] options:NSJSONReadingAllowFragments error:nil] : nil;
        
        NSDictionary *parsedMetrics = parseUnityMetrics(metrics);
        NSLog(@"metrics = %@, parsedMetrics = %@", metrics, parsedMetrics);
        
        UIButton *button = [UIButton buttonWithType:UIButtonTypeCustom];
        [button addTarget:self action:@selector(noop) forControlEvents:UIControlEventTouchUpInside];
        button.frame = CGRectFromString(parseUnityProperties(metrics[@"parent"])[kParsedPropertiesFrameKey]);
        _viewsStorage[placementID] = button;
        
        HBNativeADConfiguration *configuration = [HBNativeADConfiguration new];
        configuration.ADFrame = button.bounds;
        configuration.renderingViewClass = [HBUnityNativeAdView class];
        configuration.delegate = self;
        configuration.rootViewController = [UIApplication sharedApplication].delegate.window.rootViewController;
        HBUnityNativeAdView *adview = [[HBAdManager sharedManager] retriveAdViewWithPlacementID:placementID configuration:configuration scene:extraDict[kHBUnityUtilitiesAdShowingExtraScenarioKey]];
        adview.ctaLabel.hidden = [adview.nativeAd.ctaText length] == 0;
        if (adview != nil) {
            if ([adview respondsToSelector:@selector(configureMetrics:)]) {
                [adview configureMetrics:parsedMetrics];
            } else {
                [adview.subviews enumerateObjectsUsingBlock:^(__kindof UIView * _Nonnull obj, NSUInteger idx, BOOL * _Nonnull stop) {
                    if ([obj isKindOfClass:[HBUnityNativeAdView class]]) {
                        [(HBUnityNativeAdView*)obj configureMetrics:parsedMetrics];
                        *stop = YES;
                    }
                }];
            }
            [button addSubview:adview];
            
//            adview.mediaView.frame = adview.mainImageView.frame;
            adview.mediaView.frame = CGRectFromString(parsedMetrics[kNativeAssetMainImage][kParsedPropertiesFrameKey]);
            [adview bringSubviewToFront:adview.mediaView];
//            adview.mainImageView.layer.borderColor = [UIColor redColor].CGColor;
//            adview.mainImageView.layer.borderWidth = 1.0f;
            
            UILabel *label = [[UILabel alloc] initWithFrame:CGRectMake(2.0f, 2.0f, 30.0f, 16.0f)];
            label.font = [UIFont systemFontOfSize:15.0f];
            label.backgroundColor = [[UIColor blackColor] colorWithAlphaComponent:.4f];
            label.textColor = [UIColor whiteColor];
            label.text = @"AD";
            label.textAlignment = NSTextAlignmentCenter;
            label.autoresizingMask = UIViewAutoresizingFlexibleTopMargin | UIViewAutoresizingFlexibleLeftMargin;
            [adview addSubview:label];
            
            [[UIApplication sharedApplication].keyWindow.rootViewController.view addSubview:button];
        }
    }
}

-(void) noop {
    
}

-(void) removeNativeAdViewWithPlacementID:(NSString*)placementID {
    
    [_viewsStorage[placementID] removeFromSuperview];
}

-(void) clearCache {
    [[HBAdManager sharedManager] clearCache];
}

-(NSString*) scriptWrapperClass {
    return @"HBNativeAdWrapper";
}
#pragma mark - delegate
-(void) didFinishLoadingADWithPlacementID:(NSString *)placementID {
    [self invokeCallback:@"OnNativeAdLoaded" placementID:placementID error:nil extra:nil];
}

-(void) didFailToLoadADWithPlacementID:(NSString*)placementID error:(NSError*)error {
    [self invokeCallback:@"OnNativeAdLoadingFailure" placementID:placementID error:error extra:nil];
}

-(void) didShowNativeAdInAdView:(HBNativeADView*)adView placementID:(NSString*)placementID extra:(NSDictionary *)extra {
    [self invokeCallback:@"OnNaitveAdShow" placementID:placementID error:nil extra:extra];
}
    
-(void) didClickNativeAdInAdView:(HBNativeADView*)adView placementID:(NSString*)placementID extra:(NSDictionary *)extra {
    //Drop ad view
    [self invokeCallback:@"OnNativeAdClick" placementID:placementID error:nil extra:extra];
}

-(void) didTapCloseButtonInAdView:(HBNativeADView*)adView placementID:(NSString*)placementID extra:(NSDictionary *)extra {
    [self invokeCallback:@"OnNativeAdCloseButtonClick" placementID:placementID error:nil extra:extra];
}
    
-(void) didStartPlayingVideoInAdView:(HBNativeADView*)adView placementID:(NSString*)placementID extra:(NSDictionary *)extra {
    //Drop ad view
    [self invokeCallback:@"OnNativeAdVideoStart" placementID:placementID error:nil extra:extra];
}
    
-(void) didEndPlayingVideoInAdView:(HBNativeADView*)adView placementID:(NSString*)placementID extra:(NSDictionary *)extra {
    //Drop ad view
    [self invokeCallback:@"OnNativeAdVideoEnd" placementID:placementID error:nil extra:extra];
}
@end
