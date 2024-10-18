using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CheonjiinKeyboard : MonoBehaviour
{
    // 상태를 추적하기 위한 전역 변수
    private bool lPressed = false;  // l이 눌렸는지 여부를 추적
    private bool kReplaced = false; // k로 교체했는지 여부를 추적
    private bool iReplaced = false; // i로 교체했는지 여부를 추적
    private bool mPressed = false;  // m이 눌렸는지 여부를 추적
    private bool nReplaced = false; // n로 교체했는지 여부를 추적
    private bool consonant = false; // 자음 선택 여부를 추적
    private int dotCount = 0;       // 점 카운트
    private bool jReplaced = false; // j로 교체했는지 여부를 추적
    private bool uReplaced = false; // u로 교체했는지 여부를 추적

    // 상태를 초기화하는 함수
    private void ResetState()
    {
        lPressed = false;
        kReplaced = false;
        mPressed = false;
        nReplaced = false;
        iReplaced = false;
        jReplaced = false;
        uReplaced = false;
        consonant = false;
        dotCount = 0;
    }

    // Unity의 업데이트 함수에서 키 입력을 처리
    void Update()
    {
        // 각 키에 대한 처리
        if (Input.GetKeyDown(KeyCode.L))
        {
            ProcessIN();
        }
        else if (Input.GetKeyDown(KeyCode.A)) // ARAE_A
        {
            ProcessAraeA();
        }
        else if (Input.GetKeyDown(KeyCode.J)) // JI
        {
            ProcessJI();
        }
        else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            ResetState();
        }
        // 자음 입력 처리
        else if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.E) ||
                 Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.T) || Input.GetKeyDown(KeyCode.A) ||
                 Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.F) ||
                 Input.GetKeyDown(KeyCode.G) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X) ||
                 Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.V))
        {
            ResetState();
            consonant = true; // 자음 상태 true
        }
    }

    private void ProcessIN()
    {
        if (kReplaced)
        {
            // i를 지우고 o를 출력 (ㅐ)
            BackspaceAndType('o');
            ResetState();
        }
        else if (iReplaced)
        {
            // i를 지우고 O를 출력 (ㅒ)
            BackspaceAndType('O', true); // 대문자 O
            ResetState();
        }
        else if (consonant && dotCount == 1)
        {
            // j를 출력 (ㅓ)
            TypeChar('j');
            ResetState();
            jReplaced = true;
        }
        else if (consonant && dotCount == 2)
        {
            // u를 출력 (ㅕ)
            TypeChar('u');
            ResetState();
            uReplaced = true;
        }
        else if (jReplaced)
        {
            // p를 출력 (ㅔ)
            BackspaceAndType('p');
            ResetState();
        }
        else if (uReplaced)
        {
            // P를 출력 (ㅖ)
            BackspaceAndType('P', true); // 대문자 P
            ResetState();
        }
        else if (dotCount == 0)
        {
            // l을 출력 (ㅣ)
            TypeChar('l');
            lPressed = true;
        }
    }

    private void ProcessAraeA()
    {
        if (lPressed && !kReplaced)
        {
            // l을 지우고 k를 출력 (ㅏ)
            BackspaceAndType('k');
            kReplaced = true;
            dotCount = 0;
        }
        else if (lPressed && kReplaced)
        {
            // k를 지우고 i를 출력 (ㅑ)
            BackspaceAndType('i');
            ResetState();
            iReplaced = true;
        }
        else if (mPressed && !nReplaced)
        {
            // m을 지우고 n을 출력 (ㅜ)
            BackspaceAndType('n');
            nReplaced = true;
            dotCount = 0;
        }
        else if (mPressed && nReplaced)
        {
            // n을 지우고 b를 출력 (ㅠ)
            BackspaceAndType('b');
            ResetState();
        }
        else if (consonant)
        {
            dotCount++;
        }
        else if (dotCount == 3)
        {
            dotCount = 0;
            consonant = false;
        }
    }

    private void ProcessJI()
    {
        if (dotCount == 0)
        {
            // m을 출력 (ㅡ)
            TypeChar('m');
            mPressed = true;
        }
        else if (consonant && dotCount == 1)
        {
            // h를 출력 (ㅗ)
            TypeChar('h');
            ResetState();
        }
        else if (consonant && dotCount == 2)
        {
            // y를 출력 (ㅛ)
            TypeChar('y');
            ResetState();
        }
    }

    // 백스페이스 후 문자를 출력하는 함수
    private void BackspaceAndType(char c, bool shift = false)
    {
        // 백스페이스 (유니티에서는 InputField 또는 Text를 직접 제어해야 함)
        // InputField.text = InputField.text.Substring(0, InputField.text.Length - 1);

        if (shift)
        {
            // Shift 키 입력
            // Shift를 눌러서 대문자 입력 처리
        }

        // 해당 문자를 출력
        // InputField.text += c;
    }

    // 문자를 출력하는 함수
    private void TypeChar(char c)
    {
        // 해당 문자를 출력
        // InputField.text += c;
    }
}
