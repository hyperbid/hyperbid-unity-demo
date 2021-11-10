//
//  HBOfferModel.h
//  HyperBidSDK
//
//  Created by Leo on 21/8/2020.
//  Copyright © 2020 HyperBid. All rights reserved.
//

#import "HBModel.h"
#import <UIKit/UIKit.h>

extern NSString *const kHBOfferBannerSize320_50;
extern NSString *const kHBOfferBannerSize320_90;
extern NSString *const kHBOfferBannerSize300_250;
extern NSString *const kHBOfferBannerSize728_90;

@interface HBVideoPlayingTKItem : NSObject

@property (nonatomic, copy) NSArray<NSString *> *urls;
@property (nonatomic) NSInteger triggerTime;
@property (nonatomic) BOOL sent;

- (instancetype)initWithDict:(NSDictionary *)dict;

@end

// v5.7.24
typedef NS_ENUM(NSInteger, HBOfferInterActableArea) {
    HBOfferInterActableAreaAll,
    HBOfferInterActableAreaVisibleItems,
    HBOfferInterActableAreaCTA
};

@interface HBOfferModel : HBModel

@property(nonatomic) HBOfferInterActableArea interActableArea;
@property(nonatomic, readwrite) NSString *resourceID;
@property(nonatomic, readwrite) NSString *offerID;
@property(nonatomic, readwrite) NSString *pkgName;
@property(nonatomic, readwrite) NSString *title;
@property(nonatomic, readwrite) NSString *text;
@property(nonatomic, readwrite) NSInteger rating;
@property(nonatomic, readwrite) NSString *iconURL;
@property(nonatomic, readwrite) NSString *fullScreenImageURL;
@property(nonatomic, readwrite) HBInterstitialType interstitialType;//check the offer for video or image
@property(nonatomic, readwrite) NSString *logoURL;//adv_u
@property(nonatomic, readwrite) NSString *CTA;
@property(nonatomic, readwrite) NSString *videoURL;
@property(nonatomic, readwrite) NSString *storeURL;
@property(nonatomic, readwrite) HBLinkType linkType;
@property(nonatomic, readwrite) NSString *clickURL;
@property(nonatomic, readwrite) NSString *deeplinkUrl; 
@property(nonatomic, readwrite) NSString *localResourceID;
@property(nonatomic, readwrite) HBOfferModelType offerModelType;
@property(nonatomic, readwrite) HBOfferCrtType crtType;

@property(nonatomic, copy) NSString *jumpUrl;
@property(nonatomic) NSInteger offerFirmID;

// ad attributes
@property(nonatomic, readwrite) NSString *adPublisher;
@property(nonatomic, readwrite) NSString *adVersion;
@property(nonatomic, readwrite) NSString *adPrivacy;
@property(nonatomic, readwrite) NSString *adPermission;

//banner(myoffer:5.6.6)
@property(nonatomic, readwrite) NSString *bannerImageUrl;
@property(nonatomic, readwrite) NSString *bannerBigImageUrl;
@property(nonatomic, readwrite) NSString *rectangleImageUrl;
@property(nonatomic, readwrite) NSString *homeImageUrl;

@property(nonatomic, readwrite) NSArray<NSString*>* resourceURLs;

@property(nonatomic) NSInteger displayDuration;

@property(nonatomic, readwrite) NSArray<NSString*>* clickTKUrl;
@property(nonatomic, readwrite) NSArray<NSString*>* openSchemeFailedTKUrl;

//to do
@property(nonatomic) NSInteger videoCurrentTime;
@property(nonatomic) NSInteger videoResumeTime;

@property(nonatomic, copy) NSDictionary *tapInfoDict;

// If it is YES, it means that the feedback has been done.
@property(nonatomic) BOOL feedback;

// v5.7.47 +
@property(nonatomic, readwrite) NSArray<NSString*>* deeplinkSuccessUrl;
@property(nonatomic, readwrite) NSDictionary* hb_deeplinkSuccessUrl;

// v5.7.56+
@property(nonatomic, readwrite) HBSplashImageScaleType splashImageScaleType;


- (BOOL)showAdAttributes;
@end


