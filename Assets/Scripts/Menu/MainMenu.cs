using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Starting games:")]
    [SerializeField] NetworkManager networkManager;
    [SerializeField] GameObject mainMenuCanvas;
    [Space(20)]
    [Header("Joining:")]
    [SerializeField] TMP_InputField input;
    [SerializeField] GameObject joinButton;
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
    }

    private void Update()
    {
        if (networkManager != null)
        {
            networkManager.networkAddress = input.text;
        }

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
    }

    public void Host()
    {
        mainMenuCanvas.SetActive(false);
        networkManager.StartHost();
    }

    public void Join()
    {
        mainMenuCanvas.SetActive(false);
        networkManager.StartClient();
    }

    public void OnJoinHover()
    {
        input.gameObject.GetComponent<Animator>().SetBool("Open", true);
        joinButton.GetComponent<Animator>().SetBool("Open", true);
    }

    public void OnJoinUnhover()
    {
        input.gameObject.GetComponent<Animator>().SetBool("Open", false);
        joinButton.GetComponent<Animator>().SetBool("Open", false);
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

    public void OnBackButtonSelect()
    {
        goBackButton.color = backButtonSelectedColor;
    }

    public void OnBackButtonClick()
    {
        goBackButton.color = backButtonClickColor;
    }

    public void OnBackButtonReset()
    {
        goBackButton.color = backButtonNormalColor;
    }
}
