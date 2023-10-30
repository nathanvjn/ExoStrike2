using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMPBullet : MonoBehaviour
{
    public float bulletDamage;
    public float lifetime;
    public GameObject particle;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Health>().healthCounter -= bulletDamage;
            collision.gameObject.GetComponent<PlayerMovement>().EMPhit = true;
        }

        if (collision.gameObject.tag == "Bullet")
        {
            //ignore collision with other bullets
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        }

        else
        {
            GameObject prefab = Instantiate(particle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        Destroy(gameObject, lifetime);
    }
}
