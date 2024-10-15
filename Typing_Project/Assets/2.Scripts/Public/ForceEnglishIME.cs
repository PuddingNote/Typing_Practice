using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;

public class ForceEnglishIME
{
    // imm32.dll ��� ����
    [DllImport("imm32.dll")]
    public static extern IntPtr ImmGetContext(IntPtr hWnd);

    [DllImport("imm32.dll")]
    public static extern bool ImmSetConversionStatus(IntPtr hIMC, int conversion, int sentence);

    [DllImport("imm32.dll")]
    public static extern bool ImmReleaseContext(IntPtr hWnd, IntPtr hIMC);

    // ������ �ڵ� ��������
    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    // IME ���� ����
    private const int IME_CMODE_ALPHANUMERIC = 0x0000; // ���� �Է� ���

    public void Start()
    {
        // ������ �ڵ� ��������
        IntPtr hwnd = GetForegroundWindow();
        IntPtr hIMC = ImmGetContext(hwnd);

        // ���� �Է� ���� ����
        ImmSetConversionStatus(hIMC, IME_CMODE_ALPHANUMERIC, 0);

        // IME ���ؽ�Ʈ ����
        ImmReleaseContext(hwnd, hIMC);

        Debug.Log("���� �Է� ���� ������.");
    }
}
