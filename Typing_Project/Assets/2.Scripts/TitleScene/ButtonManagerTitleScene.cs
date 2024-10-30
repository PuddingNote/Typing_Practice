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
        explainTextUI.text = "<�Ѽ� Ű���� Ÿ�� ����>\n\nSKEW : Single-handed Keyboard for People with Disabilities";

        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            var button = buttons[i];

            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = buttonsInfo[i].buttonText;

            button.onClick.AddListener(() => OnButtonClick(buttonsInfo[index].sceneToLoad));

            EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();

            // ��ư ���̶���Ʈ
            EventTrigger.Entry pointerEnter = new EventTrigger.Entry();
            pointerEnter.eventID = EventTriggerType.PointerEnter;
            pointerEnter.callback.AddListener((eventData) => OnButtonHighlighted(button, buttonsInfo[index].explainText));
            trigger.triggers.Add(pointerEnter);

            // ��ư ���̶���Ʈ X
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

            // extra ��ư ���̶���Ʈ
            EventTrigger.Entry pointerEnter = new EventTrigger.Entry();
            pointerEnter.eventID = EventTriggerType.PointerEnter;
            pointerEnter.callback.AddListener((eventData) => OnButtonHighlighted(button, ""));
            trigger.triggers.Add(pointerEnter);

            // extra ��ư ���̶���Ʈ X
            EventTrigger.Entry pointerExit = new EventTrigger.Entry();
            pointerExit.eventID = EventTriggerType.PointerExit;
            pointerExit.callback.AddListener((eventData) => OnButtonUnhighlighted(button));
            trigger.triggers.Add(pointerExit);

            // extra ��ư Ŭ��
            EventTrigger.Entry pointerClick = new EventTrigger.Entry();
            pointerClick.eventID = EventTriggerType.PointerClick;
            pointerClick.callback.AddListener((eventData) => OnButtonClicked(button));
            trigger.triggers.Add(pointerClick);
        }
    }

    // ��ư Ŭ��
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

    // ��ư ���̶���Ʈ
    private void OnButtonHighlighted(Button button, string explanationText)
    {
        explainTextUI.text = explanationText;
        explainTextUI.ForceMeshUpdate();

        button.transform.DOScale(Vector3.one * 1.1f, 0.2f);
    }

    // ��ư ���̶���Ʈ X
    private void OnButtonUnhighlighted(Button button)
    {
        button.transform.DOScale(Vector3.one, 0.2f);
    }

    // extra ��ư Ŭ��
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

    // ����
    public void EndGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

}
