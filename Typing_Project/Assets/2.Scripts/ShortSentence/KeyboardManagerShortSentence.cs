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
        "��Ÿ", "����", "�ѱ�",
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
        "��Ÿ", "����", "�ѱ�",
        "ESC", "��", "��", "��",
        "RAlt", "��", "��", "��",
        "��", "��", "��", "��",
        "��", "��", "��", "��",
        "-", "��", "��", "��",
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

    // ����
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

    // ������ Ű���� Ȱ��ȭ
    private void ActivateRightKeyboard()
    {
        rightKeyboardPanel.SetActive(true);
        leftKeyboardPanel.SetActive(false);

        SetMainKeyboardLanguage(rightKeyboardButtons, englishCharacters, koreanCharacters);
        SetSubKeyboardLanguage(rightKeyboardSubTexts, englishSubCharacters, koreanSubCharacters);
        NotFocusKeyboardButton(rightKeyboardButtons);
    }

    // ���� Ű���� Ȱ��ȭ
    private void ActivateLeftKeyboard()
    {
        rightKeyboardPanel.SetActive(false);
        leftKeyboardPanel.SetActive(true);

        SetMainKeyboardLanguage(leftKeyboardButtons, englishCharacters, koreanCharacters);
        SetSubKeyboardLanguage(leftKeyboardSubTexts, englishSubCharacters, koreanSubCharacters);
        NotFocusKeyboardButton(leftKeyboardButtons);
    }
    
    // ����Ű ����
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

    // ����Ű ����
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

    // Key ��ư ���̶���Ʈ or Ŭ�� �� ���� ��� �� ��Ȱ��ȭ
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
