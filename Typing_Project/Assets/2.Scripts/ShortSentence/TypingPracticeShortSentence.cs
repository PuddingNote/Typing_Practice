using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;

public static class PersistentDataShortSentence
{
    public static string selectedLanguage = "";
}

public class TypingPracticeShortSentence : MonoBehaviour, ITypingPractice
{
    // UI
    public GameObject gameOverPanel;
    public TextMeshProUGUI displayText;             // ȭ�鿡 �־����� ���� Text
    public TMP_InputField inputField;               // ����� �Է� �ʵ�
    public TextMeshProUGUI nextDisplayText;         // ���� ���� �̸����� Text
    public TextMeshProUGUI leftText;                // ���� ���� ī��Ʈ Text
    public GameObject backPanel;                    // SubPanel -> BackPanel
    public GameObject selectPanel;                  // SubPanel -> SelectPanel

    // Buttons
    public Button backButton;                       // Back Panel�� Button
    public Button resumeButton;                     // Select Panel�� Resume Button
    public Button titleButton;                      // Select Panel�� Title Button

    // Typing Variables
    private string currentText;                     // ���� ȭ�鿡 ǥ�õ� ����
    private int currentIndex;                       // ���� ���� �ε���
    private int nextIndex;                          // ���� ���� �ε���
    private List<string> texts;                     // �޸��忡�� �ҷ��� ���� ����Ʈ

    private int totalWordsTyped;                    // �Էµ� ������ ��
    private int maxWords;                           // �Է� �� �ִ� ������ ��
    private int totalTypedChars;                    // �� �Էµ� ���� ��
    private int correctTypedChars;                  // �ùٸ��� �Էµ� ���� ��
    private int totalTypos;                         // �� ��Ÿ ��

    // ETC
    private TypingStatisticsShortSentence typingStatistics;
    private bool isGameEnded;
    private bool isPaused;

    // �ʱ� Data ����
    private void SetData()
    {
        currentIndex = -1;
        nextIndex = -1;
        totalWordsTyped = 0;
        maxWords = 10;                              // �ܹ����� �ִ� ���� �� ����
        totalTypedChars = 0;
        correctTypedChars = 0;
        totalTypos = 0;
        isGameEnded = false;
        isPaused = false;
    }

    // Interface ���
    public void StartPractice()
    {
        SetTypingLanguage();
        LoadTextsFromFile();

        this.enabled = true;
        inputField.ActivateInputField();
    }

    // Awake()
    private void Awake()
    {
        typingStatistics = GetComponent<TypingStatisticsShortSentence>();
        texts = new List<string>();

        selectPanel.SetActive(false);
        SetButtons();
        SetData();
        SetInputfield();
        
        inputField.ActivateInputField();
        this.enabled = false;

        inputField.onValueChanged.AddListener(delegate { CheckInput(); });
    }

    // Update()
    private void Update()
    {
        if (isPaused) return;
        if (isGameEnded) return;

        if (!inputField.isFocused)
        {
            inputField.ActivateInputField();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            OnEnterPressed();
        }

        UpdateCPM(false, totalTypedChars + inputField.text.Length);
        UpdateLeftText();
    }

    // �ʱ� ��ư�� ����
    private void SetButtons()
    {
        backButton.onClick.AddListener(OnBackButtonPressed);
        resumeButton.onClick.AddListener(OnResumeButtonPressed);
        titleButton.onClick.AddListener(OnTitleButtonPressed);
    }

    // �ʱ� Inputfield ����
    private void SetInputfield()
    {
        var background = inputField.GetComponentInChildren<Image>();
        background.color = Color.white;

        var outline = inputField.gameObject.AddComponent<Outline>();
        outline.effectColor = new Color(93f / 255f, 158f / 255f, 235f / 255f, 170f / 255f);
        outline.effectDistance = new Vector2(2, 2);

        inputField.contentType = TMP_InputField.ContentType.Standard;
        inputField.lineType = TMP_InputField.LineType.SingleLine;
    }

    // �ʱ� Ÿ���ξ�� ����
    private void SetTypingLanguage()
    {
        if (PersistentDataShortSentence.selectedLanguage == "Korean")
        {
            ForceKoreanIME forceKoreanIME = new ForceKoreanIME();
            forceKoreanIME.Start();
        }
        else
        {
            ForceEnglishIME forceEnglishIME = new ForceEnglishIME();
            forceEnglishIME.Start();
        }
    }

