using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class Score : MonoBehaviour
{
    public TextMeshProUGUI comboText;
    public float comboCount;
    private float comboTime;

    void Update()
    {
        comboText.text = comboCount.ToString();
        comboTime += Time.deltaTime;
        if(comboTime > 1)
        {
            comboCount = 0;
        }
    }

    public void ResetComboTime()
    {
        comboTime = 0;
    }
}
