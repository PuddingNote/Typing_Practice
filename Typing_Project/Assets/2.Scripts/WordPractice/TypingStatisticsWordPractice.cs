using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypingStatisticsWordPractice : MonoBehaviour, ITypingStatistics
{
    // UI
    public TextMeshProUGUI timeText;                // �ð�
    public TextMeshProUGUI accuracyText;            // ��Ȯ��
    public TextMeshProUGUI typoCountText;           // ��Ÿ ����

    // Typing Stats
    [HideInInspector] public float elapsedTime;     // �ð� (��)

    // ETC
    private bool isPracticeStarted = false;         // ���� ����
    private bool isPracticePaused = false;          // ���� ����

    // ����
    public void StartPractice()
    {
        isPracticeStarted = true;
        isPracticePaused = false;

        ResetStatistics();
    }

    // ��� �ʱ�ȭ
    private void ResetStatistics()
    {
        elapsedTime = 0f;
    }

    // Update()
    private void Update()
    {
        if (isPracticeStarted && !isPracticePaused)
        {
            UpdateTime();
        }
    }

    // ��ü �ð� Update
    private void UpdateTime()
    {
        elapsedTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timeText.text = $"{minutes:D2}:{seconds:D2}";
    }

    // ��Ȯ�� Update
    public void UpdateAccuracy(float accuracy)
    {
        accuracyText.text = $"{accuracy:F2}";
    }

    // ��Ÿ Update
    public void UpdateTypo(int currentTypo)
    {
        typoCountText.text = $"{currentTypo}";
    }

    // �Ͻ�����
    public void PausePractice()
    {
        isPracticePaused = true;
    }

    // �簳
    public void ResumePractice()
    {
        isPracticePaused = false;
    }

    // ����
    public void EndPractice()
    {
        isPracticeStarted = false;
    }
}
