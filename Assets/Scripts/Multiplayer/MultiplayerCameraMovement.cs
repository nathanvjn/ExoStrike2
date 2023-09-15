using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MultiplayerCameraMovement : NetworkBehaviour
{
    public float mouseX;
    public float mouseY;

    public Transform orientation;

    private float xRotation;
    private float yRotation;

    // Update is called once per frame
    void Update()
    {
        if (this.isLocalPlayer)
        {
            float mouseMovementX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * mouseX;
            float mouseMovementY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * mouseY;

            yRotation += mouseMovementX;
            xRotation -= mouseMovementY;

            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}
