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




    void Update()
    {
        if(usingNormalBarrel)
        {
            NormalBarrel();
            barrelText.text = ("normal");
        }

        else if(usingMultiBarrel)
        {
            MultiBarrel();
            barrelText.text = ("multi");
        }

        else if(usingBigBarrel)
        {
            BigBarrel();
            barrelText.text = ("big");
        }
    }

    void NormalBarrel()
    {

    }

    void MultiBarrel()
    {

    }

    void BigBarrel()
    {
        
    }
}
