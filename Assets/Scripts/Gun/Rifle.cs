using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    public Transform raycastPosition;
    private RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Physics.Raycast(raycastPosition.position, transform.forward, out hit, 100);

        if (hit.transform.gameObject.tag == "Player" && Input.GetButtonDown("Fire1"))
        {
            print("hittingEnemy");
        }
    }
}
