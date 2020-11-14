using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DethWallController : MonoBehaviour
{
    public float wallSpeed;
    public GameManager _gameManager;
    public float offset;
    
    // Start is called before the first frame update
    void Start()
    {
        DOTween.Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup()
    {
        print(_gameManager.progressController.CurrentLevel);
        print(_gameManager.checkpoints[_gameManager.progressController.CurrentLevel + 1].transform.position.x - offset);
        transform.position = new Vector3(_gameManager.checkpoints[_gameManager.progressController.CurrentLevel].transform.position.x - offset, 0);
        MoveBySegment();
    }
    void MoveBySegment()
    {
        transform.DOMoveX(_gameManager.checkpoints[_gameManager.progressController.CurrentLevel + 1].transform.position.x - offset, (_gameManager.checkpoints[_gameManager.progressController.CurrentLevel + 1].transform.position.x - _gameManager.checkpoints[_gameManager.progressController.CurrentLevel].transform.position.x) * wallSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            _gameManager.player.GetComponent<PlayerManager>().Deth();
            _gameManager.playSceneUIController.StarDethPanel();
        }
    }
}
