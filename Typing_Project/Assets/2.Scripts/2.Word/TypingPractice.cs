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
    private TypingStatistics typingStatistics;

    private string currentText;                         // 현재 연습할 텍스트
    private List<string> texts = new List<string>();

    private int totalWordsTyped = 0;                    // 총 입력된 단어 수
    private const int maxWords = 20;                    // 최대 입력할 단어 수

    private void Awake()
    {
        LoadTextsFromFile();
        inputField.ActivateInputField();                // 실행하자마자 inputField 포커스
        SetRandomText();
        this.enabled = false;                           // 연습 시작 전까지 비활성화
        typingStatistics = GetComponent<TypingStatistics>();
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
        bool isCorrect = inputField.text == currentText;
        resultText.text = isCorrect ? "Correct" : "Wrong";

        // 입력된 단어 수 증가
        totalWordsTyped++;

        typingStatistics.OnCharacterTyped(isCorrect, inputField.text.Length);

        // 20개 단어를 입력했는지 확인
        if (totalWordsTyped >= maxWords)
        {
            EndPractice();
        }
        else
        {
            inputField.text = "";
            SetRandomText();
            inputField.ActivateInputField();
        }
    }

    // 타자 연습 종료
    private void EndPractice()
    {
        inputField.interactable = false;                // 입력 필드 비활성화
        resultText.text = "Practice Complete!";         // 종료 메시지
        typingStatistics.EndPractice();                 // 통계 종료 호출
    }
}
