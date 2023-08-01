using System;
using System.Collections;
using System.Collections.Generic;
using AppsFlyerSDK;
using UnityEngine;
using FlurrySDK;
using UnityEngine.PlayerLoop;

public class SDKManager : MonoBehaviour
{
#if UNITY_ANDROID
    private string FLURRY_API_KEY = "ANDROID_API_KEY";
#elif UNITY_IPHONE
   private string FLURRY_API_KEY = "IOS_API_KEY";
#else
   private string FLURRY_API_KEY = null;
#endif
    
    #region Instance Method / GameState
    public static SDKManager Instance;
    private void InstanceMethod()
    {
        if (Instance)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    private void Awake()
    {
        #region Instance Method
            InstanceMethod();
            #endregion

            if (GameManager.Instance.sDKEnabled)
            {
                //Start Initialize Flurry;
                Flurry.Builder asd = new Flurry.Builder();

                asd.WithCrashReporting(true);
                asd.WithLogEnabled(true);
                asd.WithLogLevel(Flurry.LogLevel.VERBOSE);
                asd.WithMessaging(true);
                asd.Build(FLURRY_API_KEY);
                //End Initialize Flurry;
            }
    }
    
    
    public void LevelResult(bool win)
    {
        Dictionary<string, string> eventValues = new Dictionary<string, string>();

        if (win)
        {
            eventValues.Add(AFInAppEvents.LEVEL, "Level Complete" + LevelManager.Instance.currentLevelNumber);
            AppsFlyer.sendEvent(AFInAppEvents.LEVEL, eventValues);
        }
        
        if(!win)
        {
            eventValues.Add(AFInAppEvents.LEVEL, "Level Failed" + LevelManager.Instance.currentLevelNumber);
            AppsFlyer.sendEvent(AFInAppEvents.LEVEL,eventValues);
        }
        
        
    }
    
}
