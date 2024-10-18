using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyboardManager : MonoBehaviour
{
    // UI
    public Button[] keyboardButtons;
    public TextMeshProUGUI[] keyboardSubTexts;
    public GameObject keyboardPanel;
    public Button[] keyGuideButtons;

    // Text Setting
    private string[] englishCharacters =
    {
        "기타", "영문", "한글",
        "ESC", "R", "U", "D",
        "Alt", "H", "A", "W",
        "G", "N", "E", "Y",
        "V", "T", "O", "L",
        "F", "S", "I", "M",
        "Back Space", ".,", "Enter",
        "Space", "Shift", "Ctrl"
    };
    private string[] englishSubCharacters =
    {
        "", "P", "C", "K",
        "", "B", "", "X",
        "Q", "", "", ";",
        "Z", "", "", "'",
        "", "", "J", "/",
    };

    private string[] koreanCharacters = 
    { 
        "기타", "영문", "한글", 
        "ESC", "ㅂ", "ㄴ", "ㅌ", 
        "Alt", "ㅈ", "ㅣ", "ㄷ",
        "ㅍ", "ㅇ", "ㆍ", "ㅁ",
        "ㅋ", "ㄱ", "ㅡ", "ㄹ",
        "-", "ㅎ", "ㅅ", "ㅊ",
        "Back Space", ".,", "Enter",
        "Space", "Shift", "Ctrl"
    };
    private string[] koreanSubCharacters =
    {
        "", "", "", "",
        "", "", "", "",
        "", "", "", ";",
        "", "", "", "'",
        "=", "", "", "/",
    };
    private string[] koreanShiftCharacters =
    {
        "", "ㅃ", "", "",
        "", "ㅉ", "", "ㄸ",
        "", "", "", "",
        "", "ㄲ", "", "",
        "", "", "ㅆ", "",
    };

    private string[] otherCharacters =
    {
        "기타", "영문", "한글",
        "1", "6", "Home", "End",
        "2", "7", "[", "←",
        "3", "8", "↑", "↓",
        "4", "9", "'", "→",
        "5", "0", "PgUp", "PgDn",
        "Del", "Ins", "Enter",
        "Space", "Shift", "Ctrl"
    };
    private string[] otherSubCharacters =
    {
        "F1", "F6", "", "",
        "F2", "F7", "]", "",
        "F3", "F8", "", "",
        "F4", "F9", "\\", "'",
        "F5", "F10", "", "",
    };

    // 영어
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
    // 천지인 자음
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
        { "ㅃ", new string[] { "ㅂ", "ㅂ" } },
        { "ㄸ", new string[] { "ㄷ", "ㄷ" } },
        { "ㅉ", new string[] { "ㅈ", "ㅈ" } },
        { "ㄲ", new string[] { "ㄱ", "ㄱ" } },
        { "ㅆ", new string[] { "ㅅ", "ㅅ" } }
    };
    // 천지인 모음
    Dictionary<string, string[]> chonjiinVowels = new Dictionary<string, string[]>()
    {
        { "ㅏ", new string[] { "ㅣ", "ㆍ" } },
        { "ㅑ", new string[] { "ㅣ", "ㆍ", "ㆍ" } },
        { "ㅓ", new string[] { "ㆍ", "ㅣ" } },
        { "ㅕ", new string[] { "ㆍ", "ㆍ", "ㅣ" } },
        { "ㅗ", new string[] { "ㆍ", "ㅡ" } },
        { "ㅛ", new string[] { "ㆍ", "ㆍ", "ㅡ" } },
        { "ㅜ", new string[] { "ㅡ", "ㆍ" } },
        { "ㅠ", new string[] {  "ㅡ", "ㆍ", "ㆍ" } },
        { "ㅡ", new string[] { "ㅡ" } },
        { "ㅣ", new string[] { "ㅣ" } },
        { "ㅐ", new string[] { "ㅣ", "ㆍ", "ㅣ" } },
        { "ㅔ", new string[] { "ㆍ", "ㅣ", "ㅣ" } },
        { "ㅒ", new string[] { "ㅣ", "ㆍ", "ㆍ", "ㅣ" } },
        { "ㅖ", new string[] { "ㆍ", "ㆍ", "ㅣ", "ㅣ" } }
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
            SetMainKeyboardLanguage();
            SetSubKeyboardLanguage();
            DisableButtonInteractions();
        }
    }

    // 메인키 세팅
    private void SetMainKeyboardLanguage()
    {
        for (int i = 0; i < keyboardButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = keyboardButtons[i].GetComponentInChildren<TextMeshProUGUI>();

            if (PersistentDataPositionPractice.selectedType == "English")
            {
                buttonText.text = englishCharacters[i];
            }
            else if (PersistentDataPositionPractice.selectedType == "Korean")
            {
                buttonText.text = koreanCharacters[i];
            }
            else                // 임시
            {
                buttonText.text = otherCharacters[i];
            }
        }
    }

    // 서브키 세팅
    private void SetSubKeyboardLanguage()
    {
        for (int i = 0; i < keyboardSubTexts.Length; i++)
        {
            if (PersistentDataPositionPractice.selectedType == "English")
            {
                keyboardSubTexts[i].text = englishSubCharacters[i];
            }
            else if (PersistentDataPositionPractice.selectedType == "Korean")
            {
                keyboardSubTexts[i].text = koreanSubCharacters[i];
            }
            else
            {
                keyboardSubTexts[i].text = otherSubCharacters[i];
            }
        }
    }

    // 버튼 클릭 비활성화
    private void DisableButtonInteractions()
    {
        foreach (Button button in keyboardButtons)
        {
            button.GetComponent<Graphic>().raycastTarget = false;
            button.onClick.RemoveAllListeners();
        }
    }

    // 특정 문자를 가진 버튼 색상 변경
    public void HighlightKey(string highlightText)
    {
        ResetKeyColors();
        DisableKeyGuideButtons();

        string[] characters;

        // 영어 입력 뭔가 문제가 많은데
        // English
        if (PersistentDataPositionPractice.selectedType == "English")
        {
            bool isUpper = char.IsUpper(highlightText[0]);
            highlightText = highlightText.ToUpper();
            characters = englishCharacters;

            // Shift가 필요한 경우
            for (int i = 0; i < englishCharacters.Length; i++)
            {
                if (isUpper && characters[i] == "Shift")
                {
                    ColorBlock colorBlock = keyboardButtons[i].colors;
                    colorBlock.normalColor = new Color(253f / 255f, 224f / 255f, 71f / 255f);
                    keyboardButtons[i].colors = colorBlock;
                    break;
                }
            }
            // 메인에서 발견되는 경우
            for (int i = 0; i < englishCharacters.Length; i++)
            {
                if (characters[i] == highlightText)
                {
                    ColorBlock colorBlock = keyboardButtons[i].colors;
                    colorBlock.normalColor = new Color(253f / 255f, 224f / 255f, 71f / 255f);
                    keyboardButtons[i].colors = colorBlock;
                    break;
                }
            }
            // 서브에서 발견되는 경우
            for (int i = 0; i < englishSubCharacters.Length; i++)
            {
                if (englishSubCharacters[i] == highlightText)
                {
                    ColorBlock colorBlock = keyboardButtons[i + 3].colors;
                    colorBlock.normalColor = new Color(253f / 255f, 224f / 255f, 71f / 255f);
                    keyboardButtons[i + 3].colors = colorBlock;
                    break;
                }
            }

            if(!isUpper) highlightText = highlightText.ToLower();

            ActivateKeyGuideButtons(highlightText);
        }
        // Korean
        else if (PersistentDataPositionPractice.selectedType == "Korean")
        {
            characters = koreanCharacters;

            // 모음 조합 처리
            if (chonjiinVowels.ContainsKey(highlightText))
            {
                string[] vowelComposition = chonjiinVowels[highlightText];

                foreach (string part in vowelComposition)
                {
                    for (int i = 0; i < koreanCharacters.Length; i++)
                    {
                        if (koreanCharacters[i] == part)
                        {
                            ColorBlock colorBlock = keyboardButtons[i].colors;
                            colorBlock.normalColor = new Color(253f / 255f, 224f / 255f, 71f / 255f);
                            keyboardButtons[i].colors = colorBlock;
                            break;
                        }
                    }
                }
            }
            // 자음 조합 처리
            if (chonjiinConsonants.ContainsKey(highlightText))
            {
                string[] consonantComposition = chonjiinConsonants[highlightText];

                foreach (string part in consonantComposition)
                {
                    for (int i = 0; i < koreanCharacters.Length; i++)
                    {
                        if (koreanCharacters[i] == part)
                        {
                            ColorBlock colorBlock = keyboardButtons[i].colors;
                            colorBlock.normalColor = new Color(253f / 255f, 224f / 255f, 71f / 255f);
                            keyboardButtons[i].colors = colorBlock;
                            break;
                        }
                    }
                }
            }
            // 기존 Shift 처리 (이게 쌍자음인데 일단 넣어둠)
            for (int i = 0; i < koreanShiftCharacters.Length; i++)
            {
                if (koreanShiftCharacters[i] == highlightText)
                {
                    ColorBlock colorBlock = keyboardButtons[i + 3].colors;
                    colorBlock.normalColor = new Color(253f / 255f, 224f / 255f, 71f / 255f);
                    keyboardButtons[i + 3].colors = colorBlock;
                    break;
                }
            }
            // 메인에서 발견되는 경우
            for (int i = 0; i < koreanCharacters.Length; i++)
            {
                if (characters[i] == highlightText)
                {
                    ColorBlock colorBlock = keyboardButtons[i].colors;
                    colorBlock.normalColor = new Color(253f / 255f, 224f / 255f, 71f / 255f);
                    keyboardButtons[i].colors = colorBlock;
                    break;
                }
            }

            ActivateKeyGuideButtons(highlightText);
        }
        // Other
        else
        {
            characters = otherCharacters;
        }

    }

    // KeyGuide 버튼 비활성화
    public void DisableKeyGuideButtons()
    {
        foreach (Button button in keyGuideButtons)
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = ""; // 텍스트 초기화
            button.gameObject.SetActive(false);
        }
    }

    // KeyGuide 버튼 활성화
    private void ActivateKeyGuideButtons(string highlightText)
    {
        List<string> subCharacters = new List<string>();

        if (PersistentDataPositionPractice.selectedType == "English")
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
        else if (PersistentDataPositionPractice.selectedType == "Korean")
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
        else
        {
            // Other
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

    // 모든 키 색상 초기화
    public void ResetKeyColors()
    {
        for (int i = 0; i < keyboardButtons.Length; i++)
        {
            ColorBlock colorBlock = keyboardButtons[i].colors;
            colorBlock.normalColor = Color.white;
            keyboardButtons[i].colors = colorBlock;
        }
    }

    // 입력 완료 시 호출하는 함수
    public void OnInputComplete()
    {
        DisableKeyGuideButtons(); // 입력 완료 시 버튼 비활성화
        // 추가 로직 작성 가능
    }

}
