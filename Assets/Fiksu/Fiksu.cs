using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

// Only need the UnityEngine framework for Android implementation
#if UNITY_ANDROID
using UnityEngine;
#endif

public class Fiksu 
{
    public static string ITunesApplicationIDKey = "ITunesApplicationIDKey";
    
    public static string DebugModeEnabledKey = "DebugModeEnabledKey";
    
    public static string ProductIdentifiersKey = "ProductIdentifiersKey";

    #if UNITY_ANDROID
    private static AndroidJavaClass _jniFiksuTrackingManager;
    
    private static Dictionary<FiksuRegistrationEvent,AndroidJavaObject> _jniFiksuRegistrationEvents;
    
    private static Dictionary<FiksuPurchaseEvent,AndroidJavaObject> _jniFiksuPurchaseEvents;
    #endif

    public enum FiksuRegistrationEvent
    {
        EVENT1 = 1,
        EVENT2,
        EVENT3,
    };

    public enum FiksuPurchaseEvent
    {
        EVENT1 = 1,
        EVENT2,
        EVENT3,
        EVENT4,
        EVENT5
    };

    static Fiksu() 
    {
        #if UNITY_ANDROID
        _jniFiksuTrackingManager = new AndroidJavaClass("com.fiksu.asotracking.FiksuTrackingManager");
        _jniFiksuRegistrationEvents = new Dictionary<FiksuRegistrationEvent, AndroidJavaObject>();
        AndroidJavaClass jniFiksuRegistrationEventClass = new AndroidJavaClass("com.fiksu.asotracking.FiksuTrackingManager$RegistrationEvent");
        _jniFiksuRegistrationEvents.Add(FiksuRegistrationEvent.EVENT1,jniFiksuRegistrationEventClass.GetStatic<AndroidJavaObject>("EVENT1"));
        _jniFiksuRegistrationEvents.Add(FiksuRegistrationEvent.EVENT2,jniFiksuRegistrationEventClass.GetStatic<AndroidJavaObject>("EVENT2"));
        _jniFiksuRegistrationEvents.Add(FiksuRegistrationEvent.EVENT3,jniFiksuRegistrationEventClass.GetStatic<AndroidJavaObject>("EVENT3"));

        _jniFiksuPurchaseEvents = new Dictionary<FiksuPurchaseEvent, AndroidJavaObject>();
        AndroidJavaClass jniFiksuPurchaseEventClass = new AndroidJavaClass("com.fiksu.asotracking.FiksuTrackingManager$PurchaseEvent");
        _jniFiksuPurchaseEvents.Add(FiksuPurchaseEvent.EVENT1,jniFiksuPurchaseEventClass.GetStatic<AndroidJavaObject>("EVENT1"));
        _jniFiksuPurchaseEvents.Add(FiksuPurchaseEvent.EVENT2,jniFiksuPurchaseEventClass.GetStatic<AndroidJavaObject>("EVENT2"));
        _jniFiksuPurchaseEvents.Add(FiksuPurchaseEvent.EVENT3,jniFiksuPurchaseEventClass.GetStatic<AndroidJavaObject>("EVENT3"));
        _jniFiksuPurchaseEvents.Add(FiksuPurchaseEvent.EVENT4,jniFiksuPurchaseEventClass.GetStatic<AndroidJavaObject>("EVENT4"));
        _jniFiksuPurchaseEvents.Add(FiksuPurchaseEvent.EVENT5,jniFiksuPurchaseEventClass.GetStatic<AndroidJavaObject>("EVENT5"));
        #endif
    }

    public static void Initialize(Dictionary<string, object> config)
    {
          #if UNITY_ANDROID
          _jniFiksuTrackingManager.CallStatic("initialize",currentActivity().Call<AndroidJavaObject>("getApplication"));
          #endif

          #if UNITY_IPHONE
          FiksuInitialize(
              itunesID: (string)config[Fiksu.ITunesApplicationIDKey], 
              debugMode: (bool)config[Fiksu.DebugModeEnabledKey], 
              productIDs: (string[])config[Fiksu.ProductIdentifiersKey], 
              productIDCount: ((string[])config[Fiksu.ProductIdentifiersKey]).Length
          );
          #endif
    }

    public static void UploadRegistrationEvent(string username)
    {
        #if UNITY_ANDROID
        _jniFiksuTrackingManager.CallStatic("uploadRegistration",currentActivity(),_jniFiksuRegistrationEvents[FiksuRegistrationEvent.EVENT1]);
        #elif UNITY_IPHONE
        FiksuUploadRegistrationEvent(username);
        #endif
    }

