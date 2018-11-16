using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ExitGame()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

    public void ShowLeaderboard()
    {
        SceneManager.LoadScene("ShowLeaderboard");
    }
}


// Credits:
// Mario Haberle - wooden fence models
// Jeff Murray - offline leaderboard functionality and sample
// ZUGZUG Art - Stone texture
// ROBOCG - key model 
