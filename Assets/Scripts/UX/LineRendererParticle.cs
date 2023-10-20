using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererParticle : MonoBehaviour
{

    private void Update()
    {
        Destroy(gameObject, 0.2f);
    }
}
