using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    public float healthCounter;

    private float previousDamageCount;
    public float comboCount;

    public Score score;

    private void Start()
    {
        // Initialize the previousValue with the initial value of the float
        previousDamageCount = healthCounter;
    }

    private void Update()
    {

        //combo counter
        if (healthCounter != previousDamageCount)
        {
            comboCount += healthCounter;
            score.comboCount += comboCount;
            comboCount = 0;

            // Update the previousValue to the new value
            healthCounter = 0;
            previousDamageCount = 0;

            score.ResetComboTime();
        }

    }
}
