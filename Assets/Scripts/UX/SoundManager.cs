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
    public AudioSource noAmmo;

    //player
    public PlayerSliding playerSliding;
    public AudioSource sliding;
    public AudioSource jumping;
    public AudioSource landing;
    public bool isGrounded;
    private float inAirTime;

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

    public void JumpingSound()
    {
        jumping.Play();
    }

    public void LandingSound()
    {
        if(inAirTime > 0.5)
        {
            landing.Play();
            inAirTime = 0;
        }
    }

    public void NoAmmoSound()
    {
        if(noAmmo.isPlaying == false)
        {
            noAmmo.Play();
        }
    }

    /*
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

        //in air time
        if(isGrounded == false)
        {
            inAirTime += Time.deltaTime;
        }
    }
    */
}
