using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
   
    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.tag == "Player")
        {

            int bulletCount = (other.GetComponent<PlayerMovement>().gun.GetComponent<Gun>().mag.magSize / 2); //50% of mag size

            other.GetComponent<PlayerMovement>().gun.GetComponent<Gun>().currentBulletCount += bulletCount;

            //reset ammo spawn time
            transform.parent.gameObject.GetComponent<AmmoPlate>().respawnTime = 0;
        }
    }
}
