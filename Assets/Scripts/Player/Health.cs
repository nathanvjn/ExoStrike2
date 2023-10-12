using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    public float healthCounter;
    public TextMeshProUGUI hpText;

    private void Update()
    {
        if(hpText != null)
        {
            hpText.text = healthCounter.ToString();
        }
      
    }
}
