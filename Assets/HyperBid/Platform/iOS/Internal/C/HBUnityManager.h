//
//  HBUnityManager.h
//  UnityContainer
//
//  Created by Martin Lau on 08/08/2018.
//  Copyright © 2018 Martin Lau. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "HBUnityWrapper.h"

@interface HBUnityManager : NSObject<HBUnityWrapper>
+(instancetype)sharedInstance;
- (id)selWrapperClassWithDict:(NSDictionary *)dict callback:(void(*)(const char*))callback;
@end
