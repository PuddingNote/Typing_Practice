using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManagerLongSentence : MonoBehaviour
{
    public GameObject countdownPanel;
    public GameObject selectLanguagePanel;
    public GameObject selectTitlePanel;
    

    private void Start()
    {
        // ���õ� ����� �� �ִ� ��� CountDown �гη� ����
        if (!string.IsNullOrEmpty(PersistentData.selectedTitle) && !string.IsNullOrEmpty(PersistentData.selectedLanguage))
        {
            GameObject countDownObject = GameObject.Find("TypingManager");
            CountDown countDown = countDownObject.GetComponent<CountDown>();

            selectLanguagePanel.SetActive(false);
            selectTitlePanel.SetActive(false);
            countDown.StartCoroutine("StartCountdown");
        }
    }

    public void GoTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
        PersistentData.selectedTitle = "";
        PersistentData.selectedLanguage = "";
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
