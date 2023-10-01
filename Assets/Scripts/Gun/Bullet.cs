using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //time when explode
    private float counter;
    public float explodeTime;

    //explosion particle
    public GameObject bulletParticle;

    //visible range sphere explosion
    public GameObject explosionRangeObject;

    public GameObject player;

    
    void Update()
    {
        
        counter += Time.deltaTime;
        if(counter > explodeTime)
        {
            //spawn
            GameObject particlePrefab = Instantiate(bulletParticle, transform.position, Quaternion.identity);
            GameObject explosionRangePrefab = Instantiate(explosionRangeObject, transform.position, Quaternion.identity);

            //despawn
            Destroy(explosionRangePrefab, 1);
            Destroy(particlePrefab, 1);
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        //ignore collision with player shooting bullet
        Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            //ignore collision with other bullets
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }
}
