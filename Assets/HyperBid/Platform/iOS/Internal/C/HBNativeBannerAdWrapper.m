//
//  HBNativeBannerAdWrapper.m
//  UnityContainer
//
//  Created by Martin Lau on 2019/4/23.
//  Copyright © 2019 Martin Lau. All rights reserved.
//

#import "HBNativeBannerAdWrapper.h"
#import <HyperBidNative/HBNativeBannerWrapper.h>
#import "HBUnityUtilities.h"
#import <objc/runtime.h>
@interface UIViewController(ATUtilities)
-(void)AT_presentViewController:(UIViewController *)viewControllerToPresent animated:(BOOL)flag completion:(void (^)(void))completion;
-(void) AT_dismissViewControllerAnimated:(BOOL)flag completion:(void (^)(void))completion;
@end

@interface HBNativeBannerAdWrapper()<HBNativeBannerDelegate>
@property(nonatomic, readonly) NSMutableDictionary<NSString*, UIView*> *nativeBannerAdViews;
@end

static NSString *const kATSharedCallbackKey = @"placement_id_placement_holder";
@implementation HBNativeBannerAdWrapper
+(void) swizzleMethodWithSelector:(SEL)originalSel swizzledMethodSelector:(SEL)swizzledMethodSel inClass:(Class)inClass {
    if (originalSel != NULL && swizzledMethodSel != NULL && inClass != nil) {
        Method originalMethod = class_getInstanceMethod(inClass, originalSel);
        Method swizzledMethod = class_getInstanceMethod(inClass, swizzledMethodSel);
        
        BOOL didAddMethod = class_addMethod(inClass, originalSel, method_getImplementation(swizzledMethod), method_getTypeEncoding(swizzledMethod));
        
        if (didAddMethod) {
            class_replaceMethod(inClass, swizzledMethodSel, method_getImplementation(originalMethod), method_getTypeEncoding(originalMethod));
        } else {
            method_exchangeImplementations(originalMethod, swizzledMethod);
        }
    }
}

+(instancetype)sharedInstance {
    static HBNativeBannerAdWrapper *sharedInstance = nil;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        sharedInstance = [[HBNativeBannerAdWrapper alloc] init];
    });
    return sharedInstance;
}

-(instancetype) init {
    self = [super init];
    if (self != nil) {
        _nativeBannerAdViews = [NSMutableDictionary dictionary];
        [HBNativeBannerAdWrapper swizzleMethodWithSelector:@selector(presentViewController:animated:completion:) swizzledMethodSelector:@selector(AT_presentViewController:animated:completion:) inClass:[UIViewController class]];
        [HBNativeBannerAdWrapper swizzleMethodWithSelector:@selector(dismissViewControllerAnimated:completion:) swizzledMethodSelector:@selector(AT_dismissViewControllerAnimated:completion:) inClass:[UIViewController class]];
    }
    return self;
}

-(NSString*) scriptWrapperClass {
    return @"HBNativeBannerAdWrapper";
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
    
    if ([selector isEqualToString:@"loadNativeBannerAdWithPlacementID:customDataJSONString:callback:"]) {
        [self loadNativeBannerAdWithPlacementID:firstObject customDataJSONString:secondObject callback:callback];
    } else if ([selector isEqualToString:@"isNativeBannerAdReadyForPlacementID:"]) {
        return [NSNumber numberWithBool:[self isNativeBannerAdReadyForPlacementID:firstObject]];
    } else if ([selector isEqualToString:@"showNativeBannerAdWithPlacementID:rect:extra:"]) {
        [self showNativeBannerAdWithPlacementID:firstObject rect:secondObject extra:lastObject];
    } else if ([selector isEqualToString:@"removeNativeBannerAdWithPlacementID:"]) {
        [self removeNativeBannerAdWithPlacementID:firstObject];
    } else if ([selector isEqualToString:@"showNativeBannerAdWithPlacementID:"]) {
        [self showNativeBannerAdWithPlacementID:firstObject];
    } else if ([selector isEqualToString:@"hideNativeBannerAdWithPlacementID:"]) {
        [self hideNativeBannerAdWithPlacementID:firstObject];
    }
    return nil;
}

-(void) noop {
    
}

