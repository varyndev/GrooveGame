//
//  FiksuAttributionPlugin.h
//  FiksuSDK
//
//  Copyright (c) 2014 Fiksu, Inc. All rights reserved.
//

#import <Foundation/Foundation.h>

#ifndef FIKSU_EXPORT
  #define FIKSU_EXPORT __attribute__((visibility("default")))
#endif

FIKSU_EXPORT extern NSString *const FiksuURLHandledNotification;
FIKSU_EXPORT extern NSString *const FiksuURLHandledURLKey;
FIKSU_EXPORT extern NSString *const FiksuURLHandledSourceApplicationKey;
FIKSU_EXPORT extern NSString *const FiksuURLSchemePrefix;

typedef void (^AttributionBlock)(BOOL, NSDictionary *);

FIKSU_EXPORT @protocol FiksuAttributionPlugin <NSObject>

+ (NSString *)pluginVersion;

- (void)attemptAttributionForApp:(NSString *)appId completion:(AttributionBlock)attributionBlock;

@end
