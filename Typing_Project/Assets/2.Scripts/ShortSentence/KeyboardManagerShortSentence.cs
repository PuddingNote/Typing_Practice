using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyboardManagerShortSentence : MonoBehaviour
{
    // UI
    public Button[] rightKeyboardButtons;
    public TextMeshProUGUI[] rightKeyboardSubTexts;
    public Button[] leftKeyboardButtons;
    public TextMeshProUGUI[] leftKeyboardSubTexts;

    public GameObject keyboardPanel;
    public GameObject rightKeyboardPanel;
    public GameObject leftKeyboardPanel;

    public Button rightButton;
    public Button leftButton;

    // Text Setting
    private string[] englishCharacters =
    {
        "기타", "영문", "한글",
        "ESC", "R", "U", "D",
        "RAlt", "H", "A", "W",
        "G", "N", "E", "Y",
        "V", "T", "O", "L",
        "-", "S", "I", "M",
        "Back Space", ".,", "Enter",
        "Space", "Shift", "Ctrl"
    };
    private string[] englishSubCharacters =
    {
        "", "P", "C", "K",
        "LAlt", "B", "", "X",
        "Q", "F", "", ";",
        "Z", "", "", "'",
        "=", "", "J", "/",
        "Win"
    };
    private string[] koreanCharacters =
    {
        "기타", "영문", "한글",
        "ESC", "ㅂ", "ㄴ", "ㅌ",
        "RAlt", "ㅈ", "ㅣ", "ㄷ",
        "ㅍ", "ㅇ", "ㆍ", "ㅁ",
        "ㅋ", "ㄱ", "ㅡ", "ㄹ",
        "-", "ㅎ", "ㅅ", "ㅊ",
        "Back Space", ".,", "Enter",
        "Space", "Shift", "Ctrl"
    };
    private string[] koreanSubCharacters =
    {
        "", "", "", "",
        "LAlt", "", "", "",
        "", "", "", ";",
        "", "", "", "'",
        "=", "", "", "/",
        "Win"
    };

    // ETC
    private TypingPracticeShortSentence typingPractice;

    // 시작
    public void StartPractice()
    {
        keyboardPanel.SetActive(true);
        this.enabled = true;
    }

    // Awake()
    private void Awake()
    {
        keyboardPanel.SetActive(false);
        this.enabled = false;
    }

    // Start()
    private void Start()
    {
        typingPractice = GameObject.Find("TypingManager").GetComponent<TypingPracticeShortSentence>();

        if (typingPractice != null)
        {
            ActivateRightKeyboard();
            leftKeyboardPanel.SetActive(false);
        }

        rightButton.onClick.AddListener(ActivateRightKeyboard);
        leftButton.onClick.AddListener(ActivateLeftKeyboard);
    }

    // 오른쪽 키보드 활성화
    private void ActivateRightKeyboard()
    {
        rightKeyboardPanel.SetActive(true);
        leftKeyboardPanel.SetActive(false);

        SetMainKeyboardLanguage(rightKeyboardButtons, englishCharacters, koreanCharacters);
        SetSubKeyboardLanguage(rightKeyboardSubTexts, englishSubCharacters, koreanSubCharacters);
        NotFocusKeyboardButton(rightKeyboardButtons);
    }

    // 왼쪽 키보드 활성화
    private void ActivateLeftKeyboard()
    {
        rightKeyboardPanel.SetActive(false);
        leftKeyboardPanel.SetActive(true);

        SetMainKeyboardLanguage(leftKeyboardButtons, englishCharacters, koreanCharacters);
        SetSubKeyboardLanguage(leftKeyboardSubTexts, englishSubCharacters, koreanSubCharacters);
        NotFocusKeyboardButton(leftKeyboardButtons);
    }
    
    // 메인키 세팅
    private void SetMainKeyboardLanguage(Button[] keyboardButtons, string[] englishKeys, string[] koreanKeys)
    {
        for (int i = 0; i < keyboardButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = keyboardButtons[i].GetComponentInChildren<TextMeshProUGUI>();

            if (PersistentDataShortSentence.selectedLanguage == "English")
            {
                buttonText.text = englishKeys[i];
            }
            else if (PersistentDataShortSentence.selectedLanguage == "Korean")
            {
                buttonText.text = koreanKeys[i];
            }
        }
    }

    // 서브키 세팅
    private void SetSubKeyboardLanguage(TextMeshProUGUI[] subTexts, string[] englishSubKeys, string[] koreanSubKeys)
    {
        for (int i = 0; i < subTexts.Length; i++)
        {
            if (PersistentDataShortSentence.selectedLanguage == "English")
            {
                subTexts[i].text = englishSubKeys[i];
            }
            else if (PersistentDataShortSentence.selectedLanguage == "Korean")
            {
                subTexts[i].text = koreanSubKeys[i];
            }
        }
    }

    // Key 버튼 하이라이트 or 클릭 등 관련 모든 것 비활성화
    private void NotFocusKeyboardButton(Button[] keyboardButtons)
    {
        foreach (Button button in keyboardButtons)
        {
            foreach (Graphic graphic in button.GetComponentsInChildren<Graphic>())
            {
                graphic.raycastTarget = false;
            }

            button.onClick.RemoveAllListeners();
        }
    }

}
