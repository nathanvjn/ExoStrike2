using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerChamber : MonoBehaviour
{
    public float chamberTimer;
    public float chamberResetTime;
    private int chamberNumber;
    public int chamberPickupNumber;
    public ChamberType chamberType;

    public GameObject[] chamberModels;

    //charge chamber
    public bool usingCharge;
    private float chargeTime;
    public float maxChargeTime;
    public LineRenderer chargeParticle;

    //other components
    public Gun gun;

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

        if (usingCharge)
        {
            if (Input.GetButton("Fire1") && gun.currentBulletCount >= 1)
            {
                if (gun.barrel.barrelPosition != null)
                {
                    chargeTime += Time.deltaTime;
                    chargeParticle.enabled = true;
                    chargeParticle.positionCount = 2;

                    Vector3 startPoint = gun.barrel.barrelPosition.position;
                    Vector3 endPoint = gun.barrel.barrelPosition.position + transform.forward * (chargeTime * 4 / maxChargeTime);

                    chargeParticle.SetPosition(0, startPoint);
                    chargeParticle.SetPosition(1, endPoint);
                }

            }

            else
            {
                chargeTime = 0;
                chargeParticle.enabled = false;
            }

            if (Input.GetButtonUp("Fire1") || chargeTime > maxChargeTime)
            {
                gun.currentBulletCount -= 1;
                chargeTime = 0;
                chargeParticle.enabled = false;

                //schoot prefab
                if (gun.mag.currentBulletTypeNumber != 0)
                {
                    gun.barrel.cam.gameObject.GetComponent<ServerCommands>().ShootBulletOverNetwork();
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
    }

    public void ResetChamber()
    {
        //randomize type
        chamberNumber = Random.Range(0, 4);

        chamberType = (ChamberType)chamberNumber;

        switch (chamberType)
        {
            case ChamberType.REVOLVER: chamberResetTime = 1.2f; chamberModels[0].SetActive(true); chamberModels[1].SetActive(false); chamberModels[2].SetActive(false); chamberModels[3].SetActive(false); usingCharge = false; break;
            case ChamberType.AUTO: chamberResetTime = 0.1f; chamberModels[1].SetActive(true); chamberModels[0].SetActive(false); chamberModels[2].SetActive(false); chamberModels[3].SetActive(false); usingCharge = false; break;
            case ChamberType.SINGLE: chamberResetTime = 0.5f; chamberModels[2].SetActive(true); chamberModels[0].SetActive(false); chamberModels[1].SetActive(false); chamberModels[3].SetActive(false); usingCharge = false; break;
            case ChamberType.CHARGE: usingCharge = true; chamberModels[3].SetActive(true); chamberModels[0].SetActive(false); chamberModels[2].SetActive(false); chamberModels[1].SetActive(false); break;
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
