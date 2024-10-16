using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectTitleLongSentence : MonoBehaviour
{
    // UI
    public GameObject selectTitlePanel;

    // Buttons
    public List<Button> englishChapterButtons;
    public List<Button> koreanChapterButtons;

    // Variables
    [HideInInspector] public string selectedTitle;
    private CountDown countDown;

    // Awake()
    private void Awake()
    {
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
    }

    // ��� ����
    private void SelectTitle(string title)
    {
        PersistentDataLongSentence.selectedTitle = title;
        selectedTitle = title;
        selectTitlePanel.SetActive(false);

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
}
