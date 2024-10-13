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
            Debug.LogError("�ؽ�Ʈ ������ ã�� �� �����ϴ�");
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
            inputField.text = ""; // �Է� �ʵ� �ʱ�ȭ
        }
        else
        {
            displayText.text = "�ؽ�Ʈ�� �����ϴ�.";
        }
    }

    private void CheckInput()
    {
        string typedText = inputField.text;
        totalTypedChars = typedText.Length;

        correctTypedChars = 0; // �ùٸ��� �Էµ� ���� �� �ʱ�ȭ

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
            // �� �Էµ� �ܾ� �� ����
            totalWordsTyped++;

            // ���� �� �� ��Ȯ�� ���
            for (int i = 0; i < currentText.Length; i++)
            {
                if (i >= typedText.Length || typedText[i] != currentText[i])
                {
                    typingStatistics.OnCharacterTyped(false, 1);
                    correctTypedChars++; // ���⼭ correctTypedChars�� ������Ű�� �ȵ�
                }
                else
                {
                    correctTypedChars++;
                }
            }

            // �� �Էµ� ���� �� ������Ʈ
            totalTypedChars += currentText.Length; // �߰�: ���� ������ ���� �� �߰�

            typingStatistics.PausePractice();
            isPaused = true;

            // ��Ȯ�� ������Ʈ
            UpdateAccuracy();

            // ���ο� ���� ����
            if (totalWordsTyped < maxWords) // �ִ� �ܾ� �� üũ
            {
                SetRandomText();
                inputField.ActivateInputField();
            }
            else
            {
                EndPractice(); // �ִ� �ܾ� ���� �����ϸ� ���� ����
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
                    coloredText += $"<color=blue>{currentText[i]}</color>"; // �ùٸ� �Է��� �Ķ���
                }
                else
                {
                    coloredText += $"<color=red>{currentText[i]}</color>";  // Ʋ�� �Է��� ������
                }
            }
            else
            {
                coloredText += currentText[i];  // �Էµ��� ���� �κ��� �⺻ ����
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
