using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressController : MonoBehaviour
{
    public int CurrentLevel { get; private set; }

    private void Start()
    {
        // get the current Level
        if (PlayerPrefs.GetInt("level") != null)
        {
            PlayerPrefs.SetInt("level", 0);
            CurrentLevel = 0;
        }
        else
        {
            CurrentLevel = PlayerPrefs.GetInt("level");
        }
    }
    
    public void AddLevel()
    {
        CurrentLevel += 1;
        PlayerPrefs.SetInt("level",CurrentLevel);
    }
    
    public void ResetLevel()
    {
        CurrentLevel = 0;
        PlayerPrefs.SetInt("level", 0);
    }
}
