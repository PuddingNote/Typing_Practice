using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypingStatisticsLongSentence : MonoBehaviour, ITypingStatistics
{
    // UI
    public TextMeshProUGUI timeText;                // 시간
    public TextMeshProUGUI cpmText;                 // 타수
    public TextMeshProUGUI highestCpmText;          // 최고 타수
    public TextMeshProUGUI accuracyText;            // 정확도
    public TextMeshProUGUI typoCountText;           // 오타 개수

    // Typing Stats
    [HideInInspector] public float elapsedTime;     // 시간 (초)
    private int highestCpm;                         // 최고 타수

    // ETC
    private bool isPracticeStarted = false;         // 시작 여부
    private bool isPracticePaused = false;          // 타수 계산 멈춤 여부

    // 시작
    public void StartPractice()
    {
        isPracticeStarted = true;
        isPracticePaused = false;

        ResetStatistics();
    }

    // 통계 초기화
    private void ResetStatistics()
    {
        elapsedTime = 0f;
        highestCpm = 0;
    }

    // Update()
    private void Update()
    {
        if (isPracticeStarted && !isPracticePaused)
        {
            UpdateTime();
        }
    }

    // 전체시간 Update
    private void UpdateTime()
    {
        elapsedTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timeText.text = $"{minutes:D2}:{seconds:D2}";
    }

    // 타수 Update
    public void UpdateCPM(float currentCpm)
    {
        cpmText.text = $"{(int)currentCpm}";
    }

    // 최고타수 Update
    public void UpdateHighestCPM(int currentCpm)
    {
        if (currentCpm > highestCpm)
        {
            highestCpm = currentCpm;
            highestCpmText.text = $"{highestCpm}";
        }
    }

    // 정확도 Update
    public void UpdateAccuracy(float accuracy)
    {
        accuracyText.text = $"{accuracy:F2}";
    }

    // 오타 Update
    public void UpdateTypo(int currentTypo)
    {
        typoCountText.text = $"{currentTypo}";
    }

    // 일시정지
    public void PausePractice()
    {
        isPracticePaused = true;
    }

    // 재개
    public void ResumePractice()
    {
        isPracticePaused = false;
    }

    // 종료
    public void EndPractice()
    {
        isPracticeStarted = false;
    }
}