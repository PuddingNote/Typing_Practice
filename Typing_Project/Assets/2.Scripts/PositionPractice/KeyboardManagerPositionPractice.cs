using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyboardManagerPositionPractice : MonoBehaviour
{
    // UI
    public Button[] rightKeyboardButtons;
    public TextMeshProUGUI[] rightKeyboardSubTexts;
    public Button[] leftKeyboardButtons;
    public TextMeshProUGUI[] leftKeyboardSubTexts;

    public GameObject keyboardPanel;
    public GameObject rightKeyboardPanel;
    public GameObject leftKeyboardPanel;
    public Button[] keyGuideButtons;

    public Button rightButton;
    public Button leftButton;

    // Keyboard Text Setting
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
    
    // (천지인) Key Setting
    Dictionary<string, string[]> englishDic = new Dictionary<string, string[]>()
    {
        { "R", new string[] { "R" } },
        { "U", new string[] { "U" } },
        { "D", new string[] { "D" } },
        { "H", new string[] { "H" } },
        { "A", new string[] { "A" } },
        { "W", new string[] { "W" } },
        { "G", new string[] { "G" } },
        { "N", new string[] { "N" } },
        { "E", new string[] { "E" } },
        { "Y", new string[] { "Y" } },
        { "V", new string[] { "V" } },
        { "T", new string[] { "T" } },
        { "O", new string[] { "O" } },
        { "L", new string[] { "L" } },
        { "S", new string[] { "S" } },
        { "I", new string[] { "I" } },
        { "M", new string[] { "M" } },
        { "P", new string[] { "R", "R" } },
        { "C", new string[] { "U", "U" } },
        { "K", new string[] { "D", "D" } },
        { "B", new string[] { "H", "H" } },
        { "X", new string[] { "W", "W" } },
        { "Q", new string[] { "G", "G" } },
        { "F", new string[] { "N", "N" } },
        { "Z", new string[] { "V", "V" } },
        { "J", new string[] { "I", "I" } }
    };
    Dictionary<string, string[]> chonjiinConsonants = new Dictionary<string, string[]>()
    {
        { "ㅂ", new string[] { "ㅂ" } },
        { "ㅁ", new string[] { "ㅁ" } },
        { "ㅋ", new string[] { "ㅋ" } },
        { "ㅈ", new string[] { "ㅈ" } },
        { "ㄴ", new string[] { "ㄴ" } },
        { "ㅌ", new string[] { "ㅌ" } },
        { "ㄷ", new string[] { "ㄷ" } },
        { "ㅇ", new string[] { "ㅇ" } },
        { "ㅊ", new string[] { "ㅊ" } },
        { "ㄱ", new string[] { "ㄱ" } },
        { "ㄹ", new string[] { "ㄹ" } },
        { "ㅍ", new string[] { "ㅍ" } },
        { "ㅅ", new string[] { "ㅅ" } },
        { "ㅎ", new string[] { "ㅎ" } },
        { "ㅃ", new string[] { "Shift", "ㅂ" } },
        { "ㄸ", new string[] { "Shift", "ㄷ" } },
        { "ㅉ", new string[] { "Shift", "ㅈ" } },
        { "ㄲ", new string[] { "Shift", "ㄱ" } },
        { "ㅆ", new string[] { "Shift", "ㅅ" } }
    };
    public Dictionary<string, string[]> chonjiinVowels = new Dictionary<string, string[]>()
    {
        { "ㅏ", new string[] { "ㅣ", "ㆍ" } },
        { "ㅑ", new string[] { "ㅣ", "ㆍ", "ㆍ" } },
        { "ㅓ", new string[] { "ㆍ", "ㅣ" } },
        { "ㅕ", new string[] { "ㆍ", "ㆍ", "ㅣ" } },
        { "ㅗ", new string[] { "ㆍ", "ㅡ" } },
        { "ㅛ", new string[] { "ㆍ", "ㆍ", "ㅡ" } },
        { "ㅜ", new string[] { "ㅡ", "ㆍ" } },
        { "ㅠ", new string[] { "ㅡ", "ㆍ", "ㆍ" } },
        { "ㅡ", new string[] { "ㅡ" } },
        { "ㅣ", new string[] { "ㅣ" } },
        { "ㅐ", new string[] { "ㅣ", "ㆍ", "ㅣ" } },
        { "ㅒ", new string[] { "ㅣ", "ㆍ", "ㆍ", "ㅣ" } },
        { "ㅔ", new string[] { "ㆍ", "ㅣ", "ㅣ" } },
        { "ㅖ", new string[] { "ㆍ", "ㆍ", "ㅣ", "ㅣ" } }
    };
    public Dictionary<string, string[]> chonjiinVowelsInputCheck = new Dictionary<string, string[]>()
    {
        { "ㅏ", new string[] { "ㅣ", "ㅏ" } },
        { "ㅑ", new string[] { "ㅣ", "ㅏ", "ㅑ" } },
        { "ㅓ", new string[] { "ㅓ" } },
        { "ㅕ", new string[] { "ㅕ" } },
        { "ㅗ", new string[] { "ㅗ" } },
        { "ㅛ", new string[] { "ㅛ" } },
        { "ㅜ", new string[] { "ㅡ", "ㅜ" } },
        { "ㅠ", new string[] { "ㅡ", "ㅜ", "ㅠ" } },
        { "ㅡ", new string[] { "ㅡ" } },
        { "ㅣ", new string[] { "ㅣ" } },
        { "ㅐ", new string[] { "ㅣ", "ㅏ", "ㅐ" } },
        { "ㅒ", new string[] { "ㅣ", "ㅏ", "ㅑ", "ㅒ" } },
        { "ㅔ", new string[] { "ㅓ", "ㅔ" } },
        { "ㅖ", new string[] { "ㅕ", "ㅖ" } }
    };

    // ETC
    private TypingPracticePositionPractice typingPractice;

    // 시작
    public void StartPractice()
    {
        keyboardPanel.SetActive(true);
        DisableKeyGuideButtons();
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
        typingPractice = GameObject.Find("TypingManager").GetComponent<TypingPracticePositionPractice>();

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

            if (PersistentDataPositionPractice.selectedLanguage == "English")
            {
                buttonText.text = englishKeys[i];
            }
            else if (PersistentDataPositionPractice.selectedLanguage == "Korean")
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
            if (PersistentDataPositionPractice.selectedLanguage == "English")
            {
                subTexts[i].text = englishSubKeys[i];
            }
            else if (PersistentDataPositionPractice.selectedLanguage == "Korean")
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

    // 특정 문자를 가진 버튼 색상 변경
    public void HighlightKey(string highlightText)
    {
        ResetKeyColors();
        DisableKeyGuideButtons();

        string[] characters;

        // 오른손
        if (PersistentDataPositionPractice.selectedLanguage == "English")
            {
                bool isUpper = char.IsUpper(highlightText[0]);
                highlightText = highlightText.ToUpper();
                characters = englishCharacters;

                // Shift
                for (int i = 0; i < englishCharacters.Length; i++)
                {
                    if (isUpper && characters[i] == "Shift")
                    {
                        ColorBlock colorBlock = rightKeyboardButtons[i].colors;
                        colorBlock.normalColor = new Color(253f / 255f, 224f / 255f, 71f / 255f);
                        rightKeyboardButtons[i].colors = colorBlock;
                        break;
                    }
                }

                // Main
                for (int i = 0; i < englishCharacters.Length; i++)
                {
                    if (characters[i] == highlightText)
                    {
                        ColorBlock colorBlock = rightKeyboardButtons[i].colors;
                        colorBlock.normalColor = new Color(253f / 255f, 224f / 255f, 71f / 255f);
                        rightKeyboardButtons[i].colors = colorBlock;
                        break;
                    }
                }

                // Sub
                for (int i = 0; i < englishSubCharacters.Length; i++)
                {
                    if (englishSubCharacters[i] == highlightText)
                    {
                        ColorBlock colorBlock = rightKeyboardButtons[i + 3].colors;
                        colorBlock.normalColor = new Color(253f / 255f, 224f / 255f, 71f / 255f);
                        rightKeyboardButtons[i + 3].colors = colorBlock;
                        break;
                    }
                }

                if (!isUpper) highlightText = highlightText.ToLower();

                ActivateKeyGuideButtons(highlightText);
            }
        else if (PersistentDataPositionPractice.selectedLanguage == "Korean")
            {
                characters = koreanCharacters;

                // 모음
                if (chonjiinVowels.ContainsKey(highlightText))
                {
                    string[] vowelComposition = chonjiinVowels[highlightText];

                    foreach (string part in vowelComposition)
                    {
                        for (int i = 0; i < koreanCharacters.Length; i++)
                        {
                            if (koreanCharacters[i] == part)
                            {
                                ColorBlock colorBlock = rightKeyboardButtons[i].colors;
                                colorBlock.normalColor = new Color(253f / 255f, 224f / 255f, 71f / 255f);
                                rightKeyboardButtons[i].colors = colorBlock;
                                break;
                            }
                        }
                    }
                }
                // 자음
                if (chonjiinConsonants.ContainsKey(highlightText))
                {
                    string[] consonantComposition = chonjiinConsonants[highlightText];

                    foreach (string part in consonantComposition)
                    {
                        for (int i = 0; i < koreanCharacters.Length; i++)
                        {
                            if (koreanCharacters[i] == part)
                            {
                                ColorBlock colorBlock = rightKeyboardButtons[i].colors;
                                colorBlock.normalColor = new Color(253f / 255f, 224f / 255f, 71f / 255f);
                                rightKeyboardButtons[i].colors = colorBlock;
                                break;
                            }
                        }
                    }
                }

                // Main
                for (int i = 0; i < koreanCharacters.Length; i++)
                {
                    if (characters[i] == highlightText)
                    {
                        ColorBlock colorBlock = rightKeyboardButtons[i].colors;
                        colorBlock.normalColor = new Color(253f / 255f, 224f / 255f, 71f / 255f);
                        rightKeyboardButtons[i].colors = colorBlock;
                        break;
                    }
                }

                ActivateKeyGuideButtons(highlightText);
            }
        
        // 왼손
        if (PersistentDataPositionPractice.selectedLanguage == "English")
            {
                bool isUpper = char.IsUpper(highlightText[0]);
                highlightText = highlightText.ToUpper();
                characters = englishCharacters;

                // Shift
                for (int i = 0; i < englishCharacters.Length; i++)
                {
                    if (isUpper && characters[i] == "Shift")
                    {
                        ColorBlock colorBlock = leftKeyboardButtons[i].colors;
                        colorBlock.normalColor = new Color(253f / 255f, 224f / 255f, 71f / 255f);
                        leftKeyboardButtons[i].colors = colorBlock;
                        break;
                    }
                }

                // Main
                for (int i = 0; i < englishCharacters.Length; i++)
                {
                    if (characters[i] == highlightText)
                    {
                        ColorBlock colorBlock = leftKeyboardButtons[i].colors;
                        colorBlock.normalColor = new Color(253f / 255f, 224f / 255f, 71f / 255f);
                        leftKeyboardButtons[i].colors = colorBlock;
                        break;
                    }
                }

                // Sub
                for (int i = 0; i < englishSubCharacters.Length; i++)
                {
                    if (englishSubCharacters[i] == highlightText)
                    {
                        ColorBlock colorBlock = leftKeyboardButtons[i + 3].colors;
                        colorBlock.normalColor = new Color(253f / 255f, 224f / 255f, 71f / 255f);
                        leftKeyboardButtons[i + 3].colors = colorBlock;
                        break;
                    }
                }

                if (!isUpper) highlightText = highlightText.ToLower();

                ActivateKeyGuideButtons(highlightText);
            }
        else if (PersistentDataPositionPractice.selectedLanguage == "Korean")
            {
                characters = koreanCharacters;

                // 모음
                if (chonjiinVowels.ContainsKey(highlightText))
                {
                    string[] vowelComposition = chonjiinVowels[highlightText];

                    foreach (string part in vowelComposition)
                    {
                        for (int i = 0; i < koreanCharacters.Length; i++)
                        {
                            if (koreanCharacters[i] == part)
                            {
                                ColorBlock colorBlock = leftKeyboardButtons[i].colors;
                                colorBlock.normalColor = new Color(253f / 255f, 224f / 255f, 71f / 255f);
                                leftKeyboardButtons[i].colors = colorBlock;
                                break;
                            }
                        }
                    }
                }
                // 자음
                if (chonjiinConsonants.ContainsKey(highlightText))
                {
                    string[] consonantComposition = chonjiinConsonants[highlightText];

                    foreach (string part in consonantComposition)
                    {
                        for (int i = 0; i < koreanCharacters.Length; i++)
                        {
                            if (koreanCharacters[i] == part)
                            {
                                ColorBlock colorBlock = leftKeyboardButtons[i].colors;
                                colorBlock.normalColor = new Color(253f / 255f, 224f / 255f, 71f / 255f);
                                leftKeyboardButtons[i].colors = colorBlock;
                                break;
                            }
                        }
                    }
                }

                // Main
                for (int i = 0; i < koreanCharacters.Length; i++)
                {
                    if (characters[i] == highlightText)
                    {
                        ColorBlock colorBlock = leftKeyboardButtons[i].colors;
                        colorBlock.normalColor = new Color(253f / 255f, 224f / 255f, 71f / 255f);
                        leftKeyboardButtons[i].colors = colorBlock;
                        break;
                    }
                }

                ActivateKeyGuideButtons(highlightText);
            }
        
    }

    // KeyGuide 버튼 활성화
    private void ActivateKeyGuideButtons(string highlightText)
    {
        List<string> subCharacters = new List<string>();

        if (PersistentDataPositionPractice.selectedLanguage == "English")
        {
            bool isUpper = char.IsUpper(highlightText[0]);
            highlightText = highlightText.ToUpper();

            if (englishDic.ContainsKey(highlightText))
            {
                if (isUpper)
                {
                    subCharacters.Add("Shift");
                }

                subCharacters.AddRange(englishDic[highlightText]);
            }
        }
        else if (PersistentDataPositionPractice.selectedLanguage == "Korean")
        {
            if (chonjiinConsonants.ContainsKey(highlightText))
            {
                subCharacters.AddRange(chonjiinConsonants[highlightText]);
            }
            else if (chonjiinVowels.ContainsKey(highlightText))
            {
                subCharacters.AddRange(chonjiinVowels[highlightText]);
            }
        }

        for (int i = 0; i < keyGuideButtons.Length; i++)
        {
            if (i < subCharacters.Count)
            {
                keyGuideButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = subCharacters[i];
                keyGuideButtons[i].gameObject.SetActive(true);
            }
            else
            {
                keyGuideButtons[i].gameObject.SetActive(false);
            }
        }
    }

    // KeyGuide 버튼 비활성화
    public void DisableKeyGuideButtons()
    {
        foreach (Button button in keyGuideButtons)
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = "";
            button.gameObject.SetActive(false);
        }
    }

    // 모든 키 색상 초기화
    public void ResetKeyColors()
    {
        // 오른손
        for (int i = 0; i < rightKeyboardButtons.Length; i++)
        {
            ColorBlock colorBlock = rightKeyboardButtons[i].colors;
            colorBlock.normalColor = Color.white;
            rightKeyboardButtons[i].colors = colorBlock;
        }

        // 왼손
        for (int i = 0; i < leftKeyboardButtons.Length; i++)
        {
            ColorBlock colorBlock = leftKeyboardButtons[i].colors;
            colorBlock.normalColor = Color.white;
            leftKeyboardButtons[i].colors = colorBlock;
        }
    }

}
