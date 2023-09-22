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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(usingNormalBarrel)
        {
            NormalBarrel(); 
        }

        else if(usingMultiBarrel)
        {
            MultiBarrel();
        }

        else if(usingBigBarrel)
        {
            BigBarrel();
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
