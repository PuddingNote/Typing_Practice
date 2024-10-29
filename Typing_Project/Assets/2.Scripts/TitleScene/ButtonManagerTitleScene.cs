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
    }

    // ��ư Ŭ��
    private void OnButtonClick(string sceneToLoad)
    {
        if (sceneToLoad == "")
        {
            EndGame();
        }

        SceneManager.LoadScene(sceneToLoad);
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

    // ����
    public void EndGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}