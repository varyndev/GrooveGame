using UnityEngine;
using System.Collections;

public class DownloadObb : MonoBehaviour {

	public static bool fetchingStarted = false;

	void OnGUI()
	{
#if UNITY_ANDROID
		if ( ! GooglePlayDownloader.RunningOnAndroid()) {
			GUI.Label(new Rect(10, 10, Screen.width-10, 40), "Use GooglePlayDownloader only on Android device!");
			return;
		}
		string expPath = GooglePlayDownloader.GetExpansionFilePath();
		if (expPath == null) {
			GUI.Label(new Rect(10, 10, Screen.width-10, 40), "External storage is not available!");
		} else {
			string mainPath = GooglePlayDownloader.GetMainOBBPath(expPath); // must be called before calling FetchOBB.
			string patchPath = GooglePlayDownloader.GetPatchOBBPath(expPath);

			GUI.Label(new Rect(10, 10, Screen.width-10, 40), "Main: " + (mainPath == null ? " NOT AVAILABLE" :  mainPath.Substring(expPath.Length)));
			GUI.Label(new Rect(10, 55, Screen.width-10, 40), "Patch: " + (patchPath == null ? " NOT AVAILABLE" : patchPath.Substring(expPath.Length)));

//			GooglePlayDownloader.FetchOBB();
//			fetchingStarted = true;
//			StartCoroutine(Load());
			
			if (mainPath == null || patchPath == null) {
				if (GUI.Button(new Rect(10, 100, 150, 150), "Fetch OBBs")) {
					GooglePlayDownloader.FetchOBB();
					fetchingStarted = true;
					StartCoroutine(Load());
					Debug.Log ("Started from button pressed");
				}
			} else {
				GooglePlayDownloader.FetchOBB();
				fetchingStarted = true;
				StartCoroutine(Load());
				Debug.Log ("Started from paths being set");
			}
		}
#endif
	}

	IEnumerator Load()
	{
#if UNITY_IPHONE
		Handheld.SetActivityIndicatorStyle(iOS.ActivityIndicatorStyle.Gray);
#elif UNITY_ANDROID
		Handheld.SetActivityIndicatorStyle(AndroidActivityIndicatorStyle.Small);
#endif
		
		Handheld.StartActivityIndicator();
		yield return new WaitForSeconds(0);
	}
}
