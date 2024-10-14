using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountDown : MonoBehaviour
{
    // UI
    public GameObject countdownPanel;
    public TextMeshProUGUI countdownText;

    // ETC
    private ITypingPractice typingPractice;
    private ITypingStatistics typingStatistics;

    // Awake()
    private void Awake()
    {
        typingPractice = GetComponent<ITypingPractice>();
        typingStatistics = GetComponent<ITypingStatistics>();

        if (typingPractice == null || typingStatistics == null)
        {
            Debug.LogError("TypingPractice 또는 TypingStatistics 스크립트가 설정되지 않았습니다.");
            return;
        }

        StartCoroutine(StartCountdown());
    }

    // 시작 코루틴
    IEnumerator StartCountdown()
    {
        countdownPanel.SetActive(true);
        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        countdownPanel.SetActive(false);
        StartPractice();
    }

    // 시작
    private void StartPractice()
    {
        typingPractice.StartPractice();
        typingStatistics.StartPractice();
    }

}