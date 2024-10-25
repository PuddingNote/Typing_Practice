using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;
using System;

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
    private int correctTypedChars;                  // 올바르게 입력된 문자 수
    private int totalTypos;                         // 총 오타 문자 수
    private int totalInput;                         // 총 입력 수

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
        correctTypedChars = 0;
        totalTypos = 0;
        totalInput = 0;

        isGameEnded = false;
        isPaused = false;
    }

    // 시작
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
        typingStatistics = GetComponent<TypingStatisticsLongSentence>();
        texts = new List<string>();

        SetButtons();
        SetData();
        SetInputfield();
        LoadTextsFromFile();
        SetNextText();
        UpdateLeftText();

        inputField.ActivateInputField();
        inputField.onValueChanged.AddListener(delegate { CheckInput(); });
        inputField.onSelect.AddListener(OnInputFieldClick);
    }

    // 화면 클릭 시 InputField의 caretPosition을 텍스트 끝으로 이동
    private void OnInputFieldClick(string value)
    {
        inputField.caretPosition = inputField.text.Length;
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
        UpdateCPM();
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
        inputField.contentType = TMP_InputField.ContentType.Standard;
        inputField.lineType = TMP_InputField.LineType.SingleLine;
    }

    // File Load (빈 줄이나 공백 제외)
    private void LoadTextsFromFile()
    {
        string path = Path.Combine(Application.streamingAssetsPath, SceneManager.GetActiveScene().name, "Texts", 
            PersistentDataLongSentence.selectedLanguage, PersistentDataLongSentence.selectedTitle + ".txt");

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

    // 입력 검사
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

    // Enter키 입력시
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
            UpdateCPM();
            UpdateAccuracy(true, correctCharsInSentence, typoWords);
            UpdateTypo(true, typoWords);
            UpdateLeftText();
            SetNextText();
        }
        else
        {
            UpdateCPM();
            UpdateAccuracy(true, correctCharsInSentence, typoWords);
            EndPractice();
        }
    }

    // 타수 Update
    private void UpdateCPM()
    {
        float elapsedTime = typingStatistics.elapsedTime / 60f;
        float cpm = (float)(totalInput / (elapsedTime * 5));

        typingStatistics.UpdateCPM(cpm);
    }

    // 정확도 Update
    private void UpdateAccuracy(bool isEnter, int correctCharsInSentence, int typoWords)
    {
        float accuracy;

        if (isEnter)
        {
            correctTypedChars += correctCharsInSentence;
        }

        int totalCount = correctTypedChars + correctCharsInSentence + totalTypos + typoWords;
        if (totalCount > 0)
        {
            accuracy = (float)(correctTypedChars + correctCharsInSentence) / totalCount * 100f;
        }
        else
        {
            accuracy = 0f;
        }

        typingStatistics.UpdateAccuracy(accuracy);
    }

    // 오타 Update
    private void UpdateTypo(bool isEnter, int typoWords)
    {
        if (isEnter)
        {
            totalTypos += typoWords;
        }
        typingStatistics.UpdateTypo(totalTypos + typoWords);
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