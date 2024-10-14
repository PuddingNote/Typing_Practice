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
            Debug.LogError("TypingPractice �Ǵ� TypingStatistics ��ũ��Ʈ�� �������� �ʾҽ��ϴ�.");
            return;
        }

        StartCoroutine(StartCountdown());
    }

    // ���� �ڷ�ƾ
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

    // ����
    private void StartPractice()
    {
        typingPractice.StartPractice();
        typingStatistics.StartPractice();
    }

}