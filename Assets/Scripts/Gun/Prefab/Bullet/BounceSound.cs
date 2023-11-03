using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceSound : MonoBehaviour
{
    public AudioSource bounceSoundEffect;

    public void Start()
    {
        bounceSoundEffect.Play();
    }

    private void Update()
    {
        Destroy(gameObject, 1);
    }
}
