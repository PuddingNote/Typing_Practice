using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TypingPractice : MonoBehaviour
{
    public TextMeshProUGUI displayText;         // 연습할 텍스트를 표시하는 Text UI
    public TMP_InputField inputField;           // 사용자의 입력을 받을 InputField UI
    public TextMeshProUGUI resultText;          // 결과를 표시할 Text UI

    private string currentText;     // 현재 연습할 텍스트

    private void Awake()
    {
        // 실행하자마자 inputField에 포커스 설정
        inputField.ActivateInputField();

        // 연습할 텍스트 초기화
        currentText = GetRandomText();
        displayText.text = currentText;
    }

    private void Update()
    {
        // 만약 포커스가 풀리면 다시 설정
        if (!inputField.isFocused)
        {
            inputField.ActivateInputField();
        }

        // 엔터 키가 눌렸는지 확인
        if (Input.GetKeyDown(KeyCode.Return))
        {
            CheckInput(); // 입력값을 검사
        }
    }

    // 무작위 텍스트를 반환하는 메서드
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

        // 무작위로 텍스트 선택
        int index = Random.Range(0, texts.Count);
        return texts[index];
    }

    // 사용자가 입력한 내용을 검사하는 메서드
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

        // 다음 연습을 위해 InputField 초기화
        inputField.text = "";
        currentText = GetRandomText();
        displayText.text = currentText;

        // 포커스를 다시 InputField로 설정
        inputField.ActivateInputField();
    }
}
