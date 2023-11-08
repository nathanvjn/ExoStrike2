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
    public AudioSource bounce;
    public AudioSource charge;

    //player
    public PlayerSliding playerSliding;
    public PlayerMovement playerMovement;
    public AudioSource sliding;
    public AudioSource jumping;
    public AudioSource landing;
    private float inAirTime;

    public void GatlingShotSound()
    {
        gatling.Play();
    }

    public void NormalShotSound()
    {
        shot.Play();
    }

    public void Bounce()
    {
        bounce.Play();
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


    private void Update()
    {
        if(playerMovement.isGrounded == false)
        {
            inAirTime += Time.deltaTime;
        }
    }

    public void LandingSound()
    {
        if(inAirTime > 0.5f)
        {
            landing.Play();
            inAirTime = 0;
        }
    }

    public void ChargeSound()
    {
        if(charge.isPlaying == false)
        {
            charge.Play();
        }
    }

    public void StopCharge()
    {
        charge.Stop();
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
