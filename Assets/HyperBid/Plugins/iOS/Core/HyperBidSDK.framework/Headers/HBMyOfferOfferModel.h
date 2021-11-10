//
//  HBMyOfferOfferModel.h
//  HyperBidMyOffer
//
//  Created by Leo on 2019/9/23.
//  Copyright © 2019 Leo. All rights reserved.
//

#import "HBOfferModel.h"
#import "HBMyOfferSetting.h"

@interface HBMyOfferOfferModel : HBOfferModel
-(instancetype) initWithDictionary:(NSDictionary *)dictionary placeholders:(NSDictionary*)placeholders format:(NSInteger)format setting:(HBMyOfferSetting*)setting;

@property(nonatomic, readwrite) HBScreenOrientation imageOrientation;
@property(nonatomic, readwrite) HBScreenOrientation videoOrientation;
@property(nonatomic, readwrite) NSString *videoStartTKURL;
@property(nonatomic, readwrite) NSString *video25TKURL;
@property(nonatomic, readwrite) NSString *video50TKURL;
@property(nonatomic, readwrite) NSString *video75TKURL;
@property(nonatomic, readwrite) NSString *videoEndTKURL;
@property(nonatomic, readwrite) NSString *endCardShowTKURL;
@property(nonatomic, readwrite) NSString *endCardCloseTKURL;
@property(nonatomic, readwrite) NSString *impURL;
@property(nonatomic, readwrite) NSString *impTKURL;
@property(nonatomic, readwrite) NSString *clickTKURL;
@property(nonatomic, readwrite) NSInteger dailyCap;
@property(nonatomic, readwrite) NSTimeInterval pacing;
@property(nonatomic, readwrite) NSDictionary<NSString*, NSString*> *placeholders;

//@property(nonatomic, readonly) NSInteger screenOrientation;
@property(nonatomic, readwrite) HBClickMode performsAsynchronousRedirection;

// v5.7.47
@property(nonatomic, readwrite) HBMyOfferInterType interType;

@property(nonatomic, readwrite) HBUserAgentType impressionUAType;
@property(nonatomic, readwrite) HBUserAgentType clickUAType;

@property (nonatomic, readwrite) HBMyOfferSetting *myOfferSetting;


+(instancetype) mockOfferModel;
@end


