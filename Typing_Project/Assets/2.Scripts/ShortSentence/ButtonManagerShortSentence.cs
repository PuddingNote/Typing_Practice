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
        // ���õ� ����� �� �ִ� ��� CountDown �гη� ����
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
