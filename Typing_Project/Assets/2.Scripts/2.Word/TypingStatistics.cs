using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypingStatistics : MonoBehaviour
{
    public TextMeshProUGUI timeText;        // ��� �ð��� ǥ���� Text UI

    public TextMeshProUGUI wpmText;         // Ÿ���� ǥ���� Text UI
    public TextMeshProUGUI highestWpmText;  // �ְ� Ÿ���� ǥ���� Text UI

    public TextMeshProUGUI accuracyText;    // ��Ȯ���� ǥ���� Text UI
    public TextMeshProUGUI typoCountText;   // ��Ÿ ������ ǥ���� Text UI

    private float elapsedTime = 0f;         // ��� �ð� (��)
    private int totalTypedChars = 0;        // �� �Էµ� ���� ��
    private int correctTypedChars = 0;      // �ùٸ��� �Էµ� ���� ��
    private int typoCount = 0;              // ��Ÿ ����
    private float highestWpm = 0f;          // �ְ� Ÿ��

    private TypingPractice typingPractice;
    private bool isPracticeStarted = false;

    private void Start()
    {
        typingPractice = GetComponent<TypingPractice>();
        ResetStatistics();  // �ʱ�ȭ
    }

    private void Update()
    {
        if (isPracticeStarted)
        {
            elapsedTime += Time.deltaTime; // �ð� ����
            UpdateStatistics();            // ��� ������Ʈ
        }
    }

    // Ÿ�� ���� ����
    public void StartPractice()
    {
        isPracticeStarted = true;
        ResetStatistics();
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

    // ��� ������Ʈ (��� �ð�, WPM, ��Ȯ��, ��Ÿ ����)
    private void UpdateStatistics()
    {
        // 1. ��� �ð� ǥ�� (00:00 ����)
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timeText.text = $"{minutes:D2}:{seconds:D2}";

        // 2. WPM ���: (�ùٸ� ���� �� / 5) / (��� �ð� / 60) �� 000 ����
        float wpm = (elapsedTime > 0) ? (totalTypedChars / 5f) / (elapsedTime / 60f) : 0f;
        wpmText.text = $"{wpm:F0}"; // �Ҽ��� ���� ������ ���

        // 3. �ְ� Ÿ�� ��� �� 000 ����
        if (wpm > highestWpm)
        {
            highestWpm = wpm;
        }
        highestWpmText.text = $"{highestWpm:F0}"; // �Ҽ��� ���� ������ ���

        // 4. ��Ȯ�� ���: (�ùٸ� ���� / �� �Էµ� ����) * 100 �� 0.00 ����
        float accuracy = (totalTypedChars > 0) ? ((float)correctTypedChars / totalTypedChars) * 100f : 0f;
        accuracyText.text = $"{accuracy:F2}"; // �Ҽ��� 2�ڸ��� ���

        // 5. ��Ÿ ���� ǥ�� �� 0 ����
        typoCountText.text = $"{typoCount}"; // ������ ���
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

    // Ÿ�� ���� ����
    public void EndPractice()
    {
        isPracticeStarted = false;
    }
}
