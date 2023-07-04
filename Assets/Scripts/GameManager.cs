using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int type;
    public int currentLevel;
    public int currentMoney;
    public bool isWin;

    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Application.targetFrameRate = 120;
         
    }

    public void GameOver(bool isWin)
    {
        this.isWin = isWin;
        SceneManager.LoadScene("End");
    }

    public void LoadData()
    {
        currentMoney = PlayerPrefs.GetInt("Money", 0);
        currentLevel = PlayerPrefs.GetInt("Level", 1);

    }

    public void SaveInfo()
    {
        PlayerPrefs.SetInt("Money", currentMoney);
        PlayerPrefs.SetInt("Level", currentLevel);

        PlayerPrefs.Save();

    }

    public void GetMoney(int _money)
    {
        currentMoney += _money;
        SaveInfo();
    }
}

