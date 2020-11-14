using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private float BlackPanelFadeTime;
    [SerializeField] private CanvasGroup BlackPanel;
    // Start is called before the first frame update
    void Start()
    {
        DOTween.Init();
        StartCoroutine(waitForBlackPanel(0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        StartCoroutine(waitForBlackPanel(1));
    }

    IEnumerator waitForBlackPanel(float fade, int StartNewScene=-1)
    {
        BlackPanel.DOFade(fade, BlackPanelFadeTime);
        if(!BlackPanel.gameObject)
            BlackPanel.gameObject.SetActive(true);
        yield return new WaitForSeconds(BlackPanelFadeTime);
        if(BlackPanel.gameObject)
            BlackPanel.gameObject.SetActive(false);
        if (StartNewScene >= 0)
        {
            SceneManager.LoadScene(StartNewScene);
        }
    }
}
