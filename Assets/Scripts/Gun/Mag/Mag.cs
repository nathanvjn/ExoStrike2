using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType
{
    BULLET = 0, SHRAPNEL = 1, GRENADE = 2
}

public class Mag : Effects
{
    public int magSize;
    public BulletType bulletMagType;

    //check number
    public float currentBulletTypeNumber;

    public Gun gun;

    //bullet types
    public GameObject shrapnelBullet;
    public GameObject grenadeBullet;

    //mag models
    public GameObject[] magModels;

    public void Start()
    {
        //edit gun bullet count
        gun.currentBulletCount = magSize;

    }

    public void ResetMag()
    {
        //randomize bullet type
        bulletMagType = (BulletType)Random.Range(0, 3);

        switch (bulletMagType)
        {
            case BulletType.BULLET:
                magSize = Random.Range(12, 120);
                currentBulletTypeNumber = 0;
                magModels[0].SetActive(true); magModels[1].SetActive(false); magModels[2].SetActive(false);
                gun.barrel.usingShrapnel = false;
                break; //raycast
            case BulletType.SHRAPNEL:
                magSize = Random.Range(6, 24); gun.barrel.bulletType = shrapnelBullet;
                gun.barrel.usingShrapnel = true;
                currentBulletTypeNumber = 1;
                magModels[1].SetActive(true); magModels[2].SetActive(false); magModels[0].SetActive(false);
                print("schrapnel");
                break;
            case BulletType.GRENADE:
                magSize = Random.Range(3, 8); gun.barrel.bulletType = grenadeBullet;
                currentBulletTypeNumber = 2;
                magModels[2].SetActive(true); magModels[1].SetActive(false); magModels[0].SetActive(false);
                gun.barrel.usingShrapnel = false;
                print("grenade");
                break;
        }


        //edit gun bullet count
        gun.currentBulletCount = magSize;
    }

    private void Update()
    {
        
    }
}
