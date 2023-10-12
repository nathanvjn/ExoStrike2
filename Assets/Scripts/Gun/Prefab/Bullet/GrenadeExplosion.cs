using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeExplosion : MonoBehaviour
{
    public float explosionDamage;
    public float lifetime;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Health>().healthCounter -= explosionDamage;
        }
    }

    private void Update()
    {
        Destroy(gameObject, lifetime);
    }
}
