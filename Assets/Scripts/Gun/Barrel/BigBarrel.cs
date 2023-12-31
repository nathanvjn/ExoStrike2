using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBarrel : Barrel
{
    public float maxRaycastRange;

    //particle
    public GameObject particle;



    private void Start()
    {
        //elke barrel type wordt automatisch bij spawning in de gun script gegooit, het gun script kan door inheritance van de barrel types bij het barrel script om bullets te spawnen
    }

    public override void Shoot()
    {

        //raycast bigBarrel

        print("bigBarrel");
        //calculate a random rotation within the specified spread angle
        Quaternion spreadRotation = Quaternion.Euler(Random.Range(-shrapnelSpread, shrapnelSpread), Random.Range(-shrapnelSpread, shrapnelSpread), 0f);

        //create a raycast direction from the spread rotation
        Vector3 rayDirection = spreadRotation * cam.forward;

        //perform the raycast
        RaycastHit bigBarrelHit;
        if (Physics.Raycast(barrelPosition.position, rayDirection, out bigBarrelHit, maxRaycastRange))
        {
            //particle line raycast
            GameObject prefabLineRenderer = Instantiate(lineRenderer, transform.position, Quaternion.identity);

            prefabLineRenderer.GetComponent<LineRenderer>().positionCount = 2;
            prefabLineRenderer.GetComponent<LineRenderer>().SetPosition(0, barrelPosition.position);
            prefabLineRenderer.GetComponent<LineRenderer>().SetPosition(1, bigBarrelHit.point);

            GameObject prefabRaycast = Instantiate(particleRaycast, bigBarrelHit.point, Quaternion.identity);
            Destroy(prefabRaycast, 1);
            Debug.DrawLine(cam.position, bigBarrelHit.point, Color.red, 0.1f);

            //raycast bullet
            if (bigBarrelHit.transform.gameObject.tag == "Player")
            {
                bigBarrelHit.transform.gameObject.GetComponent<Health>().healthCounter -= barrelDamage;
            }

            else if (bigBarrelHit.transform.gameObject.tag == "Target")
            {

                Destroy(bigBarrelHit.transform.gameObject);
            }
        }

        else
        {
            // Handle a miss
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

        //sound
        soundManager.BigBarrelShotSound();
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

                Quaternion spreadRotation = Quaternion.Euler(Random.Range(-shrapnelSpread, shrapnelSpread), Random.Range(-shrapnelSpread, shrapnelSpread), 0f);

                //create a raycast direction from the spread rotation
                Vector3 rayDirection = spreadRotation * cam.forward;

                prefabBullet.transform.LookAt(prefabBullet.transform.position + rayDirection);
                prefabBullet.GetComponent<Rigidbody>().AddForce(prefabBullet.transform.forward * bulletForce * Time.deltaTime);
            }
        }

        else
        {
            // Calculate a random rotation within the specified spread angle
            Quaternion spreadRotation = Quaternion.Euler(Random.Range(-shrapnelSpread, shrapnelSpread), Random.Range(-shrapnelSpread, shrapnelSpread), 0f);

            // Combine the spreadRotation with the barrelPosition.forward
            Quaternion combinedRotation = Quaternion.LookRotation(spreadRotation * cam.forward);

            // Instantiate the bullet with the correct initial rotation
            GameObject prefabBullet = Instantiate(bulletType, barrelPosition.position, combinedRotation);
            prefabBullet.transform.localScale = new Vector3(bulletSize, bulletSize, bulletSize);

            // Apply force to the bullet
            prefabBullet.GetComponent<Rigidbody>().AddForce(prefabBullet.transform.forward * bulletForce * Time.deltaTime);
        }

        //particle
        GameObject particlePrefab = Instantiate(particle, barrelPosition.position, Quaternion.identity);
        particlePrefab.transform.rotation = barrelPosition.rotation;
        Destroy(particlePrefab, 1);

        //sound
        soundManager.BigBarrelShotSound();
    }

    
}
