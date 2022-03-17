//
//  HBAdManager+Internal.h
//  HyperBidSDK
//
//  Created by Leo on 04/05/2018.
//  Copyright © 2018 Leo. All rights reserved.
//

/**
 * This file contains methods&properties implemented by HBAdManager intented for internal use only.
 */
#ifndef HBAdManager_Internal_h
#define HBAdManager_Internal_h
#import "HBAdManager.h"
#import "HBAd.h"
//The value is (subclass of) UIViewController
extern NSString *const kHBAdLoadingExtraRefreshFlagKey;//Defined in loader
extern NSString *const kHBAdLoadingExtraAutoloadFlagKey;
extern NSString *const kHBAdLoadingTrackingExtraStatusKey;
extern NSString *const kHBAdLoadingTrackingExtraFlagKey;
extern NSString *const kHBAdLoadingExtraDefaultLoadKey;
extern NSString *const kHBAdLoadingExtraFilledByReadyFlagKey;
extern NSString *const kHBAdLoadingExtraAutoLoadOnCloseFlagKey;

/*
 Defined in Storage Utility
 */
extern NSString *const kHBAdStorageExtraNotReadyReasonKey;
extern NSString *const kHBAdStorageExtraNeedReloadFlagKey;
extern NSString *const kHBAdStorageExtraPlacementIDKey;
extern NSString *const kHBAdStorageExtraRequestIDKey;
extern NSString *const kHBAdStorageExtraReadyFlagKey;
extern NSString *const kHBAdStorageExtraCallerInfoKey;
extern NSString *const kHBAdStorageExtraPSIDKey;
extern NSString *const kHBAdStorageExtraSessionIDKey;
extern NSString *const kHBAdStorageExtraHeaderBiddingInfo;
extern NSString *const kHBAdStoreageExtraUnitGroupUnitID;
extern NSString *const kHBAdStorageExtraNetworkFirmIDKey;
extern NSString *const kHBAdStorageExtraNetworkSDKVersion;
extern NSString *const kHBAdStorageExtraPriorityKey;
extern NSString *const kHBAdStorageExtraUnitGroupInfosKey;
extern NSString *const kHBAdStorageExtraUnitGroupInfoContentKey;
extern NSString *const kHBAdStorageExtraUnitGroupInfoPriorityKey;
extern NSString *const kHBAdStorageExtraUnitGroupInfoNetworkFirmIDKey;
extern NSString *const kHBAdStorageExtraUnitGroupInfoUnitIDKey;
extern NSString *const kHBAdStorageExtraUnitGroupInfoNetworkSDKVersionKey;
extern NSString *const kHBAdStorageExtraUnitGroupInfoReadyFlagKey;
extern NSString *const kHBAdStorageExtraFinalWaterfallKey;
extern NSString *const kHBAdStorageExtraAdapterClassKey;

typedef NS_ENUM(NSInteger, HBAdManagerReadyAPICaller) {
    HBAdManagerReadyAPICallerReady = 0,
    HBAdManagerReadyAPICallerShow = 1
};


@protocol HBBaiduTemplateRenderingAttributeDelegate;

@interface HBAdManager(Internal)
#pragma mark - for inner usage
//TODO: Packing the following method in a category and hide it from the client code.
/**
 The following ps id accessing methods are thread-safe.
 */
-(void) clearPSID;
-(void) setPSID:(NSString*)psID interval:(NSTimeInterval)interval;
-(BOOL) psIDExpired;
@property(nonatomic, readonly) dispatch_queue_t show_api_control_queue;
@property(nonatomic, readonly) NSString *psID;

@property(nonatomic, strong) id<HBBaiduTemplateRenderingAttributeDelegate> baiduTemplateRenderingAttributeValue;

/**
 Contains all the placement ids the developer has configured for this app. This property is thread-safe.
 */
@property(nonatomic, readonly) NSSet *placementIDs;

/**
 placementID will be added and stored if it's not previous been added, otherwise do nothing.
 This method is thread-safe.
 */
-(void) addNewPlacementID:(NSString*)placementID;

/**
 nil might be returned on one of the following conditions:
 1) No offer's been successfully loaded for the placement;
 2) Pacing/caps has exceeded the limit.
 when this happens, clients are expected to behave as if ad load request has failed.
 */
-(id<HBAd>) offerWithPlacementID:(NSString*)placementID error:(NSError**)error refresh:(BOOL)refresh;

/*
 Check if ad's ready for a placement, context is a storage specific block
 */
- (BOOL)adReadyForPlacementID:(NSString*)placementID scene:(NSString*)scene caller:(HBAdManagerReadyAPICaller)caller context:(BOOL(^)(NSDictionary *__autoreleasing *extra))context;
- (BOOL)adReadyForPlacementID:(NSString*)placementID caller:(HBAdManagerReadyAPICaller)caller context:(BOOL(^)(NSDictionary *__autoreleasing *extra))context;
- (BOOL)adReadyForPlacementID:(NSString*)placementID scene:(NSString*)scene caller:(HBAdManagerReadyAPICaller)caller sendTK:(BOOL)send context:(BOOL(^)(NSDictionary *__autoreleasing *extra))context;

/*
 *For internal use only
 */
- (BOOL)adReadyForPlacementID:(NSString*)placementID;
- (BOOL)adReadyForPlacementID:(NSString*)placementID sendTK:(BOOL)send;


-(NSDictionary*)extraInfoForPlacementID:(NSString*)placementID requestID:(NSString*)requestID;
-(void) setExtraInfo:(NSDictionary*)extraInfo forPlacementID:(NSString*)placementID requestID:(NSString*)requestID;
-(void) removeExtraInfoForPlacementID:(NSString*)placementID requestID:(NSString*)requestID;
-(NSDictionary*)lastExtraInfoForPlacementID:(NSString*)placementID;
-(void) clearCacheWithPlacementModel:(HBPlacementModel*)placementModel unitGroupModel:(HBUnitGroupModel*)unitGroupModel;

-(void) setAdBeingShownFlagForPlacementID:(NSString*)placementID;
-(void) clearAdBeingShownFlagForPlacementID:(NSString*)placementID;
-(BOOL) adBeingShownForPlacementID:(NSString*)placementID;

#pragma mark - auto_refresh

- (void)autoRefreshIsReadyPlacementID:(NSString *)placementID;

- (BOOL)getFirstSplashLoadStatus:(NSString *)placementID;

- (void)setFirstSplashLoadStatus:(NSString *)placementID status:(BOOL)status;

- (BOOL)getFirstSplashTimeoutStatus:(NSString *)placementID;

- (void)setFirstSplashTimeoutStatus:(NSString *)placementID status:(BOOL)status;


#pragma mark - real time TK

+ (NSArray *)getRealTimeNetworkArray;

#pragma mark - send_tracking
- (void)sendEntryScenarioTrackingWithPlacementID:(NSString *)placementID scene:(NSString *)scene isLoading:(BOOL)isLoading isReady:(BOOL)isReady extraInfo: (NSDictionary *)extraInfo;

@end

@interface NSObject(DelegateBinding)
@property(nonatomic, weak) id delegateToBePassed;
@end
#endif /* HBAdManager_Internal_h */
