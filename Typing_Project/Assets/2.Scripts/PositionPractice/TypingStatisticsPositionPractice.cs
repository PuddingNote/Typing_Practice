using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypingStatisticsPositionPractice : MonoBehaviour
{
    // UI
    public TextMeshProUGUI typeText;                // 타입형식
    public TextMeshProUGUI timeText;                // 시간
    public TextMeshProUGUI accuracyText;            // 정확도
    public TextMeshProUGUI typoCountText;           // 오타 개수

    // Typing Stats
    [HideInInspector] public float elapsedTime;     // 시간 (초)

    // ETC
    private bool isPracticeStarted = false;         // 시작 여부
    private bool isPracticePaused = false;          // 타수 계산 멈춤 여부

    // 시작
    public void StartPractice()
    {
        isPracticeStarted = true;
        isPracticePaused = false;

        SetUI();
        ResetStatistics();
    }

    // UI 세팅
    private void SetUI()
    {
        if (PersistentDataPositionPractice.selectedType == "English")
        {
            typeText.text = "영문자리";
        }
        else if (PersistentDataPositionPractice.selectedType == "Korean")
        {
            typeText.text = "한글자리";
        }
        else if (PersistentDataPositionPractice.selectedType == "Other")
        {
            typeText.text = "특수자리";
        }
    }

    // 통계 초기화
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

    // 전체 시간 Update
    private void UpdateTime()
    {
        elapsedTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timeText.text = $"{minutes:D2}:{seconds:D2}";
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
