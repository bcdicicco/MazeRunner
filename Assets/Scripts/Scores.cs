using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Scores : MonoBehaviour
{
    // Need the leaderboard for operations
    public Leaderboard _scoreManager;

    // Used for if new score qualifies to be added to the leaderboard
    private bool newHighScore;

    // New score
    private int finalGameScore;

    // UI stuff
    public Text leaderboardUIText_rank;
    public Text leaderboardUIText_name;
    public Text leaderboardUIText_score;
    public InputField playerNameInputField;
    public GameObject enterNamePanel;

    public void GotoMainMenu()
    {
        SceneManager.LoadScene("Title");
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Game");
    }

    // ---------------------------------------------------------------------

    void Start()
    {
        // Get current leaderboard
        _scoreManager.LoadInitial();

        // Get the score saved when the game ended
        finalGameScore = PlayerPrefs.GetInt("finalScore");

        // Check the rank (has to be between 0-4) or if there are currently no scores
        if (_scoreManager.scoreCheck(finalGameScore) < 5 || _scoreManager.getNumberOfScores() == 0)
        {
            newHighScore = true;
        }

        // Not good enough to rank
        else
        {
            newHighScore = false;
        }

        // Current leaderboard without the possible new score
        UpdateUIText();

        // Allow name entry if the score ranks between 0-4 and is a valid score
        if (newHighScore && finalGameScore != 0)
        {
            ShowEnterNamePanel();
        }

        else
        {
            HideEnterNamePanel();
        }
    }

    public void ShowEnterNamePanel()
    {

        // Limit to 3 character identifier 
        playerNameInputField.characterLimit = 3;

        enterNamePanel.SetActive(true);   
        playerNameInputField.Select();
    }

    public void HideEnterNamePanel()
    {
        enterNamePanel.SetActive(false);
    }

    public void UpdateUIText()
    {
        // Clear old leaderboard display
        leaderboardUIText_rank.text = "";
        leaderboardUIText_name.text = "";
        leaderboardUIText_score.text = "";

        // Display each rank line-by-line
        for (int count = 1; count <= _scoreManager.getNumberOfScores(); count++)
        {
            leaderboardUIText_rank.text = leaderboardUIText_rank.text + count.ToString() + ".\n";
            leaderboardUIText_name.text = leaderboardUIText_name.text + PlayerPrefs.GetString("lbName"+(count-1)) + "\n";
            leaderboardUIText_score.text = leaderboardUIText_score.text + PlayerPrefs.GetInt("lbScore"+(count-1)) + "\n";
        }

    }

    public void NameSubmit()
    {
        HideEnterNamePanel();

        // Add score with the inputted name
        _scoreManager.addScore(playerNameInputField.text, finalGameScore);
        
        // Update display with new score
        UpdateUIText();

        // Reset game state
        PlayerPrefs.SetInt("finalScore", 0);
    }
}