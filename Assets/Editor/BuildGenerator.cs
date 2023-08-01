using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEditor.Build.Reporting;

public class BuildGenerator : EditorWindow
{
    private string GameName;
    private string versionNumber;
    
    //iOS
    private string buildIOSNumber;
    
    //Android
    private string buildAPKNumber;
    private string keyaliasPassword;

    private bool groupEnabled;
    private bool buttonActive;
    private bool targetIOS;
    private bool targetAndroid;
    private bool setupReady = false;
    private bool testBuild;
    
    private GUIStyle guiStyle = new GUIStyle();
    
    [MenuItem("Get Build/Get Build", false, 1)]
    private static void NewMenuOption()
    {
        GetWindow(typeof(BuildGenerator));
    }
    
    void OnGUI()
    {
        //GUI Style Edit Start
        guiStyle.alignment = TextAnchor.MiddleCenter;
        guiStyle.fontSize = 20;
        guiStyle.fontStyle = FontStyle.Bold;
        guiStyle.normal.textColor = Color.green;
        guiStyle.normal.background = Texture2D.linearGrayTexture;
        //GUI Style Edit End
        EditorStyles.helpBox.fontSize = 13;
        EditorStyles.helpBox.normal.textColor = Color.red;

        GUILayout.Space(10);
        GUILayout.Label("Build Settings", guiStyle);
        GUILayout.Space(15);
        GameName = EditorGUILayout.TextField ("Game Name", GameName);
        GUILayout.Space(5);
        versionNumber = EditorGUILayout.TextField ("Version Number", versionNumber);
        
        GUILayout.Space(20);
        
        //TODO: iOS build gereklilikleri
        buildIOSNumber = EditorGUILayout.TextField ("iOS Build Number", buildIOSNumber);
        GUILayout.Space(20);
        
        //TODO: Android build gereklilikleri
        buildAPKNumber = EditorGUILayout.TextField ("APK Build Number", buildAPKNumber);
        GUILayout.Space(15);
        GUILayout.TextArea("KeyAlias/KeyStore şifresi = bg123123.",EditorStyles.helpBox);
        keyaliasPassword = EditorGUILayout.TextField ("KeyAlias/KeyStore Password", keyaliasPassword);
        GUILayout.Space(10);
        
        GUILayout.Space(20);
        if (GameName != "" && versionNumber != "" && buildAPKNumber != "" && keyaliasPassword != "")
        {
            groupEnabled = true;
        }
        else
        {
            groupEnabled = false;
        }

        if (!groupEnabled)
            setupReady = false;
        
        groupEnabled = EditorGUILayout.BeginToggleGroup ("Done", groupEnabled);
        if (GUILayout.Button("Start Setup"))
        {
            PlayerSettings.productName = GameName;
            
            PlayerSettings.bundleVersion = versionNumber;
            PlayerSettings.iOS.buildNumber = buildIOSNumber;
            PlayerSettings.Android.bundleVersionCode = int.Parse(buildAPKNumber);
            
            PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.iOS,"com.burgergames."+GameName);
            PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android,"com.burgergames."+GameName);

            PlayerSettings.Android.keyaliasPass = keyaliasPassword;
            PlayerSettings.Android.keystorePass = keyaliasPassword;

            setupReady = true;
        }
        EditorGUILayout.EndToggleGroup ();

        GUILayout.Space(10);
        GUILayout.TextArea("Herhangi bir mekanik testi için 'is TestBuild?' boolunu tikleyin.",EditorStyles.helpBox);
        testBuild = EditorGUILayout.BeginToggleGroup ("is TestBuild?", testBuild);
        EditorGUILayout.EndToggleGroup();
        
        //Release build
        if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android)
        {
            targetAndroid = true;
            targetIOS = false;
        }

        if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.iOS)
        {
            targetIOS = true;
            targetAndroid = false;
        }
        
        GUILayout.Space(40);
        GUILayout.TextArea("'Start Setup' işlemini yapmadan buildi başlatmayın.",EditorStyles.helpBox);
        setupReady = EditorGUILayout.BeginToggleGroup ("Setup is Ready", setupReady);
        targetIOS = EditorGUILayout.BeginToggleGroup("Start from iOS", targetIOS);
        if (GUILayout.Button("Start Build Process from iOS"))
        {
            iOSBuilder();
        }
        EditorGUILayout.EndToggleGroup();

        GUILayout.Space(10);
        targetAndroid = EditorGUILayout.BeginToggleGroup("Start from Android", targetAndroid);
        if (GUILayout.Button("Start Build Process from Android"))
        {
            AndroidBuilder();
        }
        EditorGUILayout.EndToggleGroup();
        
        EditorGUILayout.EndToggleGroup();
        
        GUILayout.TextArea("Test buildi değilse 'Version numarası' ve 'Build numarası' için bilgi alın.",EditorStyles.helpBox);
    }
    
    
    
    //Build process
    private void iOSBuilder()
    {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.locationPathName = GameName + "_Build";
        buildPlayerOptions.scenes = new[] { "Assets/_Scenes/GameScene.unity"};
        buildPlayerOptions.target = BuildTarget.iOS;
        buildPlayerOptions.options = BuildOptions.CompressWithLz4HC;
        
        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;
        
        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("iOS Build succeeded");

            AfterAndroid();
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build failed");
        }
    }

    private void AfterAndroid()
    {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        if (testBuild)
        {
                EditorUserBuildSettings.buildAppBundle = false;
                buildPlayerOptions.locationPathName = GameName+ "_Build" + ".apk";
        }
        else
        {
                EditorUserBuildSettings.buildAppBundle = true;
                buildPlayerOptions.locationPathName = GameName+ "_Build" + ".aab";
        }
        //buildPlayerOptions.locationPathName = GameName+ "_Build" + ".apk";
        buildPlayerOptions.scenes = new[] { "Assets/_Scenes/GameScene.unity"};
        buildPlayerOptions.target = BuildTarget.Android;
        buildPlayerOptions.options = BuildOptions.CompressWithLz4HC;
        
        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;
        
        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Android Build succeeded");
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build failed");
        }
    }
    
    private void AndroidBuilder()
    {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        if (testBuild)
        {
            if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android)
            {
                EditorUserBuildSettings.buildAppBundle = false;
                buildPlayerOptions.locationPathName = GameName+ "_Build" + ".apk";
            }
        }
        if(!testBuild)
        {
            if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android)
            {
                EditorUserBuildSettings.buildAppBundle = true;
                buildPlayerOptions.locationPathName = GameName+ "_Build" + ".aab";
            }
        }
        //buildPlayerOptions.locationPathName = GameName+ "_Build" + ".apk";
        buildPlayerOptions.scenes = new[] { "Assets/_Scenes/GameScene.unity"};
        buildPlayerOptions.target = BuildTarget.Android;
        buildPlayerOptions.options = BuildOptions.CompressWithLz4HC;
        
        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;
        
        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Android Build succeeded");
            AfterIOS();
        }
        
        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build failed");
        }
    }

    private void AfterIOS()
    {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.locationPathName = GameName + "_Build";
        buildPlayerOptions.scenes = new[] { "Assets/_Scenes/GameScene.unity"};
        buildPlayerOptions.target = BuildTarget.iOS;
        buildPlayerOptions.options = BuildOptions.CompressWithLz4HC;
        
        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;
        
        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("iOS Build succeeded");

            AfterAndroid();
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build failed");
        }
    }
}
