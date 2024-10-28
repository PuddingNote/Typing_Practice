using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgramManager : MonoBehaviour
{
    // Singleton instance
    public static ProgramManager instance = null;

    // Variables
    private const float TargetAspectRatio = 16f / 9f;       // 화면 비율
    private Vector2 _lastScreenSize;                        // 마지막 화면 크기 저장
    private Coroutine _resizeCoroutine;                     // 디바운스용 코루틴 저장
    private const float DebounceTime = 0.1f;                // 디바운스 시간
    private const float MinWidth = 1080f;                   // 최소 너비
    private const float MinHeight = 540f;                   // 최소 높이
    private const float MaxWidth = 1440f;                   // 최대 너비
    private const float MaxHeight = 810f;                   // 최대 높이

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

    // 디바운스 (동시 변경 및 최소 사이즈 적용)
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

    // 게임 시작 시 최소/최대 해상도를 설정하는 함수
    private void SetMinimumResolution()
    {
        int currentWidth = Screen.width;
        int currentHeight = Screen.height;

        // 최소 및 최대 해상도 검사
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
