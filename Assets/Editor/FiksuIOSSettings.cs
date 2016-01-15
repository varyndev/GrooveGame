
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;

public class FiksuIOSSettings : EditorWindow
{
	private static bool ShowAddAttributionPlugin = false;
	
	private static List<string> outerStrings = new List<string>();
	private static List<object> values = new List<object>();
	private static List<string> names = new List<string>();
	
	public static bool customURLScheme = false;
	//private static string URLIdentifier = "";
	//private static string iTunesConnectAppID = "";
	//private static bool debugModeEnabled = true;
	//private static bool UDIDEnabled = true;
	//private static bool HTML5Enabled = false;
	//private static bool advertisingIdentifierEnabled = true;
	
	
	// Add menu item named "My Window" to the Window menu
	[MenuItem("Fiksu/iOS Settings")]
	public static void ShowWindow()
	{
		//Show existing window instance. If one doesn't exist, make one.
		EditorWindow.GetWindow(typeof(FiksuIOSSettings));
		LoadSettings();
	}
	
	
	public static void LoadSettings(){
		outerStrings.Clear();
		values.Clear();
		names.Clear();
		string[] text = File.ReadAllLines(GetConfigurationPath() ,System.Text.Encoding.UTF8);
		int index = 0;
		string s = "";
		while(index < text.Length && !text[index].Contains("<dict>")){
			s += text[index] + "\n";
			index++;
		}
		s += text[index] + "\n";
		outerStrings.Add(s);
		index++;
		while(index < text.Length && !text[index].Contains("</dict>")){
			names.Add(text[index].Trim().Replace("<key>","").Replace("</key>",""));
			if(text[index+1].Contains("<string>")){
				values.Add(text[index+1].Trim().Replace("<string>","").Replace("</string>",""));
			}else if(text[index+1].Contains("<true/>")){
				values.Add(true);
			}else if(text[index+1].Contains("<false/>")){
				values.Add(false);
			}
			index += 2;
		}
		s = "";
		for(int i = index; i < text.Length; i++){
			s += text[i] + "\n";
		}
		outerStrings.Add(s);
		
		string[] settings = File.ReadAllLines(GetSettingsPath(),System.Text.Encoding.UTF8);
		customURLScheme = settings[0] == "1";
		
		/*
		
		if(text.Length == 6){
			iTunesConnectAppID = text[0].Trim();
			URLIdentifier = text[1].Trim();
			debugModeEnabled = bool.Parse(text[2].Trim());
			UDIDEnabled = bool.Parse(text[3].Trim());
			HTML5Enabled = bool.Parse(text[4].Trim());
			advertisingIdentifierEnabled = bool.Parse(text[5].Trim());
		}
		*/
	}
	
	void OnFocus(){
		
	}
	
