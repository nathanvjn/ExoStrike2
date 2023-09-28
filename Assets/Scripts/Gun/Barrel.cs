using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Barrel : MonoBehaviour
{
    public bool usingNormalBarrel;
    public bool usingMultiBarrel; //randomize from 2 to 5
    public bool usingBigBarrel;

    public TextMeshProUGUI barrelText;

    [Header("normalBarrel")]
    public int normalBarrelDamage;

    [Header("bigBarrel")]
    public int numBullets;
    public float spreadAngle;
    public float maxRange;
    public float bigBarrelDamage;

    [Header("multiBarrel")]
    private bool randomize;
    public int amountOfBarrels;




    void Update()
    {
        if(usingNormalBarrel)
        {
            NormalBarrel();
            barrelText.text = ("normal");
        }

        else if(usingBigBarrel)
        {
            BigBarrel();
            barrelText.text = ("big");
        }

        else if (usingMultiBarrel)
        {
            MultiBarrel();
            barrelText.text = ("multi");
        }

        if(usingMultiBarrel == false)
        {
            randomize = true;
        }
    }

    void NormalBarrel()
    {
    
    }

    void MultiBarrel()
    {
        if (randomize)
        {
            amountOfBarrels = Random.Range(2, 6);
            randomize = false;
        }
    }

    void BigBarrel()
    {
        
    }
}
