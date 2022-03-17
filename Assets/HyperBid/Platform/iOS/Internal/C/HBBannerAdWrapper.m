//
//  HBBannerAdWrapper.m
//  UnityContainer
//
//  Created by Martin Lau on 2019/1/8.
//  Copyright © 2019 Martin Lau. All rights reserved.
//

#import "HBBannerAdWrapper.h"
#import <HyperBidBanner/HyperBidBanner.h>
#import "HBUnityUtilities.h"
//5.6.6版本以上支持 admob 自适应banner （用到时再import该头文件）
//#import <GoogleMobileAds/GoogleMobileAds.h>

@interface HBBannerAdWrapper()<HBBannerDelegate>
@property(nonatomic, readonly) NSMutableDictionary<NSString*, HBBannerView*> *bannerViewStorage;
@property(nonatomic, readonly) BOOL interstitialOrRVBeingShown;
@end

static NSString *kHBBannerSizeUsesPixelFlagKey = @"uses_pixel";
static NSString *kHBBannerAdLoadingExtraInlineAdaptiveWidthKey = @"inline_adaptive_width";
static NSString *kHBBannerAdLoadingExtraInlineAdaptiveOrientationKey = @"inline_adaptive_orientation";

@implementation HBBannerAdWrapper
+(instancetype)sharedInstance {
    static HBBannerAdWrapper *sharedInstance = nil;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        sharedInstance = [[HBBannerAdWrapper alloc] init];
    });
    return sharedInstance;
}

-(instancetype) init {
    self = [super init];
    if (self != nil) {
        _bannerViewStorage = [NSMutableDictionary<NSString*, HBBannerView*> dictionary];
    }
    return self;
}

-(NSString*) scriptWrapperClass {
    return @"HBBannerAdWrapper";
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
    
    if ([selector isEqualToString:@"loadBannerAdWithPlacementID:customDataJSONString:callback:"]) {
        [self loadBannerAdWithPlacementID:firstObject customDataJSONString:secondObject callback:callback];
    } else if ([selector isEqualToString:@"showBannerAdWithPlacementID:rect:extraJsonString:"]) {
        [self showBannerAdWithPlacementID:firstObject rect:secondObject extraJsonString:lastObject];
    } else if ([selector isEqualToString:@"removeBannerAdWithPlacementID:"]) {
        [self removeBannerAdWithPlacementID:firstObject];
    } else if ([selector isEqualToString:@"showBannerAdWithPlacementID:"]) {
        [self showBannerAdWithPlacementID:firstObject];
    } else if ([selector isEqualToString:@"hideBannerAdWithPlacementID:"]) {
        [self hideBannerAdWithPlacementID:firstObject];
    } else if ([selector isEqualToString:@"checkAdStatus:"]) {
        return [self checkAdStatus:firstObject];
    }   else if ([selector isEqualToString:@"clearCache"]) {
        [self clearCache];
    }   else if ([selector isEqualToString:@"getValidAdCaches:"]) {
        return [self getValidAdCaches:firstObject];
    }
    return nil;
}

