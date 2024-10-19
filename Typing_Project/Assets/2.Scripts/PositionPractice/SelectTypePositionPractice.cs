using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectTypePositionPractice : MonoBehaviour
{
    // UI
    public GameObject selectTypePanel;

    // Buttons
    public Button englishButton;
    public Button koreanButton;
    public Button otherButton;

    // ETC
    private TypingPracticePositionPractice typingPractice;
    private TypingStatisticsPositionPractice typingStatistics;
    private KeyboardManagerPositionPractice keyboardManager;

    // Awake()
    private void Awake()
    {
        typingPractice = GetComponent<TypingPracticePositionPractice>();
        typingStatistics = GetComponent<TypingStatisticsPositionPractice>();
        keyboardManager = GameObject.Find("KeyboardManager").GetComponent<KeyboardManagerPositionPractice>();

        englishButton.onClick.AddListener(() => SelectType("English"));
        koreanButton.onClick.AddListener(() => SelectType("Korean"));
        otherButton.onClick.AddListener(() => SelectType("Other"));
    }

    // Start()
    private void Start()
    {
        if (string.IsNullOrEmpty(PersistentDataPositionPractice.selectedType))
        {
            selectTypePanel.SetActive(true);
        }
        else
        {
            selectTypePanel.SetActive(false);
            StartPractice();
        }
    }

    // 타입 선택 결과
    private void SelectType(string type)
    {
        PersistentDataPositionPractice.selectedType = type;
        selectTypePanel.SetActive(false);

        StartPractice();
    }

    // 시작
    public void StartPractice()
    {
        typingPractice.StartPractice();
        typingStatistics.StartPractice();
        keyboardManager.StartPractice();
    }

}
