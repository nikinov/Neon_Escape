using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public delegate void EnteredCheckpointAction(int checkpointNumber);
    public event EnteredCheckpointAction OnEnteredCheckpoint;
    public int CheckpointNum;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (OnEnteredCheckpoint != null)
            OnEnteredCheckpoint(CheckpointNum);
    }
}
