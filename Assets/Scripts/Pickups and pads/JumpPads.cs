using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPads : MonoBehaviour
{
    public float jumpPadForce;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.GetComponent<Rigidbody>().AddForce(transform.up * jumpPadForce * Time.deltaTime);
        }
    }
}
