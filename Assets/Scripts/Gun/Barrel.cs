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

    [Header("bigBarrel")]
    public bool bigBarrelHit;

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

        else
        {
            bigBarrelHit = false;
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
        //look under this void (on trigger stay)
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            bigBarrelHit = true;
        }

        else
        {
            bigBarrelHit = false;
        }
    }
}
