using UnityEngine;
using System.Collections;

public class Example : MonoBehaviour {
	
	private string registrationEventNumber = "";
	
	private string purchaseEventNumber = "";
	private string purchasePrice = "2.5";
	private string purchaseCurrency = "USD";
	
	private string clientID = "";
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Space)){
			Application.LoadLevel(1);
		}
	}
	
	void OnGUI() {
		TextAnchor backupAlignment = GUI.skin.label.alignment;
		GUI.skin.label.alignment = TextAnchor.MiddleLeft;
		GUI.Label(new Rect(Screen.width/4f,Screen.height/17f,Screen.width/8f,Screen.height/17f), "Event number (1-3): ");
		registrationEventNumber = GUI.TextField(new Rect(Screen.width/8f*3,Screen.height/17f,Screen.width/8f*3,Screen.height/17f),registrationEventNumber);
		if(GUI.Button(new Rect(Screen.width/4f,Screen.height/17f*2f,Screen.width/2f,Screen.height/17f),"Upload Registration Event")){
			int eventNumber = 1;
			if(int.TryParse(registrationEventNumber,out eventNumber)){
				if(eventNumber >= 1 && eventNumber <= 3){
					Fiksu.UploadRegistration((Fiksu.FiksuRegistrationEvent)eventNumber);
				}
			}else{
				Fiksu.UploadRegistration(Fiksu.FiksuRegistrationEvent.EVENT1);
			}
		}
		
		GUI.Label(new Rect(Screen.width/4f,Screen.height/17f*4,Screen.width/8f,Screen.height/17f), "Event number (1-5): ");
		purchaseEventNumber = GUI.TextField(new Rect(Screen.width/8f*3,Screen.height/17f*4,Screen.width/8f*3,Screen.height/17f),purchaseEventNumber);
		GUI.Label(new Rect(Screen.width/4f,Screen.height/17f*5,Screen.width/8f,Screen.height/17f), "Price: ");
		purchasePrice = GUI.TextField(new Rect(Screen.width/8f*3,Screen.height/17f*5,Screen.width/8f*3,Screen.height/17f),purchasePrice);
		GUI.Label(new Rect(Screen.width/4f,Screen.height/17f*6,Screen.width/8f,Screen.height/17f), "Currency: ");
		purchaseCurrency = GUI.TextField(new Rect(Screen.width/8f*3,Screen.height/17f*6,Screen.width/8f*3,Screen.height/17f),purchaseCurrency);
		if(GUI.Button(new Rect(Screen.width/4f,Screen.height/17f*7f,Screen.width/2f,Screen.height/17f),"Upload Purchase Event")){
			double price = 0f;
			if(!double.TryParse(purchasePrice, out price)){
				price = 0f;
			}
			int eventNumber = 1;
			if(!int.TryParse(purchaseEventNumber, out eventNumber)){
				eventNumber = 1;
			}
			Fiksu.UploadPurchase((Fiksu.FiksuPurchaseEvent)eventNumber,price,purchaseCurrency);
		}
		
		if(GUI.Button(new Rect(Screen.width/4f,Screen.height/17f*9f,Screen.width/2f,Screen.height/17f),"UploadCustomEvent")){
			Fiksu.UploadCustomEvent();
		}
		
		GUI.Label(new Rect(Screen.width/4f,Screen.height/17f*11,Screen.width/8f,Screen.height/17f), "ClientID: ");
		clientID = GUI.TextField(new Rect(Screen.width/8f*3,Screen.height/17f*11,Screen.width/8f*3,Screen.height/17f),clientID);
		if(GUI.Button(new Rect(Screen.width/4f,Screen.height/17f*12f,Screen.width/2f,Screen.height/17f),"Set Client ID")){
			Fiksu.SetClientID(clientID);
			Debug.Log ("Fiksu is intitialized: " + Fiksu.initialized);
		}
		
		GUI.skin.label.alignment = TextAnchor.MiddleCenter;
		if(Fiksu.IsAppTrackingEnabled()){
			if(GUI.Button(new Rect(Screen.width/4f,Screen.height/17f*14f,Screen.width/2f,Screen.height/17f),"Disable app tracking")){
				Fiksu.SetAppTrackingEnabled(false);
			}
			GUI.Label(new Rect(Screen.width/4f,Screen.height/17f*15f,Screen.width/2f,Screen.height/17f), "App tracking is enabled");
		}else{
			if(GUI.Button(new Rect(Screen.width/4f,Screen.height/17f*14f,Screen.width/2f,Screen.height/17f),"Enable app tracking")){
				Fiksu.SetAppTrackingEnabled(true);
			}
			GUI.Label(new Rect(Screen.width/4f,Screen.height/17f*15f,Screen.width/2f,Screen.height/17f), "App tracking is disabled");
		}
		
		GUI.skin.label.alignment = backupAlignment;
	}
}
