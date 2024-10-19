using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

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
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            var button = buttons[i];

            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = buttonsInfo[i].buttonText;

            button.onClick.AddListener(() => OnButtonClick(buttonsInfo[index].sceneToLoad));

            // 마우스 하이라이트(포인터 진입) 이벤트
            EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();

            EventTrigger.Entry pointerEnter = new EventTrigger.Entry();
            pointerEnter.eventID = EventTriggerType.PointerEnter;
            pointerEnter.callback.AddListener((eventData) => OnButtonHighlighted(buttonsInfo[index].explainText));
            trigger.triggers.Add(pointerEnter);

            // 마우스가 버튼에서 벗어날 때
            EventTrigger.Entry pointerExit = new EventTrigger.Entry();
            pointerExit.eventID = EventTriggerType.PointerExit;
            pointerExit.callback.AddListener((eventData) => OnButtonExit());
            trigger.triggers.Add(pointerExit);
        }
    }

    // 버튼 클릭
    private void OnButtonClick(string sceneToLoad)
    {
        SceneManager.LoadSceneAsync(sceneToLoad);
    }

    // 버튼 하이라이트 시 설명 표시
    private void OnButtonHighlighted(string explanationText)
    {
        explainTextUI.text = explanationText;
        explainTextUI.ForceMeshUpdate();
    }

    // 버튼에서 마우스 벗어날 시 설명 비우기
    private void OnButtonExit()
    {
        explainTextUI.text = "";
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