//
//  FiksuTrackingManager.h
//
//  Copyright 2014 Fiksu, Inc. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#import <FiksuSDK/FiksuAttributionPlugin.h>

#ifndef FIKSU_EXPORT
  #define FIKSU_EXPORT __attribute__((visibility("default")))
#endif

/**************** Event Enumerations ****************/

typedef NS_ENUM(NSInteger, FiksuRegistrationEvent) {
  FiksuRegistrationEvent1,
  FiksuRegistrationEvent2,
  FiksuRegistrationEvent3
};

typedef NS_ENUM(NSInteger, FiksuPurchaseEvent) {
  FiksuPurchaseEvent1,
  FiksuPurchaseEvent2,
  FiksuPurchaseEvent3,
  FiksuPurchaseEvent4,
  FiksuPurchaseEvent5
};

/**************** Configuration Keys ****************/

/*
  Description: The ID of your application in the app store.
  Format:      9-digit number as an NSString.
  Default:     none
  REQUIRED
*/
FIKSU_EXPORT extern NSString * FiksuTrackingConfigurationAppIdentifierKey;

/*
  Description: Pops up a ViewController which shows you the status of your tracking
               events after an event is sent. Also outputs diagnostic info to the log.
  Format:      NSNumber (use a boxed BOOL like @(NO) or @(YES))
  Default:     @(NO)
*/
FIKSU_EXPORT extern NSString * FiksuTrackingConfigurationDebugModeEnabledKey;

/*
  Description: An array of valid product identifiers recognizable by the App Store.
               The Fiksu SDK can determine the country of the user's App Store by
               querying these products, which allows the client to target specific
               App Store users.
  Format:      NSArray containing NSString instances
  Default:     nil
*/
FIKSU_EXPORT extern NSString * FiksuTrackingConfigurationProductIdentifiersKey;


/**************** FiksuTrackingManager ****************/

FIKSU_EXPORT @interface FiksuTrackingManager : NSObject

/*
  Initializes the SDK.
  A configuration is needed for the SDK to function. If you submit one procedurally, you must
  pass a configuration into the applicationDidFinishLaunching:configuration: method.

  You can use the keys and value types below. Here is a suggested sample:
 
[FiksuTrackingManager applicationDidFinishLaunching:launchOptions configuration:@{
#ifdef DEBUG
    FiksuTrackingConfigurationDebugModeEnabledKey : @(YES),
#endif
    FiksuTrackingConfigurationAppIdentifierKey : @"" // <--- App ID here as string
}];
*/
+ (void)applicationDidFinishLaunching:(NSDictionary *)launchOptions configuration:(NSDictionary*)configuration;

/*
  Like applicationDidFinishLaunching:configuration:, but instead, the configuration is read
  from the resource "FiksuConfiguration.plist", located in the application's main bundle.
*/
+ (void)applicationDidFinishLaunching:(NSDictionary *)launchOptions;

+ (void)setClientID:(NSString *)clientID;
+ (NSString *)clientID;

+ (void)setAppTrackingEnabled:(BOOL)enabled;
+ (BOOL)isAppTrackingEnabled;

+ (BOOL)handleURL:(NSURL *)url sourceApplication:(NSString *)sourceApplication;

+ (void)uploadRegistration:(FiksuRegistrationEvent)event;

+ (void)uploadPurchase:(FiksuPurchaseEvent)event
                 price:(double)price
              currency:(NSString *)currency;

+ (void)uploadCustomEvent;

+ (void)displayAdInSuperview:(UIView *)superView withinFrame:(CGRect)frame;

+ (void)setAttributionPlugin:(NSObject<FiksuAttributionPlugin> *)plugin;
+ (NSObject<FiksuAttributionPlugin> *)attributionPlugin;

@end

@interface FiksuTrackingManager (Deprecated)

+ (void)uploadRegistrationEvent:(NSString *)username __attribute__((deprecated));

+ (void)uploadPurchaseEvent:(NSString *)username
                   currency:(NSString *)currency     __attribute__((deprecated));

+ (void)uploadPurchaseEvent:(NSString *)username
                      price:(double)price
                   currency:(NSString *)currency     __attribute__((deprecated));
@end

@interface FiksuTrackingManager (Private)

+ (void)uploadEvent:(NSString *)event
           withInfo:(NSDictionary *)eventInfo        __attribute__((deprecated));
@end
