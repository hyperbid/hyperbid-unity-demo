//
//  HBOfferSetting.h
//  HyperBidSDK
//
//  Created by Leo on 26/8/2020.
//  Copyright © 2020 HyperBid. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "HBModel.h"

extern NSString *const kHBOfferTrackerExtraLifeCircleID;
extern NSString *const kHBOfferTrackerExtraScene;

extern NSString *const kHBOfferTrackerGDTClickID;
extern NSString *const kHBOfferTrackerGDTWidth;
extern NSString *const kHBOfferTrackerGDTHeight;
extern NSString *const kHBOfferTrackerGDTDownX;
extern NSString *const kHBOfferTrackerGDTDownY;
extern NSString *const kHBOfferTrackerGDTUpX;
extern NSString *const kHBOfferTrackerGDTUpY;
extern NSString *const kHBOfferTrackerGDTRequestWidth;
extern NSString *const kHBOfferTrackerGDTRequestHeight;

// For v5.7.7+ onlineApi. Some relative coordinates.
extern NSString *const kHBOfferTrackerRelativeDownX;
extern NSString *const kHBOfferTrackerRelativeDownY;
extern NSString *const kHBOfferTrackerRelativeUpX;
extern NSString *const kHBOfferTrackerRelativeUpY;
extern NSString *const kHBOfferTrackerTimestamp;
extern NSString *const kHBOfferTrackerMilliTimestamp;
extern NSString *const kHBOfferTrackerEndTimestamp;
extern NSString *const kHBOfferTrackerEndMilliTimestamp;
extern NSString *const kHBOfferTrackerVideoTimePlayed;
extern NSString *const kHBOfferTrackerVideoMilliTimePlayed;

// for v5.7.40+
extern NSString *const kHBOfferTrackerKYAbsoluteCoord;
extern NSString *const kHBOfferTrackerKYRelativeCoord;

// v5.7.42+
extern NSString *const kHBOfferTrackerDeeplinkType;

extern NSString *const kHBOfferTrackerOPDPType;

//typedef NS_ENUM(NSInteger, HBOfferCloseButtonSizeType) {
//    HBOfferCloseButtonSizeTypeNormal = 1,
//    HBOfferCloseButtonSizeType150Percent,
//    HBOfferCloseButtonSizeType75Percent,
//    HBOfferCloseButtonSizeType50Percent
//};
@interface HBOfferSetting : HBModel
-(instancetype) initWithDictionary:(NSDictionary *)dictionary;

@property(nonatomic, readwrite) NSString *placementID;
@property(nonatomic, readwrite) NSInteger format;
@property(nonatomic, readwrite) HBVideoClickable videoClickable;
@property(nonatomic, readwrite) NSTimeInterval resourceDownloadTimeout;
@property(nonatomic, readwrite) NSTimeInterval bannerAppearanceInterval;
@property(nonatomic, readwrite) HBEndCardClickable endCardClickable;
@property(nonatomic, readwrite) BOOL unMute;
@property(nonatomic, readwrite) NSTimeInterval closeButtonAppearanceInterval;
@property(nonatomic, readwrite) HBLoadStorekitTime storekitTime;
@property(nonatomic, readwrite) NSInteger lastOfferidsNum;

// The inherent attributes of the ad, such as the version number, etc.
//@property(nonatomic, readwrite) BOOL showAdAttrs;

//setting for banner
@property(nonatomic, readwrite) NSString *bannerSize;
@property(nonatomic, readwrite) BOOL showBannerCloseBtn;
@property(nonatomic, readwrite) NSInteger splashCountDownTime;
@property(nonatomic, readwrite) BOOL skipable;
@property(nonatomic, readwrite) NSInteger splashOrientation;

@property(nonatomic, readwrite) HBOfferLayoutType layoutType;
@property(nonatomic, readwrite) HBClickMode clickMode;
@property(nonatomic, readwrite) HBLoadType loadType;
@property(nonatomic, readwrite) HBUserAgentType impressionUAType;
@property(nonatomic, readwrite) HBUserAgentType clickUAType;
@property(nonatomic, readwrite) HBDeepLinkClickMode deeplinkClickMoment;
@property(nonatomic, readwrite) HBSplashType splashType;
@property(nonatomic, readwrite) HBSplashImageScaleType splashImageScaleType;

/// The minimum value of the close button delayed display
@property(nonatomic) NSInteger closeBtnDelayMinTime;
/// The maximum value of the close button delayed display
@property(nonatomic) NSInteger closeBtnDelayMaxTime;

@property(nonatomic) NSInteger closeBtnDelayRate;

// v5.7.56+
@property(nonatomic) CGFloat closeBtnSize;
@property(nonatomic) NSInteger onRewardTime;
@property(nonatomic) BOOL rewardWhenFailed;

// v5.7.71+
@property(nonatomic) NSInteger automatedClick;
@property(nonatomic) NSInteger automatedClickExtendTime;
@property(nonatomic) BOOL showEendCard;




@end


