using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverLongSentence : MonoBehaviour
{
    // UI
    public GameObject gameOverPanel;
    public TextMeshProUGUI timeText;                // �ð�
    public TextMeshProUGUI cpmText;                 // Ÿ��
    public TextMeshProUGUI accuracyText;            // ��Ȯ��
    public TextMeshProUGUI typoCountText;           // ��Ÿ

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

        // ����ð�
        float elapsedTime = typingStatistics.elapsedTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timeText.text = $"{minutes:D2}:{seconds:D2}";

        // Ÿ��
        cpmText.text = typingStatistics.cpmText.text + "Ÿ";

        // ��Ȯ��
        accuracyText.text = typingStatistics.accuracyText.text + "%";

        // ��Ÿ
        typoCountText.text = typingStatistics.typoCountText.text + "��";
    }

}
