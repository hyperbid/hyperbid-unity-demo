//
//  HBUnityUtilities.h
//  UnityContainer
//
//  Created by Martin Lau on 14/08/2018.
//  Copyright © 2018 Martin Lau. All rights reserved.
//

#import <UIKit/UIKit.h>
extern NSString *const kHBUnityUtilitiesInterstitialImpressionNotification;
extern NSString *const kHBUnityUtilitiesInterstitialCloseNotification;
extern NSString *const kHBUnityUtilitiesRewardedVideoImpressionNotification;
extern NSString *const kHBUnityUtilitiesRewardedVideoCloseNotification;
extern NSString *const kHBUnityUtilitiesAdShowingExtraScenarioKey;
@interface HBUnityUtilities : NSObject
+(BOOL)isEmpty:(id)object;
@end

@interface NSDictionary (KAKit)
-(NSString*) jsonString;
-(BOOL)containsObjectForKey:(id)key;
@end

@interface NSData(ATKit)
+(instancetype) dataWithUTF8String:(const char*)string;
@end

@interface UIColor (Hex)
// 透明度固定为1，以0x开头的十六进制转换成的颜色
+ (UIColor *)colorWithHex:(long)hexColor;
// 0x开头的十六进制转换成的颜色,透明度可调整
+ (UIColor *)colorWithHex:(long)hexColor alpha:(float)opacity;
// 颜色转换三：iOS中十六进制的颜色（以#开头）转换为UIColor
+ (UIColor *) colorWithHexString: (NSString *)color;

@end
