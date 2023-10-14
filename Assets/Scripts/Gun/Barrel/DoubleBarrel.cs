using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleBarrel : Barrel
{
    public Transform[] barrelPositions;
    private RaycastHit hit;
    public Transform[] particlePositions;

    public override void Shoot()
    {

        //raycast version
        Physics.Raycast(cam.position, cam.forward, out hit, 100);
        Debug.DrawLine(cam.position, hit.point, Color.red);
        if (usingShrapnel == false)
        {
            if (hit.transform != null)
            {
                for (int i = 0; i < barrelPositions.Length; i++)
                {
                    //particle line raycast
                    lineRenderer.positionCount = 2;
                    lineRenderer.SetPosition(0, barrelPositions[i].position);
                    lineRenderer.SetPosition(1, hit.point);
                }

                GameObject prefabRaycast = Instantiate(particleRaycast, hit.point, Quaternion.identity);
                Destroy(prefabRaycast, 1);
                if (hit.transform.gameObject.tag == "Player")
                {
                    for (int i = 0; i < barrelPositions.Length; i++)
                    {
                        //damage multiplies with each chamber
                        hit.transform.gameObject.GetComponent<Health>().healthCounter -= normalBarrelDamage;
                    }

                }
            }
        }

        else
        {
            for (int i = 0; i < barrelPositions.Length; i++)
            {
                float numBullets = Random.Range(7, 15);
                for (int i2 = 0; i2 < numBullets; i2++)
                {
                    //calculate a random rotation within the specified spread angle
                    Quaternion spreadRotation = Quaternion.Euler(Random.Range(-10, 10), Random.Range(-10, 10), 0f);

                    //create a raycast direction from the spread rotation
                    Vector3 rayDirection = spreadRotation * cam.forward;

                    //perform the raycast
                    RaycastHit doubleBarrelHit;
                    if (Physics.Raycast(barrelPositions[i].position, rayDirection, out doubleBarrelHit, 100))
                    {
                        //particle line raycast
                        lineRenderer.positionCount = 2;
                        lineRenderer.SetPosition(0, barrelPositions[i].position);
                        lineRenderer.SetPosition(1, doubleBarrelHit.point);

                        GameObject prefabRaycast = Instantiate(particleRaycast, doubleBarrelHit.point, Quaternion.identity);
                        Destroy(prefabRaycast, 1);
                        Debug.DrawLine(cam.position, doubleBarrelHit.point, Color.red, 0.1f);

                        //raycast bullet
                        if (doubleBarrelHit.transform.gameObject.tag == "Player")
                        {
                            doubleBarrelHit.transform.gameObject.GetComponent<Health>().healthCounter -= normalBarrelDamage;
                        }
                    }

                    else
                    {
                        // Handle a miss
                    }

                }
            }

        }
        for (int i = 0; i < barrelPositions.Length; i++)
        {
            //particle
            GameObject prefab = Instantiate(particleMuzzle, particlePositions[i].position, Quaternion.identity);
            prefab.transform.parent = particlePositions[i];
            prefab.transform.rotation = particlePositions[i].rotation;
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

            Destroy(prefab, 0.13f);
        }
    }

    public override void ShootBullet()
    {

        if (usingShrapnel)
        {
            for (int i = 0; i < barrelPositions.Length; i++)
            {
                float numBullets = Random.Range(7, 15);
                for (int i2 = 0; i2 < numBullets; i2++)
                {
                    // Instantiate the bullet with the correct initial rotation
                    GameObject prefabBullet = Instantiate(bulletType, barrelPositions[i].position, Quaternion.identity);

                    Quaternion spreadRotation = Quaternion.Euler(Random.Range(-4, 4), Random.Range(-4, 4), 0f);

                    //create a raycast direction from the spread rotation
                    Vector3 rayDirection = spreadRotation * cam.forward;

                    prefabBullet.transform.LookAt(prefabBullet.transform.position + rayDirection);
                    prefabBullet.GetComponent<Rigidbody>().AddForce(prefabBullet.transform.forward * bulletForce * Time.deltaTime);
                }
            }

        }

        else
        {
            for (int i = 0; i < barrelPositions.Length; i++)
            {
                GameObject bulletPrefab = Instantiate(bulletType, barrelPositions[i].position, Quaternion.identity);
                bulletPrefab.transform.localScale = new Vector3(bulletSize, bulletSize, bulletSize);
                bulletPrefab.GetComponent<Rigidbody>().AddForce(cam.forward * bulletForce * Time.deltaTime);
            }
        }

        for(int i = 0; i < barrelPositions.Length; i++)
        {
            //particle
            GameObject prefab = Instantiate(particleMuzzle, particlePositions[i].position, Quaternion.identity);
            prefab.transform.parent = particlePositions[i];
            prefab.transform.rotation = particlePositions[i].rotation;
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

            Destroy(prefab, 0.13f);
        }
    }

    
}
