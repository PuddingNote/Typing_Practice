using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManagerShortSentence : MonoBehaviour
{
    public GameObject countdownPanel;
    public GameObject selectLanguagePanel;

    private void Start()
    {
        // 선택된 제목과 언어가 있는 경우 CountDown 패널로 시작
        if (!string.IsNullOrEmpty(PersistentDataShortSentence.selectedLanguage))
        {
            GameObject countDownObject = GameObject.Find("TypingManager");
            CountDown countDown = countDownObject.GetComponent<CountDown>();

            selectLanguagePanel.SetActive(false);
            countDown.StartCoroutine("StartCountdown");
        }
    }

    public void GoTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
        PersistentDataShortSentence.selectedLanguage = "";
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
