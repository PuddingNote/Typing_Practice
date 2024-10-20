using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverLongSentence : MonoBehaviour
{
    // UI
    public GameObject gameOverPanel;
    public TextMeshProUGUI timeText;                // 시간
    public TextMeshProUGUI cpmText;                 // 타수
    public TextMeshProUGUI accuracyText;            // 정확도
    public TextMeshProUGUI typoCountText;           // 오타

    // ETC
    private TypingStatisticsLongSentence typingStatistics;

    // Awake()
    private void Awake()
    {
        typingStatistics = FindObjectOfType<TypingStatisticsLongSentence>();
        gameOverPanel.SetActive(false);
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

        // 타수
        cpmText.text = typingStatistics.cpmText.text + "타";

        // 정확도
        accuracyText.text = typingStatistics.accuracyText.text + "%";

        // 오타
        typoCountText.text = typingStatistics.typoCountText.text + "개";
    }

}
