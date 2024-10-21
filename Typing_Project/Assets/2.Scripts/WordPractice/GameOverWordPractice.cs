using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverWordPractice : MonoBehaviour
{
    // UI
    public GameObject gameOverPanel;
    public TextMeshProUGUI timeText;                // �ð�
    public TextMeshProUGUI accuracyText;            // ��Ȯ��
    public TextMeshProUGUI typoCountText;           // ��Ÿ

    // ETC
    private TypingStatisticsWordPractice typingStatistics;

    // Awake()
    private void Awake()
    {
        typingStatistics = FindObjectOfType<TypingStatisticsWordPractice>();
        gameOverPanel.SetActive(false);
    }

    // GameOverStats
    public void ShowGameOverStats()
    {
        gameOverPanel.SetActive(true);

        // ����ð�
        float elapsedTime = typingStatistics.elapsedTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timeText.text = $"{minutes:D2}:{seconds:D2}";

        // ��Ȯ��
        accuracyText.text = typingStatistics.accuracyText.text + "%";

        // ��Ÿ
        typoCountText.text = typingStatistics.typoCountText.text + "��";
    }

}
