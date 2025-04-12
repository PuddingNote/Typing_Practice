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

            // 버튼 하이라이트
            EventTrigger.Entry pointerEnter = new EventTrigger.Entry();
            pointerEnter.eventID = EventTriggerType.PointerEnter;
            pointerEnter.callback.AddListener((eventData) => OnButtonHighlighted(button));
            trigger.triggers.Add(pointerEnter);

            // 버튼 하이라이트 X
            EventTrigger.Entry pointerExit = new EventTrigger.Entry();
            pointerExit.eventID = EventTriggerType.PointerExit;
            pointerExit.callback.AddListener((eventData) => OnButtonUnhighlighted(button));
            trigger.triggers.Add(pointerExit);

            // 버튼 클릭
            EventTrigger.Entry pointerClick = new EventTrigger.Entry();
            pointerClick.eventID = EventTriggerType.PointerClick;
            pointerClick.callback.AddListener((eventData) => OnButtonClicked(button));
            trigger.triggers.Add(pointerClick);
        }
    }

    // 버튼 하이라이트
    private void OnButtonHighlighted(Button button)
    {
        button.transform.DOScale(Vector3.one * 1.1f, 0.2f);
    }

    // 버튼 하이라이트 X
    private void OnButtonUnhighlighted(Button button)
    {
        button.transform.DOScale(Vector3.one, 0.2f);
    }

    // 버튼 클릭 (SelectTitleScene에서 뒤로가기했다가 다시오면 뒤로가기버튼이 계속 커져있던 현상 저지)
    private void OnButtonClicked(Button button)
    {
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
        PersistentDataLongSentence.selectedTitle = "";
        PersistentDataLongSentence.selectedLanguage = "";
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
