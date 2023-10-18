using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoPickup : MonoBehaviour
{
    public int bulletCount;
    public SoundManager soundManager;
    public TextMeshProUGUI currentAmmo;
    public TextMeshProUGUI maxAmmo;
    public TextMeshProUGUI ammoPickup;

    public Gun gun;

    //rotation
    public float rotationSpeed;

    private void Update()
    {
        transform.Rotate(transform.up * rotationSpeed * Time.deltaTime);

        currentAmmo.text = gun.currentBulletCount.ToString();
        maxAmmo.text = gun.mag.magSize.ToString();

        bulletCount = (gun.mag.magSize / 2); //50% of mag size
        ammoPickup.text = bulletCount.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.tag == "Player")
        {

            gun.currentBulletCount += bulletCount;

            //reset ammo spawn time
            transform.parent.gameObject.GetComponent<AmmoPlate>().respawnTime = 0;

            //sound
            soundManager.PickupSound();
        }
    }
}
