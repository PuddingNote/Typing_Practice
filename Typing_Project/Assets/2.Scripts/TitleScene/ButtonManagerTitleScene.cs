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
    public Button[] extraButtons;

    private ProjectManagerTitleScene projectManager;

    // Awake()
    private void Awake()
    {
        projectManager = GameObject.Find("ProjectManager").GetComponent<ProjectManagerTitleScene>();
        explainTextUI.text = "<한손 키보드 타자 연습>\n\nSKEW : Single-handed Keyboard for People with Disabilities";

        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            var button = buttons[i];

            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = buttonsInfo[i].buttonText;

            button.onClick.AddListener(() => OnButtonClick(buttonsInfo[index].sceneToLoad));

            EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();

            // 버튼 하이라이트
            EventTrigger.Entry pointerEnter = new EventTrigger.Entry();
            pointerEnter.eventID = EventTriggerType.PointerEnter;
            pointerEnter.callback.AddListener((eventData) => OnButtonHighlighted(button, buttonsInfo[index].explainText));
            trigger.triggers.Add(pointerEnter);

            // 버튼 하이라이트 X
            EventTrigger.Entry pointerExit = new EventTrigger.Entry();
            pointerExit.eventID = EventTriggerType.PointerExit;
            pointerExit.callback.AddListener((eventData) => OnButtonUnhighlighted(button));
            trigger.triggers.Add(pointerExit);
        }

        for (int i = 0; i < extraButtons.Length; i++)
        {
            int index = i;
            var button = extraButtons[i];

            EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();

            // extra 버튼 하이라이트
            EventTrigger.Entry pointerEnter = new EventTrigger.Entry();
            pointerEnter.eventID = EventTriggerType.PointerEnter;
            pointerEnter.callback.AddListener((eventData) => OnButtonHighlighted(button, ""));
            trigger.triggers.Add(pointerEnter);

            // extra 버튼 하이라이트 X
            EventTrigger.Entry pointerExit = new EventTrigger.Entry();
            pointerExit.eventID = EventTriggerType.PointerExit;
            pointerExit.callback.AddListener((eventData) => OnButtonUnhighlighted(button));
            trigger.triggers.Add(pointerExit);

            // extra 버튼 클릭
            EventTrigger.Entry pointerClick = new EventTrigger.Entry();
            pointerClick.eventID = EventTriggerType.PointerClick;
            pointerClick.callback.AddListener((eventData) => OnButtonClicked(button));
            trigger.triggers.Add(pointerClick);
        }
    }

    // 버튼 클릭
    private void OnButtonClick(string sceneToLoad)
    {
        if (sceneToLoad == "EndGame")
        {
            EndGame();
        }

        if (sceneToLoad != "")
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            projectManager.StartKeySetting();
        }
    }

    // 버튼 하이라이트
    private void OnButtonHighlighted(Button button, string explanationText)
    {
        explainTextUI.text = explanationText;
        explainTextUI.ForceMeshUpdate();

        button.transform.DOScale(Vector3.one * 1.1f, 0.2f);
    }

    // 버튼 하이라이트 X
    private void OnButtonUnhighlighted(Button button)
    {
        button.transform.DOScale(Vector3.one, 0.2f);
    }

    // extra 버튼 클릭
    private void OnButtonClicked(Button button)
    {
        button.transform.DOScale(Vector3.one, 0.2f);
    }

    // GoTitleScene()
    public void GoTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
        PersistentDataTitleScene.selectedLanguage = "";
        PersistentDataTitleScene.selectedHandType = "";
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
