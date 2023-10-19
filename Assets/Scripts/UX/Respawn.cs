using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Respawn : MonoBehaviour
{
    public GameObject player;
    public GameObject cam;
    public GameObject gun;
    public PlayerCamera playerCamera;

    public bool isGrounded;
    private Vector3 lastPositionStanding;
    private Vector3 camRespawningPosition;

    public GameObject respawnTextObject;
    public TextMeshProUGUI respawnText;
    private float respawnCount;
    public float maxRespawnTime;


    private void Start()
    {
        respawnCount = maxRespawnTime;
        
    }

    void Update()
    {
        //if player position is lower than barrier
        if(transform.position.y > player.transform.position.y && playerCamera.isrespawning == false && isGrounded == false)
        {
            Respawning();
        }
        
        isGrounded = player.GetComponent<PlayerMovement>().isGrounded;

        //calculate last position when player was on ground
        if(isGrounded)
        {
            lastPositionStanding = player.transform.position;
        }

        if(playerCamera.isrespawning)
        {
            cam.transform.position = camRespawningPosition;
            respawnCount -= Time.deltaTime;
            respawnText.text = respawnCount.ToString();

            if(respawnCount < 0)
            {
                respawnCount = maxRespawnTime;
                playerCamera.isrespawning = false;

                //set canvas to not active
                respawnTextObject.SetActive(false);

                //set player position to last time standing
                player.transform.position = lastPositionStanding;

                //reset cam position in player
                cam.transform.position = new Vector3(0, 0.65f, 0);

                //set gun active
                gun.SetActive(true);
            }
        }
    }

    void Respawning()
    {
        //stop cam movement
        playerCamera.isrespawning = true;
        camRespawningPosition = cam.transform.position;
        respawnTextObject.SetActive(true);
        gun.SetActive(false);

    }
}
