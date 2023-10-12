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

    [Header("BulletMagSize")]
    public int minBulletSize;
    public int maxBulletSize;

    [Header("ShrapnelMagSize")]
    public int minShrapnelSize; 
    public int maxShrapnelSize; 

    [Header("GrenadeMagSize")]
    public int minGrenadeSize; 
    public int maxGrenadeSize; 

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
                magSize = Random.Range(minBulletSize, maxBulletSize);
                currentBulletTypeNumber = 0;
                magModels[0].SetActive(true); magModels[1].SetActive(false); magModels[2].SetActive(false);
                gun.barrel.usingShrapnel = false;
                break; //raycast
            case BulletType.SHRAPNEL:
                magSize = Random.Range(minShrapnelSize, maxShrapnelSize); gun.barrel.bulletType = shrapnelBullet;
                gun.barrel.usingShrapnel = true;
                currentBulletTypeNumber = 1;
                magModels[1].SetActive(true); magModels[2].SetActive(false); magModels[0].SetActive(false);
                print("schrapnel");
                break;
            case BulletType.GRENADE:
                magSize = Random.Range(minGrenadeSize, maxGrenadeSize); gun.barrel.bulletType = grenadeBullet;
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
