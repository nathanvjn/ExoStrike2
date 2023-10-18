using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public SoundManager soundManager;

    //rotation
    public float rotationSpeed;

    private void Update()
    {
        transform.Rotate(transform.up * rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.tag == "Player")
        {

            int bulletCount = (other.GetComponent<PlayerMovement>().gun.GetComponent<Gun>().mag.magSize / 2); //50% of mag size

            other.GetComponent<PlayerMovement>().gun.GetComponent<Gun>().currentBulletCount += bulletCount;

            //reset ammo spawn time
            transform.parent.gameObject.GetComponent<AmmoPlate>().respawnTime = 0;

            //sound
            soundManager.PickupSound();
        }
    }
}
