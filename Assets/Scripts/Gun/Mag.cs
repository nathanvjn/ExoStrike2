using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Mag : MonoBehaviour
{
    public bool usingNormalMag;
    public bool usingShrapnelMag; 
    public bool usingGrenadeMag;

    public TextMeshProUGUI magText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (usingNormalMag)
        {
            NormalMag();
            magText.text = ("normal");
        }

        else if (usingGrenadeMag)
        {
            GrenadeMag();
            magText.text = ("grenade");
        }
    }

    void NormalMag()
    {

    }

    void GrenadeMag()
    {

    }
}
