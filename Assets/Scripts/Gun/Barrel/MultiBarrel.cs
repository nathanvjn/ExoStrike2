using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiBarrel : Barrel
{
    public Transform[] barrelPositions;
    private RaycastHit hit;

    [Header("Rotation")]
    public float rotationSpeed;  // Base rotation speed.
    public float maxRotationSpeed;  // Maximum rotation speed.
    private float currentRotationSpeed = 0f;  // Current rotation speed that changes based on the number of shots.

    private Quaternion startRotation;
    private Quaternion targetRotation;
    private bool isRotating;

    private void Start()
    {
        startRotation = transform.rotation;
    }

    private void OnEnable()
    {
        //generate a random number when the GameObject becomes active
        int randomNumber = Random.Range(1, 15); //generate a random number between 1 and 100 (inclusive)

    }

    private void Update()
    {
        //rotate gatling gun
        if (isRotating)
        {
            // Interpolate between the current rotation and the target rotation using Lerp.
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            // Check if we've reached the target rotation.
            if (Quaternion.Angle(transform.rotation, targetRotation) < 1f)
            {
                isRotating = false;
                currentRotationSpeed = Mathf.Clamp(currentRotationSpeed, 0f, maxRotationSpeed);
            }
        }
    }

    public override void ShootBullet()
    {
        print("multiBarrel");
        if (usingShrapnel)
        {
            for (int i = 0; i < barrelPositions.Length; i++)
            {
                float numBullets = Random.Range(7, 15);
                for (int i2 = 0; i2 < numBullets; i2++)
                {
                    // Instantiate the bullet with the correct initial rotation
                    GameObject prefabBullet = Instantiate(bulletType, barrelPositions[i].position, Quaternion.identity);

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
                bulletPrefab.GetComponent<Rigidbody>().AddForce(cam.forward * bulletForce * Time.deltaTime);
            }
        }

        //rotate gatling gun
        currentRotationSpeed += 0.1f;

        //set the new target rotation based on the current rotation speed.
        targetRotation = startRotation * Quaternion.Euler(0f, currentRotationSpeed * Time.deltaTime, 0f);

        isRotating = true;
    }

    public override void Shoot()
    {
        print("multiBarrel");
        base.Shoot();

        //raycast version
        Physics.Raycast(cam.position, cam.forward, out hit, 100);
        Debug.DrawLine(cam.position, hit.point, Color.red);
        if(usingShrapnel == false)
        {
            if (hit.transform != null)
            {
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
            float numBullets = Random.Range(7, 15);
            for (int i = 0; i < numBullets; i++)
            {
                //calculate a random rotation within the specified spread angle
                Quaternion spreadRotation = Quaternion.Euler(Random.Range(-10, 10), Random.Range(-10, 10), 0f);

                //create a raycast direction from the spread rotation
                Vector3 rayDirection = spreadRotation * cam.forward;

                //perform the raycast
                RaycastHit multiBarrelHit;
                if (Physics.Raycast(barrelPosition.position, rayDirection, out multiBarrelHit, 100))
                {
                    GameObject prefabRaycast = Instantiate(particleRaycast, multiBarrelHit.point, Quaternion.identity);
                    Destroy(prefabRaycast, 1);
                    Debug.DrawLine(cam.position, multiBarrelHit.point, Color.red, 0.1f);

                    //raycast bullet
                    if (multiBarrelHit.transform.gameObject.tag == "Player")
                    {
                        multiBarrelHit.transform.gameObject.GetComponent<Health>().healthCounter -= normalBarrelDamage;
                    }
                }

                else
                {
                    // Handle a miss
                }

            }
        }

        //rotate gatling gun
        currentRotationSpeed += 0.1f;

        //set the new target rotation based on the current rotation speed.
        targetRotation = startRotation * Quaternion.Euler(0f, currentRotationSpeed * Time.deltaTime, 0f);

        isRotating = true;

    }
}
