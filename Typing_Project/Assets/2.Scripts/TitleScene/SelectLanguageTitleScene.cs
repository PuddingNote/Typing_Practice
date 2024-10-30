using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLanguageTitleScene : MonoBehaviour
{
    // UI
    public GameObject languagePanel;
    public GameObject handTypePanel;

    // Buttons
    public Button englishButton;
    public Button koreanButton;
    public Button backToTitleButtonOnLanguagePanel;

    // ETC
    private ButtonManagerTitleScene buttonManager;

    // Awake()
    private void Awake()
    {
        buttonManager = GameObject.Find("ButtonManager").GetComponent<ButtonManagerTitleScene>();

        englishButton.onClick.AddListener(() => SelectLanguage("English"));
        koreanButton.onClick.AddListener(() => SelectLanguage("Korean"));
        backToTitleButtonOnLanguagePanel.onClick.AddListener(() => buttonManager.GoTitleScene());
    }

    // 언어 선택에 따른 처리
    private void SelectLanguage(string language)
    {
        PersistentDataTitleScene.selectedLanguage = language;
        languagePanel.SetActive(false);
        handTypePanel.SetActive(true);
    }
}
