using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    public float bulletForce;
    public float normalBarrelDamage;

    public Transform cam;
    public Transform barrelPosition;
    public GameObject bulletType;
    public bool usingShrapnel;
    private RaycastHit rayMagHit;

    [Header("Particles")]
    public GameObject particleRaycast;
    public GameObject particleMuzzle;

    //overloading (give bool when raycasting)
    //als er geen andere barrel scripts zijn die overriden is het de default barrel
    public virtual void Shoot()
    {
        print("schootingRay");
        Physics.Raycast(cam.position, cam.forward, out rayMagHit, 100);
        Debug.DrawLine(cam.position, rayMagHit.point, Color.red);
        if(usingShrapnel == false)
        {
            if (rayMagHit.transform != null)
            {
                GameObject prefabRaycast = Instantiate(particleRaycast, rayMagHit.point, Quaternion.identity);
                Destroy(prefabRaycast, 1);

                if (rayMagHit.transform.gameObject.tag == "Player")
                {
                    rayMagHit.transform.gameObject.GetComponent<Health>().healthCounter -= normalBarrelDamage;
                }
            }
        }
        
        else
        {
            float numBullets = Random.Range(7, 15);
            for (int i = 0; i < numBullets; i++)
            {
                //calculate a random rotation within the specified spread angle
                Quaternion spreadRotation = Quaternion.Euler(Random.Range(-4, 4), Random.Range(-4, 4), 0f);

                //create a raycast direction from the spread rotation
                Vector3 rayDirection = spreadRotation * cam.forward;

                //perform the raycast
                RaycastHit bigBarrelHit;
                if (Physics.Raycast(barrelPosition.position, rayDirection, out bigBarrelHit, 100))
                {
                    GameObject prefabRaycast = Instantiate(particleRaycast, bigBarrelHit.point, Quaternion.identity);
                    Destroy(prefabRaycast, 1);
                    Debug.DrawLine(cam.position, bigBarrelHit.point, Color.red, 0.1f);

                    //raycast bullet
                    if (bigBarrelHit.transform.gameObject.tag == "Player")
                    {
                        bigBarrelHit.transform.gameObject.GetComponent<Health>().healthCounter -= normalBarrelDamage;
                    }
                }

                else
                {
                    // Handle a miss
                }

            }
        }

        //particle
        GameObject prefab = Instantiate(particleMuzzle, barrelPosition.position, Quaternion.identity);
        prefab.transform.parent = barrelPosition;
        prefab.transform.rotation = barrelPosition.rotation;
        StartCoroutine(ScaleParticlesOverTime());

        IEnumerator ScaleParticlesOverTime()
        {
            float elapsedTime = 0f;
            float scalingDuration = 0.1f; //adjust the duration as needed

            while (elapsedTime < scalingDuration)
            {
                float scale = Mathf.Lerp(0f, 0.1f, elapsedTime / scalingDuration);
                prefab.transform.localScale = new Vector3(scale, scale, scale); //set the particle size

                elapsedTime += Time.deltaTime;
                yield return null;
            }


        }

        Destroy(prefab, 0.11f);


    }

    public virtual void ShootBullet()
    {
        if(usingShrapnel == false)
        {
            print("schootingBullet");
            GameObject bulletPrefab = Instantiate(bulletType, barrelPosition.position, Quaternion.identity);
            bulletPrefab.GetComponent<Rigidbody>().AddForce(cam.forward * bulletForce * Time.deltaTime);
        }

        else
        {
            float numBullets = Random.Range(7, 15);
            for (int i = 0; i < numBullets; i++)
            {

                // Instantiate the bullet with the correct initial rotation
                GameObject prefabBullet = Instantiate(bulletType, barrelPosition.position, Quaternion.identity);

                Quaternion spreadRotation = Quaternion.Euler(Random.Range(-20, 20), Random.Range(-20, 20), 0f);

                //create a raycast direction from the spread rotation
                Vector3 rayDirection = spreadRotation * cam.forward;

                prefabBullet.transform.LookAt(prefabBullet.transform.position + rayDirection);
                prefabBullet.GetComponent<Rigidbody>().AddForce(prefabBullet.transform.forward * bulletForce * Time.deltaTime);
            }
        }

        
    }
}