	void OnGUI()
	{
		GUILayout.Label ("Fiksu iOS Settings", EditorStyles.boldLabel);
		for(int i = 0; i < values.Count; i++){
			if(values[i].GetType() == typeof(string)){
				values[i] = (string)EditorGUILayout.TextField(names[i],(string)values[i]);
			}else{
				values[i] = (bool)EditorGUILayout.Toggle(names[i],(bool)values[i]);
			}
		}
		if(ShowAddAttributionPlugin){
			customURLScheme = (bool)EditorGUILayout.Toggle("Add Attribution Plugin",customURLScheme);
		}
		EditorGUILayout.Separator();
		GUILayout.Label ("Fiksu Post Build Settings", EditorStyles.boldLabel);
		if(!EditorPrefs.HasKey("FiksuManualPostBuild")){
			EditorPrefs.SetBool("FiksuManualPostBuild",false);
		}
		EditorPrefs.SetBool("FiksuManualPostBuild",(bool)EditorGUILayout.Toggle("Manual Post Build",EditorPrefs.GetBool("FiksuManualPostBuild")));
		if(GUILayout.Button("Save and close")){
			//testing for valid appId
			bool validAppId = false;
			for(int i = 0; i < values.Count; i++){
				if(names[i] == "itunes_application_id"){
					values[i] = ((string)values[i]).Trim();
					long appId = 0;
					if(((string)values[i]).Length == 9 && long.TryParse((string)values[i],out appId)){
						validAppId = true;
					}
					/*
					else{
						Debug.Log("length: " + ((string)values[i]).Length + ", tryParse: " + long.TryParse((string)values[i],out appId));
					}
					*/
				}
			}
			if(validAppId){
				string text = outerStrings[0];
				for(int i = 0; i < values.Count; i++){
					text += "\t<key>"+names[i]+"</key>\n";
					if(values[i].GetType() == typeof(string)){
						text += "\t<string>"+values[i]+"</string>\n";
					}else{
						if((bool)values[i]){
							text += "\t<true/>\n";
						}else{
							text += "\t<false/>\n";
						}
					}
				}
				text += outerStrings[1];
				string settingsText = "";
				if(customURLScheme){
					settingsText = "1";
				}else{
					settingsText = "0";
				}
				File.WriteAllText(GetConfigurationPath(),text,System.Text.Encoding.UTF8);
				File.WriteAllText(GetSettingsPath(),settingsText,System.Text.Encoding.UTF8);
				Close();
			}else{
				
				EditorUtility.DisplayDialog("Invalid iTunes Connect Application ID","The itunes_connect_application_id should be a 9 digit number.","Ok");
			}
		}
		/*
		iTunesConnectAppID = EditorGUILayout.TextField ("iTunes Connect App ID", iTunesConnectAppID);
		URLIdentifier = EditorGUILayout.TextField ("URL Identifier", URLIdentifier);
		debugModeEnabled = EditorGUILayout.Toggle("Debug Mode Enabled", debugModeEnabled);
		UDIDEnabled = EditorGUILayout.Toggle("UDID Enabled", UDIDEnabled);
		HTML5Enabled = EditorGUILayout.Toggle("HTML5 Enabled", HTML5Enabled);
		advertisingIdentifierEnabled = EditorGUILayout.Toggle("Advertising Identifier enabled", advertisingIdentifierEnabled);
		if(GUILayout.Button("Save and close")){
			string text = iTunesConnectAppID;
			text += "\n" + URLIdentifier;
			text += "\n" + debugModeEnabled;
			text += "\n" + UDIDEnabled;
			text += "\n" + HTML5Enabled;
			text += "\n" + advertisingIdentifierEnabled;
			File.WriteAllText(GetSettingsPath(),text,System.Text.Encoding.UTF8);
			text = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n";
			text += "<!DOCTYPE plist PUBLIC \"-//Apple//DTD PLIST 1.0//EN\" \"http://www.apple.com/DTDs/PropertyList-1.0.dtd\">\n";
			text += "<plist version=\"1.0\">\n";
			text += "<dict>\n";
			text += "	<key>itunes_application_id</key>\n";
			text += "	<string>" + iTunesConnectAppID + "</string>\n";
			text += "	<key>debug_mode_enabled</key>\n";
			text += "	<" + debugModeEnabled.ToString().ToLower() + "/>\n";
			text += "	<key>udid_enabled</key>\n";
			text += "	<" + UDIDEnabled.ToString().ToLower() + "/>\n";
			text += "	<key>html5_enabled</key>\n";
			text += "	<" + HTML5Enabled.ToString().ToLower() + "/>\n";
			text += "	<key>advertising_identifier_enabled</key>\n";
			text += "	<" + advertisingIdentifierEnabled.ToString().ToLower() + "/>\n";
			text += "</dict>\n";
			text += "</plist>\n";
			File.WriteAllText(GetConfigurationPath(),text,System.Text.Encoding.UTF8);
			Close();
		}
		*/
	}
	
	public static string GetSettingsPath(){
		return Application.dataPath + Path.DirectorySeparatorChar + "Fiksu" + Path.DirectorySeparatorChar + "PostBuildScripts" + Path.DirectorySeparatorChar + "FiksuSettings.txt";
	}
			
	public static string GetConfigurationPath(){
		return Application.dataPath + Path.DirectorySeparatorChar + "Fiksu" + Path.DirectorySeparatorChar + "iOSFiles" + Path.DirectorySeparatorChar + "FiksuConfiguration.plist";
	}
}

