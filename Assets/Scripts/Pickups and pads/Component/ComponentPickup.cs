using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {

            //other.GetComponent<PlayerMovement>().gun.GetComponent<Gun>()
        }
    }
}
