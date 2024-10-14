using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManagerLongSentence : MonoBehaviour
{
    public void GoTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
