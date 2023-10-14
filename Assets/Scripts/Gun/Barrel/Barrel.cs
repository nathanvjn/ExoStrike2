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
    private RaycastHit rayHit;
    public float bulletSize;

    [Header("Particles")]
    public GameObject particleRaycast;
    public GameObject particleMuzzle;
    public Transform particlePosition;
    public LineRenderer lineRenderer;

    //overloading (give bool when raycasting)
    //als er geen andere barrel scripts zijn die overriden is het de default barrel
    public virtual void Shoot()
    {
        print("schootingRay");
        Physics.Raycast(barrelPosition.position, cam.forward, out rayHit, 100);
        Debug.DrawLine(barrelPosition.position, rayHit.point, Color.red);
        if(usingShrapnel == false)
        {

            //check if bullet hit
            if (rayHit.transform != null)
            {
                //particle line raycast
                lineRenderer.positionCount = 2;
                lineRenderer.SetPosition(0, barrelPosition.position);
                lineRenderer.SetPosition(1, rayHit.point);

                GameObject prefabRaycast = Instantiate(particleRaycast, rayHit.point, Quaternion.identity);
                Destroy(prefabRaycast, 1);

                if (rayHit.transform.gameObject.tag == "Player")
                {
                    rayHit.transform.gameObject.GetComponent<Health>().healthCounter -= normalBarrelDamage;
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
                RaycastHit spreadHit;
                if (Physics.Raycast(barrelPosition.position, rayDirection, out spreadHit, 100))
                {
                    //particle line raycast
                    lineRenderer.positionCount = 2;
                    lineRenderer.SetPosition(0, barrelPosition.position);
                    lineRenderer.SetPosition(1, spreadHit.point);

                    GameObject prefabRaycast = Instantiate(particleRaycast, spreadHit.point, Quaternion.identity);
                    Destroy(prefabRaycast, 1);
                    Debug.DrawLine(barrelPosition.position, spreadHit.point, Color.red, 0.1f);

                    //raycast bullet
                    if (spreadHit.transform.gameObject.tag == "Player")
                    {
                        spreadHit.transform.gameObject.GetComponent<Health>().healthCounter -= normalBarrelDamage;
                    }
                }

                else
                {
                    // Handle a miss
                }

            }
        }

        //particle
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

    public virtual void ShootBullet()
    {
        if(usingShrapnel == false)
        {
            print("schootingBullet");
            GameObject bulletPrefab = Instantiate(bulletType, barrelPosition.position, Quaternion.identity);
            bulletPrefab.transform.localScale = new Vector3(bulletSize, bulletSize, bulletSize);
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

        //particle
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
