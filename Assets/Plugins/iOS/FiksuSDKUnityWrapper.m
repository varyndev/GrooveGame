#import <FiksuSDK/FiksuSDK.h>

static NSString * NSStringFromUTF8String(const char * cString) {
    if ( !cString )
        return nil;
    
    return [NSString stringWithUTF8String:cString];
}

NSDictionary * FiksuConfigurationDictionary(const char* itunesApplicationID, bool debugMode, char** productIdentifiers, int productIdentifierCount) {
  NSNumber* isDebugMode = debugMode ? @(YES): @(NO);
  
  NSMutableDictionary* configuration = [[NSMutableDictionary alloc] init];
  if (itunesApplicationID) {
    [configuration setObject:NSStringFromUTF8String(itunesApplicationID) forKey:FiksuTrackingConfigurationAppIdentifierKey];
  }
  
  [configuration setObject:isDebugMode forKey:FiksuTrackingConfigurationDebugModeEnabledKey];
  
  if(productIdentifierCount > 0) {
    NSMutableArray* identifiers = [[NSMutableArray alloc ] init];
    for(int i = 0;i < productIdentifierCount;++i) {
      if (productIdentifiers[i] == nil)
        continue;
      [identifiers addObject:[NSString stringWithUTF8String:productIdentifiers[i]]];
    }
    
    [configuration setObject:identifiers forKey:FiksuTrackingConfigurationProductIdentifiersKey];
  }
  return configuration;
}

void FiksuInitialize(const char* itunesApplicationID, bool debugMode, char** productIdentifiers, int productIdentifierCount){
    [FiksuTrackingManager applicationDidFinishLaunching:nil configuration:FiksuConfigurationDictionary(itunesApplicationID, debugMode, productIdentifiers, productIdentifierCount)];
}

void FiksuUploadRegistrationEvent(const char* username){
    [FiksuTrackingManager uploadRegistrationEvent:NSStringFromUTF8String(username)];
}

void FiksuUploadPurchaseEventWithUsername(const char* username, double price_, const char* currency_){
    [FiksuTrackingManager uploadPurchaseEvent:NSStringFromUTF8String(username) price:price_ currency:NSStringFromUTF8String(currency_)];
}

void FiksuUploadPurchase(int eventNumber, double price_, const char* currency_){
    if( eventNumber > 5 || eventNumber < 1 )
        return;

    FiksuPurchaseEvent array[] = {
        FiksuPurchaseEvent1,
        FiksuPurchaseEvent2,
        FiksuPurchaseEvent3,
        FiksuPurchaseEvent4,
        FiksuPurchaseEvent5
    };
  
    [FiksuTrackingManager uploadPurchase:array[eventNumber-1] price:price_ currency:NSStringFromUTF8String(currency_)];
}

void FiksuUploadPurchaseEventNoPrice(const char* username, const char* currency_){
    [FiksuTrackingManager uploadPurchaseEvent:NSStringFromUTF8String(username) currency:NSStringFromUTF8String(currency_)];
}

void FiksuUploadCustomEvent(){
    [FiksuTrackingManager uploadCustomEvent];
}

void FiksuSetFiksuClientID(const char* clientID){
    [FiksuTrackingManager setClientID:NSStringFromUTF8String(clientID)];
}

void FiksuUploadRegistration(int eventNumber){
    if(eventNumber > 3 || eventNumber < 1)
        return;
    
    FiksuRegistrationEvent array[] = {
        FiksuRegistrationEvent1,
        FiksuRegistrationEvent2,
        FiksuRegistrationEvent3
    };

    [FiksuTrackingManager uploadRegistration:array[eventNumber-1]];
}

void FiksuSetAppTrackingEnabled(bool enabled){
    [FiksuTrackingManager setAppTrackingEnabled:(enabled ? YES : NO)];
} 

bool FiksuIsAppTrackingEnabled(){
  return [FiksuTrackingManager isAppTrackingEnabled] ? true : false;
}