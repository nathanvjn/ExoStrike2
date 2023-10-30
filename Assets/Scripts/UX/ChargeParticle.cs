using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeParticle : MonoBehaviour
{
    public Chamber chamber;
    public Gun gun;

    public GameObject chargeObject;
    public Material chargeMaterial;

    private Vector3 startPoint;

    public Color newEmissionColor;
    private Color beginningColor;

    private float intensitySize;
    public float intensitySizeIncreaseAmount;

    private float intensityEmmision;
    public float intensityEmmisionIncreaseAmount;

    // Start is called before the first frame update
    void Start()
    {
        beginningColor = chargeMaterial.color;
    }

    // Update is called once per frame
    void Update()
    {
        float chargeTime = chamber.chargeTime;
        float maxChargeTime = chamber.maxChargeTime;

        if(chamber.usingChargeParticle && chamber.usingCharge)
        {
            chargeObject.SetActive(true);

            startPoint = gun.barrel.barrelPosition.position;
            Vector3 endPoint = gun.barrel.barrelPosition.position + transform.forward * (chargeTime * 4 / maxChargeTime);
            chargeObject.transform.localScale = new Vector3(intensitySize, intensitySize, intensitySize);

            chargeObject.transform.position = endPoint;
            chargeMaterial.SetColor("_EmissionColor", newEmissionColor * intensityEmmision);
            chargeMaterial.EnableKeyword("_EMISSION");

            intensityEmmision += intensityEmmisionIncreaseAmount;
            intensitySize += intensitySizeIncreaseAmount;
        }

        else
        {
            chargeObject.SetActive(false);
            chargeObject.transform.position = startPoint;
            chargeMaterial.color = beginningColor;

            intensitySize = 0;
            intensityEmmision = 0;
        }
    }
}
