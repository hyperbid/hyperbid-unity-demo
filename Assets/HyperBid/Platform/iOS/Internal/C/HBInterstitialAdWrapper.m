//
//  HBInterstitialAdWrapper.m
//  UnityContainer
//
//  Created by Martin Lau on 2019/1/8.
//  Copyright © 2019 Martin Lau. All rights reserved.
//

#import "HBInterstitialAdWrapper.h"
#import "HBUnityUtilities.h"
#import <HyperBidInterstitial/HyperBidInterstitial.h>

NSString *const kLoadUseRVAsInterstitialKey = @"UseRewardedVideoAsInterstitial";
NSString *const kInterstitialExtraAdSizeKey = @"interstitial_ad_size";
static NSString *kHBInterstitialSizeUsesPixelFlagKey = @"uses_pixel";

@interface HBInterstitialAdWrapper()<HBInterstitialDelegate>
@end
@implementation HBInterstitialAdWrapper
+(instancetype)sharedInstance {
    static HBInterstitialAdWrapper *sharedInstance = nil;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        sharedInstance = [[HBInterstitialAdWrapper alloc] init];
    });
    return sharedInstance;
}

-(NSString*) scriptWrapperClass {
    return @"HBInterstitialAdWrapper";
}

- (id)selWrapperClassWithDict:(NSDictionary *)dict callback:(void(*)(const char*, const char*))callback {
    NSString *selector = dict[@"selector"];
    NSArray<NSString*>* arguments = dict[@"arguments"];
    NSString *firstObject = @"";
    NSString *lastObject = @"";
    if (![HBUnityUtilities isEmpty:arguments]) {
        for (int i = 0; i < arguments.count; i++) {
            if (i == 0) { firstObject = arguments[i]; }
            else { lastObject = arguments[i]; }
        }
    }
    
    if ([selector isEqualToString:@"loadInterstitialAdWithPlacementID:customDataJSONString:callback:"]) {
        [self loadInterstitialAdWithPlacementID:firstObject customDataJSONString:lastObject callback:callback];
    } else if ([selector isEqualToString:@"interstitialAdReadyForPlacementID:"]) {
        return [NSNumber numberWithBool:[self interstitialAdReadyForPlacementID:firstObject]];
    } else if ([selector isEqualToString:@"showInterstitialAdWithPlacementID:extraJsonString:"]) {
        [self showInterstitialAdWithPlacementID:firstObject extraJsonString:lastObject];
    } else if ([selector isEqualToString:@"checkAdStatus:"]) {
        return [self checkAdStatus:firstObject];
    } else if ([selector isEqualToString:@"clearCache"]) {
        [self clearCache];
    } else if ([selector isEqualToString:@"getValidAdCaches:"]) {
        return [self getValidAdCaches:firstObject];
    }else if ([selector isEqualToString:@"entryScenarioWithPlacementID:scenarioID:"]) {
        [self entryScenarioWithPlacementID:firstObject scenarioID:lastObject];
    }
    // auto
    else if ([selector isEqualToString:@"addAutoLoadAdPlacementID:callback:"]){
        [self addAutoLoadAdPlacementID:firstObject callback:callback];
    }else if ([selector isEqualToString:@"removeAutoLoadAdPlacementID:"]){
        [self removeAutoLoadAdPlacementID:firstObject];
    }else if ([selector isEqualToString:@"autoLoadInterstitialAdReadyForPlacementID:"]){
        return [NSNumber numberWithBool:[self autoLoadInterstitialAdReadyForPlacementID:firstObject]];
    }else if ([selector isEqualToString:@"getAutoValidAdCaches:"]){
        return [self getAutoValidAdCaches:firstObject];
    }else if ([selector isEqualToString:@"setAutoLocalExtra:customDataJSONString:"]){
        [self setAutoLocalExtra:firstObject customDataJSONString:lastObject];
    }else if ([selector isEqualToString:@"entryAutoAdScenarioWithPlacementID:scenarioID:"]){
        [self entryAutoAdScenarioWithPlacementID:firstObject scenarioID:lastObject];
    }else if ([selector isEqualToString:@"showAutoInterstitialAd:extraJsonString:"]){
        [self showAutoInterstitialAd:firstObject extraJsonString:lastObject];
    }else if ([selector isEqualToString:@"checkAutoAdStatus:"]) {
        return [self checkAutoAdStatus:firstObject];
    }
    
    return nil;
}

