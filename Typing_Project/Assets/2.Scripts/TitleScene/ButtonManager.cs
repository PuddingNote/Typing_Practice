using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class ButtonInfo
{
    public string buttonText;
    public string sceneToLoad;
}

public class ButtonManager : MonoBehaviour
{
    // UI Button 및 Text 컴포넌트
    public Button[] buttons;
    public ButtonInfo[] buttonsInfo;

    // 색상 변수
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

            // 버튼 텍스트 설정
            button.GetComponentInChildren<Text>().text = buttonsInfo[i].buttonText;

            // 버튼의 배경 이미지를 가져옵니다 (Image 컴포넌트)
            var buttonImage = button.GetComponent<Image>();

            // 버튼의 기본 상태 설정
            buttonImage.color = defaultColor;
            button.GetComponentInChildren<Text>().color = textDefaultColor;

            // 버튼 클릭 시 해당 씬으로 이동하는 리스너 등록
            button.onClick.AddListener(() => OnButtonClick(buttonsInfo[index].sceneToLoad, buttonImage));

            // 하이라이트 및 클릭 이벤트 처리
            var buttonEventTrigger = button.gameObject.AddComponent<UnityEngine.EventSystems.EventTrigger>();

            var entryHighlight = new UnityEngine.EventSystems.EventTrigger.Entry
            {
                eventID = UnityEngine.EventSystems.EventTriggerType.PointerEnter
            };
            entryHighlight.callback.AddListener((eventData) => OnButtonHighlight(true, buttonImage, button.GetComponentInChildren<Text>()));
            buttonEventTrigger.triggers.Add(entryHighlight);

            var entryUnhighlight = new UnityEngine.EventSystems.EventTrigger.Entry
            {
                eventID = UnityEngine.EventSystems.EventTriggerType.PointerExit
            };
            entryUnhighlight.callback.AddListener((eventData) => OnButtonHighlight(false, buttonImage, button.GetComponentInChildren<Text>()));
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
    private void OnButtonHighlight(bool isHighlighted, Image buttonImage, Text buttonText)
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
