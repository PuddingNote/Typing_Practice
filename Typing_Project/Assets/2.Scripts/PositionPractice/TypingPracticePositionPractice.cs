using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using System.Linq;

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
    private List<string> texts;                     // 메모장에서 불러온 단어 리스트

    private string currentText;                     // 현재 화면에 표시된 단어
    private int currentIndex;                       // 현재 단어 인덱스
    private int nextIndex;                          // 다음 단어 인덱스

    private int totalCharactersTyped;               // 입력된 단어의 수
    private int maxWords;                           // 입력 할 최대 단어의 수
    private int correctWords;                       // 맞은 단어 수
    private int totalTypos;                         // 총 오타 수

    private string currentVowelSequence;            // 현재 입력된 모음 시퀀스

    private List<string> consonants;                // 자음 리스트
    private List<string> vowels;                    // 모음 리스트
    private bool isConsonantTurn;                   // 자음-모음 교차를 위한 변수

    // ETC
    private TypingStatisticsPositionPractice typingStatistics;
    private KeyboardManagerPositionPractice keyboardManager;
    private bool isGameEnded;
    private bool isWaiting;

    // 초기 데이터 설정
    private void SetData()
    {
        currentIndex = -1;
        nextIndex = -1;

        totalCharactersTyped = 0;
        maxWords = 10;                              // 자리연습 최대 단어 수 설정
        correctWords = 0;
        totalTypos = 0;

        currentVowelSequence = "";

        consonants = new List<string>();
        vowels = new List<string>();
        isConsonantTurn = false;
        isGameEnded = false;
        isWaiting = false;         
    }

    // 시작
    public void StartPractice()
    {
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
        typingStatistics = GetComponent<TypingStatisticsPositionPractice>();
        keyboardManager = GameObject.Find("KeyboardManager").GetComponent<KeyboardManagerPositionPractice>();
        texts = new List<string>();

        SetKeyBoardLanguage();
        SetButtons();
        SetData();
        LoadTextsFromFile();
        SetNextText();
        UpdateLeftText();

        HighlightKey();
    }

    // Update
    private void Update()
    {
        if (isGameEnded || isWaiting) return;

        if (PersistentDataPositionPractice.selectedType == "Korean")
        {
            bool isShiftPressed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

            // 모든 KeyCode에 대해 입력 체크
            foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
            {
                // 키가 눌려 있으면
                if (Input.GetKey(keyCode))
                {
                    // 한 번만 체크하기 위해 키가 눌린 후 특정 시간 동안 감지
                    string input = KeyCodeToKoreanChar(keyCode, isShiftPressed);

                    if (!string.IsNullOrEmpty(input))
                    {
                        CheckInput(input);
                    }
                    break; // 첫 번째 입력 처리 후 루프 종료
                }
            }
        }
        else
        {
            // 기본 입력 처리 (문자열 입력)
            string input = Input.inputString;
            if (!string.IsNullOrEmpty(input) && input != "\n" && input != "\r")
            {
                CheckInput(input);
            }
        }
    }

    // 영어 > 한글 강제 변환
    private string KeyCodeToKoreanChar(KeyCode keyCode, bool isShiftPressed)
    {
        if (isShiftPressed)
        {
            switch (keyCode)
            {
                // 자음
                case KeyCode.R: return "ㄲ";     // Shift + ㄱ -> ㄲ
                case KeyCode.E: return "ㄸ";     // Shift + ㄷ -> ㄸ
                case KeyCode.Q: return "ㅃ";     // Shift + ㅂ -> ㅃ
                case KeyCode.T: return "ㅆ";     // Shift + ㅅ -> ㅆ
                case KeyCode.W: return "ㅉ";     // Shift + ㅈ -> ㅉ

                // 모음
                case KeyCode.O: return "ㅒ";     // Shift + ㅐ -> ㅒ
                case KeyCode.P: return "ㅖ";     // Shift + ㅔ -> ㅖ
            }
        }

        switch (keyCode)
        {
            // 자음
            case KeyCode.Q: return "ㅂ";         // Q -> ㅂ
            case KeyCode.W: return "ㅈ";         // W -> ㅈ
            case KeyCode.E: return "ㄷ";         // E -> ㄷ
            case KeyCode.R: return "ㄱ";         // R -> ㄱ
            case KeyCode.T: return "ㅅ";         // T -> ㅅ
            case KeyCode.A: return "ㅁ";         // A -> ㅁ
            case KeyCode.S: return "ㄴ";         // S -> ㄴ
            case KeyCode.D: return "ㅇ";         // D -> ㅇ
            case KeyCode.F: return "ㄹ";         // F -> ㄹ
            case KeyCode.G: return "ㅎ";         // G -> ㅎ
            case KeyCode.Z: return "ㅋ";         // Z -> ㅋ
            case KeyCode.X: return "ㅌ";         // X -> ㅌ
            case KeyCode.C: return "ㅊ";         // C -> ㅊ
            case KeyCode.V: return "ㅍ";         // V -> ㅍ

            // 모음
            case KeyCode.Y: return "ㅛ";         // Y -> ㅛ
            case KeyCode.U: return "ㅕ";         // U -> ㅕ
            case KeyCode.I: return "ㅑ";         // I -> ㅑ
            case KeyCode.O: return "ㅐ";         // O -> ㅐ
            case KeyCode.P: return "ㅔ";         // P -> ㅔ
            case KeyCode.H: return "ㅗ";         // H -> ㅗ
            case KeyCode.J: return "ㅓ";         // J -> ㅓ
            case KeyCode.K: return "ㅏ";         // K -> ㅏ
            case KeyCode.L: return "ㅣ";         // L -> ㅣ
            case KeyCode.B: return "ㅠ";         // B -> ㅠ
            case KeyCode.N: return "ㅜ";         // N -> ㅜ
            case KeyCode.M: return "ㅡ";         // M -> ㅡ

            default: return string.Empty;
        }
    }

    // 강제 영어타이핑 전환
    private void SetKeyBoardLanguage()
    {
        GetComponent<IMEManager>().StartIME();
    }

    // 버튼 설정
    private void SetButtons()
    {
        backButton.onClick.AddListener(OnBackButtonPressed);
        resumeButton.onClick.AddListener(OnResumeButtonPressed);
        titleButton.onClick.AddListener(OnTitleButtonPressed);
    }

    // File Load (빈 줄이나 공백 제외)
    private void LoadTextsFromFile()
    {
        string path = Path.Combine(Application.streamingAssetsPath, SceneManager.GetActiveScene().name, "Texts", PersistentDataPositionPractice.selectedType + ".txt");
        
        if (PersistentDataPositionPractice.selectedType == "Korean")
        {
            string[] lines = File.ReadAllLines(path);
            foreach (string line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    if (IsVowel(line.Trim()))
                    {
                        vowels.Add(line.Trim());
                    }
                    else
                    {
                        consonants.Add(line.Trim());
                    }
                }
            }
        }
        else
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
    }

    // 다음 Text 설정 (자음 모음이 번갈아 나오도록 설정)
    private void SetNextText()
    {
        if (PersistentDataPositionPractice.selectedType == "Korean")
        {
            if (currentIndex == -1)
            {
                currentIndex = UnityEngine.Random.Range(0, consonants.Count);
                isConsonantTurn = true;
            }
            else
            {
                currentIndex = nextIndex;
            }

            currentText = isConsonantTurn ? consonants[currentIndex] : vowels[currentIndex];
            displayText.text = currentText;

            if (totalCharactersTyped == (maxWords - 1))
            {
                nextDisplayText.text = "";
            }
            else
            {
                if (isConsonantTurn)
                {
                    nextIndex = UnityEngine.Random.Range(0, vowels.Count);
                    nextDisplayText.text = vowels[nextIndex];
                }
                else
                {
                    nextIndex = UnityEngine.Random.Range(0, consonants.Count);
                    nextDisplayText.text = consonants[nextIndex];
                }
            }

            isConsonantTurn = !isConsonantTurn;
        }
        else
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

            if (totalCharactersTyped == (maxWords - 1))
            {
                nextDisplayText.text = "";
            }
            else
            {
                nextDisplayText.text = texts[nextIndex];
            }
        }
    }







    private void CheckInput(string input)
    {
        Debug.Log(input);

        // 현재 입력이 모음이라면
        if (IsVowel(input))
        {
            // 모음 입력 처리
            CheckVowelInput(input);
        }
        // 자음이라면
        else
        {
            // 첫 입력이 자음이면
            if (currentVowelSequence.Length == 0)
            {
                // 자음만 입력했을 때
                if (input == currentText)
                {
                    UpdateDisplayText(true);
                    correctWords++;
                    totalCharactersTyped++;
                    //currentVowelSequence = "";

                    StartCoroutine(WaitAndSetNextText(true));
                }
                else
                {
                    UpdateDisplayText(false);
                    totalTypos++;
                    //currentVowelSequence = "";

                    StartCoroutine(WaitAndSetNextText(false));
                }
            }
            else
            {
                // 모음 입력 중에 자음이 들어온 경우 오타 처리
                UpdateDisplayText(false);
                totalTypos++;
                //currentVowelSequence = "";

                StartCoroutine(WaitAndSetNextText(false));
            }
        }

        UpdateAccuracy();
        UpdateTypo();
    }

    // 모음 입력 처리
    private void CheckVowelInput(string input)
    {
        currentVowelSequence += input;

        string resultVowel = GetFullVowelFromCombination(currentVowelSequence);

        // 모음 조합이 완성 되지 않았다면
        if (resultVowel == null)
        {
            return;
        }
        // 완성된 모음이라면
        else
        {
            if (resultVowel == currentText)
            {
                UpdateDisplayText(true);  // 정답 처리
                correctWords++;
                totalCharactersTyped++;
                //currentVowelSequence = "";

                StartCoroutine(WaitAndSetNextText(true));
            }
            else
            {
                UpdateDisplayText(false);
                totalTypos++;
                //currentVowelSequence = "";

                StartCoroutine(WaitAndSetNextText(false));
            }

            UpdateAccuracy();
            UpdateTypo();
        }
    }

    // 천지인 모음 조합이 완성되었는지 확인하는 함수
    private string GetFullVowelFromCombination(string combination)
    {
        // 천지인 모음 딕셔너리에서 일치하는 모음이 있는지 찾음
        foreach (var vowelEntry in keyboardManager.chonjiinVowelsInputCheck)
        {
            string vowelKey = vowelEntry.Key;
            string[] correctCombination = vowelEntry.Value;

            // 찾은 키가 정답과 같다면
            if (vowelKey == currentText)
            {
                // 입력한 길이가 입력해야 할 길이와 같다면
                if (combination.Length == correctCombination.Length)
                {
                    Debug.Log("combination : " + combination);
                    Debug.Log("correctCombination: " + string.Join("", correctCombination));

                    if (correctCombination.SequenceEqual(combination.Select(c => c.ToString())))
                    {
                        return vowelKey;
                    }
                    else
                    {
                        return "Wrong";
                    }
                }
                // 길이가 다르다면
                else
                {
                    string correctCombinationTemp = string.Join("", correctCombination);

                    // 제일 마지막으로 입력된 내용이 vowelKey와 같다면
                    if (combination[combination.Length - 1].ToString() == vowelKey)
                    {
                        return vowelKey;
                    }
                    // 같지 않다면
                    else
                    {
                        for (int i = 0; i < combination.Length; i++)
                        {
                            if (combination[i] != correctCombinationTemp[i])
                            {
                                return "Wrong";
                            }
                        }
                    }
                }
            }
        }

        return null;
    }




    //// 입력 처리
    //private void CheckInput(string input)
    //{
    //    Debug.Log(input);

    //    // 현재 입력이 모음이라면
    //    if (IsVowel(input))
    //    {
    //        CheckVowelInput(input);
    //    }
    //    // 자음이라면
    //    else
    //    {
    //        // 모음 입력 중이었는데 자음이 들어온거라면 오타 처리
    //        if (currentVowelSequence.Length > 0)
    //        {
    //            UpdateDisplayText(false);
    //            totalTypos++;
    //            currentVowelSequence = "";

    //            StartCoroutine(WaitAndSetNextText(false));
    //        }
    //        // 첫 입력이 자음이면
    //        else
    //        {
    //            if (input == currentText)
    //            {
    //                UpdateDisplayText(true);
    //                correctWords++;
    //                totalCharactersTyped++;

    //                StartCoroutine(WaitAndSetNextText(true));
    //            }
    //            else
    //            {
    //                UpdateDisplayText(false);
    //                totalTypos++;

    //                StartCoroutine(WaitAndSetNextText(false));
    //            }
    //        }

    //        UpdateAccuracy();
    //        UpdateTypo();
    //    }
    //}

    //// 모음 입력 처리
    //private void CheckVowelInput(string input)
    //{
    //    currentVowelSequence += input;

    //    string resultVowel = GetFullVowelFromCombination(currentVowelSequence);

    //    // 모음 조합이 완성 되지 않았다면
    //    if (resultVowel == null)
    //    {
    //        return;
    //    }
    //    // 완성된 모음이라면
    //    else
    //    {
    //        if (resultVowel == currentText)
    //        {
    //            UpdateDisplayText(true);  // 정답 처리
    //            correctWords++;
    //            totalCharactersTyped++;
    //            currentVowelSequence = "";

    //            StartCoroutine(WaitAndSetNextText(true));
    //        }
    //        else
    //        {
    //            UpdateDisplayText(false);
    //            totalTypos++;
    //            currentVowelSequence = "";

    //            StartCoroutine(WaitAndSetNextText(false));
    //        }

    //        UpdateAccuracy();
    //        UpdateTypo();
    //    }
    //}

    //// 천지인 모음 조합이 완성되었는지 확인하는 함수
    //private string GetFullVowelFromCombination(string combination)
    //{
    //    // 천지인 모음 딕셔너리에서 일치하는 모음이 있는지 찾음
    //    foreach (var vowelEntry in keyboardManager.chonjiinVowelsInputCheck)
    //    {
    //        string vowelKey = vowelEntry.Key;
    //        string[] correctCombination = vowelEntry.Value;

    //        // 찾은 키가 정답과 같다면
    //        if (vowelKey == currentText)
    //        {
    //            // 입력한 길이가 입력해야할 길이와 같다면
    //            if (combination.Length == correctCombination.Length)
    //            {
    //                Debug.Log("combination : " + combination);
    //                //Debug.Log("correctCombination: " + string.Join("", correctCombination));

    //                //if (correctCombination.SequenceEqual(combination.Select(c => c.ToString())))
    //                if (combination == string.Join("", correctCombination))
    //                {
    //                    return vowelKey;
    //                }
    //                else
    //                {
    //                    return "Wrong";
    //                }
    //            }
    //            else
    //            {
    //                string correctCombinationTemp = string.Join("", correctCombination);
    //                for (int i = 0; i < combination.Length; i++)
    //                {
    //                    if (combination[i] != correctCombinationTemp[i])
    //                    {
    //                        return "Wrong";
    //                    }
    //                }
    //            }
    //        }
    //    }

    //    return null;
    //}




    




    // 모음 여부 확인
    private bool IsVowel(string input)
    {
        return keyboardManager.chonjiinVowelsInputCheck.ContainsKey(input);
    }

    // 입력시 0.3초 대기 후 다음 텍스트 설정
    private IEnumerator WaitAndSetNextText(bool isCorrect)
    {
        currentVowelSequence = "";
        isWaiting = true;
        yield return new WaitForSeconds(0.3f);

        if (isCorrect)
        {
            keyboardManager.ResetKeyColors();

            if (totalCharactersTyped < maxWords)
            {
                SetNextText();
                UpdateLeftText();
                HighlightKey();
            }
            else
            {
                UpdateAccuracy();
                EndPractice();
            }
        }

        isWaiting = false;
    }

    // 입력할 키 강조
    private void HighlightKey()
    {
        string highlightText = currentText;
        keyboardManager.HighlightKey(highlightText);
    }

    // 정확도 Update
    private void UpdateAccuracy()
    {
        float accuracy = (float)correctWords / (correctWords + totalTypos) * 100f;
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
        leftText.text = $"{totalCharactersTyped + 1} / {maxWords}";
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