-(void) loadInterstitialAdWithPlacementID:(NSString*)placementID customDataJSONString:(NSString*)customDataJSONString callback:(void(*)(const char*, const char*))callback {
    
    [self setCallBack:callback forKey:placementID];
    NSMutableDictionary *extra = [NSMutableDictionary dictionary];
    if ([customDataJSONString isKindOfClass:[NSString class]] && [customDataJSONString length] > 0) {
        NSDictionary *extraDict = [NSJSONSerialization JSONObjectWithData:[customDataJSONString dataUsingEncoding:NSUTF8StringEncoding] options:NSJSONReadingAllowFragments error:nil];
        NSLog(@"extraDict = %@", extraDict);
        if (extraDict[kLoadUseRVAsInterstitialKey] != nil) {
            extra[kHBInterstitialExtraUsesRewardedVideo] = @([extraDict[kLoadUseRVAsInterstitialKey] boolValue]);
        }
        
        CGFloat scale = [extraDict[kHBInterstitialSizeUsesPixelFlagKey] boolValue] ? [UIScreen mainScreen].nativeScale : 1.0f;
        if ([extraDict[kInterstitialExtraAdSizeKey] isKindOfClass:[NSString class]] && [[extraDict[kInterstitialExtraAdSizeKey] componentsSeparatedByString:@"x"] count] == 2) {
            NSArray<NSString*>* com = [extraDict[kInterstitialExtraAdSizeKey] componentsSeparatedByString:@"x"];
            extra[kHBInterstitialExtraAdSizeKey] = [NSValue valueWithCGSize:CGSizeMake([com[0] doubleValue] / scale, [com[1] doubleValue] / scale)];
        }
    }
    
    NSLog(@"HBInterstitialAdWrapper::extra = %@", extra);
    [[HBAdManager sharedManager] loadAd:placementID extra:extra != nil ? extra : nil delegate:self];
}

-(BOOL) interstitialAdReadyForPlacementID:(NSString*)placementID {
    return [[HBAdManager sharedManager] isReadyInterstitialWithPlacementID:placementID];
}

-(NSString*) getValidAdCaches:(NSString *)placementID {
    NSArray *array = [[HBAdManager sharedManager] getInterstitialValidAdsForPlacementID:placementID];
    NSLog(@"HBNativeAdWrapper::array = %@", array);
    return array.jsonString;
}

-(void) showInterstitialAdWithPlacementID:(NSString*)placementID extraJsonString:(NSString*)extraJsonString {
    NSDictionary *extraDict = ([extraJsonString isKindOfClass:[NSString class]] && [extraJsonString dataUsingEncoding:NSUTF8StringEncoding] != nil) ? [NSJSONSerialization JSONObjectWithData:[extraJsonString dataUsingEncoding:NSUTF8StringEncoding] options:NSJSONReadingAllowFragments error:nil] : nil;
    [[HBAdManager sharedManager] showInterstitialAd:placementID scene:extraDict[kHBUnityUtilitiesAdShowingExtraScenarioKey] inViewController:[UIApplication sharedApplication].delegate.window.rootViewController delegate:self];
}

-(NSString*) checkAdStatus:(NSString *)placementID {
    HBCheckLoadModel *checkLoadModel = [[HBAdManager sharedManager] checkInterstitialReadyAdInfo:placementID];
    NSMutableDictionary *statusDict = [NSMutableDictionary dictionary];
    statusDict[@"isLoading"] = @(checkLoadModel.isLoading);
    statusDict[@"isReady"] = @(checkLoadModel.isReady);
    statusDict[@"adInfo"] = checkLoadModel.adOfferInfo;
    NSLog(@"HBInterstitialAdWrapper::statusDict = %@", statusDict);
    return statusDict.jsonString;
}
- (void)entryScenarioWithPlacementID:(NSString *)placementID scenarioID:(NSString *)scenarioID{
    
    [[HBAdManager sharedManager] entryInterstitialScenarioWithPlacementID:placementID scene:scenarioID];
}
-(void) clearCache {
    [[HBAdManager sharedManager] clearCache];
}