-(void) showNativeBannerAdWithPlacementID:(NSString*)placementID rect:(NSString*)rect extra:(NSString*)extraStr {
    dispatch_async(dispatch_get_main_queue(), ^{
        if ([rect isKindOfClass:[NSString class]] && [rect dataUsingEncoding:NSUTF8StringEncoding] != nil) {
            NSDictionary *rectDict = [NSJSONSerialization JSONObjectWithData:[rect dataUsingEncoding:NSUTF8StringEncoding] options:NSJSONReadingAllowFragments error:nil];
            NSMutableDictionary *extra = [NSMutableDictionary dictionaryWithDictionary:@{kHBExtraInfoNativeAdTypeKey:@(HBGDTNativeAdTypeSelfRendering), kHBNativeBannerAdShowingExtraAdSizeKey:[NSValue valueWithCGSize:CGSizeMake([rectDict[@"width"] doubleValue], [rectDict[@"height"] doubleValue])], kHBExtraNativeImageSizeKey:kHBExtraNativeImageSize690_388, kHBNativeBannerAdShowingExtraBackgroundColorKey:[UIColor whiteColor]}];
            if ([extraStr isKindOfClass:[NSString class]] && [extraStr dataUsingEncoding:NSUTF8StringEncoding] != nil) {
                NSDictionary *extraDict = [NSJSONSerialization JSONObjectWithData:[extraStr dataUsingEncoding:NSUTF8StringEncoding] options:NSJSONReadingAllowFragments error:nil];
                if ([extraDict[kHBNativeBannerAdShowingExtraBackgroundColorKey] isKindOfClass:[NSString class]]) {
                    extra[kHBNativeBannerAdShowingExtraBackgroundColorKey] = [UIColor colorWithHexString:extraDict[kHBNativeBannerAdShowingExtraBackgroundColorKey]];
                }
                if ([extraDict[kHBNativeBannerAdShowingExtraAutorefreshIntervalKey] respondsToSelector:@selector(doubleValue)]) {
                    extra[kHBNativeBannerAdShowingExtraAutorefreshIntervalKey] = @([extraDict[kHBNativeBannerAdShowingExtraAutorefreshIntervalKey] doubleValue]);
                }
                if ([extraDict[kHBNativeBannerAdShowingExtraHideCloseButtonFlagKey] respondsToSelector:@selector(boolValue)]) {
                    extra[kHBNativeBannerAdShowingExtraHideCloseButtonFlagKey] = @([extraDict[kHBNativeBannerAdShowingExtraHideCloseButtonFlagKey] boolValue]);
                }
                if ([extraDict[kHBNativeBannerAdShowingExtraCTAButtonBackgroundColorKey] isKindOfClass:[NSString class]]) {
                    extra[kHBNativeBannerAdShowingExtraCTAButtonBackgroundColorKey] = [UIColor colorWithHexString:extraDict[kHBNativeBannerAdShowingExtraCTAButtonBackgroundColorKey]];
                }
                if ([extraDict[kHBNativeBannerAdShowingExtraCTAButtonTitleFontKey] respondsToSelector:@selector(doubleValue)]) {
                    extra[kHBNativeBannerAdShowingExtraCTAButtonTitleFontKey] = @([extraDict[kHBNativeBannerAdShowingExtraCTAButtonTitleFontKey] doubleValue]);
                }
                if ([extraDict[kHBNativeBannerAdShowingExtraCTAButtonTitleColorKey] isKindOfClass:[NSString class]]) {
                    extra[kHBNativeBannerAdShowingExtraCTAButtonTitleColorKey] = [UIColor colorWithHexString:extraDict[kHBNativeBannerAdShowingExtraCTAButtonTitleColorKey]];
                }
                if ([extraDict[kHBNativeBannerAdShowingExtraTitleFontKey] respondsToSelector:@selector(doubleValue)]) {
                    extra[kHBNativeBannerAdShowingExtraTitleFontKey] = @([extraDict[kHBNativeBannerAdShowingExtraTitleFontKey] doubleValue]);
                }
                if ([extraDict[kHBNativeBannerAdShowingExtraTitleColorKey] isKindOfClass:[NSString class]]) {
                    extra[kHBNativeBannerAdShowingExtraTitleColorKey] = [UIColor colorWithHexString:extraDict[kHBNativeBannerAdShowingExtraTitleColorKey]];
                }
                if ([extraDict[kHBNativeBannerAdShowingExtraTextFontKey] respondsToSelector:@selector(doubleValue)]) {
                    extra[kHBNativeBannerAdShowingExtraTextFontKey] = @([extraDict[kHBNativeBannerAdShowingExtraTextFontKey] doubleValue]);
                }
                if ([extraDict[kHBNativeBannerAdShowingExtraTextColorKey] isKindOfClass:[NSString class]]) {
                    extra[kHBNativeBannerAdShowingExtraTextColorKey] = [UIColor colorWithHexString:extraDict[kHBNativeBannerAdShowingExtraTextColorKey]];
                }
                if ([extraDict[kHBNativeBannerAdShowingExtraAdvertiserTextFontKey] respondsToSelector:@selector(doubleValue)]) {
                    extra[kHBNativeBannerAdShowingExtraAdvertiserTextFontKey] = @([extraDict[kHBNativeBannerAdShowingExtraAdvertiserTextFontKey] doubleValue]);
                }
                if ([extraDict[kHBNativeBannerAdShowingExtraAdvertiserTextColorKey] isKindOfClass:[NSString class]]) {
                    extra[kHBNativeBannerAdShowingExtraAdvertiserTextColorKey] = [UIColor colorWithHexString:extraDict[kHBNativeBannerAdShowingExtraAdvertiserTextColorKey]];
                }
            }
            HBNativeBannerView *bannerView = [HBNativeBannerWrapper retrieveNativeBannerAdViewWithPlacementID:placementID extra:extra delegate:self];
            UIButton *bannerCointainer = [UIButton buttonWithType:UIButtonTypeCustom];
            [bannerCointainer addTarget:self action:@selector(noop) forControlEvents:UIControlEventTouchUpInside];
            bannerCointainer.frame = CGRectMake([rectDict[@"x"] doubleValue], [rectDict[@"y"] doubleValue], [rectDict[@"width"] doubleValue], [rectDict[@"height"] doubleValue]);
            bannerView.frame = bannerCointainer.bounds;
            [bannerCointainer addSubview:bannerView];
            [[UIApplication sharedApplication].keyWindow.rootViewController.view addSubview:bannerCointainer];
            self->_nativeBannerAdViews[placementID] = bannerCointainer;
        }
    });
}

