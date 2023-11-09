using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporting : MonoBehaviour
{
  
    public Transform spawnPosition;
    public Transform respawnPosition;
    public Respawn respawn;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" && respawn.teleportReset == false)
        {
            collision.gameObject.transform.position = spawnPosition.position;
            respawn.respawnPosition = respawnPosition;
            respawn.teleportReset = true;
        }
    }
}
