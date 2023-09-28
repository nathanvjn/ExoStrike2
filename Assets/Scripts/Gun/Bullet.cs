using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float counter;
    public float explodeTime;
    public GameObject bulletParticle;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>());

        counter += Time.deltaTime;
        if(counter > explodeTime)
        {
            GameObject particlePrefab = Instantiate(bulletParticle, transform.position, Quaternion.identity);
            Destroy(particlePrefab, 1);
            Destroy(gameObject);
        }
    }
}
