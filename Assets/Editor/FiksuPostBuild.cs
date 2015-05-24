using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.iOS.XcodeEditor;
using System.IO;

public class FiksuPostBuild 
{
    [PostProcessBuild]
    public static void OnPostprocessBuild(BuildTarget buildTarget, string path) 
    {
        #if UNITY_IPHONE
        string projectPath = path + "/Unity-iPhone.xcodeproj/project.pbxproj";
        
        PBXProject project = new PBXProject();

        project.ReadFromString(File.ReadAllText(projectPath));

        // This is the project name that Unity generates for iOS, isn't editable until after post processing
        string target = project.TargetGuidByName(PBXProject.GetUnityTargetName());
        
        #if UNITY_5_0
        project.AddBuildProperty(target, "FRAMEWORK_SEARCH_PATHS", "$(PROJECT_DIR)/Frameworks/Plugins/iOS");
        #else
        CopyAndReplaceDirectory("Assets/Plugins/iOS/FiksuSDK.bundle", Path.Combine(path, "Frameworks/FiksuSDK.bundle"));
        project.AddFileToBuild(target, project.AddFile("Frameworks/FiksuSDK.bundle", "Frameworks/FiksuSDK.bundle", PBXSourceTree.Source));

        CopyAndReplaceDirectory("Assets/Plugins/iOS/FiksuSDK.framework", Path.Combine(path, "Frameworks/FiksuSDK.framework"));
        project.AddFileToBuild(target, project.AddFile("Frameworks/FiksuSDK.framework", "Frameworks/FiksuSDK.framework", PBXSourceTree.Source));
        
        project.SetBuildProperty(target, "FRAMEWORK_SEARCH_PATHS", "$(inherited)");
        project.AddBuildProperty(target, "FRAMEWORK_SEARCH_PATHS", "$(PROJECT_DIR)/Frameworks");
        #endif

        project.AddFrameworkToProject(target, "AdSupport.framework", true);
        project.AddFrameworkToProject(target, "StoreKit.framework", true);
        project.AddFrameworkToProject(target, "Security.framework", true);
        project.AddFrameworkToProject(target, "SystemConfiguration.framework", false);
        project.AddFrameworkToProject(target, "MessageUI.framework", false);

        File.WriteAllText(projectPath, project.WriteToString());
        #endif
    }

    private static void CopyAndReplaceDirectory(string srcPath, string dstPathDir)
    {
        if (Directory.Exists(dstPathDir))
            Directory.Delete(dstPathDir);

        Directory.CreateDirectory(dstPathDir);

        foreach (var file in Directory.GetFiles(srcPath))
        {
            if(!IsExcludedFileType(file))
                File.Copy(file, Path.Combine(dstPathDir, Path.GetFileName(file)));
        }

        foreach (var dir in Directory.GetDirectories(srcPath))
            CopyAndReplaceDirectory(dir, Path.Combine(dstPathDir, Path.GetFileName(dir)));
    }

    private static bool IsExcludedFileType(string file)
    {
        if(Path.GetExtension(file).Equals(".meta"))
            return true;

        return false;
    }
}


