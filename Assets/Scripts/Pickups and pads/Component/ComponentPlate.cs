using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentPlate : MonoBehaviour
{

    public float timeBeforeRespawn; //tijd tot de ammo weer spawned
    public float respawnTime; //counter
    public GameObject componentPrefab;

    void Update()
    {
        respawnTime += Time.deltaTime;

        if (respawnTime > timeBeforeRespawn)
        {
            SetAmmoPickupActive();
        }

        else
        {
            componentPrefab.SetActive(false);
        }
    }

    void SetAmmoPickupActive()
    {
        componentPrefab.SetActive(true);
    }
}
