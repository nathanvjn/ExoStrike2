using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.tag == "Player")
        {

            float magType = other.GetComponent<PlayerMovement>().gun.GetComponent<Gun>().mag.currentBulletTypeNumber;
            int bulletCount = other.GetComponent<PlayerMovement>().gun.GetComponent<Gun>().currentBulletCount;

            //ammo dat de speler krijgt van de max ammo van de mag(zo het ligt aan welke mag de speler heeft)

            if (magType == 0) //bullet
            {
                bulletCount += other.GetComponent<PlayerMovement>().gun.GetComponent<Gun>().mag.maxBulletSize / 2; //50% of max size
            }

            else if (magType == 1) //shrapnel
            {
                bulletCount += other.GetComponent<PlayerMovement>().gun.GetComponent<Gun>().mag.maxShrapnelSize / 2; //50% of max size
            }

            else if (magType == 2) //grenade
            {
                bulletCount += other.GetComponent<PlayerMovement>().gun.GetComponent<Gun>().mag.maxGrenadeSize / 2; //50% of max size
            }

            other.GetComponent<PlayerMovement>().gun.GetComponent<Gun>().currentBulletCount += bulletCount;

            //reset ammo spawn time
            transform.parent.gameObject.GetComponent<AmmoPlate>().respawnTime = 0;
        }
    }
}
