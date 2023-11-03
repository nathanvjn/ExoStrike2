using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chamber : MonoBehaviour
{
    public float chamberTimer;
    public float chamberResetTime;
    public bool usingRevolver;
    private int chamberNumber;
    public int chamberPickupNumber;
    public ChamberType chamberType;

    public GameObject[] chamberModels;

    //charge chamber
    public bool usingChargeParticle;
    public bool usingCharge;
    public float chargeTime;
    public float maxChargeTime;

    //charge timout time
    private float chargeTimeoutTime;
    public float chargeTimeoutMax;
    private bool chargeTimeout;
    

    //other components
    public Gun gun;
    public SoundManager soundManager;

    private void Start()
    {
        
    }

    public enum ChamberType
    {
        REVOLVER = 0, AUTO = 1, SINGLE = 2, CHARGE = 3
    }

    void Update()
    {
        chamberTimer += Time.deltaTime;


        //charge component code
        if(usingCharge && chargeTimeout == false)
        {
            if (Input.GetButton("Fire1"))
            {
                if(gun.currentBulletCount >= 1)
                {
                    if (gun.barrel.barrelPosition != null)
                    {
                        usingChargeParticle = true;
                        chargeTime += Time.deltaTime;

                    }
                }

                else
                {
                    soundManager.NoAmmoSound();
                }
                
            }

            else
            {
                usingChargeParticle = false;
                chargeTime = 0;
            }

            if (Input.GetButtonUp("Fire1") && gun.currentBulletCount >= 1 || chargeTime > maxChargeTime && gun.currentBulletCount >= 1)
            {
                gun.currentBulletCount -= 1;
                chargeTime = 0;
                usingChargeParticle = false;
                chargeTimeout = true;

                //schoot prefab
                if (gun.mag.currentBulletTypeNumber != 0)
                {
                    gun.barrel.ShootBullet();
                    chamberTimer = 0;
                    print("workingNotRay");
                }

                //schoot raycast
                else
                {
                    gun.barrel.Shoot();
                    chamberTimer = 0;
                    print("workingRay");
                }
            }
        }

        if(chargeTimeout)
        {
            chargeTimeoutTime += Time.deltaTime;
            if(chargeTimeoutTime > chargeTimeoutMax)
            {
                chargeTimeout = false;
                chargeTimeoutTime = 0;
            }
        }
    }

    public void ResetChamber()
    {
        //randomize type
        chamberNumber = Random.Range(0, 4);

        chamberType = (ChamberType)chamberNumber;

        switch (chamberType)
        {
            case ChamberType.REVOLVER: chamberResetTime = 1.2f; usingRevolver = true;  chamberModels[0].SetActive(true); chamberModels[1].SetActive(false); chamberModels[2].SetActive(false); chamberModels[3].SetActive(false); usingCharge = false; break;
            case ChamberType.AUTO: chamberResetTime = 0.1f; usingRevolver = false; chamberModels[1].SetActive(true); chamberModels[0].SetActive(false); chamberModels[2].SetActive(false); chamberModels[3].SetActive(false); usingCharge = false; break;
            case ChamberType.SINGLE: chamberResetTime = 0.5f; usingRevolver = false; chamberModels[2].SetActive(true); chamberModels[0].SetActive(false); chamberModels[1].SetActive(false); chamberModels[3].SetActive(false); usingCharge = false; break;
            case ChamberType.CHARGE: usingCharge = true; usingRevolver = false; chamberModels[3].SetActive(true); chamberModels[0].SetActive(false); chamberModels[2].SetActive(false); chamberModels[1].SetActive(false); break;
        }
    }

    public void UpdateChamber()
    {
        chamberType = (ChamberType)chamberPickupNumber;
        switch (chamberType)
        {
            case ChamberType.REVOLVER: chamberResetTime = 1.2f; chamberModels[0].SetActive(true); chamberModels[1].SetActive(false); chamberModels[2].SetActive(false); chamberModels[3].SetActive(false); usingCharge = false; break;
            case ChamberType.AUTO: chamberResetTime = 0.1f; chamberModels[1].SetActive(true); chamberModels[0].SetActive(false); chamberModels[2].SetActive(false); chamberModels[3].SetActive(false); usingCharge = false; break;
            case ChamberType.SINGLE: chamberResetTime = 0.5f; chamberModels[2].SetActive(true); chamberModels[0].SetActive(false); chamberModels[1].SetActive(false); chamberModels[3].SetActive(false); usingCharge = false; break;
            case ChamberType.CHARGE: usingCharge = true; chamberModels[3].SetActive(true); chamberModels[0].SetActive(false); chamberModels[2].SetActive(false); chamberModels[1].SetActive(false); break;
        }
    }
}
