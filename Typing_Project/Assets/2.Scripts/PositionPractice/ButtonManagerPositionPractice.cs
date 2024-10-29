using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class ButtonManagerPositionPractice : MonoBehaviour
{
    // UI
    public Button[] buttons;

    // Awake()
    private void Awake()
    {
        SetButtonsDotween();
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

    // GoTitleScene()
    public void GoTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
        PersistentDataPositionPractice.selectedLanguage = "";
    }

    // RestartScene()
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
