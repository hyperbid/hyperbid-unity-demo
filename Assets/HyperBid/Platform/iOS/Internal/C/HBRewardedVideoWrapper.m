//
//  HBRewardedVideoWrapper.m
//  UnityContainer
//
//  Created by Martin Lau on 08/08/2018.
//  Copyright © 2018 Martin Lau. All rights reserved.
//

#import "HBRewardedVideoWrapper.h"
#import "HBUnityUtilities.h"
#import <HyperBidRewardedVideo/HyperBidRewardedVideo.h>

NSString *const kLoadExtraUserIDKey = @"UserId";
NSString *const kLoadExtraMediaExtraKey = @"UserExtraData";
@interface HBRewardedVideoWrapper()<HBRewardedVideoDelegate>
@end
@implementation HBRewardedVideoWrapper
+(instancetype)sharedInstance {
    static HBRewardedVideoWrapper *sharedInstance = nil;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        sharedInstance = [[HBRewardedVideoWrapper alloc] init];
    });
    return sharedInstance;
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
    
    if ([selector isEqualToString:@"loadRewardedVideoWithPlacementID:customDataJSONString:callback:"]) {
        [self loadRewardedVideoWithPlacementID:firstObject customDataJSONString:lastObject callback:callback];
    } else if ([selector isEqualToString:@"rewardedVideoReadyForPlacementID:"]) {
        return [NSNumber numberWithBool:[self rewardedVideoReadyForPlacementID:firstObject]];
    } else if ([selector isEqualToString:@"showRewardedVideoWithPlacementID:extraJsonString:"]) {
        [self showRewardedVideoWithPlacementID:firstObject extraJsonString:lastObject];
    } else if ([selector isEqualToString:@"checkAdStatus:"]) {
        return [self checkAdStatus:firstObject];
    } else if ([selector isEqualToString:@"clearCache"]) {
        [self clearCache];
    } else if ([selector isEqualToString:@"setExtra:"]) {
        [self setExtra:firstObject];
    }
    return nil;
}

-(void) loadRewardedVideoWithPlacementID:(NSString*)placementID customDataJSONString:(NSString*)customDataJSONString callback:(void(*)(const char*, const char*))callback {
    [self setCallBack:callback forKey:placementID];
    NSMutableDictionary *extra = [NSMutableDictionary dictionary];
    if ([customDataJSONString isKindOfClass:[NSString class]] && [customDataJSONString length] > 0) {
        NSDictionary *extraDict = [NSJSONSerialization JSONObjectWithData:[customDataJSONString dataUsingEncoding:NSUTF8StringEncoding] options:NSJSONReadingAllowFragments error:nil];
        NSLog(@"extraDict = %@", extraDict);
        
        if (extraDict[kLoadExtraUserIDKey] != nil) { extra[kHBAdLoadingExtraUserIDKey] = extraDict[kLoadExtraUserIDKey]; }
        if (extraDict[kLoadExtraMediaExtraKey] != nil) { extra[kHBAdLoadingExtraMediaExtraKey] = extraDict[kLoadExtraMediaExtraKey]; }
    }
    
    [[HBAdManager sharedManager] loadAd:placementID extra:[extra isKindOfClass:[NSMutableDictionary class]] ? extra : nil delegate:self];
}

-(BOOL) rewardedVideoReadyForPlacementID:(NSString*)placementID {
    return [[HBAdManager sharedManager] isReadyRewardedVideoWithPlacementID:placementID];
}

-(NSString*) checkAdStatus:(NSString *)placementID {
    HBCheckLoadModel *checkLoadModel = [[HBAdManager sharedManager] checkRewardedVideoReadyAdInfo:placementID];
    NSMutableDictionary *statusDict = [NSMutableDictionary dictionary];
    statusDict[@"isLoading"] = @(checkLoadModel.isLoading);
    statusDict[@"isReady"] = @(checkLoadModel.isReady);
    statusDict[@"adInfo"] = checkLoadModel.adOfferInfo;
    NSLog(@"HBRewardedVideoWrapper::statusDict = %@", statusDict);
    return statusDict.jsonString;
}

