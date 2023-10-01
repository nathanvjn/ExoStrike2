using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour
{
    public bool usingRubber;
    public bool usingVirus;
    public bool usingEMP;

    //rubber
    public PhysicMaterial rubberMaterial;
    public Material greenMaterial;

    //material bullet
    public Material normalMaterial;

    
    void Update()
    {
        if (usingRubber)
        {
            Rubber();
        }

        else if (usingVirus)
        {
            Virus();
        }

        else if (usingEMP)
        {
            EMP();
        }


        if(usingRubber == false)
        {
            //remove bounce material and green color
            GetComponent<Gun>().bullet.GetComponent<SphereCollider>().material = null;
            GetComponent<Gun>().bullet.GetComponent<Renderer>().material = normalMaterial;
        }
    }

    void Rubber()
    {
        //add bounce material
        GetComponent<Gun>().bullet.GetComponent<SphereCollider>().material = rubberMaterial;
        GetComponent<Gun>().bullet.GetComponent<Renderer>().material = greenMaterial;
    }

    void Virus()
    {

    }

    void EMP()
    {

    }
    
}