-(void) loadBannerAdWithPlacementID:(NSString*)placementID customDataJSONString:(NSString*)customDataJSONString callback:(void(*)(const char*, const char*))callback {
    [self setCallBack:callback forKey:placementID];
    NSMutableDictionary *extra = [NSMutableDictionary dictionary];
    if ([customDataJSONString isKindOfClass:[NSString class]] && [customDataJSONString length] > 0) {
        NSDictionary *extraDict = [NSJSONSerialization JSONObjectWithData:[customDataJSONString dataUsingEncoding:NSUTF8StringEncoding] options:NSJSONReadingAllowFragments error:nil];
        NSLog(@"extraDict = %@", extraDict);
        CGFloat scale = [extraDict[kHBBannerSizeUsesPixelFlagKey] boolValue] ? [UIScreen mainScreen].nativeScale : 1.0f;
        if ([extraDict[kHBAdLoadingExtraBannerAdSizeKey] isKindOfClass:[NSString class]] && [[extraDict[kHBAdLoadingExtraBannerAdSizeKey] componentsSeparatedByString:@"x"] count] == 2) {
            NSArray<NSString*>* com = [extraDict[kHBAdLoadingExtraBannerAdSizeKey] componentsSeparatedByString:@"x"];

            extra[kHBAdLoadingExtraBannerAdSizeKey] = [NSValue valueWithCGSize:CGSizeMake([com[0] doubleValue] / scale, [com[1] doubleValue] / scale)];
        }
        
//        // admob 自适应banner，5.6.6版本以上支持
//        if (extraDict[kHBBannerAdLoadingExtraInlineAdaptiveWidthKey] != nil && extraDict[kHBBannerAdLoadingExtraInlineAdaptiveOrientationKey] != nil) {
//            //GADCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth 自适应
//            //GADPortraitAnchoredAdaptiveBannerAdSizeWithWidth 竖屏
//            //GADLandscapeAnchoredAdaptiveBannerAdSizeWithWidth 横屏
//            CGFloat admobBannerWidth = [extraDict[kHBBannerAdLoadingExtraInlineAdaptiveWidthKey] doubleValue];
//            GADAdSize admobSize;
//            if ([extraDict[kHBBannerAdLoadingExtraInlineAdaptiveOrientationKey] integerValue] == 1) {
//                admobSize = GADPortraitAnchoredAdaptiveBannerAdSizeWithWidth(admobBannerWidth);
//            } else if ([extraDict[kHBBannerAdLoadingExtraInlineAdaptiveOrientationKey] integerValue] == 2) {
//                admobSize = GADLandscapeAnchoredAdaptiveBannerAdSizeWithWidth(admobBannerWidth);
//            } else {
//                admobSize = GADCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(admobBannerWidth);
//            }
//
//            extra[kHBAdLoadingExtraAdmobBannerSizeKey] = [NSValue valueWithCGSize:admobSize.size];
//            extra[kHBAdLoadingExtraAdmobAdSizeFlagsKey] = @(admobSize.flags);
//        }
    }
    if (extra[kHBAdLoadingExtraBannerAdSizeKey] == nil) {
        extra[kHBAdLoadingExtraBannerAdSizeKey] = [NSValue valueWithCGSize:CGSizeMake(320.0f, 50.0f)];
    }
    NSLog(@"extra = %@", extra);
    [[HBAdManager sharedManager] loadAd:placementID extra:extra delegate:self];
}

-(NSString*) checkAdStatus:(NSString *)placementID {
    HBCheckLoadModel *checkLoadModel = [[HBAdManager sharedManager] checkBannerLoadStatusForPlacementID:placementID];
    NSMutableDictionary *statusDict = [NSMutableDictionary dictionary];
    statusDict[@"isLoading"] = @(checkLoadModel.isLoading);
    statusDict[@"isReady"] = @(checkLoadModel.isReady);
    statusDict[@"adInfo"] = checkLoadModel.adOfferInfo;
    NSLog(@"HBBannerAdWrapper::statusDict = %@", statusDict);
    return statusDict.jsonString;
}

-(NSString*) getValidAdCaches:(NSString *)placementID {
    NSArray *array = [[HBAdManager sharedManager] getBannerValidAdsForPlacementID:placementID];
    NSLog(@"HBNativeAdWrapper::array = %@", array);
    return array.jsonString;
}

UIEdgeInsets SafeAreaInsets_HBUnityBanner() {
    return ([[UIApplication sharedApplication].keyWindow respondsToSelector:@selector(safeAreaInsets)] ? [UIApplication sharedApplication].keyWindow.safeAreaInsets : UIEdgeInsetsZero);
}

