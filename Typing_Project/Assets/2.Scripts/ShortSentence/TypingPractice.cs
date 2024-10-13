using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class TypingPractice : MonoBehaviour
{
    public TextMeshProUGUI displayText;
    public TMP_InputField inputField;
    private TypingStatistics typingStatistics;

    private string currentText;
    private List<string> texts = new List<string>();
    private int totalWordsTyped = 0;
    private const int maxWords = 5;
    private int totalTypedChars = 0;
    private int correctTypedChars = 0;
    private bool isPaused = false;

    private void Awake()
    {
        typingStatistics = GetComponent<TypingStatistics>();
        SetInputfield();
        LoadTextsFromFile();
        SetRandomText();
        inputField.ActivateInputField();
        this.enabled = false;

        inputField.onValueChanged.AddListener(delegate { CheckInput(); });
        inputField.contentType = TMP_InputField.ContentType.Standard;
        inputField.lineType = TMP_InputField.LineType.SingleLine;
    }

    private void Update()
    {
        if (!inputField.isFocused)
        {
            inputField.ActivateInputField();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            OnEnterPressed();
        }
    }

    private void SetInputfield()
    {
        var background = inputField.GetComponentInChildren<Image>();
        background.color = Color.white;

        var outline = inputField.gameObject.AddComponent<Outline>();
        outline.effectColor = new Color(93f / 255f, 158f / 255f, 235f / 255f, 170f / 255f);
        outline.effectDistance = new Vector2(2, 2);
    }

    private void LoadTextsFromFile()
    {
        string path = Application.dataPath + "/2.Scripts/ShortSentence/ShortSentence.txt";
        if (File.Exists(path))
        {
            string[] lines = File.ReadAllLines(path);
            texts.AddRange(lines);
        }
        else
        {
            Debug.LogError("텍스트 파일을 찾을 수 없습니다");
        }
    }

    private void SetRandomText()
    {
        if (texts.Count > 0)
        {
            int index = Random.Range(0, texts.Count);
            currentText = texts[index];
            displayText.text = currentText;
            inputField.characterLimit = currentText.Length;
            inputField.text = ""; // 입력 필드 초기화
        }
        else
        {
            displayText.text = "텍스트가 없습니다.";
        }
    }

    private void CheckInput()
    {
        string typedText = inputField.text;
        totalTypedChars = typedText.Length;

        correctTypedChars = 0; // 올바르게 입력된 문자 수 초기화

        for (int i = 0; i < typedText.Length; i++)
        {
            if (i < currentText.Length)
            {
                bool isCorrect = typedText[i] == currentText[i];
                ChangeCharColor(i, isCorrect);

                if (isCorrect)
                {
                    correctTypedChars++;
                }
                else
                {
                    typingStatistics.OnCharacterTyped(false, 1);
                }
            }
            else
            {
                typingStatistics.OnCharacterTyped(false, 1);
            }
        }

        if (typedText.Length < currentText.Length)
        {
            for (int i = typedText.Length; i < currentText.Length; i++)
            {
                typingStatistics.OnCharacterTyped(false, 1);
            }
        }

        UpdateAccuracy();
    }

    private void OnEnterPressed()
    {
        string typedText = inputField.text;

        if (typedText.Length > 0)
        {
            // 총 입력된 단어 수 증가
            totalWordsTyped++;

            // 문장 비교 및 정확도 계산
            for (int i = 0; i < currentText.Length; i++)
            {
                if (i >= typedText.Length || typedText[i] != currentText[i])
                {
                    typingStatistics.OnCharacterTyped(false, 1);
                    correctTypedChars++; // 여기서 correctTypedChars를 증가시키면 안됨
                }
                else
                {
                    correctTypedChars++;
                }
            }

            // 총 입력된 문자 수 업데이트
            totalTypedChars += currentText.Length; // 추가: 현재 문장의 문자 수 추가

            typingStatistics.PausePractice();
            isPaused = true;

            // 정확도 업데이트
            UpdateAccuracy();

            // 새로운 문장 설정
            if (totalWordsTyped < maxWords) // 최대 단어 수 체크
            {
                SetRandomText();
                inputField.ActivateInputField();
            }
            else
            {
                EndPractice(); // 최대 단어 수에 도달하면 연습 종료
                return;
            }

            typingStatistics.StartPractice();
            isPaused = false;
        }
    }

    private void UpdateAccuracy()
    {
        float accuracy = (totalTypedChars > 0) ? ((float)correctTypedChars / (float)totalTypedChars) * 100f : 0f;
        typingStatistics.UpdateAccuracy(accuracy);
    }

    private void ChangeCharColor(int index, bool isCorrect)
    {
        string coloredText = "";

        for (int i = 0; i < currentText.Length; i++)
        {
            if (i < inputField.text.Length)
            {
                if (i < inputField.text.Length && inputField.text[i] == currentText[i])
                {
                    coloredText += $"<color=blue>{currentText[i]}</color>"; // 올바른 입력은 파란색
                }
                else
                {
                    coloredText += $"<color=red>{currentText[i]}</color>";  // 틀린 입력은 빨간색
                }
            }
            else
            {
                coloredText += currentText[i];  // 입력되지 않은 부분은 기본 색상
            }
        }

        displayText.text = coloredText;
    }

    private void EndPractice()
    {
        inputField.interactable = false;
        typingStatistics.EndPractice();
    }
}
