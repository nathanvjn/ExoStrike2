using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource pickupSound;
    public AudioSource shot;
    public AudioSource gatling;
    public AudioSource bigBarrel;

    public void GatlingShotSound()
    {
        gatling.Play();
    }

    public void NormalShotSound()
    {
        shot.Play();
    }

    public void BigBarrelShotSound()
    {
        bigBarrel.Play();
    }

    public void PickupSound()
    {
        pickupSound.Play();
    }
}
