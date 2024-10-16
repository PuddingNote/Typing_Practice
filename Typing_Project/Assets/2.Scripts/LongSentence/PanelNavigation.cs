using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelNavigation : MonoBehaviour
{
    // UI
    public GameObject languagePanel;
    public GameObject selectTitlePanel;

    // Buttons
    public Button backToTitleButtonOnLanguagePanel;
    public Button backToLanguageButtonOnSelectTitlePanel;
    public Button backToTitleButtonOnSelectTitlePanel;

    // Awake()
    private void Awake()
    {
        backToTitleButtonOnLanguagePanel.onClick.AddListener(() => GoToTitleScene());
        backToLanguageButtonOnSelectTitlePanel.onClick.AddListener(() => GoBackToLanguagePanel());
        backToTitleButtonOnSelectTitlePanel.onClick.AddListener(() => GoToTitleScene());
    }

    // Title ¿Ãµø
    private void GoToTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }

    // Back
    private void GoBackToLanguagePanel()
    {
        selectTitlePanel.SetActive(false);
        languagePanel.SetActive(true);
    }
}
