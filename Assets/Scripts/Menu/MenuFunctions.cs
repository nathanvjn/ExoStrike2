using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MenuFunctions : MonoBehaviour
{
    [Header("Starting games:")]
    [SerializeField] GameObject mainMenuCanvas;
    bool playing;
    [Space(20)]
    [Header("Settings:")]
    [SerializeField] GameObject generalTab;
    [SerializeField] GameObject videoTab;
    [SerializeField] GameObject audioTab;
    [SerializeField] Image goBackButton;
    [SerializeField] Color backButtonNormalColor;
    [SerializeField] Color backButtonSelectedColor;
    [SerializeField] Color backButtonClickColor;
    [SerializeField] Scrollbar scroller;
    [SerializeField] GameObject content;
    Vector3 startPosContent;

    private void Start()
    {
        startPosContent = content.transform.position;
        playing = false;
        Time.timeScale = 0f;
    }

    private void Update()
    {
        if (content != null)
        {
            scroller.value += -Input.mouseScrollDelta.y / 10;
            if (scroller.value < 0)
            {
                scroller.value = 0;
            }
            else if (scroller.value > 1)
            {
                scroller.value = 1;
            }
            content.transform.position = new Vector3(startPosContent.x, startPosContent.y - scroller.value * (content.GetComponent<RectTransform>().rect.height - 800), startPosContent.z);
        }

        if (!playing)
        {
            Debug.Log("In main menu");
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void Play()
    {
        playing = true;
        Time.timeScale = 1f;
        mainMenuCanvas.SetActive(false);
    }

    public void SwitchSettingsTab(string type)
    {
        if (type == "general")
        {
            mainMenuCanvas.SetActive(false);
            generalTab.SetActive(true);
            videoTab.SetActive(false);
            audioTab.SetActive(false);
        }
        else if (type == "video")
        {
            generalTab.SetActive(false);
            videoTab.SetActive(true);
            audioTab.SetActive(false);
        }
        else if (type == "audio")
        {
            generalTab.SetActive(false);
            videoTab.SetActive(false);
            audioTab.SetActive(true);
        }
        else if (type == "exit")
        {
            goBackButton.color = backButtonNormalColor;
            mainMenuCanvas.SetActive(true);
            generalTab.SetActive(false);
            videoTab.SetActive(false);
            audioTab.SetActive(false);
        }
    }
}
