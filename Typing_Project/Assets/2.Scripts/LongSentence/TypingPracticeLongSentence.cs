using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;

// static
public static class PersistentDataLongSentence
{
    public static string selectedLanguage = "";
    public static string selectedTitle = "";
}

public class TypingPracticeLongSentence : MonoBehaviour, ITypingPractice
{
    // UI
    public GameObject gameOverPanel;
    public TextMeshProUGUI displayText;             // 화면에 주어지는 문장 Text
    public TMP_InputField inputField;               // 사용자 입력 필드
    public TextMeshProUGUI nextDisplayText;         // 다음 문장 미리보기 Text
    public TextMeshProUGUI leftText;                // 남은 문장 카운트 Text
    public GameObject backPanel;                    // SubPanel -> BackPanel
    public GameObject selectPanel;                  // SubPanel -> SelectPanel

    // Buttons
    public Button backButton;                       // Back Panel의 Button
    public Button resumeButton;                     // Select Panel의 Resume Button
    public Button titleButton;                      // Select Panel의 Title Button

    // Typing Variables
    private string currentText;                     // 현재 화면에 표시된 문장
    private int currentIndex;                       // 현재 문장 인덱스
    private int nextIndex;                          // 다음 문장 인덱스
    private List<string> texts;                     // 메모장에서 불러온 문장 리스트

    private int totalWordsTyped;                    // 입력된 문장의 수
    private int maxWords;                           // 입력 할 최대 문장의 수
    private int totalTypedChars;                    // 총 입력된 문자 수
    private int correctTypedChars;                  // 올바르게 입력된 문자 수
    private int totalTypos;                         // 총 오타 수

    // ETC
    private TypingStatisticsLongSentence typingStatistics;
    private bool isGameEnded;
    private bool isPaused;

    // 초기 Data 설정
    private void SetData()
    {
        currentIndex = 0;
        nextIndex = 0;

        totalWordsTyped = 0;
        maxWords = 0;
        totalTypedChars = 0;
        correctTypedChars = 0;
        totalTypos = 0;

        isGameEnded = false;
        isPaused = false;
    }

    // 시작
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
        typingStatistics = GetComponent<TypingStatisticsLongSentence>();
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

    // 버튼 설정
    private void SetButtons()
    {
        backButton.onClick.AddListener(OnBackButtonPressed);
        resumeButton.onClick.AddListener(OnResumeButtonPressed);
        titleButton.onClick.AddListener(OnTitleButtonPressed);
    }

    // Inputfield 설정
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

    // 타이핑 언어 설정
    private void SetTypingLanguage()
    {
        if (PersistentDataLongSentence.selectedLanguage == "English")
        {
            ForceEnglishIME forceEnglishIME = new ForceEnglishIME();
            forceEnglishIME.Start();
        }
        else if (PersistentDataLongSentence.selectedLanguage == "Korean")
        {
            ForceKoreanIME forceKoreanIME = new ForceKoreanIME();
            forceKoreanIME.Start();
        }
    }

    // File Load (빈 줄이나 공백 제외)
    private void LoadTextsFromFile()
    {
        string path = Application.dataPath + "/2.Scripts/LongSentence/Texts/" + PersistentDataLongSentence.selectedLanguage + "/" + PersistentDataLongSentence.selectedTitle + ".txt";
        if (File.Exists(path))
        {
            string[] lines = File.ReadAllLines(path);
            foreach (string line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    texts.Add(line.Trim());
                    maxWords++;
                }
            }
        }
        else
        {
            Debug.LogError("텍스트 파일을 찾을 수 없습니다");
        }

        SetNextText();
    }

    // 다음 Text 설정
    private void SetNextText()
    {
        currentIndex = nextIndex;
        nextIndex = (currentIndex + 1) % texts.Count;

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

    // Inputfield(입력) 검사
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

    // Enter키 입력시
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

    // 타수 Update
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

    // 최대타수 Update
    private void UpdateHighestCPM(float currentCpm)
    {
        typingStatistics.UpdateHighestCPM((int)currentCpm);
    }

    // 정확도 Update
    private void UpdateAccuracy(int correctCharsInSentence, int totalCharsTypedInSentence)
    {
        int totalCorrectChars = correctTypedChars + correctCharsInSentence;
        int totalCharsTyped = totalTypedChars + totalCharsTypedInSentence;

        float accuracy = (totalCharsTyped > 0) ? (float)totalCorrectChars / totalCharsTyped * 100f : 0f;
        typingStatistics.UpdateAccuracy(accuracy);
    }

    // 오타 Update
    private void UpdateTypo(bool isEnter)
    {
        typingStatistics.UpdateTypo(isEnter, totalTypos);
    }

    // 텍스트 색상 Update
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

    // 남은 문장 수 Update
    private void UpdateLeftText()
    {
        leftText.text = $"{totalWordsTyped + 1} / {maxWords}";
    }

    // Back 버튼을 눌렀을 때
    private void OnBackButtonPressed()
    {
        PausePractice();
        selectPanel.SetActive(true);
    }

    // Resume 버튼을 눌렀을 때
    private void OnResumeButtonPressed()
    {
        selectPanel.SetActive(false);
        ResumePractice();
    }

    // Title 버튼을 눌렀을 때
    private void OnTitleButtonPressed()
    {
        SceneManager.LoadScene("TitleScene");
        PersistentDataLongSentence.selectedTitle = "";
        PersistentDataLongSentence.selectedLanguage = "";
    }

    // 일시정지
    private void PausePractice()
    {
        isPaused = true;
        typingStatistics.PausePractice();
    }

    // 재개
    private void ResumePractice()
    {
        isPaused = false;
        typingStatistics.ResumePractice();

        inputField.caretPosition = inputField.text.Length;
    }

    // 종료
    private void EndPractice()
    {
        isGameEnded = true;
        inputField.interactable = false;
        inputField.onValueChanged.RemoveAllListeners();

        typingStatistics.EndPractice();

        FindObjectOfType<GameOverLongSentence>().ShowGameOverStats();
    }
}