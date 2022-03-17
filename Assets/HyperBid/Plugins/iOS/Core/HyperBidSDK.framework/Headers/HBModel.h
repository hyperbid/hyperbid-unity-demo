//
//  HBModel.h
//  HyperBidSDK
//
//  Created by Leo on 11/04/2018.
//  Copyright © 2018 Leo. All rights reserved.
//

#import <Foundation/Foundation.h>

typedef NS_ENUM(NSInteger, HBEndCardClickable) {
    HBEndCardClickableFullscreen = 1,
    HBEndCardClickableCTA,
    HBEndCardClickableBanner
};

typedef NS_ENUM(NSInteger, HBScreenOrientation) {
    HBScreenOrientationPortrait,
    HBScreenOrientationLandscape
};

typedef NS_ENUM(NSInteger, HBInterstitialType) {
    HBInterstitialVideo = 1,
    HBInterstitialPicture
};

typedef NS_ENUM(NSInteger, HBLinkType) {
    HBLinkTypeAppStore = 1,
    HBLinkTypeSafari,
    HBLinkTypeWebView,
    HBLinkTypeInnerSafari = 5
};

typedef NS_ENUM(NSInteger, HBOfferLayoutType) {
    HBOfferLayoutTypeNormal = 0,
    HBOfferLayoutTypeExpress
};

typedef NS_ENUM(NSInteger, HBClickMode) {
    HBClickModeSync = 1,
    HBClickModeAsync
};

typedef NS_ENUM(NSInteger, HBLoadType) {
    HBLoadTypeBrowser = 1,
    HBLoadTypeInternalBrowser
};

typedef NS_ENUM(NSInteger, HBUserAgentType) {
    HBUserAgentWebView = 1,
    HBUserAgentSystem
};

typedef NS_ENUM(NSInteger, HBLoadStorekitTime) {
    HBLoadStorekitTimePreload = 1,
    HBATLoadStorekitTimeClick,
    HBATLoadStorekitTimeNone
};
typedef NS_ENUM(NSInteger, HBVideoClickable) {
    HBVideoClickableNone = 1,
    HBVideoClickableWithCTA
};

typedef NS_ENUM(NSInteger, HBNetworkFirmId) {
    HBNetworkFirmIdADX =  66,
    HBNetworkFirmIdMyOffer = 35,
    HBNetworkFirmIdGDTOnline = 42,
    HBNetworkFirmIdDirectOffer =  67,

};

typedef NS_ENUM(NSInteger, HBOfferModelType) {
    HBOfferModelMyOffer = 1,
    HBOfferModelADX =  2,
    HBOfferModelOnlineApi =  3,
    HBOfferModelDirectOffer =  4,
};

typedef NS_ENUM(NSInteger, HBOfferCrtType) {
    HBOfferCrtTypeOneImage = 1,
    HBOfferCrtTypeImages = 2,
    HBOfferCrtTypeOneImageWithText = 3,
    HBOfferCrtTypeImagesWithText = 4,
    HBOfferCrtTypeVideo = 5,
    HBOfferCrtTypeXHTML = 6
};

typedef NS_ENUM(NSInteger, HBDeepLinkClickMode) {
    HBDeepLinkModeNone = 1,
    HBDeepLinkModePreClickUrl = 2,
    HBDeepLinkModeLastClickUrl = 3
};

typedef NS_ENUM(NSInteger, HBClickType) {
    HBClickTypeClickUrl = 1,
    HBClickTypeDeepLinkUrl,
    HBClickTypeClickJumpUrl
};

typedef NS_ENUM(NSInteger, HBUnitGroupType) {
    HBUnitGroupTypeNormal = 1,
    HBUnitGroupTypeAdx,
    HBUnitGroupTypeC2S,
    HBUnitGroupTypeS2S,
    HBUnitGroupTypeInHouse,
    HBUnitGroupTypeBKS,
    HBUnitGroupTypeDirectOffer,
    HBUnitGroupTypeDefault,
};

typedef NS_ENUM(NSInteger, HBSplashType) {
    HBSplashTypeSplice = 1,
    HBSplashTypeFullScreen
};

typedef NS_ENUM(NSInteger, HBSplashImageScaleType) {
    HBSplashImageScaleTypeFit = 1,
    HBSplashImageScaleTypeFill
};

typedef NS_ENUM(NSInteger, HBBiddingCurrencyType) {
    HBBiddingCurrencyTypeUS = 1,
    HBBiddingCurrencyTypeCNY
};

@interface HBModel : NSObject
-(instancetype)initWithDictionary:(NSDictionary*)dictionary;
@end
