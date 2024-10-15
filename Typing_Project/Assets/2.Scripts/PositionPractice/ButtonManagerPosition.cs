using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManagerPosition : MonoBehaviour
{
    public void ExitButton()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
