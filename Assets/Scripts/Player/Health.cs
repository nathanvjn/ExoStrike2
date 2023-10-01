using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    public float playerHealth;
    public TextMeshProUGUI hpText;

    private void Update()
    {
        if(hpText != null)
        {
            hpText.text = playerHealth.ToString();
        }
      
    }
}