-(void) showBannerAdWithPlacementID:(NSString*)placementID rect:(NSString*)rect extraJsonString:(NSString*)extraJsonString {
    dispatch_async(dispatch_get_main_queue(), ^{
        if ([rect isKindOfClass:[NSString class]] && [rect dataUsingEncoding:NSUTF8StringEncoding] != nil) {
            NSDictionary *extraDict = ([extraJsonString isKindOfClass:[NSString class]] && [extraJsonString dataUsingEncoding:NSUTF8StringEncoding] != nil) ? [NSJSONSerialization JSONObjectWithData:[extraJsonString dataUsingEncoding:NSUTF8StringEncoding] options:NSJSONReadingAllowFragments error:nil] : nil;
            
            NSDictionary *rectDict = [NSJSONSerialization JSONObjectWithData:[rect dataUsingEncoding:NSUTF8StringEncoding] options:NSJSONReadingAllowFragments error:nil];
            NSLog(@"rectDict = %@", rectDict);
            CGFloat scale = [rectDict[kHBBannerSizeUsesPixelFlagKey] boolValue] ? [UIScreen mainScreen].nativeScale : 1.0f;
            HBBannerView *bannerView = [[HBAdManager sharedManager] getBannerViewWithPlacementID:placementID scene:extraDict[kHBUnityUtilitiesAdShowingExtraScenarioKey]];
            bannerView.delegate = self;
            UIButton *bannerCointainer = [UIButton buttonWithType:UIButtonTypeCustom];
            [bannerCointainer addTarget:self action:@selector(noop) forControlEvents:UIControlEventTouchUpInside];
            
            NSString *position = rectDict[@"position"];
            CGSize totalSize = [UIApplication sharedApplication].keyWindow.rootViewController.view.bounds.size;
            UIEdgeInsets safeAreaInsets = SafeAreaInsets_HBUnityBanner();
            if ([@"top" isEqualToString:position]) {
                bannerCointainer.frame = CGRectMake((totalSize.width - CGRectGetWidth(bannerView.bounds)) / 2.0f, safeAreaInsets.top , CGRectGetWidth(bannerView.bounds), CGRectGetHeight(bannerView.bounds));
            } else if ([@"bottom" isEqualToString:position]) {
                bannerCointainer.frame = CGRectMake((totalSize.width - CGRectGetWidth(bannerView.bounds)) / 2.0f, totalSize.height - safeAreaInsets.bottom - CGRectGetHeight(bannerView.bounds) , CGRectGetWidth(bannerView.bounds), CGRectGetHeight(bannerView.bounds));
            } else {
                bannerCointainer.frame = CGRectMake([rectDict[@"x"] doubleValue] / scale, [rectDict[@"y"] doubleValue] / scale, [rectDict[@"width"] doubleValue] / scale, [rectDict[@"height"] doubleValue] / scale);
            }
            
            bannerView.frame = bannerCointainer.bounds;
            [bannerCointainer addSubview:bannerView];
            
//            bannerCointainer.layer.borderColor = [UIColor redColor].CGColor;
//            bannerCointainer.layer.borderWidth = .5f;
            [[UIApplication sharedApplication].keyWindow.rootViewController.view addSubview:bannerCointainer];
            self->_bannerViewStorage[placementID] = bannerCointainer;
        }
    });
}

-(void) noop {
    
}

-(void) removeBannerAdWithPlacementID:(NSString*)placementID {
    dispatch_async(dispatch_get_main_queue(), ^{
        [self->_bannerViewStorage[placementID] removeFromSuperview];
        [self->_bannerViewStorage removeObjectForKey:placementID];
    });
}

-(void) showBannerAdWithPlacementID:(NSString*)placementID {
    dispatch_async(dispatch_get_main_queue(), ^{
        HBBannerView *bannerView = self->_bannerViewStorage[placementID];
        if (bannerView.superview != nil && !_interstitialOrRVBeingShown) { bannerView.hidden = NO; }
    });
}

-(void) hideBannerAdWithPlacementID:(NSString*)placementID {
    dispatch_async(dispatch_get_main_queue(), ^{
        HBBannerView *bannerView = self->_bannerViewStorage[placementID];
        if (bannerView.superview != nil) { bannerView.hidden = YES; }
    });
}

-(void) clearCache {
    [[HBAdManager sharedManager] clearCache];
}

