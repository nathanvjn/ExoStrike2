using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource pickupSound;

    //gun
    public AudioSource shot;
    public AudioSource gatling;
    public AudioSource bigBarrel;

    //player
    public PlayerSliding playerSliding;
    public AudioSource sliding;

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

    private void Update()
    {
        if(playerSliding.isSliding && !sliding.isPlaying)
        {
            sliding.Play();
        }

        else if(playerSliding.isSliding == false && sliding.isPlaying)
        {
            sliding.Stop();
        }
    }
}
