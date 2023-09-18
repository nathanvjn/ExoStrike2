using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SendData : NetworkBehaviour
{
    public NetworkIdentity identity;
    [Space(20)]
    public GameObject debugCube;

    [Command]
    public void PlaceDebugCube(Vector3 location)
    {
        GameObject newDebugCube = Instantiate(debugCube, location, Quaternion.identity);
        NetworkServer.Spawn(newDebugCube);
    }
}
