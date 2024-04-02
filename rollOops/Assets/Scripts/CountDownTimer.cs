using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountDownTimer : MonoBehaviour
{

    public float timeRemaining;
    public TextMeshProUGUI timeText;
    
    void Update()
    {
        if (timeRemaining>0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            timeRemaining = 0;
        }

        timeText.text = "Time: " + Mathf.Round(timeRemaining).ToString(); 
    }
}
