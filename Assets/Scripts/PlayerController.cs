using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int redKeys;
    public int blueKeys;
    public int yellowKeys;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Red"))
        {
            other.gameObject.SetActive(false);
            redKeys += 1;
            // update red key UI
        }

        else if (other.gameObject.CompareTag("Blue"))
        {
            other.gameObject.SetActive(false);
            blueKeys += 1;
            // update blue key UI
        }

        else if (other.gameObject.CompareTag("Yellow"))
        {
            other.gameObject.SetActive(false);
            yellowKeys += 1;
            // update yellow key UI
        }
    }
}
