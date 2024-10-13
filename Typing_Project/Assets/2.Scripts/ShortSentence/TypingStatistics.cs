using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TypingStatistics : MonoBehaviour
{
    public TextMeshProUGUI timeText;        // 시간 UI
    public TextMeshProUGUI wpmText;         // 타수 UI
    public TextMeshProUGUI highestWpmText;  // 최고 타수 UI
    public TextMeshProUGUI accuracyText;    // 정확도 UI
    public TextMeshProUGUI typoCountText;   // 오타 개수 UI

    private float elapsedTime;              // 시간 (초)
    private int totalTypedChars;            // 총 입력된 문자 수
    private int correctTypedChars;          // 올바르게 입력된 문자 수
    private int typoCount;                  // 오타 개수
    private float highestWpm;               // 최고 타수

    private bool isPracticeStarted = false;
    private bool isPracticePaused = false;  // 타수 계산 멈춤 여부

    private void Awake()
    {
        ResetStatistics();
    }

    private void Update()
    {
        if (isPracticeStarted && !isPracticePaused)
        {
            elapsedTime += Time.deltaTime;
            UpdateStatistics();
        }
    }

    // 타자 연습 시작
    public void StartPractice()
    {
        isPracticeStarted = true;
        isPracticePaused = false;
    }

    // 사용자가 문자를 입력할 때 호출 (TypingPractice와 연동)
    public void OnCharacterTyped(bool isCorrect, int typedChars)
    {
        totalTypedChars += typedChars;

        if (isCorrect)
        {
            correctTypedChars += typedChars;
        }
        else
        {
            typoCount += typedChars;  // 오타 문자 수 증가
        }

    }

    public void UpdateAccuracy(float accuracy)
    {
        accuracyText.text = $"{accuracy:F2}"; // 정확도 UI 업데이트
    }

    // 통계 업데이트 (경과 시간, WPM, 정확도, 오타 개수)
    private void UpdateStatistics()
    {
        // 1. 시간
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timeText.text = $"{minutes:D2}:{seconds:D2}";

        // 2. 타수 (올바른 문자 수 / 5) / (wpm카운트시간 / 60)
        float wpm = (elapsedTime > 0) ? (totalTypedChars / 5f) / (elapsedTime / 60f) : 0f;
        wpmText.text = $"{wpm:F0}";

        // 3. 최고타수
        if (wpm > highestWpm)
        {
            highestWpm = wpm;
        }
        highestWpmText.text = $"{highestWpm:F0}";

        // 5. 정확도 (올바른 문자 / 총 입력된 문자) * 100
        float accuracy = (totalTypedChars > 0) ? ((float)correctTypedChars / totalTypedChars) * 100f : 0f;
        accuracyText.text = $"{accuracy:F2}";

        // 7. 오타 개수
        typoCountText.text = $"{typoCount}";
    }

    // 통계 초기화
    private void ResetStatistics()
    {
        elapsedTime = 0f;
        totalTypedChars = 0;
        correctTypedChars = 0;
        typoCount = 0;
        highestWpm = 0f;
    }

    // 타자 연습 일시 정지
    public void PausePractice()
    {
        isPracticePaused = true;
    }

    // 타자 연습 종료
    public void EndPractice()
    {
        isPracticeStarted = false;
        isPracticePaused = true;    // 연습 종료 시 타수 계산 멈춤
    }
}