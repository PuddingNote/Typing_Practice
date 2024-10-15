using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;

public class ForceKoreanIME
{
    // imm32.dll 사용 선언
    [DllImport("imm32.dll")]
    public static extern IntPtr ImmGetContext(IntPtr hWnd);

    [DllImport("imm32.dll")]
    public static extern bool ImmSetConversionStatus(IntPtr hIMC, int conversion, int sentence);

    [DllImport("imm32.dll")]
    public static extern bool ImmReleaseContext(IntPtr hWnd, IntPtr hIMC);

    // 윈도우 핸들 가져오기
    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    // IME 상태 변경
    private const int IME_CMODE_NATIVE = 0x0001;
    private const int IME_CMODE_HANGUL = 0x0005;

    public void Start()
    {
        // 윈도우 핸들 가져오기
        IntPtr hwnd = GetForegroundWindow();
        IntPtr hIMC = ImmGetContext(hwnd);

        // 한글 입력 모드로 변경
        ImmSetConversionStatus(hIMC, IME_CMODE_NATIVE | IME_CMODE_HANGUL, 0);

        // IME 컨텍스트 해제
        ImmReleaseContext(hwnd, hIMC);

        Debug.Log("한글 입력 모드로 설정됨.");
    }
}
