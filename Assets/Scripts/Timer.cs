using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    public Text timerText;
    private float startTime;
    private string finalTime;
    private bool start = false;
    private bool finished = false;

    // Update is called once per frame
    void Update()
    {
        if (finished)
        {
            return;
        }

        if (start)
        { 

            float timeDiff = Time.time - startTime;

            string minutes = ((int)timeDiff / 60).ToString();
            string seconds = (timeDiff % 60).ToString("f0");

            if (seconds == "0" || seconds == "1" || seconds == "2" || seconds == "3" || seconds == "4" || seconds == "5" || seconds == "6" || seconds == "7" || seconds == "8" || seconds == "9")
            {
                seconds = "0" + seconds;
            }

            timerText.text = minutes + ":" + seconds;
        }
    }

    public void StopTimer()
    {
        finished = true;
        timerText.color = Color.yellow;
    }

    public void StartTimer()
    {
        startTime = Time.time;
        start = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Start"))
            StartTimer();
    

        if (other.gameObject.CompareTag("Finish"))
            StopTimer();

    }
}
