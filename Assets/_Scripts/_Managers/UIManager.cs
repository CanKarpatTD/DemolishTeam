using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Instance Method
    public static UIManager Instance;
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

    #region Constant
    [HideInInspector]public TextMeshProUGUI levelIndex;
    [HideInInspector]public GameObject levelComplete,levelFailed,confetti;
    [HideInInspector]public List<string> winTexts = new List<string>();
    [HideInInspector]public List<string> failTexts = new List<string>();
    [HideInInspector]public TextMeshProUGUI failText,winText;
    #endregion

    public GameObject joystick;
    private void Awake()
    {
        #region Instance Method
        InstanceMethod();
        #endregion
    }
    
    private void Update()
    {
        
    }

    public void _GameStart()
    {
        levelIndex.enabled = true;
        GameManager.Instance.gameState = GameManager.GameState.Play;
        joystick.SetActive(true);
    }

    public void _GameWin()
    {
        levelIndex.enabled = false;

        winText.text = winTexts[Random.Range(0, winTexts.Count)];
        
        levelComplete.SetActive(true);
        confetti.SetActive(true);
    }

    public void _GameLose()
    {
        levelIndex.enabled = false;
        
        failText.text = failTexts[Random.Range(0, failTexts.Count)];
        
        levelFailed.SetActive(true);
    }
    
    public void SetLevelIndex()
    {
        levelIndex.text = "Level " + LevelManager.Instance.currentLevelNumber;
    }
}
