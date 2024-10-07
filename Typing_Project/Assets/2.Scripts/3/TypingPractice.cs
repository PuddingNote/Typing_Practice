using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TypingPractice : MonoBehaviour
{
    public TextMeshProUGUI displayText;         // ������ �ؽ�Ʈ�� ǥ���ϴ� Text UI
    public TMP_InputField inputField;           // ������� �Է��� ���� InputField UI
    public TextMeshProUGUI resultText;          // ����� ǥ���� Text UI

    private string currentText;     // ���� ������ �ؽ�Ʈ

    private void Awake()
    {
        // �������ڸ��� inputField�� ��Ŀ�� ����
        inputField.ActivateInputField();

        // ������ �ؽ�Ʈ �ʱ�ȭ
        currentText = GetRandomText();
        displayText.text = currentText;
    }

    private void Update()
    {
        // ���� ��Ŀ���� Ǯ���� �ٽ� ����
        if (!inputField.isFocused)
        {
            inputField.ActivateInputField();
        }

        // ���� Ű�� ���ȴ��� Ȯ��
        if (Input.GetKeyDown(KeyCode.Return))
        {
            CheckInput(); // �Է°��� �˻�
        }
    }

    // ������ �ؽ�Ʈ�� ��ȯ�ϴ� �޼���
    private string GetRandomText()
    {
        List<string> texts = new List<string>
        {
            "unity",
            "game",
            "key",
            "keyboard",
            "han",
            "while",
            "for",
            "private"
        };

        // �������� �ؽ�Ʈ ����
        int index = Random.Range(0, texts.Count);
        return texts[index];
    }

    // ����ڰ� �Է��� ������ �˻��ϴ� �޼���
    private void CheckInput()
    {
        string userInput = inputField.text;

        if (userInput == currentText)
        {
            resultText.text = "Correct";
        }
        else
        {
            resultText.text = "Wrong";
        }

        // ���� ������ ���� InputField �ʱ�ȭ
        inputField.text = "";
        currentText = GetRandomText();
        displayText.text = currentText;

        // ��Ŀ���� �ٽ� InputField�� ����
        inputField.ActivateInputField();
    }
}
