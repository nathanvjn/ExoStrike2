using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EmpHit : MonoBehaviour
{
    public Material blueMaterial;
    private Material normalMaterial;
    public GameObject body;
    private float normalSpeed;

    public bool hitByEMP;
    public float maxEMPtime;
    private float empCounter;
    void Start()
    {
        normalMaterial = body.GetComponent<Renderer>().material;
        normalSpeed = GetComponent<NavMeshAgent>().speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(hitByEMP)
        {
            GetComponent<NavMeshAgent>().speed = 0;
            body.GetComponent<Renderer>().material = blueMaterial;
            empCounter += Time.deltaTime;
            if(empCounter > maxEMPtime)
            {
                GetComponent<NavMeshAgent>().speed = normalSpeed;
                body.GetComponent<Renderer>().material = normalMaterial;
                empCounter = 0;
                hitByEMP = false;
            }
        }
    }
}
