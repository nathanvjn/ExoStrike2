using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyBullet : MonoBehaviour
{
    public float bulletDamage;
    public float lifetime;

    public Material noEmmisionMaterial;
    public Material emmisionMaterial;
    public GameObject bounceSoundObject;

    private bool activateEmmision;
    private float emmisionCounter;
    public float maxEmmisionCount;

    [Header("Particles")]
    public GameObject targetParticle;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Health>().healthCounter -= bulletDamage;
            Destroy(gameObject);
        }

        else if (collision.gameObject.tag == "Bullet")
        {
            //ignore collision with other bullets
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        }

        else if (collision.transform.gameObject.tag == "Target")
        {
            GameObject particleTarget = Instantiate(targetParticle, collision.transform.position + new Vector3(0, 1.2f, 0), Quaternion.identity);
            Destroy(particleTarget, 0.7f);
            Destroy(collision.transform.gameObject);
        }

        else
        {
            activateEmmision = true;
            GameObject prefabSound = Instantiate(bounceSoundObject, transform.position, Quaternion.identity);
        }
    }

    private void Update()
    {
        Destroy(gameObject, lifetime);

        //turn on emmision when bouncing on object
        if(activateEmmision)
        {
            GetComponent<Renderer>().material = emmisionMaterial;
            emmisionCounter += Time.deltaTime;
            if(emmisionCounter >= maxEmmisionCount)
            {
                emmisionCounter = 0;
                GetComponent<Renderer>().material = noEmmisionMaterial;
                activateEmmision = false;
            }
        }
    }
}
