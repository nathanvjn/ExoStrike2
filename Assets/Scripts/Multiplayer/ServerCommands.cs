using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ServerCommands : NetworkBehaviour
{
    public NetworkIdentity identity;
    [Space(20)]
    public GameObject debugCube;
    public Gun gun;
    Barrel barrel;

    private void Update()
    {
        barrel = gun.barrel;
    }

    [Command]
    public void PlaceDebugCube(Vector3 location)
    {
        GameObject newDebugCube = Instantiate(debugCube, location, Quaternion.identity);
        NetworkServer.Spawn(newDebugCube);
    }

    [Command]
    public virtual void ShootBulletOverNetwork()
    {
        Debug.Log("Triggering ShootBullet() command on server");

        if (barrel.usingShrapnel == false)
        {
            print("schootingBullet");
            GameObject bulletPrefab = Instantiate(barrel.bulletType, barrel.barrelPosition.position, Quaternion.identity);
            NetworkServer.Spawn(bulletPrefab);
            bulletPrefab.transform.localScale = new Vector3(barrel.bulletSize, barrel.bulletSize, barrel.bulletSize);
            bulletPrefab.GetComponent<Rigidbody>().AddForce(barrel.cam.forward * barrel.bulletForce * Time.deltaTime);
        }

        else
        {
            float numBullets = Random.Range(7, 15);
            for (int i = 0; i < numBullets; i++)
            {

                // Instantiate the bullet with the correct initial rotation
                GameObject prefabBullet = Instantiate(barrel.bulletType, barrel.barrelPosition.position, Quaternion.identity);
                NetworkServer.Spawn(prefabBullet);

                Quaternion spreadRotation = Quaternion.Euler(Random.Range(-20, 20), Random.Range(-20, 20), 0f);

                //create a raycast direction from the spread rotation
                Vector3 rayDirection = spreadRotation * barrel.cam.forward;

                prefabBullet.transform.LookAt(prefabBullet.transform.position + rayDirection);
                prefabBullet.GetComponent<Rigidbody>().AddForce(prefabBullet.transform.forward * barrel.bulletForce * Time.deltaTime);

            }
        }

        //particle
        GameObject prefab = Instantiate(barrel.particleMuzzle, barrel.particlePosition.position, Quaternion.identity);
        NetworkServer.Spawn(prefab);
        prefab.transform.parent = barrel.particlePosition;
        prefab.transform.rotation = barrel.particlePosition.rotation;

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
        barrel.soundManager.NormalShotSound();
    }
}
