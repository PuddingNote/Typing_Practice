using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManagerPositionPractice : MonoBehaviour
{
    // GoTitleScene()
    public void GoTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
        PersistentDataPositionPractice.selectedType = "";
    }

    // RestartScene()
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
