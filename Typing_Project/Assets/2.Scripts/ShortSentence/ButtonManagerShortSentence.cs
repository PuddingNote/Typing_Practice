using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class ButtonManagerShortSentence : MonoBehaviour
{
    // UI
    public Button[] buttons;
    public GameObject countdownPanel;
    public GameObject selectLanguagePanel;

    // Awake()
    private void Awake()
    {
        SetButtonsDotween();
    }

    // Start()
    private void Start()
    {
        if (!string.IsNullOrEmpty(PersistentDataShortSentence.selectedLanguage))
        {
            CountDown countDown = FindObjectOfType<CountDown>();
            selectLanguagePanel.SetActive(false);

            countDown.StartCoroutine("StartCountdown");
        }
    }

    // ��ư Tweening
    private void SetButtonsDotween()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            var button = buttons[i];

            EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();

            // ��ư ���̶���Ʈ
            EventTrigger.Entry pointerEnter = new EventTrigger.Entry();
            pointerEnter.eventID = EventTriggerType.PointerEnter;
            pointerEnter.callback.AddListener((eventData) => OnButtonHighlighted(button));
            trigger.triggers.Add(pointerEnter);

            // ��ư ���̶���Ʈ X
            EventTrigger.Entry pointerExit = new EventTrigger.Entry();
            pointerExit.eventID = EventTriggerType.PointerExit;
            pointerExit.callback.AddListener((eventData) => OnButtonUnhighlighted(button));
            trigger.triggers.Add(pointerExit);
        }
    }

    // ��ư ���̶���Ʈ
    private void OnButtonHighlighted(Button button)
    {
        button.transform.DOScale(Vector3.one * 1.1f, 0.2f);
    }

    // ��ư ���̶���Ʈ X
    private void OnButtonUnhighlighted(Button button)
    {
        button.transform.DOScale(Vector3.one, 0.2f);
    }

    // GoTitleScene()
    public void GoTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
        PersistentDataShortSentence.selectedLanguage = "";
    }

    // RestartScene()
    public void RestartScene()
    {
        PersistentDataShortSentence.selectedLanguage = "";
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
