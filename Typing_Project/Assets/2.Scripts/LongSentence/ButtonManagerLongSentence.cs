using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class ButtonManagerLongSentence : MonoBehaviour
{
    // UI
    public Button[] buttons;
    public GameObject countdownPanel;
    public GameObject selectLanguagePanel;
    public GameObject selectTitlePanel;

    // Awake()
    private void Awake()
    {
        SetButtonsDotween();
    }

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

    // 버튼 Tweening
    private void SetButtonsDotween()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            var button = buttons[i];

            EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();

            // 버튼 하이라이트시
            EventTrigger.Entry pointerEnter = new EventTrigger.Entry();
            pointerEnter.eventID = EventTriggerType.PointerEnter;
            pointerEnter.callback.AddListener((eventData) => OnButtonHighlighted(button));
            trigger.triggers.Add(pointerEnter);

            // 버튼 하이라이트 벗어났을 시
            EventTrigger.Entry pointerExit = new EventTrigger.Entry();
            pointerExit.eventID = EventTriggerType.PointerExit;
            pointerExit.callback.AddListener((eventData) => OnButtonUnhighlighted(button));
            trigger.triggers.Add(pointerExit);
        }
    }

    // 버튼 하이라이트 시 
    private void OnButtonHighlighted(Button button)
    {
        // 버튼 스케일 늘리기
        button.transform.DOScale(Vector3.one * 1.1f, 0.2f);
    }

    // 버튼 하이라이트 벗어났을 때
    private void OnButtonUnhighlighted(Button button)
    {
        //button.transform.DOKill();
        // 원래 크기로, 0.2초 동안
        button.transform.DOScale(Vector3.one, 0.2f);
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
