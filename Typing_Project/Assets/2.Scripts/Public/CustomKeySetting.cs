using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public static class PersistentDataTitleScene
{
    public static string selectedLanguage = "";
    public static string selectedHandType = "";
}

public class CustomKeySetting : MonoBehaviour
{
    // UI
    public Button[] rightKeyboardButtons;
    public TextMeshProUGUI[] rightKeyboardSubTexts;

    // First Start Keyboard Text Setting
    private string[] englishCharacters =
    {
        "기타", "영문", "한글",
        "ESC", "R", "U", "D",
        "RAlt", "H", "A", "W",
        "G", "N", "E", "Y",
        "V", "T", "O", "L",
        "-", "S", "I", "M",
        "Back Space", ".,", "Enter",
        "Space", "Shift", "Ctrl"
    };
    private string[] englishSubCharacters =
    {
        "", "P", "C", "K",
        "LAlt", "B", "", "X",
        "Q", "F", "", ";",
        "Z", "", "", "'",
        "=", "", "J", "/",
        "Win"
    };
    private string[] koreanOriginalCharacters =
    {
        "기타", "영문", "한글",
        "ESC", "ㅂ", "ㄴ", "ㅌ",
        "RAlt", "ㅈ", "ㅣ", "ㄷ",
        "ㅍ", "ㅇ", "ㆍ", "ㅁ",
        "ㅋ", "ㄱ", "ㅡ", "ㄹ",
        "-", "ㅎ", "ㅅ", "ㅊ",
        "Back Space", ".,", "Enter",
        "Space", "Shift", "Ctrl"
    };
    private string[] koreanSubOriginalCharacters =
    {
        "", "", "", "",
        "LAlt", "", "", "",
        "", "", "", ";",
        "", "", "", "'",
        "=", "", "", "/",
        "Win"
    };
    private string[] koreanCharacters =
    {
        "기타", "영문", "한글",
        "ESC", "ㅂ", "ㄴ", "ㅌ",
        "RAlt", "ㅈ", "ㅣ", "ㄷ",
        "ㅍ", "ㅇ", "ㆍ", "ㅁ",
        "ㅋ", "ㄱ", "ㅡ", "ㄹ",
        "-", "ㅎ", "ㅅ", "ㅊ",
        "Back Space", ".,", "Enter",
        "Space", "Shift", "Ctrl"
    };
    private string[] koreanSubCharacters =
    {
        "", "", "", "",
        "LAlt", "", "", "",
        "", "", "", ";",
        "", "", "", "'",
        "=", "", "", "/",
        "Win"
    };

    // Buttons
    public Button saveAndQuitButton;

    // ETC
    private ButtonManagerTitleScene buttonManager;
    private bool isWaitingForKeyInput;
    private Button selectedButton;

    private Color highlightColor = Color.yellow;
    private Color originalColor;

    // 시작
    public void StartCustomKeySetting()
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
        buttonManager = GameObject.Find("ButtonManager").GetComponent<ButtonManagerTitleScene>();
        isWaitingForKeyInput = false;

        SetInitialKeyTexts();
        AddButtonListeners();
        saveAndQuitButton.onClick.AddListener(() => SaveData());
    }

    // 데이터 저장
    private void SaveData()
    {
        // 바꾼 키들을 저장하는 코드 작성 필요 PlayerPrefs 사용 예정



        // 저장하고 바로 나가기
        buttonManager.GoTitleScene();
    }

    // 영어 > 한글 강제 변환
    private string KeyCodeToKoreanChar(KeyCode keyCode)
    {
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

    // 초기 키 텍스트 세팅
    private void SetInitialKeyTexts()
    {
        if (PersistentDataTitleScene.selectedLanguage == "Korean")
        {
            for (int i = 0; i < rightKeyboardButtons.Length; i++)
            {
                rightKeyboardButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = koreanCharacters[i];
            }
            for (int i = 0; i < rightKeyboardSubTexts.Length; i++)
            {
                rightKeyboardSubTexts[i].text = koreanSubCharacters[i];
            }
        }
        else
        {
            for (int i = 0; i < rightKeyboardButtons.Length; i++)
            {
                rightKeyboardButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = englishCharacters[i];
            }
            for (int i = 0; i < rightKeyboardSubTexts.Length; i++)
            {
                rightKeyboardSubTexts[i].text = englishSubCharacters[i];
            }
        }
    }

    // 버튼 클릭 리스너 추가
    private void AddButtonListeners()
    {
        foreach (Button button in rightKeyboardButtons)
        {
            button.onClick.AddListener(() => OnKeyButtonClicked(button));
        }
    }

    // 버튼 클릭
    private void OnKeyButtonClicked(Button button)
    {
        if (!isWaitingForKeyInput)
        {
            selectedButton = button;
            originalColor = button.GetComponent<Image>().color;
            button.GetComponent<Image>().color = highlightColor;
            StartCoroutine(WaitForKeyInput());
        }
    }

    // 키 입력 대기
    private IEnumerator WaitForKeyInput()
    {
        isWaitingForKeyInput = true;

        // 키 입력 대기
        while (!Input.anyKeyDown)
        {
            yield return null;
        }

        if (PersistentDataTitleScene.selectedLanguage == "Korean")
        {
            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode))
                {
                    string input = KeyCodeToKoreanChar(keyCode);

                    SetKeyForButton(selectedButton, input);
                    break;
                }
            }
        }
        else
        {
            string input = Input.inputString;

            SetKeyForButton(selectedButton, input.ToUpper());
        }

        selectedButton.GetComponent<Image>().color = originalColor;

        isWaitingForKeyInput = false;
    }

    // 버튼에 새로운 키 설정
    private void SetKeyForButton(Button button, string key)
    {
        button.GetComponentInChildren<TextMeshProUGUI>().text = key;

        for (int i = 0; i < rightKeyboardButtons.Length; i++)
        {
            if (rightKeyboardButtons[i] == button)
            {
                koreanCharacters[i] = key;
                break;
            }
        }
    }
}
