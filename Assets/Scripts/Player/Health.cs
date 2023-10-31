using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    public float healthCounter;
    public TextMeshProUGUI comboText;

    private float previousDamageCount;

    private float comboTimer; //time until combo resets
    public float comboCount;

    private void Start()
    {
        // Initialize the previousValue with the initial value of the float
        previousDamageCount = healthCounter;
    }

    private void Update()
    {

        if (comboText != null)
        {
            comboText.text = comboCount.ToString();
        }

        //combo counter
        if (healthCounter != previousDamageCount)
        {
            comboCount += (healthCounter - previousDamageCount);

            // Update the previousValue to the new value
            previousDamageCount = healthCounter;

            //set combo timer
            comboTimer = 1;
        }

        if(comboTimer > 0)
        {
            comboTimer -= Time.deltaTime;
        }

        else
        {
            comboCount = 0;
        }

    }
}
