using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class TypingPractice : MonoBehaviour
{
    public TextMeshProUGUI displayText;                 // 연습할 텍스트를 표시하는 Text UI
    public TMP_InputField inputField;                   // 사용자의 입력을 받을 InputField UI
    public TextMeshProUGUI resultText;                  // 결과를 표시할 Text UI

    private string currentText;                         // 현재 연습할 텍스트
    private List<string> texts = new List<string>();

    private void Awake()
    {
        LoadTextsFromFile();
        inputField.ActivateInputField();                // 실행하자마자 inputField 포커스
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

    // 텍스트 파일에서 내용 읽어오는 메서드
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
            Debug.LogError("텍스트 파일을 찾을 수 없습니다");
        }
    }

    // 텍스트 설정 메서드
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
            displayText.text = "텍스트가 없습니다.";
        }
    }

    // 사용자가 입력한 내용을 검사하는 메서드
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
