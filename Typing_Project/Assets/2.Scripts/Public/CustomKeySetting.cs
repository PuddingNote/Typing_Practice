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
        "��Ÿ", "����", "�ѱ�",
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
        "��Ÿ", "����", "�ѱ�",
        "ESC", "��", "��", "��",
        "RAlt", "��", "��", "��",
        "��", "��", "��", "��",
        "��", "��", "��", "��",
        "-", "��", "��", "��",
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
        "��Ÿ", "����", "�ѱ�",
        "ESC", "��", "��", "��",
        "RAlt", "��", "��", "��",
        "��", "��", "��", "��",
        "��", "��", "��", "��",
        "-", "��", "��", "��",
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

    // ����
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

    // ������ ����
    private void SaveData()
    {
        // �ٲ� Ű���� �����ϴ� �ڵ� �ۼ� �ʿ� PlayerPrefs ��� ����



        // �����ϰ� �ٷ� ������
        buttonManager.GoTitleScene();
    }

    // ���� > �ѱ� ���� ��ȯ
    private string KeyCodeToKoreanChar(KeyCode keyCode)
    {
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

    // �ʱ� Ű �ؽ�Ʈ ����
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

    // ��ư Ŭ�� ������ �߰�
    private void AddButtonListeners()
    {
        foreach (Button button in rightKeyboardButtons)
        {
            button.onClick.AddListener(() => OnKeyButtonClicked(button));
        }
    }

    // ��ư Ŭ��
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

    // Ű �Է� ���
    private IEnumerator WaitForKeyInput()
    {
        isWaitingForKeyInput = true;

        // Ű �Է� ���
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

    // ��ư�� ���ο� Ű ����
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
