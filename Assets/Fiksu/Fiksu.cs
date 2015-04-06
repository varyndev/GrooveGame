using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class Fiksu : MonoBehaviour {
	
//public static functions to communicate with the Fiksu SDK
	public static void Initialize(){
		if(!Application.isEditor){
			if(!initialized){
				#if UNITY_ANDROID
				AndroidJavaObject jniApplication = jniCurrentActivity.Call<AndroidJavaObject>("getApplication");
				jniFiksuTrackingManager.CallStatic("initialize",jniApplication);
				
				jniFiksuRegistrationEvents = new Dictionary<FiksuRegistrationEvent, AndroidJavaObject>();
				AndroidJavaClass jniFiksuRegistrationEventClass = new AndroidJavaClass("com.fiksu.asotracking.FiksuTrackingManager$RegistrationEvent");
				jniFiksuRegistrationEvents.Add(FiksuRegistrationEvent.EVENT1,jniFiksuRegistrationEventClass.GetStatic<AndroidJavaObject>("EVENT1"));
				jniFiksuRegistrationEvents.Add(FiksuRegistrationEvent.EVENT2,jniFiksuRegistrationEventClass.GetStatic<AndroidJavaObject>("EVENT2"));
				jniFiksuRegistrationEvents.Add(FiksuRegistrationEvent.EVENT3,jniFiksuRegistrationEventClass.GetStatic<AndroidJavaObject>("EVENT3"));
				
				jniFiksuPurchaseEvents = new Dictionary<FiksuPurchaseEvent, AndroidJavaObject>();
				AndroidJavaClass jniFiksuPurchaseEventClass = new AndroidJavaClass("com.fiksu.asotracking.FiksuTrackingManager$PurchaseEvent");
				jniFiksuPurchaseEvents.Add(FiksuPurchaseEvent.EVENT1,jniFiksuPurchaseEventClass.GetStatic<AndroidJavaObject>("EVENT1"));
				jniFiksuPurchaseEvents.Add(FiksuPurchaseEvent.EVENT2,jniFiksuPurchaseEventClass.GetStatic<AndroidJavaObject>("EVENT2"));
				jniFiksuPurchaseEvents.Add(FiksuPurchaseEvent.EVENT3,jniFiksuPurchaseEventClass.GetStatic<AndroidJavaObject>("EVENT3"));
				jniFiksuPurchaseEvents.Add(FiksuPurchaseEvent.EVENT4,jniFiksuPurchaseEventClass.GetStatic<AndroidJavaObject>("EVENT4"));
				jniFiksuPurchaseEvents.Add(FiksuPurchaseEvent.EVENT5,jniFiksuPurchaseEventClass.GetStatic<AndroidJavaObject>("EVENT5"));
				
				#endif
				//initializing on iOS happens in Xcode. (PostBuildScript adds this functionality)
			}
		}
		initialized = true;
	}
	
	public enum FiksuRegistrationEvent{
		EVENT1 = 1,
		EVENT2,
		EVENT3,
	};
	
	public enum FiksuPurchaseEvent{
		EVENT1 = 1,
		EVENT2,
		EVENT3,
		EVENT4,
		EVENT5
	}
	
	public static void UploadRegistrationEvent(string username){
		if(!Application.isEditor && initialized){
			#if UNITY_ANDROID
			jniFiksuTrackingManager.CallStatic("uploadRegistration",jniCurrentActivity,jniFiksuRegistrationEvents[FiksuRegistrationEvent.EVENT1]);
			#elif UNITY_IPHONE
			UploadRegistrationEvent_(username);
			#endif
		}
	}
	
	public static void UploadRegistration(FiksuRegistrationEvent registrationEvent){
		if(!Application.isEditor && initialized){
			#if UNITY_ANDROID
			jniFiksuTrackingManager.CallStatic("uploadRegistration",jniCurrentActivity,jniFiksuRegistrationEvents[registrationEvent]);
			#elif UNITY_IPHONE
			UploadRegistration_((int)registrationEvent);
			#endif
		}
	}
	
	public static void UploadPurchase(string username, double price, string currency){
		if(!Application.isEditor && initialized){
			if(username == null){
				username = "";
			}
			if(currency == null){
				currency = "";
			}
			#if UNITY_ANDROID
			jniFiksuTrackingManager.CallStatic("uploadPurchaseEvent",jniCurrentActivity,username,price,currency);
			#elif UNITY_IPHONE
			UploadPurchaseEventWithUsername_(username, price, currency);
			#endif
		}
	}
	
	public static void UploadPurchase(FiksuPurchaseEvent purchaseEvent, double price, string currency){
		if(!Application.isEditor && initialized){
			if(currency == null){
				currency = "";
			}
			#if UNITY_ANDROID
			jniFiksuTrackingManager.CallStatic("uploadPurchase",jniCurrentActivity,jniFiksuPurchaseEvents[purchaseEvent],price,currency);
			#elif UNITY_IPHONE
			UploadPurchase_((int)purchaseEvent, price, currency);
			#endif
		}
	}
	
	public static void UploadPurchaseEvent(string username, string currency){
		if(!Application.isEditor && initialized){
			if(username == null){
				username = "";
			}
			if(currency == null){
				currency = "";
			}
			#if UNITY_ANDROID
			jniFiksuTrackingManager.CallStatic("uploadPurchaseEvent",jniCurrentActivity,username,null,currency);
			#elif UNITY_IPHONE
			UploadPurchaseEventNoPrice_(username, currency);
			#endif
		}
	}
	
	public static void UploadCustomEvent(){
		if(!Application.isEditor && initialized){
			#if UNITY_ANDROID
			//jniFiksuTrackingManager.CallStatic("uploadCustomEvent",jniCurrentActivity);
			#elif UNITY_IPHONE
			UploadCustomEvent_();
			#endif
		}
	}
	
	public static void SetClientID(string clientID){
		if(!Application.isEditor && initialized){
			#if UNITY_ANDROID
			jniFiksuTrackingManager.CallStatic("setClientId",jniCurrentActivity,clientID);
			#elif UNITY_IPHONE
			SetFiksuClientID_(clientID);
			#endif
		}
	}
	
	public static void SetAppTrackingEnabled(bool enabled){
		if(!Application.isEditor && initialized){
			#if UNITY_ANDROID
			jniFiksuTrackingManager.CallStatic("setAppTrackingEnabled",jniCurrentActivity,enabled);
			#elif UNITY_IPHONE
			SetAppTrackingEnabled_(enabled);
			#endif
		}
	}
	
	public static bool IsAppTrackingEnabled(){
		if(!Application.isEditor && initialized){
			#if UNITY_ANDROID
			return jniFiksuTrackingManager.CallStatic<bool>("isAppTrackingEnabled");
			#elif UNITY_IPHONE
			return IsAppTrackingEnabled_();
			#endif
		}
		return false;
	}
	
	//Monobehavior functions to start the Fiksu SDK	automaticly
	void Awake(){
		if(initialized){
			Destroy(gameObject);
		}else{	
			DontDestroyOnLoad(gameObject);
			#if UNITY_ANDROID
			if(!Application.isEditor){
				jniFiksuTrackingManager = new AndroidJavaClass("com.fiksu.asotracking.FiksuTrackingManager");
				AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"); 
				jniCurrentActivity = jc.GetStatic<AndroidJavaObject>("currentActivity");
			}
			#endif		
			Initialize();
		}
	}
	
	/*
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	*/

//private function and vars to connect to the sdks
//TODO make private again.
public static bool initialized = false;
#if UNITY_ANDROID
private static AndroidJavaClass jniFiksuTrackingManager;
private static AndroidJavaObject jniCurrentActivity;
private static Dictionary<FiksuRegistrationEvent,AndroidJavaObject> jniFiksuRegistrationEvents;
private static Dictionary<FiksuPurchaseEvent,AndroidJavaObject> jniFiksuPurchaseEvents;
#elif UNITY_IOS
	[DllImport ("__Internal")]
	private static extern void UploadRegistrationEvent_(string username);
	
	[DllImport ("__Internal")]
	private static extern void UploadRegistration_(int eventNumber);
	
	[DllImport ("__Internal")]
	private static extern void UploadPurchaseEventWithUsername_(string username, double price, string currency);
	
	[DllImport ("__Internal")]
	private static extern void UploadPurchaseEventNoPrice_(string username, string currency);
	
	[DllImport ("__Internal")]
	private static extern void UploadPurchase_(int eventNumber, double price, string currency);
	
	[DllImport ("__Internal")]
	private static extern void UploadCustomEvent_();
	
	[DllImport ("__Internal")]
	private static extern void SetFiksuClientID_(string clientID);
	
	[DllImport ("__Internal")]
	private static extern void SetAppTrackingEnabled_(bool enabled);
	
	[DllImport ("__Internal")]
	private static extern bool IsAppTrackingEnabled_();
#endif

}
