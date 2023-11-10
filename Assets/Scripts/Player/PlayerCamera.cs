using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float mouseX;
    public float mouseY;

    public float mouseXstartValue;
    public float mouseYstartValue;

    public Transform orientation;

    private float xRotation;
    private float yRotation;

    public bool isrespawning;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        mouseXstartValue = mouseX;
        mouseYstartValue = mouseY;
    }

    void Update()
    {
        if(isrespawning == false)
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
            transform.LookAt(player.transform.position);
        }
        
    }
}
