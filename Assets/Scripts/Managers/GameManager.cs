using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<CheckPoint> checkpoints;
    public ProgressController progressController;
    public PlaySceneUIController playSceneUIController;
    public DethWallController dethWallController;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        foreach (CheckPoint checkpoint in checkpoints)
        {
            checkpoint.OnEnteredCheckpoint += progressController.SetCheckpoint;
            checkpoint.CheckpointNum = checkpoints.IndexOf(checkpoint);
            checkpoint.OnEnteredCheckpoint += playSceneUIController.CheckpointCollected;
        }
        checkpoints[checkpoints.Count - 1].OnEnteredCheckpoint += GameFinished;
        print(progressController.CurrentLevel);
        TakePlayerToLastCheckpoint();
        dethWallController.Setup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakePlayerToLastCheckpoint()
    {
        if (PlayerPrefs.GetInt("level") >= 0)
        {
            player.transform.position = checkpoints[progressController.CurrentLevel].transform.position;
        }
        else
        {
            player.transform.position = checkpoints[0].transform.position;
        }
    }

    private void GameFinished(int checkpointNum)
    {
        PlayerPrefs.SetInt("level", 0);
        playSceneUIController.GameFinished();
    }
}
