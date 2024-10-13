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
    // UI Button 및 Text 컴포넌트
    public Button[] buttons;
    public ButtonInfo[] buttonsInfo;

    // 색상 변수
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

            // 버튼 텍스트 설정
            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = buttonsInfo[i].buttonText;
            buttonText.color = textDefaultColor;

            // 버튼의 배경 이미지를 가져옵니다 (Image 컴포넌트)
            var buttonImage = button.GetComponent<Image>();
            buttonImage.color = defaultColor;

            // 버튼 테두리 색상 설정 (Blue)
            var buttonOutline = button.GetComponent<Outline>();
            if (buttonOutline != null)
            {
                buttonOutline.effectColor = borderColor;
                buttonOutline.effectDistance = new Vector2(2, 2);
            }

            // 버튼 클릭 시 해당 씬으로 이동하는 리스너 등록
            button.onClick.AddListener(() => OnButtonClick(buttonsInfo[index].sceneToLoad, buttonImage));

            // 하이라이트 및 클릭 이벤트 처리
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

    // 버튼 클릭 시 실행될 함수
    private void OnButtonClick(string sceneToLoad, Image buttonImage)
    {
        buttonImage.color = clickedColor;
        SceneManager.LoadScene(sceneToLoad);
    }

    // 버튼 하이라이트 (마우스 오버) 처리 함수
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
//    // UI Button 및 Text 컴포넌트
//    public Button[] buttons;
//    public ButtonInfo[] buttonsInfo;

//    // 색상 변수
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

//            // 버튼 텍스트 설정
//            var buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
//            buttonText.text = buttonsInfo[i].buttonText;
//            buttonText.color = textDefaultColor;

//            // 버튼의 배경 이미지를 가져옵니다 (Image 컴포넌트)
//            var buttonImage = button.GetComponent<Image>();
//            buttonImage.color = defaultColor;

//            // 버튼 테두리 색상 설정 (있을 경우만 처리)
//            SetButtonOutline(button);

//            // 버튼 클릭 시 해당 씬으로 이동하는 리스너 등록
//            button.onClick.AddListener(() => OnButtonClick(buttonsInfo[index].sceneToLoad, buttonImage));

//            // 하이라이트 및 클릭 이벤트 처리
//            AddButtonEventTrigger(button, buttonImage, buttonText);
//        }
//    }

//    // 버튼 테두리 {색상, 크기}
//    private void SetButtonOutline(Button button)
//    {
//        var buttonOutline = button.GetComponent<Outline>();
//        if (buttonOutline != null)
//        {
//            buttonOutline.effectColor = new Color(0f, 0f, 1f, 190f / 255f);
//            buttonOutline.effectDistance = new Vector2(2, 2);
//        }
//    }

//    // 버튼에 마우스 오버, 클릭
//    private void AddButtonEventTrigger(Button button, Image buttonImage, TextMeshProUGUI buttonText)
//    {
//        var buttonEventTrigger = button.gameObject.AddComponent<UnityEngine.EventSystems.EventTrigger>();

//        AddEventTriggerEntry(buttonEventTrigger, UnityEngine.EventSystems.EventTriggerType.PointerEnter, () => OnButtonHighlight(true, buttonImage));
//        AddEventTriggerEntry(buttonEventTrigger, UnityEngine.EventSystems.EventTriggerType.PointerExit, () => OnButtonHighlight(false, buttonImage));
//    }

//    // 버튼이 클릭되었을 때 실행될 함수. 씬을 로드하고 클릭된 버튼의 색상을 변경
//    private void AddEventTriggerEntry(UnityEngine.EventSystems.EventTrigger eventTrigger, UnityEngine.EventSystems.EventTriggerType eventID, UnityEngine.Events.UnityAction action)
//    {
//        var entry = new UnityEngine.EventSystems.EventTrigger.Entry { eventID = eventID };
//        entry.callback.AddListener((eventData) => action.Invoke());
//        eventTrigger.triggers.Add(entry);
//    }

//    // 버튼 클릭 시 실행될 함수
//    private void OnButtonClick(string sceneToLoad, Image buttonImage)
//    {
//        buttonImage.color = clickedColor;
//        SceneManager.LoadScene(sceneToLoad);
//    }

//    // 버튼 하이라이트 (마우스 오버) 처리 함수
//    private void OnButtonHighlight(bool isHighlighted, Image buttonImage)
//    {
//        buttonImage.color = isHighlighted ? highlightColor : defaultColor;
//    }
//}
