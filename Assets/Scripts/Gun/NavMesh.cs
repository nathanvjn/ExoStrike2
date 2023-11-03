using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMesh : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform[] positions;
    public bool[] bools;

    private void Start()
    {
        bools[0] = true;
    }

    void Update()
    {
        if(bools[0])
        {
            agent.destination = positions[0].position;
            if(Vector3.Distance(transform.position, positions[0].position) < 1)
            {
                bools[0] = false;
                bools[1] = true;
            }
        }

        if(bools[1])
        {
            agent.destination = positions[1].position;
            if (Vector3.Distance(transform.position, positions[1].position) < 1)
            {
                bools[1] = false;
                bools[2] = true;
            }
        }

        if(bools[2])
        {
            agent.destination = positions[2].position;
            if (Vector3.Distance(transform.position, positions[2].position) < 1)
            {
                bools[2] = false;
                bools[3] = true;
            }
        }

        if (bools[3])
        {
            agent.destination = positions[3].position;
            if (Vector3.Distance(transform.position, positions[3].position) < 1)
            {
                bools[3] = false;
                bools[0] = true;
            }
        }
    }
}
