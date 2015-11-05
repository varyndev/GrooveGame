
/*
 * Copyright 2014 Amazon.com,
 * Inc. or its affiliates. All Rights Reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the
 * "License"). You may not use this file except in compliance
 * with the License. A copy of the License is located at
 *
 * http://aws.amazon.com/apache2.0/
 *
 * or in the "license" file accompanying this file. This file is
 * distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR
 *
 CONDITIONS OF ANY KIND, either express or implied. See the
 * License for the specific language governing permissions and
 * limitations under the License.
 */

//
//  AMAZONAmazonMobileAdsImpl.m
//

#import "AMAZONAmazonMobileAdsImpl.h"
#import "AMAZONAmazonMobileAdsObjectiveCController.h"

@interface AMAZONAmazonMobileAdsImpl ()

@property (nonatomic, strong) AmazonAdView *amazonAdView;
@property (nonatomic, strong) NSMutableDictionary *amznAds;
@property (nonatomic, strong) NSMutableArray *interstitialAds;
@property (nonatomic, strong) NSMutableArray *loadedInterstitialAds;
@property (nonatomic, strong) AmazonAdOptions *options;
@property (nonatomic, strong) UIViewController *topController;
@property (nonatomic) float amazonAdCenterYOffset;

@property (nonatomic) BOOL isTestRequest;
@property (nonatomic) BOOL usesGeoLocation;

@end

@implementation AMAZONAmazonMobileAdsImpl

@synthesize controller = _controller;

- (instancetype)init {
    if (self = [super init]) {
    }
    return self;
}

- (void)setApplicationKey:(AMAZONApplicationKey *)applicationKey error:(NSError *__autoreleasing *)errorPtr {
    
    [[AmazonAdRegistration sharedRegistration] setAppKey:applicationKey.stringValue];
    
    self.amazonAdView = [AmazonAdView amazonAdViewWithAdSize:AmazonAdSize_320x50];
    self.amznAds = [[NSMutableDictionary alloc] init];
    self.interstitialAds = [[NSMutableArray alloc] init];
    self.loadedInterstitialAds = [[NSMutableArray alloc] init];
    self.options = [AmazonAdOptions options];
    self.topController = [self.controller currentViewController];
    
    self.amazonAdCenterYOffset = 0.0;
    
    if ([[UIDevice currentDevice] userInterfaceIdiom] == UIUserInterfaceIdiomPhone) {
        self.amazonAdCenterYOffset = 25.0;
    } else {
        if (UIInterfaceOrientationIsPortrait([[UIApplication sharedApplication] statusBarOrientation])) {
            self.amazonAdCenterYOffset = 45.0;
        } else {
            self.amazonAdCenterYOffset = 25.0;
        }
    }
    
    return;
}

- (void)registerApplicationWithError:(NSError *__autoreleasing *)errorPtr {
    return;
}

- (void)enableLogging:(AMAZONShouldEnable *)shouldEnable error:(NSError *__autoreleasing *)errorPtr {
    
    [[AmazonAdRegistration sharedRegistration] setLogging:shouldEnable.booleanValue];
    
    return;
}

- (void)enableTesting:(AMAZONShouldEnable *)shouldEnable error:(NSError *__autoreleasing *)errorPtr {
    
    self.isTestRequest = shouldEnable.booleanValue;
    
    return;
}

- (void)enableGeoLocation:(AMAZONShouldEnable *)shouldEnable error:(NSError *__autoreleasing *)errorPtr {
    
    self.usesGeoLocation = shouldEnable.booleanValue;
    
    return;
}

