using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMPBullet : MonoBehaviour
{
    public float bulletDamage;
    public float lifetime;
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
    }

    private void Update()
    {
        Destroy(gameObject, lifetime);
    }
}
