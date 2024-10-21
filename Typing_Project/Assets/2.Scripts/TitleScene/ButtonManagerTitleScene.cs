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
        explainTextUI.text = "Ÿ�� ���� ���α׷�";

        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            var button = buttons[i];

            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = buttonsInfo[i].buttonText;

            button.onClick.AddListener(() => OnButtonClick(buttonsInfo[index].sceneToLoad));

            EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();

            EventTrigger.Entry pointerEnter = new EventTrigger.Entry();
            pointerEnter.eventID = EventTriggerType.PointerEnter;
            pointerEnter.callback.AddListener((eventData) => OnButtonHighlighted(buttonsInfo[index].explainText));
            trigger.triggers.Add(pointerEnter);
        }
    }

    // ��ư Ŭ��
    private void OnButtonClick(string sceneToLoad)
    {
        if (sceneToLoad == "")
        {
            EndGame();
        }
        SceneManager.LoadSceneAsync(sceneToLoad);
    }

    // ��ư ���̶���Ʈ �� ���� ǥ��
    private void OnButtonHighlighted(string explanationText)
    {
        explainTextUI.text = explanationText;
        explainTextUI.ForceMeshUpdate();
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