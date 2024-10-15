using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLanguageLongSentence : MonoBehaviour
{
    // UI
    public GameObject languagePanel;
    public GameObject selectTitlePanel;

    // Buttons
    public Button englishButton;
    public Button koreanButton;

    // Chapters UI Panels
    public GameObject englishPanel;
    public GameObject koreanPanel;

    // Start()
    private void Start()
    {
        languagePanel.SetActive(true);
        selectTitlePanel.SetActive(false);
        englishPanel.SetActive(false);
        koreanPanel.SetActive(false);
    }

    // Awake()
    private void Awake()
    {
        englishButton.onClick.AddListener(() => SelectLanguage("English"));
        koreanButton.onClick.AddListener(() => SelectLanguage("Korean"));
    }

    // 언어 선택에 따른 처리
    private void SelectLanguage(string language)
    {
        PersistentData.selectedLanguage = language;
        languagePanel.SetActive(false);
        selectTitlePanel.SetActive(true);

        if (language == "English")
        {
            englishPanel.SetActive(true);
            koreanPanel.SetActive(false);
        }
        else if (language == "Korean")
        {
            englishPanel.SetActive(false);
            koreanPanel.SetActive(true);
        }
    }
}
