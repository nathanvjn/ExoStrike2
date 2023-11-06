using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrapnelBullet : MonoBehaviour
{
    public float bulletDamage;
    public float lifetime;
    public GameObject particle;
    public GameObject targetParticle;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
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
            GameObject prefab = Instantiate(particle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        Destroy(gameObject, lifetime);
    }
}
