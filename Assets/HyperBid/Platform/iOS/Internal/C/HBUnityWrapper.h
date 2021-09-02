//
//  HBUnityWrapper.h
//  HBSDK
//
//  Created by Martin Lau on 08/08/2018.
//  Copyright © 2018 Martin Lau. All rights reserved.
//

#ifndef HBUnityWrapper_h
#define HBUnityWrapper_h
@protocol HBUnityWrapper<NSObject>
+(instancetype) sharedInstance;
@optional
-(void) setCallBack:(void(*)(const char*, const char *))callback forKey:(NSString*)key;
-(void) removeCallbackForKey:(NSString*)key;
-(void(*)(const char*, const char *)) callbackForKey:(NSString*)key;
@end

#endif /* HBUnityWrapper_h */
