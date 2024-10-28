using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgramManager : MonoBehaviour
{
    // Singleton instance
    public static ProgramManager instance = null;

    // Variables
    private const float TargetAspectRatio = 16f / 9f;       // ȭ�� ����
    private Vector2 _lastScreenSize;                        // ������ ȭ�� ũ�� ����
    private Coroutine _resizeCoroutine;                     // ��ٿ�� �ڷ�ƾ ����
    private const float DebounceTime = 0.1f;                // ��ٿ �ð�
    private const float MinWidth = 1080f;                   // �ּ� �ʺ�
    private const float MinHeight = 540f;                   // �ּ� ����
    private const float MaxWidth = 1440f;                   // �ִ� �ʺ�
    private const float MaxHeight = 810f;                   // �ִ� ����

    // Start()
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Screen.fullScreenMode = FullScreenMode.Windowed;
        SetMinimumResolution();
        _lastScreenSize = new Vector2(Screen.width, Screen.height);
    }

    // Update()
    private void Update()
    {
        Vector2 screenSize = new Vector2(Screen.width, Screen.height);

        if (screenSize == _lastScreenSize)
        {
            return;
        }

        if (_resizeCoroutine != null)
        {
            StopCoroutine(_resizeCoroutine);
        }

        _resizeCoroutine = StartCoroutine(DebounceResize(screenSize));
        _lastScreenSize = screenSize;
    }

    // ��ٿ (���� ���� �� �ּ� ������ ����)
    private IEnumerator DebounceResize(Vector2 newSize)
    {
        yield return new WaitForSeconds(DebounceTime);

        bool isWidthResized = !Mathf.Approximately(newSize.x, _lastScreenSize.x);

        if (isWidthResized)
        {
            int newHeight = Mathf.RoundToInt(newSize.x / TargetAspectRatio);

            if (newHeight < MinHeight)
            {
                newHeight = (int)MinHeight;
                newSize.x = Mathf.RoundToInt(newHeight * TargetAspectRatio);
            }
            else if (newHeight > MaxHeight)
            {
                newHeight = (int)MaxHeight;
                newSize.x = Mathf.RoundToInt(newHeight * TargetAspectRatio);
            }
            Screen.SetResolution((int)newSize.x, newHeight, false);
        }
        else
        {
            int newWidth = Mathf.RoundToInt(newSize.y * TargetAspectRatio);

            if (newWidth < MinWidth)
            {
                newWidth = (int)MinWidth;
                newSize.y = Mathf.RoundToInt(newWidth / TargetAspectRatio);
            }
            else if (newWidth > MaxWidth)
            {
                newWidth = (int)MaxWidth;
                newSize.y = Mathf.RoundToInt(newWidth / TargetAspectRatio);
            }
            Screen.SetResolution(newWidth, (int)newSize.y, false);
        }
    }

    // ���� ���� �� �ּ�/�ִ� �ػ󵵸� �����ϴ� �Լ�
    private void SetMinimumResolution()
    {
        int currentWidth = Screen.width;
        int currentHeight = Screen.height;

        // �ּ� �� �ִ� �ػ� �˻�
        if (currentWidth < MinWidth || currentHeight < MinHeight || currentWidth > MaxWidth || currentHeight > MaxHeight)
        {
            int targetWidth = Mathf.Clamp(currentWidth, (int)MinWidth, (int)MaxWidth);
            int targetHeight = Mathf.Clamp(currentHeight, Mathf.RoundToInt(targetWidth / TargetAspectRatio), (int)MaxHeight);

            if (targetHeight < MinHeight)
            {
                targetHeight = (int)MinHeight;
                targetWidth = Mathf.RoundToInt(targetHeight * TargetAspectRatio);
            }
            else if (targetHeight > MaxHeight)
            {
                targetHeight = (int)MaxHeight;
                targetWidth = Mathf.RoundToInt(targetHeight * TargetAspectRatio);
            }

            Screen.SetResolution(targetWidth, targetHeight, false);
        }
    }

}
