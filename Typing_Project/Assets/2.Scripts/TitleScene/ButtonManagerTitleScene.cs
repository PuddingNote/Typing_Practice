using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

// Button Info
[System.Serializable]
public class ButtonInfo
{
    public string buttonText;
    public string sceneToLoad;
}

public class ButtonManagerTitleScene : MonoBehaviour
{
    // UI
    public Button[] buttons;
    public ButtonInfo[] buttonsInfo;

    // Color Variables
    private Color defaultColor = Color.white;
    private Color highlightColor = new Color(219f / 255f, 234f / 255f, 254f / 255f);
    private Color clickedColor = new Color(219f / 255f, 234f / 255f, 254f / 255f);
    private Color textDefaultColor = new Color(0f, 0f, 1f, 170f / 255f);
    private Color borderColor = new Color(0f, 0f, 1f, 170f / 255f);

    // Awake()
    private void Awake()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            var button = buttons[i];

            // Set Buttons
            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = buttonsInfo[i].buttonText;
            buttonText.color = textDefaultColor;

            var buttonImage = button.GetComponent<Image>();
            buttonImage.color = defaultColor;

            var buttonOutline = button.GetComponent<Outline>();
            if (buttonOutline != null)
            {
                buttonOutline.effectColor = borderColor;
                buttonOutline.effectDistance = new Vector2(2, 2);
            }

            // 버튼 클릭 시 해당 씬으로 이동하는 리스너 등록
            // 하이라이트 및 클릭 이벤트 처리
            button.onClick.AddListener(() => OnButtonClick(buttonsInfo[index].sceneToLoad, buttonImage));

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

    // 버튼 클릭
    private void OnButtonClick(string sceneToLoad, Image buttonImage)
    {
        buttonImage.color = clickedColor;
        SceneManager.LoadSceneAsync(sceneToLoad);
    }

    // 버튼 하이라이트
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

    // 종료
    public void EndGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}