using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum BulletType
{
    BULLET = 0, SHRAPNEL = 1, GRENADE = 2, BOUNCY = 3, EMP = 4
}

public class Mag : Effects
{
    public int magSize;
    public BulletType bulletMagType;

    //text for max ammo
    public TextMeshProUGUI maxMagText;
    public int currentBulletTypeNumber;
    private int magType;

    //get bullet count
    public Gun gun;

    [Header("Bullet Prefabs")]
    public GameObject shrapnelBullet;
    public GameObject grenadeBullet;
    public GameObject bounceBullet;
    public GameObject EMPBullet;

    [Header("Mag Models")]
    public GameObject[] magModels;

    [Header("Bullet MagSize")]
    public int minBulletSize;
    public int maxBulletSize;

    [Header("Shrapnel MagSize")]
    public int minShrapnelSize; 
    public int maxShrapnelSize; 

    [Header("Grenade MagSize")]
    public int minGrenadeSize; 
    public int maxGrenadeSize;

    [Header("Bounce/EMP MagSize")]
    public int minBounceAndEMPSize;
    public int maxBounceAndEMPSize;

    public void Start()
    {
        //edit gun bullet count
        gun.currentBulletCount = magSize;

    }

    public void ResetMag()
    {
        //randomize bullet type

        magType = Random.Range(0, 5);

        bulletMagType = (BulletType)magType;

        switch (bulletMagType)
        {
            case BulletType.BULLET:
                magSize = Random.Range(minBulletSize, maxBulletSize);
                currentBulletTypeNumber = 0;
                magModels[0].SetActive(true); magModels[1].SetActive(false); magModels[2].SetActive(false); magModels[3].SetActive(false);
                gun.barrel.usingShrapnel = false;
                break; //raycast
            case BulletType.SHRAPNEL:
                magSize = Random.Range(minShrapnelSize, maxShrapnelSize); gun.barrel.bulletType = shrapnelBullet;
                gun.barrel.usingShrapnel = true;
                currentBulletTypeNumber = 1;
                magModels[1].SetActive(true); magModels[2].SetActive(false); magModels[0].SetActive(false); magModels[3].SetActive(false);
                print("schrapnel");
                break;
            case BulletType.GRENADE:
                magSize = Random.Range(minGrenadeSize, maxGrenadeSize); gun.barrel.bulletType = grenadeBullet;
                currentBulletTypeNumber = 2;
                magModels[2].SetActive(true); magModels[1].SetActive(false); magModels[0].SetActive(false); magModels[3].SetActive(false);
                gun.barrel.usingShrapnel = false;
                print("grenade");
                break;
            case BulletType.BOUNCY:
                magSize = Random.Range(minBounceAndEMPSize, maxBounceAndEMPSize); gun.barrel.bulletType = bounceBullet;
                currentBulletTypeNumber = 3;
                magModels[3].SetActive(true); magModels[1].SetActive(false); magModels[0].SetActive(false); magModels[2].SetActive(false);
                gun.barrel.usingShrapnel = false;
                print("bounce");
                break;
            case BulletType.EMP:
                magSize = Random.Range(minBounceAndEMPSize, maxBounceAndEMPSize); gun.barrel.bulletType = EMPBullet;
                currentBulletTypeNumber = 4;
                magModels[3].SetActive(true); magModels[1].SetActive(false); magModels[0].SetActive(false); magModels[2].SetActive(false);
                gun.barrel.usingShrapnel = false;
                print("emp");
                break;


        }


        //edit gun bullet count
        gun.currentBulletCount = magSize;
        maxMagText.text = magSize.ToString();
    }

}
