using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public int redKeys;
    public int blueKeys;
    public int yellowKeys;
    public Text redKey;
    public Text blueKey;
    public Text yellowKey;

    void Start()
    {
        redKey.text = "";
        blueKey.text = "";
        yellowKey.text = "";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Red"))
        {
            other.gameObject.SetActive(false);
            redKeys += 1;
            redKey.text = "Red";
        }

        if (other.gameObject.CompareTag("Blue"))
        {
            other.gameObject.SetActive(false);
            blueKeys += 1;
            blueKey.text = "Blue";
        }

        if (other.gameObject.CompareTag("Yellow"))
        {
            other.gameObject.SetActive(false);
            yellowKeys += 1;
            if (yellowKeys >= 1)
                yellowKey.text = "Yellow";
        }

        if (other.gameObject.CompareTag("RedGate") && redKeys > 0)
        {
            other.gameObject.SetActive(false);
            redKeys -= 1;
            if (redKeys == 0)
                redKey.text = "";
        }

        if (other.gameObject.CompareTag("YellowGate") && yellowKeys > 0)
        {
            other.gameObject.SetActive(false);
            yellowKeys -= 1;
            if (yellowKeys == 0)
                yellowKey.text = "";
        }

        if (other.gameObject.CompareTag("BlueGate") && blueKeys > 0)
        {
            other.gameObject.SetActive(false);
            blueKeys -= 1;
            if (blueKeys == 0)
                blueKey.text = "";
        }
    }
}
