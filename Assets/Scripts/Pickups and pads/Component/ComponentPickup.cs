using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComponentPickup : MonoBehaviour
{
    public Gun gun;
    public SoundManager soundManager;
    private int randomComponent;
    public TextMeshProUGUI componentText;

    //rotation
    public float rotationSpeed;


    public ChamberType chamberType;
    public MagType magType;
    public GameObject[] barrelObjects;
    private int randomBarrelIndex;

    public enum ChamberType
    {
        REVOLVER = 0, AUTO = 1, SINGLE = 2, CHARGE = 3
    }

    public enum MagType
    {
        BULLET = 0, SHRAPNEL = 1, GRENADE = 2, BOUNCY = 3, EMP = 4
    }

    void OnEnable()
    {
        randomComponent = Random.Range(0, 3);
        if (randomComponent == 0)
        {
            //mag
            gun.mag.magPickupType = Random.Range(0, 5);

            magType = (MagType)gun.mag.magPickupType;
            switch (magType)
            {
                case MagType.BULLET:
                    break;
                case MagType.SHRAPNEL:
                    break;
                case MagType.GRENADE:
                    break;
                case MagType.BOUNCY:
                    break;
                case MagType.EMP:
                    break;
            }

            componentText.text = magType.ToString();
        }

        else if (randomComponent == 1)
        {
            //chamber
            gun.chamber.chamberPickupNumber = Random.Range(0, 4);

            chamberType = (ChamberType)gun.chamber.chamberPickupNumber;
            switch (chamberType)
            {
                case ChamberType.REVOLVER: break;
                case ChamberType.AUTO: break;
                case ChamberType.SINGLE: break;
                case ChamberType.CHARGE: break;
            }

            componentText.text = chamberType.ToString();

        }

        else if (randomComponent == 2)
        {
            //barrel
            randomBarrelIndex = Random.Range(0, barrelObjects.Length);
            // Loop through the array and set the selected GameObject to active and others to inactive.
            for (int i = 0; i < barrelObjects.Length; i++)
            {
                if (i == randomBarrelIndex)
                {
                    componentText.text = barrelObjects[i].name.ToString();
                }
            }

        }
    }

    private void Update()
    {
        transform.Rotate(transform.up * rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            if(randomComponent == 0)
            {
                //mag
                gun.mag.UpdateMag();
            }
            else if(randomComponent == 1)
            {
                //chamber
                gun.chamber.UpdateChamber();
            }
            else if(randomComponent == 2)
            {
                //barrel
                GameObject[] barrels = { gun.bigBarrel, gun.doubleBarrel, gun.normalBarrel, gun.gatlingBarrel };
                gun.barrel = barrels[randomBarrelIndex].GetComponent<Barrel>();
                for (int i = 0; i < barrels.Length; i++)
                {
                    barrels[i].SetActive(i == randomBarrelIndex);
                    if (i == randomBarrelIndex)
                    {
                        gun.barrelText.text = barrels[i].name.ToString();
                    }
                }

                gun.barrel.bulletType = gun.mag.currentBullet;
            }
            

            //sound
            soundManager.PickupSound();

            transform.parent.gameObject.GetComponent<ComponentPlate>().respawnTime = 0;
            gameObject.SetActive(false);
        }
    }
}
