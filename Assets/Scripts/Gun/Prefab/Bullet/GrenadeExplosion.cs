using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeExplosion : MonoBehaviour
{
    public float explosionDamage;
    public float lifetime;
    public AudioSource explosionSound;
    public GameObject targetParticle;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Health>().healthCounter -= explosionDamage;
        }

        else if (other.transform.gameObject.tag == "Target")
        {
            GameObject particleTarget = Instantiate(targetParticle, other.transform.position + new Vector3(0, 1.2f, 0), Quaternion.identity);
            Destroy(particleTarget, 0.7f);
            Destroy(other.transform.gameObject);
        }
    }

    private void Start()
    {
        explosionSound.Play();
    }

    private void Update()
    {
        Destroy(gameObject, lifetime);
    }
}
