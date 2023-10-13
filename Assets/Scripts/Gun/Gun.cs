using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    public Barrel barrel;
    public Chamber chamber;
    public Mag mag;

    public float currentBulletCount;
    public TextMeshProUGUI ammoText;

    public TextMeshProUGUI magText;
    public TextMeshProUGUI chamberText;
    public TextMeshProUGUI barrelText;

    //reset
    public float resetTimer;
    public TextMeshProUGUI timerText;

    [Header("BarrelType")]
    public GameObject bigBarrel;
    public GameObject doubleBarrel;
    public GameObject normalBarrel;
    public GameObject gatlingBarrel;

    public float barrelType;
    private int i;
    private int randomIndex;


    private void Start()
    {
        ResetComponents();
    }

    void Update()
    {
        ammoText.text = currentBulletCount.ToString();
        magText.text = mag.bulletMagType.ToString();
        chamberText.text = chamber.chamberType.ToString();
        timerText.text = resetTimer.ToString();

        //limit ammo size
        currentBulletCount = Mathf.Clamp(currentBulletCount, 0, mag.magSize);

        if (Input.GetButton("Fire1") && chamber.chamberResetTime < chamber.chamberTimer && currentBulletCount >= 1)
        {
  
            currentBulletCount -= 1;

            //schoot prefab
            if (mag.currentBulletTypeNumber != 0)
            {
                barrel.ShootBullet();
                chamber.chamberTimer = 0;
                print("workingNotRay");
            }

            //schoot raycast
            else
            {
                barrel.Shoot();
                chamber.chamberTimer = 0;
                print("workingRay");
            }
        }

        resetTimer += Time.deltaTime;
        if(resetTimer > 20)
        {
            ResetComponents();
        }
    }

    public void ResetComponents()
    {
        chamber.ResetChamber();

        //randomize barrel
        GameObject[] barrels = { bigBarrel, doubleBarrel, normalBarrel, gatlingBarrel };
        for (i = 0; i < barrels.Length; i++)
        {
            if(barrels[i].activeInHierarchy == true)
            {
                barrelType = i;
            }
        }

        do
        {
            randomIndex = Random.Range(0, barrels.Length);
        } while (barrelType == i);
        

        barrel = barrels[randomIndex].GetComponent<Barrel>();
        mag.ResetMag();

        // Loop through the array and set the selected GameObject to active and others to inactive.
        for (int i = 0; i < barrels.Length; i++)
        {
            barrels[i].SetActive(i == randomIndex);
        }

        //reset timer
        resetTimer = 0;
    }
}
