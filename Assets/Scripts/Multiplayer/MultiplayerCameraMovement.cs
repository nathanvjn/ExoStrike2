using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(ServerCommands))]
public class MultiplayerCameraMovement : NetworkBehaviour
{
    public float mouseX;
    public float mouseY;

    public Transform orientation;

    private float xRotation;
    private float yRotation;

    RaycastHit hit;

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

            if (Input.GetKeyDown(KeyCode.C) && Physics.Raycast(transform.position, transform.forward, out hit, 1000))
            {
                GetComponent<ServerCommands>().PlaceDebugCube(hit.point);
            }
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}
