using System.Collections.Generic;
using DG.Tweening;
using MoreMountains.NiceVibrations;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Instance Method / GameState
    public static GameManager Instance;
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
    
    public enum GameState
    {
        Play,
        Pause,
        Win,
        Lose,
        StartMenu,
    }
    public GameState gameState;

    [Tooltip("Oyun yayıncı testine gideceği zaman tikleyin.")]
    public bool sDKEnabled;

    private GameObject appsFlyer;
    #endregion

    #region Haptic Shortcuts
    /*
    MMVibrationManager.Haptic(HapticTypes.Failure);
    MMVibrationManager.Haptic(HapticTypes.Selection);
    MMVibrationManager.Haptic(HapticTypes.Success);
    MMVibrationManager.Haptic(HapticTypes.Warning);
    MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
    MMVibrationManager.Haptic(HapticTypes.LightImpact);
    MMVibrationManager.Haptic(HapticTypes.MediumImpact);
    MMVibrationManager.Haptic(HapticTypes.RigidImpact);
    MMVibrationManager.Haptic(HapticTypes.SoftImpact);
    */
    #endregion

    public GameObject player;

    [Space(10)] public GameObject brick;
    [Space(10)] public List<GameObject> collectedBricks = new List<GameObject>();
    [Space(10)] public List<GameObject> storageBricks = new List<GameObject>();
    public int listNumber;
    public int countBrick;

    public int maxBrick;

    public int playerDamage;

    public int testBricks;
    

    private void Awake()
    {
        #region Instance Method
        InstanceMethod();

        if (sDKEnabled)
            Instantiate(appsFlyer);
        #endregion
    }
    
    private void Start()
    {
        playerDamage = PlayerPrefs.GetInt("PlayerDamage");
    }

    public void SetSavePlayerDamage(int newDamage)
    {
        playerDamage = newDamage;
        PlayerPrefs.SetInt("PlayerDamage",playerDamage);
    }
    
    private void OpenBrick()
    {
        collectedBricks[listNumber - 1].SetActive(true);
        
        storageBricks.Add(collectedBricks[listNumber-1]);
    }
    
    public void TakeBrick(int i)
    {
        countBrick++;

        if (countBrick == 5)
        {
            listNumber += i;
            OpenBrick();
            countBrick = 0;
        }
    }
    
    private void LateUpdate()
    {
        if (gameState == GameState.Play)
        {
           
        }
    }
    
    private void FixedUpdate()
    {
        if (gameState == GameState.Play)
        {
            
        }
    }
    
    private void Update()
    {
        if (gameState == GameState.Play)
        {
           
        }
    }

    #region Win/Lose/CoinUpdate
    
    public void GameWin()
    {
        gameState = GameState.Win;
        //////////////////////////
        UIManager.Instance._GameWin();
        
        if(sDKEnabled)
            SDKManager.Instance.LevelResult(true);
    }

    public void GameLose()
    {
        gameState = GameState.Lose;
        ///////////////////////////
        UIManager.Instance._GameLose();

        if (sDKEnabled)
            SDKManager.Instance.LevelResult(true);
    }
    #endregion
    
    #region Constant Methods
    
    public static float ClampAngle(float angle, float min, float max)
    {
        angle = Mathf.Repeat(angle, 360);
        min = Mathf.Repeat(min, 360);
        max = Mathf.Repeat(max, 360);
        var inverse = false;
        var timing = min;
        var tangle = angle;
        if (min > 180)
        {
            inverse = true;
            timing -= 180;
        }
        if (angle > 180)
        {
            inverse = !inverse;
            tangle -= 180;
        }
        var result = !inverse ? tangle > timing : tangle < timing;
        if (!result)
            angle = min;
        inverse = false;
        tangle = angle;
        var tax = max;
        if (angle > 180)
        {
            inverse = true;
            tangle -= 180;
        }
        if (max > 180)
        {
            inverse = !inverse;
            tax -= 180;
        }
        result = !inverse ? tangle < tax : tangle > tax;
        if (!result)
            angle = max;
        return angle;
    }
    
    public Vector2 GetMousePosition()
    {
        var pos = new Vector2(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height);

        return pos;
    }
    
    #endregion
}
