using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager2 : MonoBehaviour
{
    public void GoTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
