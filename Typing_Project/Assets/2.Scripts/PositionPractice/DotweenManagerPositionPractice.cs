using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DotweenManagerPositionPractice : MonoBehaviour
{
    // UI
    public GameObject mainPanel;        
    public GameObject keyboardPanel;
    public Button HideButton;

    // ETC
    private bool isClick;

    // Awake()
    private void Awake()
    {
        isClick = false;

        HideButton.onClick.AddListener(OnHideButtonClick);
    }

    // 
    private void OnHideButtonClick()
    {
        isClick = !isClick;

        foreach (Transform child in mainPanel.transform)
        {
            if (isClick)
            {
                child.DOLocalMoveY(child.localPosition.y - 300f, 0.5f);
            }
            else
            {
                child.DOLocalMoveY(child.localPosition.y + 300f, 0.5f);
            }
        }

        if (isClick)
        {
            keyboardPanel.transform.DOLocalMoveY(keyboardPanel.transform.localPosition.y - 635f, 0.5f);
        }
        else
        {
            keyboardPanel.transform.DOLocalMoveY(keyboardPanel.transform.localPosition.y + 635f, 0.5f);
        }
    }
}
