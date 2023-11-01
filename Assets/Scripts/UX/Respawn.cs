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

    private Vector3 camRespawningPosition;
    private Transform respawnPosition;

    public GameObject respawnTextObject;
    public TextMeshProUGUI respawnText;
    private float respawnCount;
    public float maxRespawnTime;

    public Transform[] spawnLocations;


    private void Start()
    {
        respawnCount = maxRespawnTime;
        respawnPosition = spawnLocations[0];
    }

    void Update()
    {
        //if player position is lower than barrier
        if(transform.position.y > player.transform.position.y && playerCamera.isrespawning == false)
        {
            Respawning();
        }

        if(playerCamera.isrespawning)
        {
            cam.transform.position = camRespawningPosition;
            respawnCount -= Time.deltaTime;
            respawnText.text = respawnCount.ToString();

            if(respawnCount < 0)
            {

                //set canvas to not active
                respawnTextObject.SetActive(false);

                //set player position to last time standing
                player.transform.position = respawnPosition.position;

                //reset cam position in player
                cam.transform.position = new Vector3(0, 0.65f, 0);

                //set gun active
                gun.SetActive(true);

                if(respawnCount < -0.2f)
                {
                    respawnCount = maxRespawnTime;
                    playerCamera.isrespawning = false;
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.Y))
        {
            int randomNumber = Random.Range(0, spawnLocations.Length);
            for (int i = 0; i < spawnLocations.Length; i++)
            {
                if(i == randomNumber)
                {
                    player.transform.position = spawnLocations[i].position;
                    respawnPosition = spawnLocations[i];
                }
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
