using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class IMEManager : MonoBehaviour
{
    // Windows API 함수 선언
    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
    private static extern IntPtr GetKeyboardLayout(int dwLayout);

    [DllImport("imm32.dll")]
    private static extern bool ImmSetConversionStatus(IntPtr hIMC, int conversion, int sentence);

    [DllImport("imm32.dll")]
    private static extern IntPtr ImmGetContext(IntPtr hWnd);

    [DllImport("imm32.dll")]
    private static extern bool ImmReleaseContext(IntPtr hWnd, IntPtr hIMC);

    private const int IME_CMODE_ALPHANUMERIC = 0x0000;

    // Start()
    public void StartIME()
    {
        SetIMEToEnglish();
    }

    private void SetIMEToEnglish()
    {
        IntPtr hWnd = GetForegroundWindow();
        IntPtr hIMC = ImmGetContext(hWnd);

        if (hIMC != IntPtr.Zero)
        {
            ImmSetConversionStatus(hIMC, IME_CMODE_ALPHANUMERIC, 0);
            ImmReleaseContext(hWnd, hIMC);
            Debug.Log("영어 강제 전환");
        }
        else
        {
            Debug.LogWarning("Failed to get IME context.");
        }
    }
}