#pragma mark - auto
-(void) addAutoLoadAdPlacementID:(NSString*)placementID callback:(void(*)(const char*, const char*))callback {

    if (placementID == nil) {
        return;
    }
    
    [HBInterstitialAutoAdManager sharedInstance].delegate = self;
    
    NSArray *placementIDArray = [self jsonStrToArray:placementID];

    [placementIDArray enumerateObjectsUsingBlock:^(NSString * _Nonnull obj, NSUInteger idx, BOOL * _Nonnull stop) {
        [self setCallBack:callback forKey:obj];
        NSLog(@" addAutoLoadAdPlacementID--%@",placementID);
    }];
    
    
    [[HBInterstitialAutoAdManager sharedInstance] addAutoLoadAdPlacementIDArray:placementIDArray];
}

-(void) removeAutoLoadAdPlacementID:(NSString*)placementID{
    NSLog(@" removeAutoLoadAdPlacementID--%@",placementID);
    
    if (placementID == nil) {
        return;
    }
    
    NSArray *placementIDArray = [self jsonStrToArray:placementID];
    
    [[HBInterstitialAutoAdManager sharedInstance] removeAutoLoadAdPlacementIDArray:placementIDArray];
}

-(BOOL) autoLoadInterstitialAdReadyForPlacementID:(NSString*)placementID {
    
    NSLog(@"Unity: autoLoadInterstitialAdReadyForPlacementID--%@---%d",placementID,[[HBInterstitialAutoAdManager sharedInstance] autoLoadInterstitialReadyForPlacementID:placementID]);
    return [[HBInterstitialAutoAdManager sharedInstance] autoLoadInterstitialReadyForPlacementID:placementID];
}

-(NSString*) getAutoValidAdCaches:(NSString *)placementID{
    
    NSArray *array = [[HBInterstitialAutoAdManager sharedInstance] checkValidAdCachesWithPlacementID:placementID];
    
    NSLog(@"Unity: getAutoValidAdCaches::array = %@", array);
    
    return array.jsonString;
}

-(NSString*) checkAutoAdStatus:(NSString *)placementID {

    HBCheckLoadModel *checkLoadModel = [[HBInterstitialAutoAdManager sharedInstance] checkInterstitialReadyAdInfo:placementID];
    
    NSMutableDictionary *statusDict = [NSMutableDictionary dictionary];
    statusDict[@"isLoading"] = @(checkLoadModel.isLoading);
    statusDict[@"isReady"] = @(checkLoadModel.isReady);
    statusDict[@"adInfo"] = checkLoadModel.adOfferInfo;
    
    NSLog(@":checkAutoAdStatus statusDict = %@", statusDict);
    return statusDict.jsonString;
}

-(void) setAutoLocalExtra:(NSString*)placementID customDataJSONString:(NSString*)customDataJSONString{
    
    if ([customDataJSONString isKindOfClass:[NSString class]]) {
        
        NSDictionary *extraDict = [NSJSONSerialization JSONObjectWithData:[customDataJSONString dataUsingEncoding:NSUTF8StringEncoding] options:NSJSONReadingAllowFragments error:nil];
        
        NSMutableDictionary *extra = [NSMutableDictionary dictionary];

        if ([extraDict isKindOfClass:[NSDictionary class]]) {
            
            if (extraDict[kLoadUseRVAsInterstitialKey] != nil) {
                extra[kHBInterstitialExtraUsesRewardedVideo] = @([extraDict[kLoadUseRVAsInterstitialKey] boolValue]);
            }
            
            CGFloat scale = [extraDict[kHBInterstitialSizeUsesPixelFlagKey] boolValue] ? [UIScreen mainScreen].nativeScale : 1.0f;
            if ([extraDict[kInterstitialExtraAdSizeKey] isKindOfClass:[NSString class]] && [[extraDict[kInterstitialExtraAdSizeKey] componentsSeparatedByString:@"x"] count] == 2) {
                NSArray<NSString*>* com = [extraDict[kInterstitialExtraAdSizeKey] componentsSeparatedByString:@"x"];
                extra[kHBInterstitialExtraAdSizeKey] = [NSValue valueWithCGSize:CGSizeMake([com[0] doubleValue] / scale, [com[1] doubleValue] / scale)];
            }
        }
        
        NSLog(@"HBInterstitialAdWrapper::setAutoLocalExtra statusDict = %@", extraDict);
        [[HBInterstitialAutoAdManager sharedInstance] setLocalExtra:extra placementID:placementID];
        
    
    }
}

