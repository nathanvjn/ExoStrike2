using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;

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

    private void Update()
    {
        if (networkManager != null)
        {
            networkManager.networkAddress = input.text;
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
            mainMenuCanvas.SetActive(true);
            generalTab.SetActive(false);
            videoTab.SetActive(false);
            audioTab.SetActive(false);
        }
    }
}
