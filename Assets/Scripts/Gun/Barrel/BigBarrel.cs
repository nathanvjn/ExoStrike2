using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBarrel : Barrel
{
    public float spreadShrapnelAngle; //maximum spread rotation
    public float maxRaycastRange;
    public float bigBarrelDamage;
    public float bigBarrelBulletScale;

    //particle
    public GameObject particle;



    private void Start()
    {
        //elke barrel type wordt automatisch bij spawning in de gun script gegooit, het gun script kan door inheritance van de barrel types bij het barrel script om bullets te spawnen
    }

    public override void ShootBullet()
    {
        //bullet bigBarrel


        print("bigBarrel");
        if(usingShrapnel)
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

        else
        {
            // Calculate a random rotation within the specified spread angle
            Quaternion spreadRotation = Quaternion.Euler(Random.Range(-spreadShrapnelAngle, spreadShrapnelAngle), Random.Range(-spreadShrapnelAngle, spreadShrapnelAngle), 0f);

            // Combine the spreadRotation with the barrelPosition.forward
            Quaternion combinedRotation = Quaternion.LookRotation(spreadRotation * cam.forward);

            // Instantiate the bullet with the correct initial rotation
            GameObject prefabBullet = Instantiate(bulletType, barrelPosition.position, combinedRotation);
            prefabBullet.transform.localScale = new Vector3(bigBarrelBulletScale, bigBarrelBulletScale, bigBarrelBulletScale);

            // Apply force to the bullet
            prefabBullet.GetComponent<Rigidbody>().AddForce(prefabBullet.transform.forward * bulletForce * Time.deltaTime);
        }

        //particle
        GameObject particlePrefab = Instantiate(particle, barrelPosition.position, Quaternion.identity);
        particlePrefab.transform.rotation = barrelPosition.rotation;
        Destroy(particlePrefab, 1);
    }

    public override void Shoot()
    {
        base.Shoot();

        //raycast bigBarrel

        print("bigBarrel");
        if(usingShrapnel)
        {
            float numBullets = Random.Range(7, 15);
            for (int i = 0; i < numBullets; i++)
            {
                //calculate a random rotation within the specified spread angle
                Quaternion spreadRotation = Quaternion.Euler(Random.Range(-spreadShrapnelAngle, spreadShrapnelAngle), Random.Range(-spreadShrapnelAngle, spreadShrapnelAngle), 0f);

                //create a raycast direction from the spread rotation
                Vector3 rayDirection = spreadRotation * cam.forward;

                //perform the raycast
                RaycastHit bigBarrelHit;
                if (Physics.Raycast(barrelPosition.position, rayDirection, out bigBarrelHit, maxRaycastRange))
                {
                    GameObject prefabRaycast = Instantiate(particleRaycast, bigBarrelHit.point, Quaternion.identity);
                    Destroy(prefabRaycast, 1);
                    Debug.DrawLine(cam.position, bigBarrelHit.point, Color.red, 0.1f);

                    //raycast bullet
                    if (bigBarrelHit.transform.gameObject.tag == "Player")
                    {
                        bigBarrelHit.transform.gameObject.GetComponent<Health>().healthCounter -= bigBarrelDamage;
                    }
                }

                else
                {
                    // Handle a miss
                }

            }
        }
        
        else
        {
            //calculate a random rotation within the specified spread angle
            Quaternion spreadRotation = Quaternion.Euler(Random.Range(-spreadShrapnelAngle, spreadShrapnelAngle), Random.Range(-spreadShrapnelAngle, spreadShrapnelAngle), 0f);

            //create a raycast direction from the spread rotation
            Vector3 rayDirection = spreadRotation * cam.forward;

            //perform the raycast
            RaycastHit bigBarrelHit;
            if (Physics.Raycast(barrelPosition.position, rayDirection, out bigBarrelHit, maxRaycastRange))
            {
                GameObject prefabRaycast = Instantiate(particleRaycast, bigBarrelHit.point, Quaternion.identity);
                Destroy(prefabRaycast, 1);
                Debug.DrawLine(cam.position, bigBarrelHit.point, Color.red, 0.1f);

                //raycast bullet
                if (bigBarrelHit.transform.gameObject.tag == "Player")
                {
                    bigBarrelHit.transform.gameObject.GetComponent<Health>().healthCounter -= bigBarrelDamage;
                }
            }

            else
            {
                // Handle a miss
            }
        }

        //particle raycast
        GameObject prefab = Instantiate(particleMuzzle, particlePosition.position, Quaternion.identity);
        prefab.transform.parent = particlePosition;
        prefab.transform.rotation = particlePosition.rotation;
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
