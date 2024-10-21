using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionController : MonoBehaviour
{
    // Variables
    private const float TargetAspectRatio = 16f / 9f;       // 
    private Vector2 _lastScreenSize;                        // 
    private Coroutine _resizeCoroutine;                     // 
    private const float DebounceTime = 0.1f;                // 
    private const float MinWidth = 1080f;                   // 
    private const float MinHeight = 540f;                   // 

    // Start()
    private void Start()
    {
        _lastScreenSize = new Vector2(Screen.width, Screen.height);
        DontDestroyOnLoad(gameObject);
    }

    // Update()
    private void Update()
    {
        Vector2 screenSize = new Vector2(Screen.width,
        Screen.height);

        if (screenSize == _lastScreenSize) return;
        if (_resizeCoroutine != null)
            StopCoroutine(_resizeCoroutine);

        _resizeCoroutine =
        StartCoroutine(DebounceResize(screenSize));
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
            Screen.SetResolution(newWidth, (int)newSize.y, false);
        }
    }

}
