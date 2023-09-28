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

    private void Update()
    {
        networkManager.networkAddress = input.text;
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
}
