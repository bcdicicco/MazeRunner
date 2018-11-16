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
    public GameObject[] pauseObjects;

    void Start()
    {
        redKey.text = "";
        blueKey.text = "";
        yellowKey.text = "";
        Time.timeScale = 1;
        pauseObjects = GameObject.FindGameObjectsWithTag("Pause");
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                foreach (GameObject g in pauseObjects)
                {
                    g.SetActive(true);
                }
            }

            else if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                foreach (GameObject g in pauseObjects)
                {
                    g.SetActive(false);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && Time.timeScale == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.M) && Time.timeScale == 0)
        {
            SceneManager.LoadScene("Title");
        }

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
