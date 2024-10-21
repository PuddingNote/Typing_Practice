using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManagerWordPractice : MonoBehaviour
{
    // UI
    public GameObject countdownPanel;
    public GameObject selectLanguagePanel;

    // Start()
    private void Start()
    {
        if (!string.IsNullOrEmpty(PersistentDataWordPractice.selectedLanguage))
        {
            CountDown countDown = FindObjectOfType<CountDown>();
            selectLanguagePanel.SetActive(false);

            countDown.StartCoroutine("StartCountdown");
        }
    }

    // GoTitleScene()
    public void GoTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
        PersistentDataWordPractice.selectedLanguage = "";
    }

    // RestartScene()
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
