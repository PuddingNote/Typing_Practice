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
    private TypingPracticeWordPractice typingPractice;

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
        typingPractice = GameObject.Find("TypingManager").GetComponent<TypingPracticeWordPractice>();

        if (typingPractice != null)
        {
            SetMainKeyboardLanguage();
            SetSubKeyboardLanguage();
            NotFocusKeyboardButton();
        }
    }

    // ����Ű ����
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

    // ����Ű ����
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

    // Key ��ư ���̶���Ʈ or Ŭ�� �� ���� ��� �� ��Ȱ��ȭ
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