-(void) entryAutoAdScenarioWithPlacementID:(NSString*)placementID scenarioID:(NSString*)scenarioID{
    NSLog(@"HBInterstitialAdWrapper::entryAutoAdScenarioWithPlacementID = %@ scenarioID = %@", placementID,scenarioID);

    [[HBInterstitialAutoAdManager sharedInstance] entryAdScenarioWithPlacementID:placementID scenarioID:scenarioID];
}

-(void) showAutoInterstitialAd:(NSString*)placementID extraJsonString:(NSString*)extraJsonString {
    
    NSDictionary *extraDict = ([extraJsonString isKindOfClass:[NSString class]] && [extraJsonString dataUsingEncoding:NSUTF8StringEncoding] != nil) ? [NSJSONSerialization JSONObjectWithData:[extraJsonString dataUsingEncoding:NSUTF8StringEncoding] options:NSJSONReadingAllowFragments error:nil] : nil;
    
    NSLog(@"HBInterstitialAdWrapper::showAutoInterstitialAd = %@ extraJsonString = %@", placementID,extraJsonString);
    
    NSLog(@"HBInterstitialAdWrapper::extraDict = %@", extraDict);

    [[HBInterstitialAutoAdManager sharedInstance] showAutoLoadInterstitialWithPlacementID:placementID scene:extraDict[kHBUnityUtilitiesAdShowingExtraScenarioKey] inViewController:[UIApplication sharedApplication].delegate.window.rootViewController delegate:self];
}

#pragma mark - delegate method(s)
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



-(void) didFinishLoadingADWithPlacementID:(NSString *)placementID {
    [self invokeCallback:@"OnInterstitialAdLoaded" placementID:placementID error:nil extra:nil];
}

-(void) didFailToLoadADWithPlacementID:(NSString*)placementID error:(NSError*)error {
    error = error != nil ? error : [NSError errorWithDomain:@"com.hyperbid.Unity3DPackage" code:100001 userInfo:@{NSLocalizedDescriptionKey:@"AT has failed to load ad", NSLocalizedFailureReasonErrorKey:@"AT has failed to load ad"}];
    [self invokeCallback:@"OnInterstitialAdLoadFailure" placementID:placementID error:error extra:nil];
}

-(void) interstitialDidShowForPlacementID:(NSString*)placementID extra:(NSDictionary *)extra {
    [self invokeCallback:@"OnInterstitialAdShow" placementID:placementID error:nil extra:extra];
    [[NSNotificationCenter defaultCenter] postNotificationName:kHBUnityUtilitiesInterstitialImpressionNotification object:nil];
}

-(void) interstitialFailedToShowForPlacementID:(NSString*)placementID error:(NSError*)error extra:(NSDictionary *)extra {
    error = error != nil ? error : [NSError errorWithDomain:@"com.hyperbid.Unity3DPackage" code:100001 userInfo:@{NSLocalizedDescriptionKey:@"AT has failed to show ad", NSLocalizedFailureReasonErrorKey:@"AT has failed to show ad"}];
    [self invokeCallback:@"OnInterstitialAdFailedToShow" placementID:placementID error:error extra:nil];
}

-(void) interstitialDidStartPlayingVideoForPlacementID:(NSString*)placementID extra:(NSDictionary *)extra {
    [self invokeCallback:@"OnInterstitialAdVideoPlayStart" placementID:placementID error:nil extra:extra];
}

-(void) interstitialDidEndPlayingVideoForPlacementID:(NSString*)placementID extra:(NSDictionary *)extra {
    [self invokeCallback:@"OnInterstitialAdVideoPlayEnd" placementID:placementID error:nil extra:extra];
}

-(void) interstitialDidFailToPlayVideoForPlacementID:(NSString*)placementID error:(NSError*)error extra:(NSDictionary *)extra {
    [self invokeCallback:@"OnInterstitialAdVideoPlayFailure" placementID:placementID error:error extra:extra];
}

-(void) interstitialDidCloseForPlacementID:(NSString*)placementID extra:(NSDictionary *)extra {
    [self invokeCallback:@"OnInterstitialAdClose" placementID:placementID error:nil extra:extra];
    [[NSNotificationCenter defaultCenter] postNotificationName:kHBUnityUtilitiesInterstitialCloseNotification object:nil];
}

-(void) interstitialDidClickForPlacementID:(NSString*)placementID extra:(NSDictionary *)extra {
    [self invokeCallback:@"OnInterstitialAdClick" placementID:placementID error:nil extra:extra];
}
@end
