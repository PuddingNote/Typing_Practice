using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;
using System;

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

    private int totalCharactersTyped;               // �Էµ� �ܾ��� ��
    private int maxWords;                           // �Է� �� �ִ� �ܾ��� ��
    private int correctWords;                       // ���� �ܾ� ��
    private int totalTypos;                         // �� ��Ÿ ��

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
        maxWords = 10;                              // �ڸ����� �ִ� �ܾ� �� ����
        correctWords = 0;
        totalTypos = 0;

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

        if (PersistentDataPositionPractice.selectedType == "Korean")
        {
            bool isShiftPressed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

            if (Input.anyKeyDown)
            {
                foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(keyCode))
                    {
                        string input = KeyCodeToKoreanChar(keyCode, isShiftPressed);
                        if (!string.IsNullOrEmpty(input))
                        {
                            CheckInput(input);
                        }
                        break;
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
        string path = Path.Combine(Application.streamingAssetsPath, SceneManager.GetActiveScene().name, "Texts", PersistentDataPositionPractice.selectedType + ".txt");

        if (File.Exists(path))
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

        //HighlightKey();

        if (totalCharactersTyped == (maxWords - 1))
        {
            nextDisplayText.text = "";
        }
        else
        {
            nextDisplayText.text = texts[nextIndex];
        }
    }

    // �Է�
    private void CheckInput(string input)
    {
        if (input == currentText)
        {
            UpdateDisplayText(true);
            correctWords++;
            totalCharactersTyped++;

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
    }

    // �Է½� 0.3�� ��� �� ���� �ؽ�Ʈ ����
    private IEnumerator WaitAndSetNextText(bool isCorrect)
    {
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
        }
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
