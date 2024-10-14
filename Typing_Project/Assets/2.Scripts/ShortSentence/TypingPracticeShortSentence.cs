using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class TypingPracticeShortSentence : MonoBehaviour, ITypingPractice
{
    // UI
    public GameObject gameOverPanel;
    public TextMeshProUGUI displayText;             // ȭ�鿡 �־����� ���� Text
    public TMP_InputField inputField;               // ����� �Է� �ʵ�
    public TextMeshProUGUI nextDisplayText;         // ���� ���� �̸����� Text
    public TextMeshProUGUI leftText;                // ���� ���� ī��Ʈ Text

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

    // �ʱ� Data ����
    private void SetData()
    {
        currentIndex = -1;
        nextIndex = -1;
        totalWordsTyped = 0;
        maxWords = 5;
        totalTypedChars = 0;
        correctTypedChars = 0;
        totalTypos = 0;
        isGameEnded = false;
    }

    // Interface ���
    public void StartPractice()
    {
        this.enabled = true;
        inputField.ActivateInputField();
    }

    // Awake()
    private void Awake()
    {
        typingStatistics = GetComponent<TypingStatisticsShortSentence>();
        texts = new List<string>();

        SetData();
        SetInputfield();
        LoadTextsFromFile();
        SetNextText();
        inputField.ActivateInputField();
        this.enabled = false;

        inputField.onValueChanged.AddListener(delegate { CheckInput(); });
    }

    // Update()
    private void Update()
    {
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

    // �ʱ� File Load ���� (�� ���̳� ���� ����)
    private void LoadTextsFromFile()
    {
        string path = Application.dataPath + "/2.Scripts/ShortSentence/ShortSentence.txt";
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

        //UpdateCPM(false, totalTypedChars + typedText.Length);
        UpdateAccuracy(correctCharsInSentence, typedText.Length);
        UpdateTypo(false);

        UpdateDisplayText(typedText);
    }

    // EnterŰ �Է½�
    private void OnEnterPressed()
    {
        string typedText = inputField.text;

        totalWordsTyped++;

        for (int i = 0; i < currentText.Length; i++)
        {
            if (i < typedText.Length && typedText[i] == currentText[i])
            {
                correctTypedChars++;
            }
            else if (i >= typedText.Length)
            {
                totalTypos++;
            }
        }

        totalTypedChars += currentText.Length;

        if (totalWordsTyped < maxWords)
        {
            UpdateCPM(true, totalTypedChars);
            UpdateAccuracy(correctTypedChars, totalTypedChars);
            UpdateTypo(true);
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

    // leftText Update
    private void UpdateLeftText()
    {
        leftText.text = $"{totalWordsTyped + 1} / {maxWords}";
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