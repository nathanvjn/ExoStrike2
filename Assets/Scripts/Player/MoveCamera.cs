using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform camPosition;
    void Update()
    {
        transform.position = camPosition.position;
    }
}
