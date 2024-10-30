using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectManagerTitleScene : MonoBehaviour
{
    // UI
    public GameObject keySettingPanel;
    public GameObject customPanel;
    public GameObject handTypePanel;
    public GameObject languagePanel;

    // Awake()
    private void Awake()
    {
        keySettingPanel.SetActive(false);
        customPanel.SetActive(false);
        handTypePanel.SetActive(false);
        languagePanel.SetActive(true);
    }

    // Ω√¿€
    public void StartKeySetting()
    {
        keySettingPanel.SetActive(true);
    }

}
