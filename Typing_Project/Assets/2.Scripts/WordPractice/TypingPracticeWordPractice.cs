using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;
using System;

// static
public static class PersistentDataWordPractice
{
    public static string selectedLanguage = "";
}

public class TypingPracticeWordPractice : MonoBehaviour, ITypingPractice
{
    // UI
    public GameObject gameOverPanel;
    public TextMeshProUGUI displayText;             // ȭ�鿡 �־����� �ܾ� Text
    public TMP_InputField inputField;               // ����� �Է� �ʵ�
    public TextMeshProUGUI nextDisplayText;         // ���� �ܾ� �̸����� Text
    public TextMeshProUGUI leftText;                // ���� �ܾ� ī��Ʈ Text
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
    private int correctTypedChars;                  // �ùٸ��� �Էµ� �ܾ� ��
    private int totalTypos;                         // �� ��Ÿ ��
    private int totalInput;                         // �� �Է� ��

    // ETC
    private TypingStatisticsWordPractice typingStatistics;
    private bool isGameEnded;
    private bool isPaused;

    // �ʱ� Data ����
    private void SetData()
    {
        currentIndex = -1;
        nextIndex = -1;

        totalWordsTyped = 0;
        maxWords = 10;                              // �ִ� �ܾ� �� ����
        correctTypedChars = 0;
        totalTypos = 0;
        totalInput = 0;

        isGameEnded = false;
        isPaused = false;
    }

    // ����
    public void StartPractice()
    {
        selectPanel.SetActive(false);
        this.enabled = true;
    }

    // Awake()
    private void Awake()
    {
        this.enabled = false;
    }

    // Start()
    private void Start()
    {
        typingStatistics = GetComponent<TypingStatisticsWordPractice>();
        texts = new List<string>();

        SetButtons();
        SetData();
        SetInputfield();
        LoadTextsFromFile();
        SetNextText();
        UpdateLeftText();
        inputField.ActivateInputField();

        inputField.onValueChanged.AddListener(delegate { CheckInput(); });
    }

    // Update()
    private void Update()
    {
        if (isGameEnded || isPaused) return;

        if (!inputField.isFocused)
        {
            inputField.ActivateInputField();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            OnEnterPressed();
        }
    }

    // ��ư ����
    private void SetButtons()
    {
        backButton.onClick.AddListener(OnBackButtonPressed);
        resumeButton.onClick.AddListener(OnResumeButtonPressed);
        titleButton.onClick.AddListener(OnTitleButtonPressed);
    }

    // Inputfield ����
    private void SetInputfield()
    {
        inputField.contentType = TMP_InputField.ContentType.Standard;
        inputField.lineType = TMP_InputField.LineType.SingleLine;
    }

    // File Load (�� ���̳� ���� ����)
    private void LoadTextsFromFile()
    {
        string path = Path.Combine(Application.streamingAssetsPath, SceneManager.GetActiveScene().name, "Texts", PersistentDataWordPractice.selectedLanguage + ".txt");

        if (File.Exists(path))
        {
            string[] lines = File.ReadAllLines(path);
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
    }

    // ���� Text ����
    private void SetNextText()
    {
        if (currentIndex == -1)
        {
            currentIndex = UnityEngine.Random.Range(0, texts.Count);
        }
        else
        {
            currentIndex = nextIndex;
        }

        do
        {
            nextIndex = UnityEngine.Random.Range(0, texts.Count);
        } while (nextIndex == currentIndex);

        currentText = texts[currentIndex];
        displayText.text = currentText;
        inputField.characterLimit = currentText.Length;
        inputField.text = "";

        if (totalWordsTyped == (maxWords - 1))
        {
            nextDisplayText.text = "";
        }
        else
        {
            nextDisplayText.text = texts[nextIndex];
        }
    }

    // �Է� �˻�
    private void CheckInput()
    {
        string typedText = inputField.text;
        int correctCharsInSentence = 0;
        int typoWords = 0;

        for (int i = 0; i < typedText.Length; i++)
        {
            if (i < currentText.Length)
            {
                if (typedText[i] == currentText[i])
                {
                    correctCharsInSentence++;
                    totalInput++;
                }
                else
                {
                    typoWords++;
                    totalInput++;
                }
            }
        }

        UpdateAccuracy(false, correctCharsInSentence, typoWords);
        UpdateTypo(false, typoWords);

        UpdateDisplayText(typedText);
    }

    // EnterŰ �Է½�
    private void OnEnterPressed()
    {
        string typedText = inputField.text;
        int typoWords = 0;
        int correctCharsInSentence = 0;

        totalWordsTyped++;

        if (string.IsNullOrEmpty(typedText))
        {
            typoWords = currentText.Length;
        }
        else
        {
            for (int i = 0; i < currentText.Length; i++)
            {
                if (i < typedText.Length)
                {
                    if (typedText[i] == currentText[i])
                    {
                        correctCharsInSentence++;
                    }
                    else
                    {
                        typoWords++;
                    }
                }
                else
                {
                    typoWords++;
                }
            }
        }

        if (totalWordsTyped < maxWords)
        {
            UpdateAccuracy(true, correctCharsInSentence, typoWords);
            UpdateTypo(true, typoWords);
            UpdateLeftText();
            SetNextText();
        }
        else
        {
            UpdateAccuracy(true, correctCharsInSentence, typoWords);
            EndPractice();
        }
    }

    // ��Ȯ�� Update
    private void UpdateAccuracy(bool isEnter, int correctCharsInSentence, int typoWords)
    {
        float accuracy;

        if (isEnter)
        {
            correctTypedChars += correctCharsInSentence;
        }

        if (totalInput > 0)
        {
            accuracy = (float)(correctTypedChars + correctCharsInSentence) / (correctTypedChars + correctCharsInSentence + totalTypos + typoWords) * 100f;
        }
        else
        {
            accuracy = 0;
        }
        typingStatistics.UpdateAccuracy(accuracy);
    }

    // ��Ÿ Update
    private void UpdateTypo(bool isEnter, int typoWords)
    {
        if (isEnter)
        {
            totalTypos += typoWords;
        }
        typingStatistics.UpdateTypo(totalTypos + typoWords);
    }

    // �ؽ�Ʈ ���� Update
    private void UpdateDisplayText(string typedText)
    {
        string coloredText = "";

        for (int i = 0; i < currentText.Length; i++)
        {
            if (i < typedText.Length)
            {
                coloredText += (typedText[i] == currentText[i])
                    ? $"<color=blue>{currentText[i]}</color>"
                    : $"<color=red>{currentText[i]}</color>";
            }
            else
            {
                coloredText += currentText[i];
            }
        }

        displayText.text = coloredText;
    }

    // ���� �ܾ� �� Update
    private void UpdateLeftText()
    {
        leftText.text = $"{totalWordsTyped + 1} / {maxWords}";
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
        PersistentDataWordPractice.selectedLanguage = "";
    }

    // �Ͻ�����
    private void PausePractice()
    {
        isPaused = true;
        typingStatistics.PausePractice();
    }

    // �簳
    private void ResumePractice()
    {
        isPaused = false;
        typingStatistics.ResumePractice();

        inputField.caretPosition = inputField.text.Length;
    }

    // ����
    private void EndPractice()
    {
        isGameEnded = true;
        inputField.interactable = false;
        inputField.onValueChanged.RemoveAllListeners();

        typingStatistics.EndPractice();

        FindObjectOfType<GameOverWordPractice>().ShowGameOverStats();
    }
}