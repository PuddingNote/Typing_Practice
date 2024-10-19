using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyboardManagerLongSentence : MonoBehaviour
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
        "Alt", "H", "A", "W",
        "G", "N", "E", "Y",
        "V", "T", "O", "L",
        "-", "S", "I", "M",
        "Back Space", ".,", "Enter",
        "Space", "Shift", "Ctrl"
    };
    private string[] englishSubCharacters =
    {
        "", "P", "C", "K",
        "", "B", "", "X",
        "Q", "F", "", ";",
        "Z", "", "", "'",
        "=", "", "J", "/",
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

    // ETC
    private TypingPracticeLongSentence typingPractice;


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
        typingPractice = GameObject.Find("TypingManager").GetComponent<TypingPracticeLongSentence>();

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

            if (PersistentDataLongSentence.selectedLanguage == "English")
            {
                buttonText.text = englishCharacters[i];
            }
            else if (PersistentDataLongSentence.selectedLanguage == "Korean")
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
            if (PersistentDataLongSentence.selectedLanguage == "English")
            {
                keyboardSubTexts[i].text = englishSubCharacters[i];
            }
            else if (PersistentDataLongSentence.selectedLanguage == "Korean")
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