-(void) removeNativeBannerAdWithPlacementID:(NSString*)placementID {
    dispatch_async(dispatch_get_main_queue(), ^{
        [self->_nativeBannerAdViews[placementID] removeFromSuperview];
        [self->_nativeBannerAdViews removeObjectForKey:placementID];
    });
}

-(void) showNativeBannerAdWithPlacementID:(NSString*)placementID {
    dispatch_async(dispatch_get_main_queue(), ^{
        self->_nativeBannerAdViews[placementID].hidden = NO;
    });
}

-(void) hideNativeBannerAdWithPlacementID:(NSString*)placementID {
    dispatch_async(dispatch_get_main_queue(), ^{
        self->_nativeBannerAdViews[placementID].hidden = YES;
    });
}

-(void) loadNativeBannerAdWithPlacementID:(NSString*)placementID customDataJSONString:(NSString*)customDataJSONString callback:(void(*)(const char*, const char*))callback {
    [self setCallBack:callback forKey:kATSharedCallbackKey];
    [self setCallBack:callback forKey:placementID];
    [HBNativeBannerWrapper loadNativeBannerAdWithPlacementID:placementID extra:nil customData:([customDataJSONString isKindOfClass:[NSString class]] && [customDataJSONString dataUsingEncoding:NSUTF8StringEncoding] != nil) ? [NSJSONSerialization JSONObjectWithData:[customDataJSONString dataUsingEncoding:NSUTF8StringEncoding] options:NSJSONReadingAllowFragments error:nil] : nil delegate:self];
}

-(BOOL) isNativeBannerAdReadyForPlacementID:(NSString*)placementID {
    return [HBNativeBannerWrapper nativeBannerAdReadyForPlacementID:placementID];
}
#pragma mark - delegate(s)
-(void) didFinishLoadingNativeBannerAdWithPlacementID:(NSString *)placementID {
    NSLog(@"HBNativeBannerAdWrapper::didFinishLoadingNativeBannerAdWithPlacementID:%@", placementID);
    [self invokeCallback:@"OnNativeBannerAdLoaded" placementID:placementID error:nil extra:nil];
}