- (AMAZONAd *)createFloatingBannerAd:(AMAZONPlacement *)placement error:(NSError *__autoreleasing *)errorPtr {
    // TODO: sync operation implementation here, replace the return statement
    
    AMAZONAd * ret = [[AMAZONAd alloc] init];
    if(ret == nil) {
        if(errorPtr) {
            NSString* domain = @"com.amazon.AmazonMobileAds.ErrorDomain";
            NSUInteger errorCode = createFloatingBannerAdError;
            NSMutableDictionary* userInfo = [NSMutableDictionary dictionary];
            [userInfo setObject:@"An error occured in CreateFloatingBannerAd" forKey:NSLocalizedDescriptionKey];
            *errorPtr = [[NSError alloc] initWithDomain:domain code:errorCode userInfo:userInfo];
        }
        return nil;
    }
    
    // Create AmazonAdView
    self.amazonAdView = [AmazonAdView amazonAdViewWithAdSize:AmazonAdSize_320x50];
    
    UIUserInterfaceIdiom userInterfaceIdiom = [[UIDevice currentDevice] userInterfaceIdiom];
    UIInterfaceOrientation interfaceOrientation = [[UIApplication sharedApplication] statusBarOrientation];
    
    // IMPORTANT: Create the AmazonAd view for requesting an ad of appropriate size based on the current device and orientation if necessary
    if (userInterfaceIdiom != UIUserInterfaceIdiomPhone) {
        if (UIInterfaceOrientationIsPortrait(interfaceOrientation)) {
            // Create the Amazon Ad view of size 728x90 for iPad while in portrait mode
            self.amazonAdView = [AmazonAdView amazonAdViewWithAdSize:AmazonAdSize_728x90];
            
            // Reposition and resize the Amazon Ad view to center at bottom
            [self.amazonAdView setCenter:CGPointMake(self.view.bounds.size.width / 2.0, self.view.bounds.size.height - 45.0)];
        } else {
            // Create the Amazon Ad view of size 1024x50 for iPad while in landscape mode
            self.amazonAdView = [AmazonAdView amazonAdViewWithAdSize:AmazonAdSize_1024x50];
            
            // Reposition and resize the Amazon Ad view to center at bottom
            [self.amazonAdView setCenter:CGPointMake(self.view.bounds.size.width / 2.0, self.view.bounds.size.height - 25.0)];
        }
    }
    
    if (placement.adFit == FIT_SCREEN_WIDTH) {
        CGFloat scaleFactor = self.topController.view.bounds.size.width / self.amazonAdView.frame.size.width;
        self.amazonAdView.transform = CGAffineTransformScale(CGAffineTransformIdentity, scaleFactor, scaleFactor);
    }
    
    CGFloat adX = 0.0;
    if (placement.horizontalAlign == CENTER)
        adX = (self.topController.view.bounds.size.width - self.amazonAdView.frame.size.width)/2;
    else if (placement.horizontalAlign == LEFT)
        adX = 0.0;
    else
        adX = self.topController.view.bounds.size.width - self.amazonAdView.frame.size.width;
    
    CGFloat adY = (placement.dock == TOP) ? 20 : self.topController.view.bounds.size.height - self.amazonAdView.frame.size.height;
    
    self.amazonAdView.frame = CGRectMake(adX, adY, self.amazonAdView.frame.size.width, self.amazonAdView.frame.size.height);
    
    self.amazonAdView.delegate = self;
    
    ret.adType = FLOATING;
    ret.identifier = self.amznAds.count;
    
    [self.amznAds setObject:self.amazonAdView forKey:@(ret.identifier)];
    
    return ret;
}

- (AMAZONLoadingStarted *)loadAndShowFloatingBannerAd:(AMAZONAd *)ad error:(NSError *__autoreleasing *)errorPtr {
    
    AMAZONLoadingStarted * ret = [[AMAZONLoadingStarted alloc] init];
    if(ret == nil) {
        if(errorPtr) {
            NSString* domain = @"com.amazon.AmazonMobileAds.ErrorDomain";
            NSUInteger errorCode = loadAndShowFloatingBannerAdError;
            NSMutableDictionary* userInfo = [NSMutableDictionary dictionary];
            // TODO: change the following setObject parameter to a more appropriate error; this is the message that is passed up to the API layer
            [userInfo setObject:@"An error occured in LoadAndShowFloatingBannerAd" forKey:NSLocalizedDescriptionKey];
            *errorPtr = [[NSError alloc] initWithDomain:domain code:errorCode userInfo:userInfo];
        }
        return nil;
    }
    
    if ([[self.amznAds allKeys] containsObject:@(ad.identifier)]) {
        AmazonAdView *currAdView = self.amznAds[@(ad.identifier)];
        
        self.options = [AmazonAdOptions options];
        
        NSString *slotParameter = [self.controller getCrossPlatformTool];
        if ([slotParameter caseInsensitiveCompare:@"Unity"] == NSOrderedSame)
            slotParameter = @"UnityAMZN";
        else if ([slotParameter caseInsensitiveCompare:@"Cordova"] == NSOrderedSame)
            slotParameter = @"CordovaAMZN";
        else if ([slotParameter caseInsensitiveCompare:@"AIR"] == NSOrderedSame)
            slotParameter = @"AIRAMZN";
        else if ([slotParameter caseInsensitiveCompare:@"Xamarin"] == NSOrderedSame)
            slotParameter = @"XamarinAMZN";
        else
            slotParameter = @"cptPlugin";
        
        [self.options setAdvancedOption:slotParameter forKey:@"slot"];
        
        self.options.isTestRequest = self.isTestRequest;
        self.options.usesGeoLocation = self.usesGeoLocation;
        
        // Animate sliding Amazon Ad view off the scree with a 500 ms duration
        [UIView animateWithDuration:0.6
                         animations:^{
                             if (currAdView.frame.origin.y <= 21)
                                 currAdView.center = CGPointMake(currAdView.center.x, - self.amazonAdCenterYOffset);
                             else
                                 currAdView.center = CGPointMake(currAdView.center.x, self.view.bounds.size.height + self.amazonAdCenterYOffset);
                         }
         ];
        
        // Load an amazon ad with the given options
        [currAdView loadAd:self.options];
        ret.booleanValue = YES;
    } else {
        ret.booleanValue = NO;
    }
    
    return ret;
}

