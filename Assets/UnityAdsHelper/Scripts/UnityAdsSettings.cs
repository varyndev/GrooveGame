using UnityEngine;
using System.Collections;

public class UnityAdsSettings : ScriptableObject 
{
	public const string defaultIosGameId = "24300";
	public const string defaultAndroidGameId = "24299";

	public string iosGameId     = null;
	public string androidGameId = null;
	
	public bool enableTestMode  = true;
	public bool showInfoLogs    = false;
	public bool showDebugLogs   = false;
	public bool showWarningLogs = true;
	public bool showErrorLogs   = true;

	public UnityAdsSettings ()
	{
		iosGameId = defaultIosGameId;
		androidGameId = defaultAndroidGameId;
	}
}
