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

        // ��� ���ÿ� ���� ��ư Ȱ��ȭ
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

        // ��ư�� ���� ����
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

    // Ȱ��ȭ�� ��ư ����Ʈ
    private void ActivateButtons(List<Button> buttons)
    {
        foreach (var button in buttons)
        {
            button.gameObject.SetActive(true);
        }
    }

    // ��Ȱ��ȭ�� ��ư ����Ʈ
    private void DeactivateButtons(List<Button> buttons)
    {
        foreach (var button in buttons)
        {
            button.gameObject.SetActive(false);
        }
    }

    // ��� ����
    private void SelectTitle(string title)
    {
        PersistentDataLongSentence.selectedTitle = title;
        selectedTitle = title;
        selectTitlePanel.SetActive(false);
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

    // Back
    private void GoBackToLanguagePanel()
    {
        selectTitlePanel.SetActive(false);
        languagePanel.SetActive(true);
    }
}