    public static void UploadRegistration(FiksuRegistrationEvent registrationEvent)
    {
        #if UNITY_ANDROID
        _jniFiksuTrackingManager.CallStatic("uploadRegistration",currentActivity(),_jniFiksuRegistrationEvents[registrationEvent]);
        #elif UNITY_IPHONE
        FiksuUploadRegistration((int)registrationEvent);
        #endif
    }

    public static void UploadPurchase(string username, double price, string currency)
    {
        #if UNITY_ANDROID
        _jniFiksuTrackingManager.CallStatic("uploadPurchaseEvent",currentActivity(),username,price,currency);
        #elif UNITY_IPHONE
        FiksuUploadPurchaseEventWithUsername(username, price, currency);
        #endif
    }

    public static void UploadPurchaseEvent(string username, double price, string currency)
    {
        #if UNITY_ANDROID
        _jniFiksuTrackingManager.CallStatic("uploadPurchaseEvent",currentActivity(),username,price,currency);
        #elif UNITY_IPHONE
        FiksuUploadPurchaseEventWithUsername(username, price, currency);
        #endif
    }

    public static void UploadPurchase(FiksuPurchaseEvent purchaseEvent, double price, string currency)
    {
        
        #if UNITY_ANDROID
        _jniFiksuTrackingManager.CallStatic("uploadPurchase",currentActivity(),_jniFiksuPurchaseEvents[purchaseEvent],price,currency);
        #elif UNITY_IPHONE
        FiksuUploadPurchase((int)purchaseEvent, price, currency);
        #endif
    }

    public static void UploadPurchaseEvent(string username, string currency)
    {

        #if UNITY_ANDROID
        _jniFiksuTrackingManager.CallStatic("uploadPurchaseEvent",currentActivity(),username,null,currency);
        #elif UNITY_IPHONE
        FiksuUploadPurchaseEventNoPrice(username, currency);
        #endif
    }

    public static void UploadCustomEvent()
    {
        #if UNITY_ANDROID
        _jniFiksuTrackingManager.CallStatic("uploadCustomEvent",currentActivity());
        #elif UNITY_IPHONE
        FiksuUploadCustomEvent();
        #endif
    }

    public static void SetClientID(string clientID)
    {
        #if UNITY_ANDROID
        _jniFiksuTrackingManager.CallStatic("setClientId",currentActivity(),clientID);
        #elif UNITY_IPHONE
        FiksuSetFiksuClientID(clientID);
        #endif
    }

    public static void SetAppTrackingEnabled(bool enabled)
    {
        #if UNITY_ANDROID
        _jniFiksuTrackingManager.CallStatic("setAppTrackingEnabled",currentActivity(),enabled);
        #elif UNITY_IPHONE
        FiksuSetAppTrackingEnabled(enabled);
        #endif
    }

    public static bool IsAppTrackingEnabled()
    {
        #if UNITY_ANDROID
        return _jniFiksuTrackingManager.CallStatic<bool>("isAppTrackingEnabled");
        #elif UNITY_IPHONE
        return FiksuIsAppTrackingEnabled();
        #else
        return false;
        #endif
    }

    #if UNITY_ANDROID
    private static AndroidJavaObject currentActivity() 
    {
        return new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
    }
    #endif

    #if UNITY_IOS
    [DllImport ("__Internal")]
    private static extern void FiksuInitialize(string itunesID, bool debugMode, string[] productIDs, int productIDCount);

    [DllImport ("__Internal")]
    private static extern void FiksuUploadRegistrationEvent(string username);

    [DllImport ("__Internal")]
    private static extern void FiksuUploadRegistration(int eventNumber);

    [DllImport ("__Internal")]
    private static extern void FiksuUploadPurchaseEventWithUsername(string username, double price, string currency);

    [DllImport ("__Internal")]
    private static extern void FiksuUploadPurchaseEventNoPrice(string username, string currency);

    [DllImport ("__Internal")]
    private static extern void FiksuUploadPurchase(int eventNumber, double price, string currency);

    [DllImport ("__Internal")]
    private static extern void FiksuUploadCustomEvent();

    [DllImport ("__Internal")]
    private static extern void FiksuSetFiksuClientID(string clientID);

    [DllImport ("__Internal")]
    private static extern void FiksuSetAppTrackingEnabled(bool enabled);

    [DllImport ("__Internal")]
    private static extern bool FiksuIsAppTrackingEnabled();
    #endif
}