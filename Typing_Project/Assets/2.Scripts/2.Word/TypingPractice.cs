using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class TypingPractice : MonoBehaviour
{
    public TextMeshProUGUI displayText;                 // ������ �ؽ�Ʈ�� ǥ���ϴ� Text UI
    public TMP_InputField inputField;                   // ������� �Է��� ���� InputField UI
    public TextMeshProUGUI resultText;                  // ����� ǥ���� Text UI

    private string currentText;                         // ���� ������ �ؽ�Ʈ
    private List<string> texts = new List<string>();

    private void Awake()
    {
        LoadTextsFromFile();
        inputField.ActivateInputField();                // �������ڸ��� inputField ��Ŀ��
        SetRandomText();
    }

    private void Update()
    {
        if (!inputField.isFocused)
        {
            inputField.ActivateInputField();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            CheckInput();
        }
    }

    // �ؽ�Ʈ ���Ͽ��� ���� �о���� �޼���
    private void LoadTextsFromFile()
    {
        string path = Application.dataPath + "/2.Scripts/2.Word/Word.txt";
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

    // �ؽ�Ʈ ���� �޼���
    private void SetRandomText()
    {
        if (texts.Count > 0)
        {
            int index = Random.Range(0, texts.Count);
            currentText = texts[index];
            displayText.text = currentText;
        }
        else
        {
            displayText.text = "�ؽ�Ʈ�� �����ϴ�.";
        }
    }

    // ����ڰ� �Է��� ������ �˻��ϴ� �޼���
    private void CheckInput()
    {
        if (inputField.text == currentText)
        {
            resultText.text = "Correct";
        }
        else
        {
            resultText.text = "Wrong";
        }

        inputField.text = "";
        SetRandomText();
        inputField.ActivateInputField();
    }
}
