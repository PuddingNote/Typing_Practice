using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;

// Button Info
[System.Serializable]
public class ButtonInfo
{
    public string buttonText;
    public string sceneToLoad;
    [TextArea(3, 10)] public string explainText;
}

public class ButtonManagerTitleScene : MonoBehaviour
{
    // UI
    public Button[] buttons;
    public ButtonInfo[] buttonsInfo;
    public TextMeshProUGUI explainTextUI;

    // Awake()
    private void Awake()
    {
        explainTextUI.text = "타자 연습 프로그램";

        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            var button = buttons[i];

            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = buttonsInfo[i].buttonText;

            button.onClick.AddListener(() => OnButtonClick(buttonsInfo[index].sceneToLoad));

            EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();

            // 버튼 하이라이트시
            EventTrigger.Entry pointerEnter = new EventTrigger.Entry();
            pointerEnter.eventID = EventTriggerType.PointerEnter;
            pointerEnter.callback.AddListener((eventData) => OnButtonHighlighted(button, buttonsInfo[index].explainText));
            trigger.triggers.Add(pointerEnter);

            // 버튼 하이라이트 벗어났을 시
            EventTrigger.Entry pointerExit = new EventTrigger.Entry();
            pointerExit.eventID = EventTriggerType.PointerExit;
            pointerExit.callback.AddListener((eventData) => OnButtonUnhighlighted(button));
            trigger.triggers.Add(pointerExit);
        }
    }

    // 버튼 클릭
    private void OnButtonClick(string sceneToLoad)
    {
        if (sceneToLoad == "")
        {
            EndGame();
        }
        SceneManager.LoadSceneAsync(sceneToLoad);
    }

    // 버튼 하이라이트 시 
    private void OnButtonHighlighted(Button button, string explanationText)
    {
        // 버튼 설명 표시
        explainTextUI.text = explanationText;
        explainTextUI.ForceMeshUpdate();

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

    // 종료
    public void EndGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}