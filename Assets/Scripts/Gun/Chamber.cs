using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Chamber : MonoBehaviour
{
    //different chambers
    public bool usingRevolver;
    public bool usingSingleShot;
    public bool usingAutoChamber;

    public TextMeshProUGUI chamberText;

    [Header("auto chamber")]
    public float gunAutoCooldown;

    [Header("single chamber")]
    public float gunSingleCooldown;


    void Update()
    {
        if(usingRevolver)
        {
            Revolver();
            chamberText.text = ("Revolver");
        }

        else if(usingSingleShot)
        {
            SingleShot();
            chamberText.text = ("single");
        }

        else if(usingAutoChamber)
        {
            AutoChamber();
            chamberText.text = ("auto");
        }


    }

    void Revolver()
    {
        //no cooldown
        GetComponent<Gun>().schootingCooldownMaxTime = 0;
    }

    void SingleShot()
    {
        //singleChamber has a long cooldown
        GetComponent<Gun>().schootingCooldownMaxTime = gunSingleCooldown;
    }

    void AutoChamber()
    {
        //autoChamber has a short cooldown
        GetComponent<Gun>().schootingCooldownMaxTime = gunAutoCooldown;
    }
}
