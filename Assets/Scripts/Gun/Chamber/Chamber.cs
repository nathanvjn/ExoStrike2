using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chamber : MonoBehaviour
{
    public float chamberTimer;
    public float chamberResetTime;

    public ChamberType chamberType;

    public GameObject[] chamberModels;

    public enum ChamberType
    {
        REVOLVER = 0, AUTO = 1, SINGLE = 2
    }

    void Update()
    {
        chamberTimer += Time.deltaTime;

    }

    public void ResetChamber()
    {
        //randomize type
        chamberType = (ChamberType)Random.Range(0, 3);

        switch (chamberType)
        {
            case ChamberType.REVOLVER: chamberResetTime = 2f; chamberModels[0].SetActive(true); chamberModels[1].SetActive(false); chamberModels[2].SetActive(false); break;
            case ChamberType.AUTO: chamberResetTime = 0.1f; chamberModels[1].SetActive(true); chamberModels[0].SetActive(false); chamberModels[2].SetActive(false); break;
            case ChamberType.SINGLE: chamberResetTime = 1f; chamberModels[2].SetActive(true); chamberModels[0].SetActive(false); chamberModels[1].SetActive(false); break;
        }
    }
}
