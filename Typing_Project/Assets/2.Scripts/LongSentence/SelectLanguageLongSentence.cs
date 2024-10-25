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

    public Button backToTitleButtonOnLanguagePanel;

    // Chapters UI Panels
    public GameObject englishPanel;
    public GameObject koreanPanel;

    // ETC
    private ButtonManagerLongSentence buttonManager;

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
        buttonManager = GameObject.Find("ButtonManager").GetComponent<ButtonManagerLongSentence>();

        englishButton.onClick.AddListener(() => SelectLanguage("English"));
        koreanButton.onClick.AddListener(() => SelectLanguage("Korean"));
        backToTitleButtonOnLanguagePanel.onClick.AddListener(() => buttonManager.GoTitleScene());
    }

    // 언어 선택에 따른 처리
    private void SelectLanguage(string language)
    {
        PersistentDataLongSentence.selectedLanguage = language;
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
