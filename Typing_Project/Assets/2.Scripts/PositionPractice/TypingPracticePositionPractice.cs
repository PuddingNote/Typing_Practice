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
    public TextMeshProUGUI displayText;             // 화면에 주어지는 문장 Text
    public TextMeshProUGUI nextDisplayText;         // 다음 문장 미리보기 Text
    public TextMeshProUGUI leftText;                // 남은 문장 카운트 Text
    public GameObject backPanel;                    // SubPanel -> BackPanel
    public GameObject selectPanel;                  // SubPanel -> SelectPanel

    // Buttons
    public Button backButton;                       // Back Panel의 Button
    public Button resumeButton;                     // Select Panel의 Resume Button
    public Button titleButton;                      // Select Panel의 Title Button

    // Typing Variables
    private string currentText;                     // 현재 화면에 표시된 단어
    private int currentIndex;                       // 현재 단어 인덱스
    private int nextIndex;                          // 다음 단어 인덱스
    private List<string> texts;                     // 메모장에서 불러온 단어 리스트

    private int totalWordsTyped;                    // 입력된 단어의 수
    private int maxWords;                           // 입력 할 최대 단어의 수
    private int correctWords;                       // 맞은 단어 수
    private int totalTypos;                         // 총 오타 수

    // ETC
    private TypingStatisticsPositionPractice typingStatistics;
    private bool isGameEnded;
    private bool isWaiting;

    // 초기 데이터 설정
    private void SetData()
    {
        currentIndex = -1;
        nextIndex = -1;

        totalWordsTyped = 0;
        maxWords = 10;                              // 자리연습 최대 단어 수 설정
        correctWords = 0;
        totalTypos = 0;

        isGameEnded = false;
        isWaiting = false;
    }

    // 시작
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

    // 버튼 설정
    private void SetButtons()
    {
        backButton.onClick.AddListener(OnBackButtonPressed);
        resumeButton.onClick.AddListener(OnResumeButtonPressed);
        titleButton.onClick.AddListener(OnTitleButtonPressed);
    }

    // 타이핑 언어 설정
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

    // File Load (빈 줄이나 공백 제외)
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
            Debug.LogError("텍스트 파일을 찾을 수 없습니다");
        }

        SetNextText();
    }

    // 다음 Text 설정
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

    // 입력 검사
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

    // 입력시 0.3초 대기 후 다음 텍스트 설정
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

    // 정확도 Update
    private void UpdateAccuracy()
    {
        float accuracy = (float)correctWords / (correctWords + totalTypos) * 100;
        typingStatistics.UpdateAccuracy(accuracy);
    }

    // 오타 Update
    private void UpdateTypo()
    {
        typingStatistics.UpdateTypo(totalTypos);
    }

    // 텍스트 색상 Update
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

    // 남은 단어 수 Update
    private void UpdateLeftText()
    {
        leftText.text = $"{totalWordsTyped} / {maxWords}";
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
        PersistentDataPositionPractice.selectedType = "";
    }

    // 일시정지
    private void PausePractice()
    {
        typingStatistics.PausePractice();
    }

    // 재개
    private void ResumePractice()
    {
        typingStatistics.ResumePractice();
    }

    // 종료
    private void EndPractice()
    {
        isGameEnded = true;

        typingStatistics.EndPractice();

        FindObjectOfType<GameOverPositionPractice>().ShowGameOverStats();
    }
}
