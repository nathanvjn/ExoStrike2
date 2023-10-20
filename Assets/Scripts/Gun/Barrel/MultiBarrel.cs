using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiBarrel : Barrel
{
    public Transform[] barrelPositions;
    private RaycastHit hit;

    [Header("Rotation")]
    public Transform gatlingGun;
    public float rotationSpeed;

    private float rotationAmount;

    private void Start()
    {
        
    }

    private void Update()
    {

        // Calculate the new target rotation based on the current rotation.
        Quaternion targetRotation = gatlingGun.localRotation * Quaternion.Euler(0, 0, rotationAmount);

        // Interpolate between the current rotation and the target rotation.
        gatlingGun.localRotation = Quaternion.Slerp(gatlingGun.localRotation, targetRotation, rotationSpeed * Time.deltaTime);

        if(rotationAmount > 0)
        {
            rotationAmount -= 0.4f;
        }
        

    }

    public override void Shoot()
    {
        print("multiBarrel");
        
        rotationAmount += 20;

        for (int i = 0; i < barrelPositions.Length; i++)
        {
            //raycast version
            Physics.Raycast(barrelPositions[i].position, cam.forward, out hit, 100);
            Debug.DrawLine(barrelPositions[i].position, hit.point, Color.red);
            if (hit.transform != null)
            {
                //particle line raycast
                GameObject prefabLineRenderer = Instantiate(lineRenderer, transform.position, Quaternion.identity);

                prefabLineRenderer.GetComponent<LineRenderer>().positionCount = 2;
                prefabLineRenderer.GetComponent<LineRenderer>().SetPosition(0, barrelPosition.position);
                prefabLineRenderer.GetComponent<LineRenderer>().SetPosition(1, hit.point);

                GameObject prefabRaycast = Instantiate(particleRaycast, hit.point, Quaternion.identity);
                Destroy(prefabRaycast, 1);
                if (hit.transform.gameObject.tag == "Player")
                {
                    //damage multiplies with each chamber
                    hit.transform.gameObject.GetComponent<Health>().healthCounter -= barrelDamage;

                }
            }
        }

       

        //sound
        soundManager.GatlingShotSound();
    }

    public override void ShootBullet()
    {
        print("multiBarrel");
        rotationAmount += 20;

        if (usingShrapnel)
        {
            for (int i = 0; i < barrelPositions.Length; i++)
            {
                float numBullets = Random.Range(7, 15);
                for (int i2 = 0; i2 < numBullets; i2++)
                {
                    // Instantiate the bullet with the correct initial rotation
                    GameObject prefabBullet = Instantiate(bulletType, barrelPositions[i].position, Quaternion.identity);
                    prefabBullet.transform.localScale = new Vector3(bulletSize, bulletSize, bulletSize);

                    Quaternion spreadRotation = Quaternion.Euler(Random.Range(-20, 20), Random.Range(-20, 20), 0f);

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

        //sound
        soundManager.GatlingShotSound();
    }

  
}
