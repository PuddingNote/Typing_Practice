using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectHandTypeTitleScene : MonoBehaviour
{
    // UI
    public GameObject handTypePanel;
    public GameObject customPanel;

    // Buttons
    public Button leftButton;
    public Button rightButton;
    public Button backToTitleButtonOnHandTypePanel;

    // ETC
    private ButtonManagerTitleScene buttonManager;
    private CustomKeySetting customKeySetting;

    // Awake()
    private void Awake()
    {
        buttonManager = GameObject.Find("ButtonManager").GetComponent<ButtonManagerTitleScene>();
        customKeySetting = GetComponent<CustomKeySetting>();

        leftButton.onClick.AddListener(() => SelectLanguage("Left"));
        rightButton.onClick.AddListener(() => SelectLanguage("Right"));
        backToTitleButtonOnHandTypePanel.onClick.AddListener(() => buttonManager.GoTitleScene());
    }

    // 왼손, 오른손에 따른 처리
    private void SelectLanguage(string handType)
    {
        PersistentDataTitleScene.selectedHandType = handType;
        handTypePanel.SetActive(false);
        customPanel.SetActive(true);
        customKeySetting.StartCustomKeySetting();
    }
}
