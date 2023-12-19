using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    public void quit()
    {
        Application.Quit();
    }

    public void Replay()
    {
        SceneManager.LoadScene("LVL-1");
    }
}
