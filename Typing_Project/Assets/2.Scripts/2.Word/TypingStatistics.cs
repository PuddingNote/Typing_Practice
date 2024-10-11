using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypingStatistics : MonoBehaviour
{
    public TextMeshProUGUI timeText;        // 경과 시간을 표시할 Text UI

    public TextMeshProUGUI wpmText;         // 타수를 표시할 Text UI
    public TextMeshProUGUI highestWpmText;  // 최고 타수를 표시할 Text UI

    public TextMeshProUGUI accuracyText;    // 정확도를 표시할 Text UI
    public TextMeshProUGUI typoCountText;   // 오타 개수를 표시할 Text UI

    private float elapsedTime = 0f;         // 경과 시간 (초)
    private int totalTypedChars = 0;        // 총 입력된 문자 수
    private int correctTypedChars = 0;      // 올바르게 입력된 문자 수
    private int typoCount = 0;              // 오타 개수
    private float highestWpm = 0f;          // 최고 타수

    private TypingPractice typingPractice;
    private bool isPracticeStarted = false;

    private void Start()
    {
        typingPractice = GetComponent<TypingPractice>();
        ResetStatistics();  // 초기화
    }

    private void Update()
    {
        if (isPracticeStarted)
        {
            elapsedTime += Time.deltaTime; // 시간 누적
            UpdateStatistics();            // 통계 업데이트
        }
    }

    // 타자 연습 시작
    public void StartPractice()
    {
        isPracticeStarted = true;
        ResetStatistics();
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

    // 통계 업데이트 (경과 시간, WPM, 정확도, 오타 개수)
    private void UpdateStatistics()
    {
        // 1. 경과 시간 표시 (00:00 형식)
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timeText.text = $"{minutes:D2}:{seconds:D2}";

        // 2. WPM 계산: (올바른 문자 수 / 5) / (경과 시간 / 60) 및 000 형식
        float wpm = (elapsedTime > 0) ? (totalTypedChars / 5f) / (elapsedTime / 60f) : 0f;
        wpmText.text = $"{wpm:F0}"; // 소수점 없이 정수로 출력

        // 3. 최고 타수 계산 및 000 형식
        if (wpm > highestWpm)
        {
            highestWpm = wpm;
        }
        highestWpmText.text = $"{highestWpm:F0}"; // 소수점 없이 정수로 출력

        // 4. 정확도 계산: (올바른 문자 / 총 입력된 문자) * 100 및 0.00 형식
        float accuracy = (totalTypedChars > 0) ? ((float)correctTypedChars / totalTypedChars) * 100f : 0f;
        accuracyText.text = $"{accuracy:F2}"; // 소수점 2자리로 출력

        // 5. 오타 개수 표시 및 0 형식
        typoCountText.text = $"{typoCount}"; // 정수로 출력
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

    // 타자 연습 종료
    public void EndPractice()
    {
        isPracticeStarted = false;
    }
}
