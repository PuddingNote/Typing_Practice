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
    public static string selectedLanguage = "";
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
    private List<string> texts;                     // �޸��忡�� �ҷ��� �ܾ� ����Ʈ

    private string currentText;                     // ���� ȭ�鿡 ǥ�õ� �ܾ�
    private int currentIndex;                       // ���� �ܾ� �ε���
    private int nextIndex;                          // ���� �ܾ� �ε���

    private int totalCharactersTyped;               // �Էµ� �ܾ��� ��
    private int maxWords;                           // �Է� �� �ִ� �ܾ��� ��
    private int correctWords;                       // ���� �ܾ� ��
    private int totalTypos;                         // �� ��Ÿ ��

    private string currentVowelSequence;            // ���� �Էµ� ���� ������

    private List<string> consonants;                // ���� ����Ʈ
    private List<string> vowels;                    // ���� ����Ʈ
    private bool isConsonantTurn;                   // ����-���� ������ ���� ����

    // ETC
    private TypingStatisticsPositionPractice typingStatistics;
    private KeyboardManagerPositionPractice keyboardManager;
    private bool isGameEnded;
    private bool isWaiting;

    // �ʱ� ������ ����
    private void SetData()
    {
        currentIndex = -1;
        nextIndex = -1;

        totalCharactersTyped = 0;
        maxWords = 20;                              // �ڸ����� �ִ� �ܾ� �� ����
        correctWords = 0;
        totalTypos = 0;

        currentVowelSequence = "";

        consonants = new List<string>();
        vowels = new List<string>();
        isConsonantTurn = false;

        isGameEnded = false;
        isWaiting = false;         
    }

    // ����
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

        if (PersistentDataPositionPractice.selectedLanguage == "Korean")
        {
            bool isShiftPressed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

            foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode))                      // GetKey ���ٰ� GetKeyDown���ϱ� �Է��� �ߵȴ�??
                {
                    string input = KeyCodeToKoreanChar(keyCode, isShiftPressed);

                    if (!string.IsNullOrEmpty(input))
                    {
                        CheckInput(input);
                    }
                }
            }
        }
        else
        {
            string input = Input.inputString;

            if (!string.IsNullOrEmpty(input) && input != "\n" && input != "\r")
            {
                CheckInput(input);
            }
        }
    }

    // ���� > �ѱ� ���� ��ȯ
    private string KeyCodeToKoreanChar(KeyCode keyCode, bool isShiftPressed)
    {
        if (isShiftPressed)
        {
            switch (keyCode)
            {
                // ����
                case KeyCode.R: return "��";     // Shift + �� -> ��
                case KeyCode.E: return "��";     // Shift + �� -> ��
                case KeyCode.Q: return "��";     // Shift + �� -> ��
                case KeyCode.T: return "��";     // Shift + �� -> ��
                case KeyCode.W: return "��";     // Shift + �� -> ��

                // ����
                case KeyCode.O: return "��";     // Shift + �� -> ��
                case KeyCode.P: return "��";     // Shift + �� -> ��
            }
        }

        switch (keyCode)
        {
            // ����
            case KeyCode.Q: return "��";         // Q -> ��
            case KeyCode.W: return "��";         // W -> ��
            case KeyCode.E: return "��";         // E -> ��
            case KeyCode.R: return "��";         // R -> ��
            case KeyCode.T: return "��";         // T -> ��
            case KeyCode.A: return "��";         // A -> ��
            case KeyCode.S: return "��";         // S -> ��
            case KeyCode.D: return "��";         // D -> ��
            case KeyCode.F: return "��";         // F -> ��
            case KeyCode.G: return "��";         // G -> ��
            case KeyCode.Z: return "��";         // Z -> ��
            case KeyCode.X: return "��";         // X -> ��
            case KeyCode.C: return "��";         // C -> ��
            case KeyCode.V: return "��";         // V -> ��

            // ����
            case KeyCode.Y: return "��";         // Y -> ��
            case KeyCode.U: return "��";         // U -> ��
            case KeyCode.I: return "��";         // I -> ��
            case KeyCode.O: return "��";         // O -> ��
            case KeyCode.P: return "��";         // P -> ��
            case KeyCode.H: return "��";         // H -> ��
            case KeyCode.J: return "��";         // J -> ��
            case KeyCode.K: return "��";         // K -> ��
            case KeyCode.L: return "��";         // L -> ��
            case KeyCode.B: return "��";         // B -> ��
            case KeyCode.N: return "��";         // N -> ��
            case KeyCode.M: return "��";         // M -> ��

            default: return string.Empty;
        }
    }

    // ���� ����Ÿ���� ��ȯ
    private void SetKeyBoardLanguage()
    {
        GetComponent<IMEManager>().StartIME();
    }

    // ��ư ����
    private void SetButtons()
    {
        backButton.onClick.AddListener(OnBackButtonPressed);
        resumeButton.onClick.AddListener(OnResumeButtonPressed);
        titleButton.onClick.AddListener(OnTitleButtonPressed);
    }

    // File Load (�� ���̳� ���� ����)
    private void LoadTextsFromFile()
    {
        string path = Path.Combine(Application.streamingAssetsPath, SceneManager.GetActiveScene().name, "Texts", PersistentDataPositionPractice.selectedLanguage + ".txt");

        if (PersistentDataPositionPractice.selectedLanguage == "Korean")
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

    // ���� Text ����
    private void SetNextText()
    {
        // �ѱ� �Է��̸� ���� ������ ������ �������� ����
        if (PersistentDataPositionPractice.selectedLanguage == "Korean")
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
        // ���� �Է�
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

    /*
    private void LoadTextsFromFile()
    {
        string path = Path.Combine(Application.streamingAssetsPath, SceneManager.GetActiveScene().name, "Texts", PersistentDataPositionPractice.selectedType + ".txt");

        string[] lines = File.ReadAllLines(path);
        foreach (string line in lines)
        {
            if (!string.IsNullOrWhiteSpace(line))
            {
                texts.Add(line.Trim());
            }
        }
    }

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

        if (totalCharactersTyped == (maxWords - 1))
        {
            nextDisplayText.text = "";
        }
        else
        {
            nextDisplayText.text = texts[nextIndex];
        }
    }
    */ // ���� �ؽ�Ʈ ���� �ڵ�

    // �Է� ó��
    private void CheckInput(string input)
    {
        Debug.Log(input);

        // ���� �Է��� �����̶��
        if (IsVowel(input))
        {
            CheckVowelInput(input);
        }
        // �����̶��
        else
        {
            CheckConsonantInput(input);
        }

        UpdateAccuracy();
        UpdateTypo();
    }

    // ���� �Է� ó��
    private void CheckConsonantInput(string input)
    {
        // ù �Է��� �����̶��
        if (currentVowelSequence.Length == 0)
        {
            if (input == currentText)
            {
                correctWords++;
                totalCharactersTyped++;

                UpdateDisplayText(true);
                StartCoroutine(WaitAndSetNextText(true));
            }
            else
            {
                totalTypos++;

                UpdateDisplayText(false);
                StartCoroutine(WaitAndSetNextText(false));
            }
        }
        // ���� �Է� �߿� ������ ���� ��� ��Ÿ ó��
        else
        {
            totalTypos++;

            UpdateDisplayText(false);
            StartCoroutine(WaitAndSetNextText(false));
        }
    }

    // ���� �Է� ó��
    private void CheckVowelInput(string input)
    {
        currentVowelSequence += input;

        string resultVowel = GetFullVowelFromCombination(currentVowelSequence);

        // ���� ������ ���� �ϼ� ���� �ʾҴٸ�
        if (resultVowel == null)
        {
            return;
        }
        // �ϼ��� �����̶��
        else
        {
            if (resultVowel == currentText)
            {
                correctWords++;
                totalCharactersTyped++;

                UpdateDisplayText(true);
                StartCoroutine(WaitAndSetNextText(true));
            }
            else
            {
                totalTypos++;

                UpdateDisplayText(false);
                StartCoroutine(WaitAndSetNextText(false));
            }

            UpdateAccuracy();
            UpdateTypo();
        }
    }

    // õ���� ���� ������ �ϼ��Ǿ����� Ȯ��
    private string GetFullVowelFromCombination(string combination)
    {
        // õ���� ���� ��ųʸ����� ��ġ�ϴ� ������ �ִ��� ã��
        foreach (var vowelEntry in keyboardManager.chonjiinVowelsInputCheck)
        {
            string vowelKey = vowelEntry.Key;
            string[] correctCombination = vowelEntry.Value;

            // ã�� Ű�� ����� ���ٸ�
            if (vowelKey == currentText)
            {
                string correctCombinationTemp = string.Join("", correctCombination);

                // �Է��� ���̰� �Է��ؾ� �� ���̿� ���ٸ�
                if (combination.Length == correctCombination.Length)
                {
                    if (combination == correctCombinationTemp)
                    {
                        return vowelKey;
                    }
                    else
                    {
                        return "Wrong";
                    }
                }
                else if (combination.Length > correctCombination.Length)
                {
                    return "Wrong";
                }
                else
                {
                    // ���� ���������� �Էµ� ������ vowelKey�� ���ٸ�
                    if (combination[combination.Length - 1].ToString() == vowelKey)
                    {
                        return vowelKey;
                    }
                    // ���� �ʴٸ�
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

    // ���� ���� Ȯ��
    private bool IsVowel(string input)
    {
        return keyboardManager.chonjiinVowelsInputCheck.ContainsKey(input);
    }

    // �Է½� 0.3�� ��� �� ���� �ؽ�Ʈ ����
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

    // �Է��� Ű ����
    private void HighlightKey()
    {
        string highlightText = currentText;
        keyboardManager.HighlightKey(highlightText);
    }

    // ��Ȯ�� Update
    private void UpdateAccuracy()
    {
        float accuracy = (float)correctWords / (correctWords + totalTypos) * 100f;
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
            StartCoroutine(ResetDisplayTextColor());
        }
    }

    // ��Ÿ�� �ؽ�Ʈ ���� 0.3�� ��� �� �ٽ� ����
    private IEnumerator ResetDisplayTextColor()
    {
        yield return new WaitForSeconds(0.3f);
        displayText.text = currentText;
    }

    // ���� �ܾ� �� Update
    private void UpdateLeftText()
    {
        leftText.text = $"{totalCharactersTyped + 1} / {maxWords}";
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
        PersistentDataPositionPractice.selectedLanguage = "";
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
