using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLanguageShortSentence : MonoBehaviour
{
    // UI
    public GameObject languagePanel;

    // Buttons
    public Button englishButton;
    public Button koreanButton;

    // ETC
    private KeyboardManagerShortSentence keyboardManager;
    private CountDown countDown;

    // Start()
    private void Start()
    {
        languagePanel.SetActive(true);
    }

    // Awake()
    private void Awake()
    {
        keyboardManager = GameObject.Find("KeyboardManager").GetComponent<KeyboardManagerShortSentence>();
        countDown = GetComponent<CountDown>();

        englishButton.onClick.AddListener(() => SelectLanguage("English"));
        koreanButton.onClick.AddListener(() => SelectLanguage("Korean"));
    }

    // 언어 선택에 따른 처리
    private void SelectLanguage(string language)
    {
        Debug.Log(language);
        Debug.Log(keyboardManager);
        PersistentDataShortSentence.selectedLanguage = language;
        languagePanel.SetActive(false);
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

}
