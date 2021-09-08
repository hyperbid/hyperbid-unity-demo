using UnityEditor;
using UnityEditor.Callbacks;
using System.IO;
#if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
using UnityEditor.iOS.Xcode;
#endif

public static class MyBuildPostprocess
{
  [PostProcessBuild(999)]
  public static void OnPostProcessBuild(BuildTarget buildTarget, string path)
  {
    #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
      if (buildTarget == BuildTarget.iOS)
      {
        string projectPath = path + "/Unity-iPhone.xcodeproj/project.pbxproj";

        PBXProject pbxProject = new PBXProject();
        pbxProject.ReadFromFile(projectPath);

        // unity 2019.4+
        string target = pbxProject.GetUnityFrameworkTargetGuid();
        // unity 2019
        // string target = pbxProject.GetUnityMainTargetGuid();
        // unity 2018, 2017
        // string target = pbxProject.TargetGuidByName("Unity-iPhone");
        pbxProject.SetBuildProperty(target, "ENABLE_BITCODE", "NO");
        pbxProject.SetBuildProperty(target, "GCC_ENABLE_OBJC_EXCEPTIONS", "YES");
        pbxProject.SetBuildProperty(target, "GCC_C_LANGUAGE_STANDARD", "gnu99");

        pbxProject.AddBuildProperty(target, "OTHER_LDFLAGS", "-ObjC");
        pbxProject.AddBuildProperty(target, "OTHER_LDFLAGS", "-fobjc-arc");
        pbxProject.AddFileToBuild(target, pbxProject.AddFile("usr/lib/libxml2.tbd", "Libraries/libxml2.tbd", PBXSourceTree.Sdk));
        pbxProject.AddFileToBuild(target, pbxProject.AddFile("usr/lib/libresolv.9.tbd", "Libraries/libresolv.9.tbd", PBXSourceTree.Sdk));
        pbxProject.AddFileToBuild(target, pbxProject.AddFile("usr/lib/libbz2.1.0.tbd", "Libraries/libbz2.1.0.tbd", PBXSourceTree.Sdk));
        pbxProject.AddFrameworkToProject(target, "JavaScriptCore.framework", false);
        pbxProject.AddFrameworkToProject(target, "VideoToolbox.framework", false);
        pbxProject.WriteToFile (projectPath);

        var plistPath = Path.Combine(path, "Info.plist");
        PlistDocument plist = new PlistDocument();
        plist.ReadFromFile(plistPath);
        plist.root.SetString("GADApplicationIdentifier", "ca-app-pub-9488501426181082/7319780494");
        plist.root.SetBoolean("GADIsAdManagerApp", true);
        plist.WriteToFile(plistPath);
    }
    #endif
  }
}

