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
