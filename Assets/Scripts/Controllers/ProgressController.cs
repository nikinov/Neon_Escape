using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressController : MonoBehaviour
{
    [HideInInspector]public int CurrentLevel;

    private void Start()
    {
        CurrentLevel = 0;
        if (PlayerPrefs.GetInt("level") >= 0)
        {
            CurrentLevel = PlayerPrefs.GetInt("level");
        }
        else
        {
            CurrentLevel = 0;
            PlayerPrefs.SetInt("level", 0);
        }
    }
    
    public void SetCheckpoint(int level=-1)
    {
        if (level == -1)
        {
            CurrentLevel += 1;
        }
        else
        {
            CurrentLevel = level;
        }
        PlayerPrefs.SetInt("level",CurrentLevel);
    }
    
    public void ResetLevel()
    {
        CurrentLevel = 0;
        PlayerPrefs.SetInt("level", 0);
    }
}