-(void) didFailToLoadNativeBannerAdWithPlacementID:(NSString*)placementID error:(NSError*)error {
    NSLog(@"HBNativeBannerAdWrapper::didFailToLoadNativeBannerAdWithPlacementID:%@ error:%@", placementID, error);
    error = error != nil ? error : [NSError errorWithDomain:@"com.hyperbid.Unity3DPackage" code:100001 userInfo:@{NSLocalizedDescriptionKey:@"AT has failed to load native banner ad", NSLocalizedFailureReasonErrorKey:@"AT has failed to load native banner ad"}];
    [self invokeCallback:@"OnNativeBannerAdLoadingFailure" placementID:placementID error:error extra:nil];
}

-(void) didShowNativeBannerAdInView:(HBNativeBannerView*)bannerView placementID:(NSString*)placementID extra:(NSDictionary *)extra {
    NSLog(@"HBNativeBannerAdWrapper::didShowNativeBannerAdInView:placementID:%@", placementID);
    [self invokeCallback:@"OnNaitveBannerAdShow" placementID:placementID error:nil extra:extra];
}

-(void) didClickNativeBannerAdInView:(HBNativeBannerView*)bannerView placementID:(NSString*)placementID extra:(NSDictionary *)extra {
    NSLog(@"HBNativeBannerAdWrapper::didClickNativeBannerAdInView:placementID:%@", placementID);
    [self invokeCallback:@"OnNativeBannerAdClick" placementID:placementID error:nil extra:extra];
}

-(void) didClickCloseButtonInNativeBannerAdView:(HBNativeBannerView*)bannerView placementID:(NSString*)placementID extra:(NSDictionary *)extra {
    NSLog(@"HBNativeBannerAdWrapper::didClickCloseButtonInNativeBannerAdView:placementID:%@", placementID);
    [self invokeCallback:@"OnNativeBannerAdCloseButtonClicked" placementID:placementID error:nil extra:extra];
}

-(void) didAutorefreshNativeBannerAdInView:(HBNativeBannerView*)bannerView placementID:(NSString*)placementID extra:(NSDictionary *)extra {
    NSLog(@"HBNativeBannerAdWrapper::didFailToAutorefreshNativeBannerAdInView:placementID:%@", placementID);
    [self invokeCallback:@"OnNativeBannerAdAutorefresh" placementID:placementID error:nil extra:extra];
    
}

-(void) didFailToAutorefreshNativeBannerAdInView:(HBNativeBannerView*)bannerView placementID:(NSString*)placementID error:(NSError*)error {
    NSLog(@"HBNativeBannerAdWrapper::didFailToAutorefreshNativeBannerAdInView:placementID:%@ error:%@", placementID, error);
    error = error != nil ? error : [NSError errorWithDomain:@"com.hyperbid.Unity3DPackage" code:100001 userInfo:@{NSLocalizedDescriptionKey:@"AT has failed to refresh native banner ad", NSLocalizedFailureReasonErrorKey:@"AT has failed to refresh native banner ad"}];
    [self invokeCallback:@"OnNativeBannerAdAutorefreshFailed" placementID:placementID error:error extra:nil];
}
@end

#pragma mark - vc swizzling
@implementation UIViewController (ATUtilities)

-(void)AT_presentViewController:(UIViewController *)viewControllerToPresent animated:(BOOL)flag completion:(void (^)(void))completion {
    [self AT_presentViewController:viewControllerToPresent animated:flag completion:completion];
    [[HBNativeBannerAdWrapper sharedInstance] invokeCallback:@"PauseAudio" placementID:kATSharedCallbackKey error:nil extra:nil];
    NSLog(@"oc : AT_presentViewController, callback to PauseAudio");
}

-(void) AT_dismissViewControllerAnimated:(BOOL)flag completion:(void (^)(void))completion {
    [self AT_dismissViewControllerAnimated:flag completion:completion];
    [[HBNativeBannerAdWrapper sharedInstance] invokeCallback:@"ResumeAudio" placementID:kATSharedCallbackKey error:nil extra:nil];
    NSLog(@"oc : AT_dismissViewControllerAnimated, callback to ResumeAudio");
}
@end