#pragma mark - banner delegate method(s)
-(void) didFinishLoadingADWithPlacementID:(NSString *)placementID {
    [self invokeCallback:@"OnBannerAdLoad" placementID:placementID error:nil extra:nil];
}

-(void) didFailToLoadADWithPlacementID:(NSString*)placementID error:(NSError*)error {
    error = error != nil ? error : [NSError errorWithDomain:@"com.hyperbid.Unity3DPackage" code:100001 userInfo:@{NSLocalizedDescriptionKey:@"AT has failed to load ad", NSLocalizedFailureReasonErrorKey:@"AT has failed to load ad"}];
    [self invokeCallback:@"OnBannerAdLoadFail" placementID:placementID error:error extra:nil];
}
// ad
- (void)didStartLoadingADSourceWithPlacementID:(NSString *)placementID extra:(NSDictionary*)extra{
    [self invokeCallback:@"startLoadingADSource" placementID:placementID error:nil extra:extra];
}

- (void)didFinishLoadingADSourceWithPlacementID:(NSString *)placementID extra:(NSDictionary*)extra{
    [self invokeCallback:@"finishLoadingADSource" placementID:placementID error:nil extra:extra];
}

- (void)didFailToLoadADSourceWithPlacementID:(NSString*)placementID extra:(NSDictionary*)extra error:(NSError*)error{
    [self invokeCallback:@"failToLoadADSource" placementID:placementID error:error extra:extra];
}

// bidding
- (void)didStartBiddingADSourceWithPlacementID:(NSString *)placementID extra:(NSDictionary*)extra{
    [self invokeCallback:@"startBiddingADSource" placementID:placementID error:nil extra:extra];
}

- (void)didFinishBiddingADSourceWithPlacementID:(NSString *)placementID extra:(NSDictionary*)extra{
    [self invokeCallback:@"finishBiddingADSource" placementID:placementID error:nil extra:extra];
}

- (void)didFailBiddingADSourceWithPlacementID:(NSString*)placementID extra:(NSDictionary*)extra error:(NSError*)error{
    [self invokeCallback:@"failBiddingADSource" placementID:placementID error:error extra:extra];
}

-(void) bannerView:(HBBannerView *)bannerView didShowAdWithPlacementID:(NSString *)placementID extra:(NSDictionary *)extra {
    [self invokeCallback:@"OnBannerAdImpress" placementID:placementID error:nil extra:extra];
}

-(void) bannerView:(HBBannerView*)bannerView didClickWithPlacementID:(NSString*)placementID extra:(NSDictionary *)extra {
    [self invokeCallback:@"OnBannerAdClick" placementID:placementID error:nil extra:extra];
}

-(void) bannerView:(HBBannerView *)bannerView didTapCloseButtonWithPlacementID:(NSString *)placementID extra:(NSDictionary *)extra {
    [self invokeCallback:@"OnBannerAdCloseButtonTapped" placementID:placementID error:nil extra:extra];
}

-(void) bannerView:(HBBannerView*)bannerView didCloseWithPlacementID:(NSString*)placementID extra:(NSDictionary *)extra {
    [self invokeCallback:@"OnBannerAdClose" placementID:placementID error:nil extra:extra];
}

-(void) bannerView:(HBBannerView *)bannerView didAutoRefreshWithPlacement:(NSString *)placementID extra:(NSDictionary *)extra {
    [self invokeCallback:@"OnBannerAdAutoRefresh" placementID:placementID error:nil extra:extra];
}

-(void) bannerView:(HBBannerView *)bannerView failedToAutoRefreshWithPlacementID:(NSString *)placementID error:(NSError *)error {
    error = error != nil ? error : [NSError errorWithDomain:@"com.hyperbid.Unity3DPackage" code:100001 userInfo:@{NSLocalizedDescriptionKey:@"AT has failed to refresh ad", NSLocalizedFailureReasonErrorKey:@"AT has failed to refresh ad"}];
    [self invokeCallback:@"OnBannerAdAutoRefreshFail" placementID:placementID error:error extra:nil];
}

@end
