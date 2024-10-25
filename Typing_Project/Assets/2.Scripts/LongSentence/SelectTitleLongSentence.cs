using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectTitleLongSentence : MonoBehaviour
{
    // UI
    public GameObject languagePanel;
    public GameObject selectTitlePanel;

    // Buttons
    public List<Button> englishChapterButtons;
    public List<Button> koreanChapterButtons;

    public Button backToLanguageButtonOnSelectTitlePanel;
    public Button backToTitleButtonOnSelectTitlePanel;

    // Variables
    [HideInInspector] public string selectedTitle;

    // ETC
    private ButtonManagerLongSentence buttonManager;
    private KeyboardManagerLongSentence keyboardManager;
    private CountDown countDown;

    // Awake()
    private void Awake()
    {
        buttonManager = GameObject.Find("ButtonManager").GetComponent<ButtonManagerLongSentence>();
        keyboardManager = GameObject.Find("KeyboardManager").GetComponent<KeyboardManagerLongSentence>();
        countDown = GetComponent<CountDown>();

        // 언어 선택에 따른 버튼 활성화
        if (PersistentDataLongSentence.selectedLanguage == "English")
        {
            ActivateButtons(englishChapterButtons);
            DeactivateButtons(koreanChapterButtons);
        }
        else if (PersistentDataLongSentence.selectedLanguage == "Korean")
        {
            ActivateButtons(koreanChapterButtons);
            DeactivateButtons(englishChapterButtons);
        }

        // 버튼과 제목 매핑
        foreach (var button in englishChapterButtons)
        {
            string buttonTitle = button.name;
            button.onClick.AddListener(() => SelectTitle(buttonTitle));
        }

        foreach (var button in koreanChapterButtons)
        {
            string buttonTitle = button.name;
            button.onClick.AddListener(() => SelectTitle(buttonTitle));
        }

        backToLanguageButtonOnSelectTitlePanel.onClick.AddListener(() => GoBackToLanguagePanel());
        backToTitleButtonOnSelectTitlePanel.onClick.AddListener(() => buttonManager.GoTitleScene());
    }

    // 활성화할 버튼 리스트
    private void ActivateButtons(List<Button> buttons)
    {
        foreach (var button in buttons)
        {
            button.gameObject.SetActive(true);
        }
    }

    // 비활성화할 버튼 리스트
    private void DeactivateButtons(List<Button> buttons)
    {
        foreach (var button in buttons)
        {
            button.gameObject.SetActive(false);
        }
    }

    // 긴글 선택
    private void SelectTitle(string title)
    {
        PersistentDataLongSentence.selectedTitle = title;
        selectedTitle = title;
        selectTitlePanel.SetActive(false);
        keyboardManager.StartPractice();

        StartCountdown();
    }

    // CountDown 시작
    private void StartCountdown()
    {
        if (countDown != null)
        {
            countDown.StartCoroutine("StartCountdown");
        }
        else
        {
            Debug.LogError("countDown 스크립트가 연결되지 않았습니다.");
        }
    }

    // Back
    private void GoBackToLanguagePanel()
    {
        selectTitlePanel.SetActive(false);
        languagePanel.SetActive(true);
    }
}
