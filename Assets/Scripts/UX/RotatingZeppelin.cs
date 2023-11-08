using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingZeppelin : MonoBehaviour
{
    public float rotationSpeed;
    void Update()
    {
        transform.Rotate(transform.up * rotationSpeed * Time.deltaTime);
    }
}
