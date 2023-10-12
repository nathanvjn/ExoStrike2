using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrapnelBullet : MonoBehaviour
{
    public float bulletDamage;
    public float lifetime;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Health>().healthCounter -= bulletDamage;
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