- (AMAZONAd *)createInterstitialAdWithError:(NSError *__autoreleasing *)errorPtr {
    
    AmazonAdInterstitial *interstitial = [AmazonAdInterstitial amazonAdInterstitial];
    interstitial.delegate = self;
    
    [self.interstitialAds insertObject:interstitial atIndex:self.interstitialAds.count];
    
    AMAZONAd * ret = [[AMAZONAd alloc] init];
    if(ret == nil) {
        if(errorPtr) {
            NSString* domain = @"com.amazon.AmazonMobileAds.ErrorDomain";
            NSUInteger errorCode = createInterstitialAdError;
            NSMutableDictionary* userInfo = [NSMutableDictionary dictionary];
            // TODO: change the following setObject parameter to a more appropriate error; this is the message that is passed up to the API layer
            [userInfo setObject:@"An error occured in CreateInterstitialAd" forKey:NSLocalizedDescriptionKey];
            *errorPtr = [[NSError alloc] initWithDomain:domain code:errorCode userInfo:userInfo];
        }
        return nil;
    }
    ret.adType = INTERSTITIAL;
    return ret;
}

- (AMAZONLoadingStarted *)loadInterstitialAdWithError:(NSError *__autoreleasing *)errorPtr {
    
    AMAZONLoadingStarted * ret = [[AMAZONLoadingStarted alloc] init];
    if(ret == nil) {
        if(errorPtr) {
            NSString* domain = @"com.amazon.AmazonMobileAds.ErrorDomain";
            NSUInteger errorCode = loadInterstitialAdError;
            NSMutableDictionary* userInfo = [NSMutableDictionary dictionary];
            // TODO: change the following setObject parameter to a more appropriate error; this is the message that is passed up to the API layer
            [userInfo setObject:@"An error occured in LoadInterstitialAd" forKey:NSLocalizedDescriptionKey];
            *errorPtr = [[NSError alloc] initWithDomain:domain code:errorCode userInfo:userInfo];
        }
        return nil;
    }
    
    if (self.interstitialAds.count !=0 ) {
        self.options = [AmazonAdOptions options];
        self.options.isTestRequest = self.isTestRequest;
        self.options.usesGeoLocation = self.usesGeoLocation;
        [[self.interstitialAds lastObject] load:self.options];
        ret.booleanValue = YES;
    } else {
        ret.booleanValue = NO;
    }
    
    return ret;
}

- (AMAZONAdShown *)showInterstitialAdWithError:(NSError *__autoreleasing *)errorPtr {
    
    AMAZONAdShown * ret = [[AMAZONAdShown alloc] init];
    if(ret == nil) {
        if(errorPtr) {
            NSString* domain = @"com.amazon.AmazonMobileAds.ErrorDomain";
            NSUInteger errorCode = showInterstitialAdError;
            NSMutableDictionary* userInfo = [NSMutableDictionary dictionary];
            // TODO: change the following setObject parameter to a more appropriate error; this is the message that is passed up to the API layer
            [userInfo setObject:@"An error occured in ShowInterstitialAd" forKey:NSLocalizedDescriptionKey];
            *errorPtr = [[NSError alloc] initWithDomain:domain code:errorCode userInfo:userInfo];
        }
        return nil;
    }
    
    NSError *isAdReadyError;
    AMAZONIsReady *isReady = [self isInterstitialAdReadyWithError:&isAdReadyError];
    if (isReady.booleanValue) {
        [[self.loadedInterstitialAds lastObject] presentFromViewController:self.topController];
        ret.booleanValue = YES;
    } else {
        ret.booleanValue = NO;
    }
    
    return ret;
}

- (void)closeFloatingBannerAd:(AMAZONAd *)ad error:(NSError *__autoreleasing *)errorPtr {
    
    [self.amznAds[@(ad.identifier)] removeFromSuperview];
    
    return;
}

