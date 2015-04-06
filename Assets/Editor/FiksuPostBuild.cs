using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditor.Callbacks;
using System.Diagnostics;
using System.IO;

public class FiksuPostBuild : MonoBehaviour {
	
	[MenuItem("Fiksu/Start PostBuild Script")]
	public static void StartManualPostBuildScript(){
		ExecutePostBuildScripts(BuildTarget.iOS,EditorPrefs.GetString("lastPathToBuildProject"),true);
	}
	
	[PostProcessBuild]
	public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject) {
		if(!EditorPrefs.HasKey("FiksuManualPostBuild")){
			EditorPrefs.SetBool("FiksuManualPostBuild",false);
		}
		ExecutePostBuildScripts(target,pathToBuiltProject,!EditorPrefs.GetBool("FiksuManualPostBuild"));
    }
	
	public static void ExecutePostBuildScripts(BuildTarget buildTarget, string pathToBuiltProject, bool runScript){
#if UNITY_IOS
		if(runScript){
			string iTunesConnectAppID = "";
			string urlSchemeEnabled = "0";
			
			string[] text = File.ReadAllLines(FiksuIOSSettings.GetConfigurationPath(),System.Text.Encoding.UTF8);
			int i = 0;
			while(i < text.Length && !text[i].Contains("<key>itunes_application_id</key>")){
				i++;
			}
			if(text[i].Contains("<key>itunes_application_id</key>")){
				iTunesConnectAppID = text[i+1].Trim().Replace("<string>","").Replace("</string>","");
			}
			FiksuIOSSettings.LoadSettings();
			if(FiksuIOSSettings.customURLScheme){
				urlSchemeEnabled = "1";
			}
			Process proc = new Process();
			proc.EnableRaisingEvents=false; 
			proc.StartInfo.FileName = Application.dataPath + "/Fiksu/PostBuildScripts/PostBuildFiksuScript";
			proc.StartInfo.Arguments = "'" + pathToBuiltProject + "' '" + iTunesConnectAppID + "' " + urlSchemeEnabled;
			UnityEngine.Debug.Log(proc.StartInfo.FileName + " " + proc.StartInfo.Arguments);
		
			proc.Start();
			proc.WaitForExit();
			UnityEngine.Debug.Log("Fiksu: build log file: " + System.IO.Directory.GetCurrentDirectory() + "/FiksuBuildLogFile.txt");
		}
		EditorPrefs.SetString("lastPathToBuildProject",pathToBuiltProject);
#endif
	}
}


