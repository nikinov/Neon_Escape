using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaySceneUIController : MonoBehaviour
{
    [SerializeField] private CanvasGroup BlackPanel;
    [SerializeField] private float BlackPanelFadeTime;
    [SerializeField] private CanvasGroup DethPanel;
    [SerializeField] private CanvasGroup FinishPanel;
    
    // Start is called before the first frame update
    void Start()
    {
        DOTween.Init();
        StartPanel();
        DethPanel.gameObject.SetActive(false);
        DethPanel.alpha = 0;
        FinishPanel.gameObject.SetActive(false);
        FinishPanel.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StarDethPanel()
    {
        StartCoroutine(waitForFadeDethPanel(2, true, 1, DethPanel));
    }

    public void StartPanel()
    {
        StartCoroutine(waitForBlackPanel(0));
    }

    public void RestartGame()
    {
        BlackPanel.gameObject.SetActive(true);
        StartCoroutine(waitForBlackPanel(1, SceneManager.GetActiveScene().buildIndex));
    }

    public void CheckpointCollected(int checkpointNum)
    {
        
    }

    public void GameFinished()
    {
        StartCoroutine(waitForFadeDethPanel(2, true, 1, FinishPanel));
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

    IEnumerator waitForFadeDethPanel(float timing, bool panelSetTo, float fadeValue, CanvasGroup Panel)
    {
        if(panelSetTo)
            Panel.gameObject.SetActive(true);
        Panel.DOFade(fadeValue, timing);
        yield return new WaitForSeconds(timing);
        if(!panelSetTo)
            Panel.gameObject.SetActive(false);
        
    }
}
