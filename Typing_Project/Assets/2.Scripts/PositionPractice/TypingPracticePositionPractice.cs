using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

// static
public static class PersistentDataPositionPractice
{
    public static string selectedType = "";
}

public class TypingPracticePositionPractice : MonoBehaviour
{
    // UI
    public GameObject gameOverPanel;
    public TextMeshProUGUI displayText;             // ȭ�鿡 �־����� ���� Text
    public TextMeshProUGUI nextDisplayText;         // ���� ���� �̸����� Text
    public TextMeshProUGUI leftText;                // ���� ���� ī��Ʈ Text
    public GameObject backPanel;                    // SubPanel -> BackPanel
    public GameObject selectPanel;                  // SubPanel -> SelectPanel

    // Buttons
    public Button backButton;                       // Back Panel�� Button
    public Button resumeButton;                     // Select Panel�� Resume Button
    public Button titleButton;                      // Select Panel�� Title Button

    // Typing Variables
    private string currentText;                     // ���� ȭ�鿡 ǥ�õ� �ܾ�
    private int currentIndex;                       // ���� �ܾ� �ε���
    private int nextIndex;                          // ���� �ܾ� �ε���
    private List<string> texts;                     // �޸��忡�� �ҷ��� �ܾ� ����Ʈ

    private int totalWordsTyped;                    // �Էµ� �ܾ��� ��
    private int maxWords;                           // �Է� �� �ִ� �ܾ��� ��
    private int correctWords;                       // ���� �ܾ� ��
    private int totalTypos;                         // �� ��Ÿ ��

    // ETC
    private TypingStatisticsPositionPractice typingStatistics;
    private bool isGameEnded;
    private bool isWaiting;

    // �ʱ� ������ ����
    private void SetData()
    {
        currentIndex = -1;
        nextIndex = -1;

        totalWordsTyped = 0;
        maxWords = 10;                              // �ڸ����� �ִ� �ܾ� �� ����
        correctWords = 0;
        totalTypos = 0;

        isGameEnded = false;
        isWaiting = false;
    }

    // ����
    public void StartPractice()
    {
        SetTypingLanguage();
        LoadTextsFromFile();

        this.enabled = true;
    }

    // Awake()
    private void Awake()
    {
        typingStatistics = GetComponent<TypingStatisticsPositionPractice>();
        texts = new List<string>();

        SetButtons();
        SetData();
        UpdateLeftText();
    }

    // Update()
    private void Update()
    {
        if (isGameEnded || isWaiting) return;

        if (Input.anyKeyDown)
        {
            if (!string.IsNullOrEmpty(Input.inputString) && Input.inputString != "\n" && Input.inputString != "\r")
            {
                CheckInput();
            }
        }
    }

    // ��ư ����
    private void SetButtons()
    {
        backButton.onClick.AddListener(OnBackButtonPressed);
        resumeButton.onClick.AddListener(OnResumeButtonPressed);
        titleButton.onClick.AddListener(OnTitleButtonPressed);
    }

    // Ÿ���� ��� ����
    private void SetTypingLanguage()
    {
        if (PersistentDataPositionPractice.selectedType == "English")
        {
            ForceEnglishIME forceEnglishIME = new ForceEnglishIME();
            forceEnglishIME.Start();
        }
        else if (PersistentDataPositionPractice.selectedType == "Korean")
        {
            ForceKoreanIME forceKoreanIME = new ForceKoreanIME();
            forceKoreanIME.Start();
        }
    }

    // File Load (�� ���̳� ���� ����)
    private void LoadTextsFromFile()
    {
        string path = Application.dataPath + "/2.Scripts/PositionPractice/Texts/" + PersistentDataPositionPractice.selectedType + ".txt";
        if (System.IO.File.Exists(path))
        {
            string[] lines = System.IO.File.ReadAllLines(path);
            foreach (string line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    texts.Add(line.Trim());
                }
            }
        }
        else
        {
            Debug.LogError("�ؽ�Ʈ ������ ã�� �� �����ϴ�");
        }

        SetNextText();
    }

    // ���� Text ����
    private void SetNextText()
    {
        if (currentIndex == -1)
        {
            currentIndex = Random.Range(0, texts.Count);
        }
        else
        {
            currentIndex = nextIndex;
        }

        do
        {
            nextIndex = Random.Range(0, texts.Count);
        } while (nextIndex == currentIndex);

        currentText = texts[currentIndex];
        displayText.text = currentText;
        totalWordsTyped++;

        if (totalWordsTyped < maxWords)
        {
            nextDisplayText.text = texts[nextIndex];
        }
        else
        {
            nextDisplayText.text = "";
        }
    }

    // �Է� �˻�
    private void CheckInput()
    {
        string inputKey = Input.inputString;
        if (string.IsNullOrEmpty(inputKey)) return;

        if (inputKey == currentText)
        {
            UpdateDisplayText(true);
            correctWords++;

            StartCoroutine(WaitAndSetNextText(true));
        }
        else
        {
            UpdateDisplayText(false);
            totalTypos++;

            StartCoroutine(WaitAndSetNextText(false));
        }

        UpdateAccuracy();
        UpdateTypo();

        UpdateLeftText();
    }

    // �Է½� 0.3�� ��� �� ���� �ؽ�Ʈ ����
    private IEnumerator WaitAndSetNextText(bool isCorrect)
    {
        isWaiting = true;
        yield return new WaitForSeconds(0.3f);

        if (isCorrect)
        {
            if (totalWordsTyped < maxWords)
            {
                SetNextText();
            }
            else
            {
                UpdateAccuracy();
                EndPractice();
            }
        }

        isWaiting = false;
    }

    // ��Ȯ�� Update
    private void UpdateAccuracy()
    {
        float accuracy = (float)correctWords / (correctWords + totalTypos) * 100;
        typingStatistics.UpdateAccuracy(accuracy);
    }

    // ��Ÿ Update
    private void UpdateTypo()
    {
        typingStatistics.UpdateTypo(totalTypos);
    }

    // �ؽ�Ʈ ���� Update
    private void UpdateDisplayText(bool isCorrect)
    {
        if (isCorrect)
        {
            displayText.text = $"<color=blue>{currentText}</color>";
        }
        else
        {
            displayText.text = $"<color=red>{currentText}</color>";
        }
    }

    // ���� �ܾ� �� Update
    private void UpdateLeftText()
    {
        leftText.text = $"{totalWordsTyped} / {maxWords}";
    }

    // Back ��ư�� ������ ��
    private void OnBackButtonPressed()
    {
        PausePractice();
        selectPanel.SetActive(true);
    }

    // Resume ��ư�� ������ ��
    private void OnResumeButtonPressed()
    {
        selectPanel.SetActive(false);
        ResumePractice();
    }

    // Title ��ư�� ������ ��
    private void OnTitleButtonPressed()
    {
        SceneManager.LoadScene("TitleScene");
        PersistentDataPositionPractice.selectedType = "";
    }

    // �Ͻ�����
    private void PausePractice()
    {
        typingStatistics.PausePractice();
    }

    // �簳
    private void ResumePractice()
    {
        typingStatistics.ResumePractice();
    }

    // ����
    private void EndPractice()
    {
        isGameEnded = true;

        typingStatistics.EndPractice();

        FindObjectOfType<GameOverPositionPractice>().ShowGameOverStats();
    }
}
