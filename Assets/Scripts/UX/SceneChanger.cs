using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public bool inLuukScene;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            if(inLuukScene)
            {
                SceneManager.LoadScene(1);
            }

            else
            {
                SceneManager.LoadScene(2);
            }
        }
    }
}
