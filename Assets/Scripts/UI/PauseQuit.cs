using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseQuit : MonoBehaviour
{
    public void ButtonQuitToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
