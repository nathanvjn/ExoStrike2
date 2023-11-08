using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupRotation : MonoBehaviour
{
    public Transform player;

    void Update()
    {
        transform.LookAt(player);
        Vector3 currentRotation = transform.rotation.eulerAngles;

        currentRotation.z = 0;

        // Apply the modified rotation back to the GameObject
        transform.rotation = Quaternion.Euler(currentRotation);
    }
}
