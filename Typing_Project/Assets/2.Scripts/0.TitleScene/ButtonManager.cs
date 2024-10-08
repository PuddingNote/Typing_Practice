using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

[System.Serializable]
public class ButtonInfo
{
    public string buttonText;
    public string sceneToLoad;
}

public class ButtonManager : MonoBehaviour
{
    // UI Button �� Text ������Ʈ
    public Button[] buttons;
    public ButtonInfo[] buttonsInfo;

    // ���� ����
    private Color defaultColor = Color.white;
    private Color highlightColor = Color.black;
    private Color clickedColor = Color.black;
    private Color textDefaultColor = Color.black;
    private Color textHighlightColor = Color.white;

    private void Awake()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            var button = buttons[i];

            // ��ư �ؽ�Ʈ ����
            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = buttonsInfo[i].buttonText;

            // ��ư�� ��� �̹����� �����ɴϴ� (Image ������Ʈ)
            var buttonImage = button.GetComponent<Image>();

            // ��ư�� �⺻ ���� ����
            buttonImage.color = defaultColor;
            buttonText.color = textDefaultColor;

            // ��ư Ŭ�� �� �ش� ������ �̵��ϴ� ������ ���
            button.onClick.AddListener(() => OnButtonClick(buttonsInfo[index].sceneToLoad, buttonImage));

            // ���̶���Ʈ �� Ŭ�� �̺�Ʈ ó��
            var buttonEventTrigger = button.gameObject.AddComponent<UnityEngine.EventSystems.EventTrigger>();

            var entryHighlight = new UnityEngine.EventSystems.EventTrigger.Entry
            {
                eventID = UnityEngine.EventSystems.EventTriggerType.PointerEnter
            };
            entryHighlight.callback.AddListener((eventData) => OnButtonHighlight(true, buttonImage, buttonText));
            buttonEventTrigger.triggers.Add(entryHighlight);

            var entryUnhighlight = new UnityEngine.EventSystems.EventTrigger.Entry
            {
                eventID = UnityEngine.EventSystems.EventTriggerType.PointerExit
            };
            entryUnhighlight.callback.AddListener((eventData) => OnButtonHighlight(false, buttonImage, buttonText));
            buttonEventTrigger.triggers.Add(entryUnhighlight);
        }
    }

    // ��ư Ŭ�� �� ����� �Լ�
    private void OnButtonClick(string sceneToLoad, Image buttonImage)
    {
        buttonImage.color = clickedColor;
        SceneManager.LoadScene(sceneToLoad);
    }

    // ��ư ���̶���Ʈ (���콺 ����) ó�� �Լ�
    private void OnButtonHighlight(bool isHighlighted, Image buttonImage, TextMeshProUGUI buttonText)
    {
        if (isHighlighted)
        {
            buttonImage.color = highlightColor;
            buttonText.color = textHighlightColor;
        }
        else
        {
            buttonImage.color = defaultColor;
            buttonText.color = textDefaultColor;
        }
    }
}
