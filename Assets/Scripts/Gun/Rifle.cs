using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    private RaycastHit hit;

    public Transform cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Physics.Raycast(cam.position, cam.forward, out hit, 100);
        Debug.DrawLine(cam.position, hit.point, Color.red);
        if (hit.transform != null)
        {
            if (hit.transform.gameObject.tag == "Player" && Input.GetButtonDown("Fire1"))
            {
                print("hittingEnemy");
            }
        }
        
    }
}
