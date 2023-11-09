using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void ButtonStart()
    {
        SceneManager.LoadScene(1);
    }
}
