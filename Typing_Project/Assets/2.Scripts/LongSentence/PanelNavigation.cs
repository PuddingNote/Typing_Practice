using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelNavigation : MonoBehaviour
{
    public GameObject languagePanel;
    public GameObject selectTitlePanel;

    public Button backToTitleButtonOnLanguagePanel;
    public Button backToLanguageButtonOnSelectTitlePanel;
    public Button backToTitleButtonOnSelectTitlePanel;

    private void Awake()
    {
        backToTitleButtonOnLanguagePanel.onClick.AddListener(() => GoToTitleScene());
        backToLanguageButtonOnSelectTitlePanel.onClick.AddListener(() => GoBackToLanguagePanel());
        backToTitleButtonOnSelectTitlePanel.onClick.AddListener(() => GoToTitleScene());
    }

    private void GoToTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }

    private void GoBackToLanguagePanel()
    {
        selectTitlePanel.SetActive(false);
        languagePanel.SetActive(true);
    }
}