-(void) showRewardedVideoWithPlacementID:(NSString*)placementID extraJsonString:(NSString*)extraJsonString {
    NSDictionary *extraDict = ([extraJsonString isKindOfClass:[NSString class]] && [extraJsonString dataUsingEncoding:NSUTF8StringEncoding] != nil) ? [NSJSONSerialization JSONObjectWithData:[extraJsonString dataUsingEncoding:NSUTF8StringEncoding] options:NSJSONReadingAllowFragments error:nil] : nil;
    NSLog(@"HBRewardedVideoWrapper::showRewardedVideoWithPlacementID = %@ extraJsonString = %@", placementID,extraJsonString);
    NSLog(@"HBRewardedVideoWrapper::extraDict = %@", extraDict);
    [[HBAdManager sharedManager] showRewardedVideoAd:placementID scene:extraDict[kHBUnityUtilitiesAdShowingExtraScenarioKey] inViewController:[UIApplication sharedApplication].delegate.window.rootViewController delegate:self];
}

-(void) clearCache {
    [[HBAdManager sharedManager] clearCache];
}

-(void) setExtra:(NSString*)extra {
    if ([extra isKindOfClass:[NSString class]]) {
        NSDictionary *extraDict = [NSJSONSerialization JSONObjectWithData:[extra dataUsingEncoding:NSUTF8StringEncoding] options:NSJSONReadingAllowFragments error:nil];
        if ([extraDict isKindOfClass:[NSDictionary class]]) [[HBAdManager sharedManager] setExtra:extraDict];
    }
}

-(NSString*) scriptWrapperClass {
    return @"HBRewardedVideoWrapper";
}

#pragma mark - delegate
-(void) didFinishLoadingADWithPlacementID:(NSString *)placementID {
    [self invokeCallback:@"OnRewardedVideoLoaded" placementID:placementID error:nil extra:nil];
}

-(void) didFailToLoadADWithPlacementID:(NSString*)placementID error:(NSError*)error {
    error = error != nil ? error : [NSError errorWithDomain:@"com.hyperbid.Unity3DPackage" code:100001 userInfo:@{NSLocalizedDescriptionKey:@"AT has failed to load ad", NSLocalizedFailureReasonErrorKey:@"AT has failed to load ad"}];
    [self invokeCallback:@"OnRewardedVideoLoadFailure" placementID:placementID error:error extra:nil];
}

-(void) rewardedVideoDidStartPlayingForPlacementID:(NSString*)placementID extra:(NSDictionary *)extra {
    [self invokeCallback:@"OnRewardedVideoPlayStart" placementID:placementID error:nil extra:extra];
    [[NSNotificationCenter defaultCenter] postNotificationName:kHBUnityUtilitiesRewardedVideoImpressionNotification object:nil];
}

-(void) rewardedVideoDidEndPlayingForPlacementID:(NSString*)placementID extra:(NSDictionary *)extra {
    [self invokeCallback:@"OnRewardedVideoPlayEnd" placementID:placementID error:nil extra:extra];
}

-(void) rewardedVideoDidFailToPlayForPlacementID:(NSString*)placementID error:(NSError*)error extra:(NSDictionary *)extra {
    error = error != nil ? error : [NSError errorWithDomain:@"com.hyperbid.Unity3DPackage" code:100001 userInfo:@{NSLocalizedDescriptionKey:@"AT has failed to play video", NSLocalizedFailureReasonErrorKey:@"AT has failed to play video"}];
    [self invokeCallback:@"OnRewardedVideoPlayFailure" placementID:placementID error:error extra:extra];
}

-(void) rewardedVideoDidCloseForPlacementID:(NSString*)placementID rewarded:(BOOL)rewarded extra:(NSDictionary *)extra {
    [self invokeCallback:@"OnRewardedVideoClose" placementID:placementID error:nil extra:@{@"rewarded":@(rewarded), @"extra":extra != nil ? extra : @{}}];
    [[NSNotificationCenter defaultCenter] postNotificationName:kHBUnityUtilitiesRewardedVideoCloseNotification object:nil];
}

-(void) rewardedVideoDidClickForPlacementID:(NSString*)placementID extra:(NSDictionary *)extra {
    [self invokeCallback:@"OnRewardedVideoClick" placementID:placementID error:nil extra:extra];
}

-(void) rewardedVideoDidRewardSuccessForPlacemenID:(NSString*)placementID extra:(NSDictionary*)extra {
    [self invokeCallback:@"OnRewardedVideoReward" placementID:placementID error:nil extra:extra];
}
@end
