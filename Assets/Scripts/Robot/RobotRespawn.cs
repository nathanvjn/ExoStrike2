using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotRespawn : MonoBehaviour
{
    public float respawnTime;
    public GameObject jumpRobot;

    void Update()
    {
        respawnTime += Time.deltaTime;
        if(respawnTime > 2)
        {
            respawnTime = 0;
            GameObject prefab = Instantiate(jumpRobot, transform.position, Quaternion.identity);
        }
    }
}
