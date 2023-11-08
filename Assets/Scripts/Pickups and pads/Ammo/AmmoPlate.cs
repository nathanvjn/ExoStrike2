using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPlate : MonoBehaviour
{
    public float timeBeforeRespawn; //tijd tot de ammo weer spawned
    public float respawnTime; //counter
    public GameObject ammoPrefab;
    public GameObject textObject;

    void Update()
    {
        respawnTime += Time.deltaTime;

        if (respawnTime > timeBeforeRespawn)
        {
            SetAmmoPickupActive();
        }

        else
        {
            ammoPrefab.SetActive(false);
            textObject.SetActive(false);
        }
    }

    void SetAmmoPickupActive()
    {
        ammoPrefab.SetActive(true);
        textObject.SetActive(true);
    }
}
