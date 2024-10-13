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

public class ButtonManager0 : MonoBehaviour
{
    // UI Button �� Text ������Ʈ
    public Button[] buttons;
    public ButtonInfo[] buttonsInfo;

    // ���� ����
    private Color defaultColor = Color.white;
    private Color highlightColor = new Color(219f / 255f, 234f / 255f, 254f / 255f);
    private Color clickedColor = new Color(219f / 255f, 234f / 255f, 254f / 255f);
    private Color textDefaultColor = new Color(0f, 0f, 1f, 170f / 255f);
    private Color borderColor = new Color(0f, 0f, 1f, 170f / 255f);

    private void Awake()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            var button = buttons[i];

            // ��ư �ؽ�Ʈ ����
            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = buttonsInfo[i].buttonText;
            buttonText.color = textDefaultColor;

            // ��ư�� ��� �̹����� �����ɴϴ� (Image ������Ʈ)
            var buttonImage = button.GetComponent<Image>();
            buttonImage.color = defaultColor;

            // ��ư �׵θ� ���� ���� (Blue)
            var buttonOutline = button.GetComponent<Outline>();
            if (buttonOutline != null)
            {
                buttonOutline.effectColor = borderColor;
                buttonOutline.effectDistance = new Vector2(2, 2);
            }

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
        }
        else
        {
            buttonImage.color = defaultColor;
        }
    }
}


//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.SceneManagement;
//using TMPro;

//[System.Serializable]
//public class ButtonInfo
//{
//    public string buttonText;
//    public string sceneToLoad;
//}

//public class ButtonManager0 : MonoBehaviour
//{
//    // UI Button �� Text ������Ʈ
//    public Button[] buttons;
//    public ButtonInfo[] buttonsInfo;

//    // ���� ����
//    private Color defaultColor = Color.white;
//    private Color highlightColor = new Color(219f / 255f, 234f / 255f, 254f / 255f);
//    private Color clickedColor = new Color(219f / 255f, 234f / 255f, 254f / 255f);
//    private Color textDefaultColor = new Color(0f, 0f, 1f, 190f / 255f);

//    private void Awake()
//    {
//        for (int i = 0; i < buttons.Length; i++)
//        {
//            int index = i;
//            var button = buttons[i];

//            // ��ư �ؽ�Ʈ ����
//            var buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
//            buttonText.text = buttonsInfo[i].buttonText;
//            buttonText.color = textDefaultColor;

//            // ��ư�� ��� �̹����� �����ɴϴ� (Image ������Ʈ)
//            var buttonImage = button.GetComponent<Image>();
//            buttonImage.color = defaultColor;

//            // ��ư �׵θ� ���� ���� (���� ��츸 ó��)
//            SetButtonOutline(button);

//            // ��ư Ŭ�� �� �ش� ������ �̵��ϴ� ������ ���
//            button.onClick.AddListener(() => OnButtonClick(buttonsInfo[index].sceneToLoad, buttonImage));

//            // ���̶���Ʈ �� Ŭ�� �̺�Ʈ ó��
//            AddButtonEventTrigger(button, buttonImage, buttonText);
//        }
//    }

//    // ��ư �׵θ� {����, ũ��}
//    private void SetButtonOutline(Button button)
//    {
//        var buttonOutline = button.GetComponent<Outline>();
//        if (buttonOutline != null)
//        {
//            buttonOutline.effectColor = new Color(0f, 0f, 1f, 190f / 255f);
//            buttonOutline.effectDistance = new Vector2(2, 2);
//        }
//    }

//    // ��ư�� ���콺 ����, Ŭ��
//    private void AddButtonEventTrigger(Button button, Image buttonImage, TextMeshProUGUI buttonText)
//    {
//        var buttonEventTrigger = button.gameObject.AddComponent<UnityEngine.EventSystems.EventTrigger>();

//        AddEventTriggerEntry(buttonEventTrigger, UnityEngine.EventSystems.EventTriggerType.PointerEnter, () => OnButtonHighlight(true, buttonImage));
//        AddEventTriggerEntry(buttonEventTrigger, UnityEngine.EventSystems.EventTriggerType.PointerExit, () => OnButtonHighlight(false, buttonImage));
//    }

//    // ��ư�� Ŭ���Ǿ��� �� ����� �Լ�. ���� �ε��ϰ� Ŭ���� ��ư�� ������ ����
//    private void AddEventTriggerEntry(UnityEngine.EventSystems.EventTrigger eventTrigger, UnityEngine.EventSystems.EventTriggerType eventID, UnityEngine.Events.UnityAction action)
//    {
//        var entry = new UnityEngine.EventSystems.EventTrigger.Entry { eventID = eventID };
//        entry.callback.AddListener((eventData) => action.Invoke());
//        eventTrigger.triggers.Add(entry);
//    }

//    // ��ư Ŭ�� �� ����� �Լ�
//    private void OnButtonClick(string sceneToLoad, Image buttonImage)
//    {
//        buttonImage.color = clickedColor;
//        SceneManager.LoadScene(sceneToLoad);
//    }

//    // ��ư ���̶���Ʈ (���콺 ����) ó�� �Լ�
//    private void OnButtonHighlight(bool isHighlighted, Image buttonImage)
//    {
//        buttonImage.color = isHighlighted ? highlightColor : defaultColor;
//    }
//}
