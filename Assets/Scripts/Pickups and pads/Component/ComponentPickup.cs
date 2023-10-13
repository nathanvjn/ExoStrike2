using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentPickup : MonoBehaviour
{
    public enum BulletType
    {
        BULLET = 0, SHRAPNEL = 1, GRENADE = 2
    }

    public enum ChamberType
    {
        REVOLVER = 0, AUTO = 1, SINGLE = 2
    }

    public enum BarrelType
    {
        GATLING = 0, NORMAL = 1, BIG = 2
    }

    public BulletType bulletMagType;
    public ChamberType chamberType;
    public BarrelType barrelType;

    public GameObject[] chamberModels;
    public GameObject[] magModels;
    public GameObject[] barrelModels;

    private void OnEnable()
    {
        bulletMagType = (BulletType)Random.Range(0, 3);

        switch (bulletMagType)
        {
            case BulletType.BULLET:
                magModels[0].SetActive(true); magModels[1].SetActive(false); magModels[2].SetActive(false);
                break; //raycast
            case BulletType.SHRAPNEL:
                magModels[1].SetActive(true); magModels[2].SetActive(false); magModels[0].SetActive(false);
                break;
            case BulletType.GRENADE:
                magModels[2].SetActive(true); magModels[1].SetActive(false); magModels[0].SetActive(false);
                break;
        }


        chamberType = (ChamberType)Random.Range(0, 3);

        switch (chamberType)
        {
            case ChamberType.REVOLVER: chamberModels[0].SetActive(true); chamberModels[1].SetActive(false); chamberModels[2].SetActive(false); break;
            case ChamberType.AUTO: chamberModels[1].SetActive(true); chamberModels[0].SetActive(false); chamberModels[2].SetActive(false); break;
            case ChamberType.SINGLE: chamberModels[2].SetActive(true); chamberModels[0].SetActive(false); chamberModels[1].SetActive(false); break;
        }

        barrelType = (BarrelType)Random.Range(0, 3);

        switch (barrelType)
        {
            case BarrelType.GATLING: chamberModels[0].SetActive(true); chamberModels[1].SetActive(false); chamberModels[2].SetActive(false); break;
            case BarrelType.NORMAL: chamberModels[1].SetActive(true); chamberModels[0].SetActive(false); chamberModels[2].SetActive(false); break;
            case BarrelType.BIG: chamberModels[2].SetActive(true); chamberModels[0].SetActive(false); chamberModels[1].SetActive(false); break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {

            //other.GetComponent<PlayerMovement>().gun.GetComponent<Gun>().
        }
    }
}
