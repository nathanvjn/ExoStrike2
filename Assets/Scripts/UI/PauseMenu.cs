using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject menu;
    public PlayerCamera playerCamera;

    public void Resume()
    {
        menu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerCamera.mouseY = playerCamera.mouseYstartValue;
        playerCamera.mouseX = playerCamera.mouseXstartValue;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(menu.activeInHierarchy)
            {
                menu.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                playerCamera.mouseY = playerCamera.mouseYstartValue;
                playerCamera.mouseX = playerCamera.mouseXstartValue;
            }

            else
            {
                menu.SetActive(true);
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                playerCamera.mouseY = 0;
                playerCamera.mouseX = 0;
            }
        }
    }
}
