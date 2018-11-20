using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{

    public int maxNameLength = 3;
    private int rank;
    private string[] names;
    private int[] scores;
    private int numberOfScores;

    public void LoadInitial()
    {
        // Get current number of scores (from 0 to 5)
        numberOfScores = PlayerPrefs.GetInt("numberScores");

        // Check to make sure it's positive for whatever reason (ex. edited player prefs)
        if (numberOfScores >= 0)
        {
            buildLeaderboard(numberOfScores);
        }
    }

    public int getNumberOfScores()
    {
        return numberOfScores;
    }

    public void buildLeaderboard(int number)
    {
        // Max 5 scores
        names = new string[5];
        scores = new int[5];

        // Set display to what is currently stored 
        for (int i = 0; i < number; i++)
        {
            names[i] = PlayerPrefs.GetString("lbName" + i);
            scores[i] = PlayerPrefs.GetInt("lbScore" + i);
        }

    }

    public int scoreCheck(int newScore)
    {
        // Rank goes from 0-4, so automatically don't rank it
        int rank = 5;

        // Go from worst scores to best
        for (int i = (numberOfScores - 1); i > -1; i-- )
        {
            // Lower score = better (best score = rank 0 / displays as 1 on leaderboard)
            if (scores[i] > newScore)
            {
                rank = i;
            }
        }

        // Return where the score should be after it is inserted (if it ranks even)
        return rank;
    }
    public void addScore(string newName, int newScore)
    {
        // If no player entry
        if (newName == "")
        {
            newName = "???";
        }

        // Restrict to 3 characters if they somehow enter more
        if (newName.Length > 3)
        {
            newName = newName.Substring(0, 3);
        }

        // Check where the score ranks
        rank = scoreCheck(newScore);

        // If no scores, then it is automatically the best score
        if (numberOfScores == 0)
        {
            numberOfScores++;
            names[0] = newName;
            scores[0] = newScore;
        }


        if (rank == 5)
        {
            // Do nothing as the score isn't good enough.
        }

        // There is more than 1 score and the new score will be a top 5 score
        else if (rank >= 0 && rank < 5)
        {
            // Temporary arrays to allow swaps
            string[] tempName = new string[5];
            int[] tempScore = new int[5];

            // Need to increment total number of scores if it isn't at the max of 5
            if (numberOfScores < 5)
            {
                numberOfScores++;
                
                for (int i = (numberOfScores - 1); i > -1; i--)
                {
                    // Move worse scores down
                    if (i > rank)
                    {
                        tempName[i] = names[i-1];
                        tempScore[i] = scores[i-1];
                    }

                    // Place the new score where it belongs
                    else if (i == rank)
                    {
                        tempScore[i] = newScore;
                        tempName[i] = newName;
                    }
                     // Keep old scores where they are
                    else
                    {
                        tempName[i] = names[i];
                        tempScore[i] = scores[i];
                    }
                }

                // Set arrays used for saving as the temp arrays
                for (int i = 0; i < numberOfScores; i++)
                {
                    names[i] = tempName[i];
                    scores[i] = tempScore[i];
                }
            }

            // Currently at the max of 5 scores. Do the same as below 5 but don't increment number of scores.
            else
            {
                for (int i = (numberOfScores - 1); i > -1; i--)
                {
                    if (i > rank)
                    {
                        tempName[i] = names[i - 1];
                        tempScore[i] = scores[i - 1];
                    }

                    else if (i == rank)
                    {
                        tempScore[i] = newScore;
                        tempName[i] = newName;
                    }

                    else
                    {
                        tempName[i] = names[i];
                        tempScore[i] = scores[i];
                    }
                }

                for (int i = 0; i < numberOfScores; i++)
                {
                    names[i] = tempName[i];
                    scores[i] = tempScore[i];
                }
            }
        }

        // Save to PlayerPrefs
        saveScores();
    }

    public void saveScores()
    {
        for (int i = 0; i < numberOfScores; i++)
        {
            PlayerPrefs.SetString("lbName" + i, names[i]);
            PlayerPrefs.SetInt("lbScore" + i, scores[i]);
        }

        // Save the number of scores if any were added / not already at 5
        PlayerPrefs.SetInt("numberScores", numberOfScores);
    }


    // 0 scores --> get score. Place as #1. Update UI
    // 1 score --> get score. Compare. Placements. Update UI
    // ... 5 scores --> get score. Compare. Placements. Update UI
    // >5 scores --> get score. Compare to current 5. Place if better than any of them. Update UI and get rid of lowest score.
}
