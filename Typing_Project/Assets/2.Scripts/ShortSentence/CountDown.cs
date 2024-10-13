using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountDown : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    public GameObject countdownPanel;
    private TypingPractice typingPractice;

    //private bool isPracticeStarted;

    private void Start()
    {
        //isPracticeStarted = false;

        typingPractice = GetComponent<TypingPractice>();

        StartCoroutine(StartCountdown());
    }

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

    private void StartPractice()
    {
        //isPracticeStarted = true;
        typingPractice.enabled = true;

        // TypingStatistics의 StartPractice 메서드 호출
        TypingStatistics typingStatistics = GetComponent<TypingStatistics>();
        if (typingStatistics != null)
        {
            typingStatistics.StartPractice();
        }
    }

}
