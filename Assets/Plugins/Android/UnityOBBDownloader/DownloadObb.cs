using UnityEngine;
using System.Collections;

public class DownloadObb : MonoBehaviour {

	public static bool fetchingStarted = false;

	void OnGUI()
	{
		if ( ! GooglePlayDownloader.RunningOnAndroid()) {
			GUI.Label(new Rect(10, 10, Screen.width-10, 20), "Use GooglePlayDownloader only on Android device!");
			return;
		}
		string expPath = GooglePlayDownloader.GetExpansionFilePath();
		if (expPath == null) {
			GUI.Label(new Rect(10, 10, Screen.width-10, 20), "External storage is not available!");
		} else if ( ! fetchingStarted) {
			GooglePlayDownloader.FetchOBB();
			fetchingStarted = true;
			GUI.Label(new Rect(10, 10, Screen.width-10, 40), "Loading additional game files, please wait...");

//			string mainPath = GooglePlayDownloader.GetMainOBBPath(expPath);
//			string patchPath = GooglePlayDownloader.GetPatchOBBPath(expPath);
//			
//			GUI.Label(new Rect(10, 10, Screen.width-10, 40), "Main: "  + ( mainPath == null ? " NOT AVAILABLE" :  mainPath.Substring(expPath.Length)));
//			GUI.Label(new Rect(10, 55, Screen.width-10, 40), "Patch: " + (patchPath == null ? " NOT AVAILABLE" : patchPath.Substring(expPath.Length)));
//			if (mainPath == null || patchPath == null) {
//				if (GUI.Button(new Rect(10, 100, 150, 150), "Fetch OBBs")) {
//					GooglePlayDownloader.FetchOBB();
//					fetchingStarted = true;
//				}
//			} else {
//				GooglePlayDownloader.FetchOBB();
//				fetchingStarted = true;
//			}
		}
	}
}