    // �ʱ� File Load ���� (�� ���̳� ���� ����)
    private void LoadTextsFromFile()
    {
        string path = Application.dataPath + "/2.Scripts/ShortSentence/Texts/" + PersistentDataShortSentence.selectedLanguage + ".txt";
        //string path = Application.dataPath + "/2.Scripts/ShortSentence/ShortSentence.txt";
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

        SetNextText();
    }

    // ��� Text ����
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

    // Inputfield(�Է�) �˻�
    private void CheckInput()
    {
        string typedText = inputField.text;
        int correctCharsInSentence = 0;

        totalTypos = 0;

        for (int i = 0; i < typedText.Length; i++)
        {
            if (i < currentText.Length)
            {
                if (typedText[i] == currentText[i])
                {
                    correctCharsInSentence++;
                }
                else
                {
                    totalTypos++;
                }
            }
        }

        UpdateAccuracy(correctCharsInSentence, typedText.Length);
        UpdateTypo(false);

        UpdateDisplayText(typedText);
    }

    // EnterŰ �Է½�
    private void OnEnterPressed()
    {
        string typedText = inputField.text;

        totalWordsTyped++;

        correctTypedChars = 0;
        totalTypos = 0;

        for (int i = 0; i < currentText.Length; i++)
        {
            if (i < typedText.Length)
            {
                if (typedText[i] == currentText[i])
                {
                    correctTypedChars++;
                }
                else
                {
                    totalTypos++;
                }
            }
            else
            {
                totalTypos++;
            }
        }

        if (string.IsNullOrEmpty(typedText))
        {
            totalTypos = currentText.Length;
        }

        totalTypedChars += currentText.Length;

        UpdateCPM(true, totalTypedChars);
        UpdateAccuracy(correctTypedChars, totalTypedChars);
        UpdateTypo(true);

        if (totalWordsTyped < maxWords)
        {
            SetNextText();
        }
        else
        {
            EndPractice();
        }
    }

    // �ǽð� Ÿ�� Update
    private void UpdateCPM(bool isEnter, int totalCharactersTyped)
    {
        float elapsedMinutes = typingStatistics.elapsedTime / 60f;
        float cpm = (elapsedMinutes > 0) ? (totalCharactersTyped * 2 / elapsedMinutes) : 0;

        if (isEnter)
        {
            UpdateHighestCPM(cpm);
        }
        typingStatistics.UpdateCPM(cpm);
    }

    // �ִ� Ÿ�� Update
    private void UpdateHighestCPM(float currentCpm)
    {
        typingStatistics.UpdateHighestCPM((int)currentCpm);
    }

    // �ǽð� ��Ȯ�� Update
    private void UpdateAccuracy(int correctCharsInSentence, int totalCharsTypedInSentence)
    {
        int totalCorrectChars = correctTypedChars + correctCharsInSentence;
        int totalCharsTyped = totalTypedChars + totalCharsTypedInSentence;

        float accuracy = (totalCharsTyped > 0) ? (float)totalCorrectChars / totalCharsTyped * 100f : 0f;
        typingStatistics.UpdateAccuracy(accuracy);
    }

    // �ǽð� ��Ÿ Update
    private void UpdateTypo(bool isEnter)
    {
        typingStatistics.UpdateTypo(isEnter, totalTypos);
    }

    // �ǽð� DisplayText �� ����
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

    // ���� ���� �� Update
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
        PersistentDataShortSentence.selectedLanguage = "";
    }

    // Ÿ���� ���� �Ͻ�����
    private void PausePractice()
    {
        isPaused = true;
        typingStatistics.PausePractice();
    }

    // Ÿ���� ���� �簳
    private void ResumePractice()
    {
        isPaused = false;
        typingStatistics.ResumePractice();

        inputField.caretPosition = inputField.text.Length;
    }

    // ���α׷� ����
    private void EndPractice()
    {
        isGameEnded = true;
        inputField.interactable = false;
        inputField.onValueChanged.RemoveAllListeners();

        typingStatistics.EndPractice();

        FindObjectOfType<GameOverShortSentence>().ShowGameOverStats();
    }
}