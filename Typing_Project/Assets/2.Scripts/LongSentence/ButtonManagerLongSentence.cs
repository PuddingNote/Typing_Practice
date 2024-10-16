using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManagerLongSentence : MonoBehaviour
{
    // UI
    public GameObject countdownPanel;
    public GameObject selectLanguagePanel;
    public GameObject selectTitlePanel;
    
    // Start()
    private void Start()
    {
        if (!string.IsNullOrEmpty(PersistentDataLongSentence.selectedTitle) && !string.IsNullOrEmpty(PersistentDataLongSentence.selectedLanguage))
        {
            CountDown countDown = FindObjectOfType<CountDown>();

            selectLanguagePanel.SetActive(false);
            selectTitlePanel.SetActive(false);
            countDown.StartCoroutine("StartCountdown");
        }
    }

    // GoTitleScene()
    public void GoTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
        PersistentDataLongSentence.selectedTitle = "";
        PersistentDataLongSentence.selectedLanguage = "";
    }

    // RestartScene()
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
