using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyboardManagerWordPractice : MonoBehaviour
{
    // UI
    public Button[] keyboardButtons;
    public TextMeshProUGUI[] keyboardSubTexts;
    public GameObject keyboardPanel;

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
    //private string[] englishSubCharacters =
    //{
    //    "", "P", "C", "K",
    //    "LAlt", "V", "", "X",
    //    "Q", "F", "", ";",
    //    "Z", "", "", "'",
    //    "=", "", "J", "/",
    //    "Win"
    //};

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
    private TypingPracticeWordPractice typingPractice;

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
        typingPractice = GameObject.Find("TypingManager").GetComponent<TypingPracticeWordPractice>();

        if (typingPractice != null)
        {
            SetMainKeyboardLanguage();
            SetSubKeyboardLanguage();
            NotFocusKeyboardButton();
        }
    }

    // 메인키 세팅
    private void SetMainKeyboardLanguage()
    {
        for (int i = 0; i < keyboardButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = keyboardButtons[i].GetComponentInChildren<TextMeshProUGUI>();

            if (PersistentDataWordPractice.selectedLanguage == "English")
            {
                buttonText.text = englishCharacters[i];
            }
            else if (PersistentDataWordPractice.selectedLanguage == "Korean")
            {
                buttonText.text = koreanCharacters[i];
            }
        }
    }

    // 서브키 세팅
    private void SetSubKeyboardLanguage()
    {
        for (int i = 0; i < keyboardSubTexts.Length; i++)
        {
            if (PersistentDataWordPractice.selectedLanguage == "English")
            {
                keyboardSubTexts[i].text = englishSubCharacters[i];
            }
            else if (PersistentDataWordPractice.selectedLanguage == "Korean")
            {
                keyboardSubTexts[i].text = koreanSubCharacters[i];
            }
        }
    }

    // Key 버튼 하이라이트 or 클릭 등 관련 모든 것 비활성화
    private void NotFocusKeyboardButton()
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
