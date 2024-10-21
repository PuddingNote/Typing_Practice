using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLanguageWordPractice : MonoBehaviour
{
    // UI
    public GameObject languagePanel;

    // Buttons
    public Button englishButton;
    public Button koreanButton;

    // ETC
    private KeyboardManagerWordPractice keyboardManager;
    private CountDown countDown;

    // Start()
    private void Start()
    {
        languagePanel.SetActive(true);
    }

    // Awake()
    private void Awake()
    {
        keyboardManager = GameObject.Find("KeyboardManager").GetComponent<KeyboardManagerWordPractice>();
        countDown = GetComponent<CountDown>();

        englishButton.onClick.AddListener(() => SelectLanguage("English"));
        koreanButton.onClick.AddListener(() => SelectLanguage("Korean"));
    }

    // ��� ���ÿ� ���� ó��
    private void SelectLanguage(string language)
    {
        PersistentDataWordPractice.selectedLanguage = language;
        languagePanel.SetActive(false);
        keyboardManager.StartPractice();

        StartCountdown();
    }

    // CountDown ����
    private void StartCountdown()
    {
        if (countDown != null)
        {
            countDown.StartCoroutine("StartCountdown");
        }
        else
        {
            Debug.LogError("countDown ��ũ��Ʈ�� ������� �ʾҽ��ϴ�.");
        }
    }

}
