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
        "��Ÿ", "����", "�ѱ�",
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
        "��Ÿ", "����", "�ѱ�", 
        "ESC", "��", "��", "��", 
        "Alt", "��", "��", "��",
        "��", "��", "��", "��",
        "��", "��", "��", "��",
        "-", "��", "��", "��",
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
        "", "��", "", "",
        "", "��", "", "��",
        "", "", "", "",
        "", "��", "", "",
        "", "", "��", "",
    };

    private string[] otherCharacters =
    {
        "��Ÿ", "����", "�ѱ�",
        "1", "6", "Home", "End",
        "2", "7", "[", "��",
        "3", "8", "��", "��",
        "4", "9", "'", "��",
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

    // ����
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
    // õ���� ����
    Dictionary<string, string[]> chonjiinConsonants = new Dictionary<string, string[]>()
    {
        { "��", new string[] { "��" } },
        { "��", new string[] { "��" } },
        { "��", new string[] { "��" } },
        { "��", new string[] { "��" } },
        { "��", new string[] { "��" } },
        { "��", new string[] { "��" } },
        { "��", new string[] { "��" } },
        { "��", new string[] { "��" } },
        { "��", new string[] { "��" } },
        { "��", new string[] { "��" } },
        { "��", new string[] { "��" } },
        { "��", new string[] { "��" } },
        { "��", new string[] { "��" } },
        { "��", new string[] { "��" } },
        { "��", new string[] { "��", "��" } },
        { "��", new string[] { "��", "��" } },
        { "��", new string[] { "��", "��" } },
        { "��", new string[] { "��", "��" } },
        { "��", new string[] { "��", "��" } }
    };
    // õ���� ����
    Dictionary<string, string[]> chonjiinVowels = new Dictionary<string, string[]>()
    {
        { "��", new string[] { "��", "��" } },
        { "��", new string[] { "��", "��", "��" } },
        { "��", new string[] { "��", "��" } },
        { "��", new string[] { "��", "��", "��" } },
        { "��", new string[] { "��", "��" } },
        { "��", new string[] { "��", "��", "��" } },
        { "��", new string[] { "��", "��" } },
        { "��", new string[] {  "��", "��", "��" } },
        { "��", new string[] { "��" } },
        { "��", new string[] { "��" } },
        { "��", new string[] { "��", "��", "��" } },
        { "��", new string[] { "��", "��", "��" } },
        { "��", new string[] { "��", "��", "��", "��" } },
        { "��", new string[] { "��", "��", "��", "��" } }
    };



    // ETC
    private TypingPracticePositionPractice typingPractice;


    // ����
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

    // ����Ű ����
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
            else                // �ӽ�
            {
                buttonText.text = otherCharacters[i];
            }
        }
    }

    // ����Ű ����
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

    // ��ư Ŭ�� ��Ȱ��ȭ
    private void DisableButtonInteractions()
    {
        foreach (Button button in keyboardButtons)
        {
            button.GetComponent<Graphic>().raycastTarget = false;
            button.onClick.RemoveAllListeners();
        }
    }

    // Ư�� ���ڸ� ���� ��ư ���� ����
    public void HighlightKey(string highlightText)
    {
        ResetKeyColors();
        DisableKeyGuideButtons();

        string[] characters;

        // ���� �Է� ���� ������ ������
        // English
        if (PersistentDataPositionPractice.selectedType == "English")
        {
            bool isUpper = char.IsUpper(highlightText[0]);
            highlightText = highlightText.ToUpper();
            characters = englishCharacters;

            // Shift�� �ʿ��� ���
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
            // ���ο��� �߰ߵǴ� ���
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
            // ���꿡�� �߰ߵǴ� ���
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

            // ���� ���� ó��
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
            // ���� ���� ó��
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
            // ���� Shift ó�� (�̰� �������ε� �ϴ� �־��)
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
            // ���ο��� �߰ߵǴ� ���
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

    // KeyGuide ��ư ��Ȱ��ȭ
    public void DisableKeyGuideButtons()
    {
        foreach (Button button in keyGuideButtons)
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = ""; // �ؽ�Ʈ �ʱ�ȭ
            button.gameObject.SetActive(false);
        }
    }

    // KeyGuide ��ư Ȱ��ȭ
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

    // ��� Ű ���� �ʱ�ȭ
    public void ResetKeyColors()
    {
        for (int i = 0; i < keyboardButtons.Length; i++)
        {
            ColorBlock colorBlock = keyboardButtons[i].colors;
            colorBlock.normalColor = Color.white;
            keyboardButtons[i].colors = colorBlock;
        }
    }

    // �Է� �Ϸ� �� ȣ���ϴ� �Լ�
    public void OnInputComplete()
    {
        DisableKeyGuideButtons(); // �Է� �Ϸ� �� ��ư ��Ȱ��ȭ
        // �߰� ���� �ۼ� ����
    }

}
