using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverPositionPractice : MonoBehaviour
{
    // UI
    public GameObject gameOverPanel;
    public TextMeshProUGUI timeText;                // 시간
    public TextMeshProUGUI accuracyText;            // 정확도
    public TextMeshProUGUI typoCountText;           // 오타

    // ETC
    private TypingStatisticsPositionPractice typingStatistics;

    // Awake()
    private void Awake()
    {
        gameOverPanel.SetActive(false);
    }

    // Start()
    private void Start()
    {
        typingStatistics = FindObjectOfType<TypingStatisticsPositionPractice>();
    }

    // GameOverStats
    public void ShowGameOverStats()
    {
        gameOverPanel.SetActive(true);

        // 진행시간
        float elapsedTime = typingStatistics.elapsedTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timeText.text = $"{minutes:D2}:{seconds:D2}";

        // 정확도
        accuracyText.text = typingStatistics.accuracyText.text + "%";

        // 오타
        typoCountText.text = typingStatistics.typoCountText.text + "개";
    }


}
