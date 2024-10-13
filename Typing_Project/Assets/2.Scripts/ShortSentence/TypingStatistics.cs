using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TypingStatistics : MonoBehaviour
{
    public TextMeshProUGUI timeText;        // �ð� UI
    public TextMeshProUGUI wpmText;         // Ÿ�� UI
    public TextMeshProUGUI highestWpmText;  // �ְ� Ÿ�� UI
    public TextMeshProUGUI accuracyText;    // ��Ȯ�� UI
    public TextMeshProUGUI typoCountText;   // ��Ÿ ���� UI

    private float elapsedTime;              // �ð� (��)
    private int totalTypedChars;            // �� �Էµ� ���� ��
    private int correctTypedChars;          // �ùٸ��� �Էµ� ���� ��
    private int typoCount;                  // ��Ÿ ����
    private float highestWpm;               // �ְ� Ÿ��

    private bool isPracticeStarted = false;
    private bool isPracticePaused = false;  // Ÿ�� ��� ���� ����

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

    // Ÿ�� ���� ����
    public void StartPractice()
    {
        isPracticeStarted = true;
        isPracticePaused = false;
    }

    // ����ڰ� ���ڸ� �Է��� �� ȣ�� (TypingPractice�� ����)
    public void OnCharacterTyped(bool isCorrect, int typedChars)
    {
        totalTypedChars += typedChars;

        if (isCorrect)
        {
            correctTypedChars += typedChars;
        }
        else
        {
            typoCount += typedChars;  // ��Ÿ ���� �� ����
        }

    }

    public void UpdateAccuracy(float accuracy)
    {
        accuracyText.text = $"{accuracy:F2}"; // ��Ȯ�� UI ������Ʈ
    }

    // ��� ������Ʈ (��� �ð�, WPM, ��Ȯ��, ��Ÿ ����)
    private void UpdateStatistics()
    {
        // 1. �ð�
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timeText.text = $"{minutes:D2}:{seconds:D2}";

        // 2. Ÿ�� (�ùٸ� ���� �� / 5) / (wpmī��Ʈ�ð� / 60)
        float wpm = (elapsedTime > 0) ? (totalTypedChars / 5f) / (elapsedTime / 60f) : 0f;
        wpmText.text = $"{wpm:F0}";

        // 3. �ְ�Ÿ��
        if (wpm > highestWpm)
        {
            highestWpm = wpm;
        }
        highestWpmText.text = $"{highestWpm:F0}";

        // 5. ��Ȯ�� (�ùٸ� ���� / �� �Էµ� ����) * 100
        float accuracy = (totalTypedChars > 0) ? ((float)correctTypedChars / totalTypedChars) * 100f : 0f;
        accuracyText.text = $"{accuracy:F2}";

        // 7. ��Ÿ ����
        typoCountText.text = $"{typoCount}";
    }

    // ��� �ʱ�ȭ
    private void ResetStatistics()
    {
        elapsedTime = 0f;
        totalTypedChars = 0;
        correctTypedChars = 0;
        typoCount = 0;
        highestWpm = 0f;
    }

    // Ÿ�� ���� �Ͻ� ����
    public void PausePractice()
    {
        isPracticePaused = true;
    }

    // Ÿ�� ���� ����
    public void EndPractice()
    {
        isPracticeStarted = false;
        isPracticePaused = true;    // ���� ���� �� Ÿ�� ��� ����
    }
}