- (AMAZONIsReady *)isInterstitialAdReadyWithError:(NSError *__autoreleasing *)errorPtr {
    
    AMAZONIsReady * ret = [[AMAZONIsReady alloc] init];
    if(ret == nil) {
        if(errorPtr) {
            NSString* domain = @"com.amazon.AmazonMobileAds.ErrorDomain";
            NSUInteger errorCode = isInterstitialAdReadyError;
            NSMutableDictionary* userInfo = [NSMutableDictionary dictionary];
            // TODO: change the following setObject parameter to a more appropriate error; this is the message that is passed up to the API layer
            [userInfo setObject:@"An error occured in IsInterstitialAdReady" forKey:NSLocalizedDescriptionKey];
            *errorPtr = [[NSError alloc] initWithDomain:domain code:errorCode userInfo:userInfo];
        }
        return nil;
    }
    ret.booleanValue = (self.loadedInterstitialAds && self.loadedInterstitialAds.count);
    return ret;
}

- (AMAZONIsEqual *)areAdsEqual:(AMAZONAdPair *)adPair error:(NSError *__autoreleasing *)errorPtr {
    
    NSString *key1 = [NSString stringWithFormat:@"%ld", adPair.adOne.identifier];
    NSString *key2 = [NSString stringWithFormat:@"%ld", adPair.adTwo.identifier];
    
    AmazonAdView *ad1 = self.amznAds[key1];
    AmazonAdView *ad2 = self.amznAds[key2];
    
    AMAZONIsEqual * ret = [[AMAZONIsEqual alloc] init];
    if(ret == nil) {
        if(errorPtr) {
            NSString* domain = @"com.amazon.AmazonMobileAds.ErrorDomain";
            NSUInteger errorCode = areAdsEqualError;
            NSMutableDictionary* userInfo = [NSMutableDictionary dictionary];
            // TODO: change the following setObject parameter to a more appropriate error; this is the message that is passed up to the API layer
            [userInfo setObject:@"An error occured in AreAdsEqual" forKey:NSLocalizedDescriptionKey];
            *errorPtr = [[NSError alloc] initWithDomain:domain code:errorCode userInfo:userInfo];
        }
        return nil;
    }
    ret.booleanValue = (ad1 == ad2);
    return ret;
}

#pragma mark AmazonAdViewDelegate

- (UIViewController *)viewControllerForPresentingModalView
{
    return self.topController;
}

- (void)adViewDidLoad:(AmazonAdView *)view
{
    [self.topController.view addSubview:view];
    
    // Animate sliding Amazon Ad view into the scree from bottom with a 500 ms duration
    [UIView animateWithDuration:0.6
                     animations:^{
                         if (view.frame.origin.y <= 21)
                             view.center = CGPointMake(view.center.x, 20.0 + self.amazonAdCenterYOffset);
                         else
                             view.center = CGPointMake(view.center.x, self.view.bounds.size.height - self.amazonAdCenterYOffset);
                     }
     ];
    
    NSLog(@"Ad loaded");
}

- (void)adViewDidCollapse:(AmazonAdView *)view
{
    NSLog(@"Ad has collapsed");
}

- (void)adViewDidFailToLoad:(AmazonAdView *)view withError:(AmazonAdError *)error
{
    NSLog(@"Ad Failed to load. Error code %d: %@", error.errorCode, error.errorDescription);
}

- (void)adViewWillExpand:(AmazonAdView *)view
{
    NSLog(@"Ad will expand");
}

#pragma mark - AmazonAdInterstitialDelegate

- (void)interstitialDidLoad:(AmazonAdInterstitial *)interstitial
{
    [self.loadedInterstitialAds insertObject:interstitial atIndex:self.loadedInterstitialAds.count];
    
    [self.interstitialAds removeObject:interstitial];
    NSLog(@"Interstial loaded.");
}

- (void)interstitialDidFailToLoad:(AmazonAdInterstitial *)interstitial withError:(AmazonAdError *)error
{
    NSLog(@"Interstitial failed to load.");
}

- (void)interstitialWillPresent:(AmazonAdInterstitial *)interstitial
{
    NSLog(@"Interstitial will be presented.");
}

- (void)interstitialDidPresent:(AmazonAdInterstitial *)interstitial
{
    NSLog(@"Interstitial has been presented.");
}

- (void)interstitialWillDismiss:(AmazonAdInterstitial *)interstitial
{
    NSLog(@"Interstitial will be dismissed.");
}

- (void)interstitialDidDismiss:(AmazonAdInterstitial *)interstitial
{
    [self.loadedInterstitialAds removeLastObject];
    NSLog(@"Interstitial has been dismissed.");
}

@end