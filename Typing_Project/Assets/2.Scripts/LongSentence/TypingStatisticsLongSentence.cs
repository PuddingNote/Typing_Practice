using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypingStatisticsLongSentence : MonoBehaviour, ITypingStatistics
{
    // UI
    public TextMeshProUGUI timeText;                // �ð�
    public TextMeshProUGUI cpmText;                 // Ÿ��
    public TextMeshProUGUI highestCpmText;          // �ְ� Ÿ��
    public TextMeshProUGUI accuracyText;            // ��Ȯ��
    public TextMeshProUGUI typoCountText;           // ��Ÿ ����

    // Typing Stats
    [HideInInspector] public float elapsedTime;     // �ð� (��)
    private int highestCpm;                         // �ְ� Ÿ��
    private int typoCount;                          // ��Ÿ ����

    // ETC
    private bool isPracticeStarted = false;         // ���� ����
    private bool isPracticePaused = false;          // Ÿ�� ��� ���� ����

    // ��� �ʱ�ȭ
    private void ResetStatistics()
    {
        elapsedTime = 0f;
        highestCpm = 0;
        typoCount = 0;
    }

    // Awake()
    private void Awake()
    {
        ResetStatistics();
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

    // Ÿ�� Update
    public void UpdateCPM(float currentCpm)
    {
        cpmText.text = $"{(int)currentCpm}";
    }

    // �ְ�Ÿ�� Update
    public void UpdateHighestCPM(int currentCpm)
    {
        if (currentCpm > highestCpm)
        {
            highestCpm = currentCpm;
            highestCpmText.text = $"{highestCpm}";
        }
    }

    // ��Ȯ�� Update
    public void UpdateAccuracy(float accuracy)
    {
        accuracyText.text = $"{accuracy:F2}";
    }

    // ��Ÿ Update
    public void UpdateTypo(bool isEnter, int currentTypo)
    {
        if (isEnter)
        {
            typoCount += currentTypo;
        }
        typoCountText.text = $"{typoCount + currentTypo}";
    }

    // ����
    public void StartPractice()
    {
        isPracticeStarted = true;
        isPracticePaused = false;
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