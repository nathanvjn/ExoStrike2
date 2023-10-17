using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentPickup : MonoBehaviour
{
    public Gun gun;
    public GameObject[] barrels;
    public SoundManager soundManager;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {

            int randomComponent = Random.Range(0, 3);
            if (randomComponent == 0)
            {
                //mag
                gun.mag.ResetMag();
            }

            else if (randomComponent == 1)
            {
                //chamber
                gun.chamber.ResetChamber();
            }

            else if (randomComponent == 2)
            {
                //barrel
                int randomIndex = Random.Range(0, barrels.Length);
                for (int i = 0; i < barrels.Length; i++)
                {
                    barrels[i].SetActive(i == randomIndex);
                }
            }

            //sound
            soundManager.PickupSound();

            transform.parent.gameObject.GetComponent<ComponentPlate>().respawnTime = 0;
            gameObject.SetActive(false);
        }
    }
}
