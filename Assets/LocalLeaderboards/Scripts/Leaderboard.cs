using UnityEngine;
using System.Collections;

public class Leaderboard : MonoBehaviour
{
    public int maxNameLength = 3; // <-- change this to allow longer names
    public int numberOfScores = 10; // <-- default value for leaderboard sizes

    private bool doneSetup;
    private int rank;

    private string[] names;
    private int[] scores;

    private int scoreBoardIndex;

    // -----------------------------------------------------------------------------------------

    public void SetUpScores()
    {
        // default is a leaderboard index of 0
        scoreBoardIndex = 0;

        // check scores will either load scores from prefs or make a new board if there isn't one
        CheckScores();
    }

    // -----------------------------------------------------------------------------------------

    public void SetUpScores( int boardIndex )
    {
        // which scoreboard should we load?
        scoreBoardIndex = boardIndex;

        // check scores will either load scores from prefs or make a new board if there isn't one
        CheckScores();
    }

    // -----------------------------------------------------------------------------------------

    public void CheckScores()
    {
        // check to see if the leaderboard already exists (in prefs)
        if (PlayerPrefs.GetInt("hasLeaderboard" + scoreBoardIndex) != 2)
        {
            // there is no leaderboard for this game, so lets go ahead and make one
            BuildDefaultTable();

            // set the prefs flag to say 'yes, we made a leaderboard'
            PlayerPrefs.SetInt("hasLeaderboard" + scoreBoardIndex, 2);
        }
        else {
            // leaderboard exists in prefs
            LoadScores();
        }
    }

    // -----------------------------------------------------------------------------------------

    public void ResetAllScores()
    {
        // when the board is called to reset, we call the same function used to build it in the first place
        // so that it just constructs and entirely new leaderboard
        BuildDefaultTable();
    }

    // -----------------------------------------------------------------------------------------

    public void BuildDefaultTable()
    {
        // quick debug message to make it easy to track what's happening in the editor if needed
        Debug.Log(">Laderboard.cs>Building default score table..");

        // set up default leaderboard table prefs
        for (var i = 0; i < numberOfScores; i++)
        {
            // PlayerPrefs, although it may not be the fastest, is the easiest, most easily transportable method
            // for storing data with Unity. If you're looking for performance then you might 
            PlayerPrefs.SetString(scoreBoardIndex + "leaderBoardName" + i, "Empty");
            PlayerPrefs.SetInt(scoreBoardIndex + "leaderBoardScore" + i, 0);
        }

        // set up arrays to store scores in for processing
        names = new string[numberOfScores];
        scores = new int[numberOfScores];

        // populate our arrays with playerprefs
        LoadScores();

        // set a flag to note this has been done
        doneSetup = true;
    }

    // GetNameAt(index)
    // -----------------------------------------------------------------------------------------

    public string GetNameAt( int index )
	{
		// quick check to make sure that the leaderboard was set up before it is accessed
		if(!doneSetup){
			Debug.LogError("ERROR: Leaderboard not set up and something is calling getFormattedStringAt.");
		}
		// return the name at the passed in index num (+1 so that it makes sense)
		return names[index - 1];
	}
	
	// getScoreAt(index)
	// -----------------------------------------------------------------------------------------
	
	public int GetScoreAt( int index )
	{
		// quick check to make sure that the leaderboard was set up before it is accessed
		if(!doneSetup){
			Debug.LogError("ERROR: Leaderboard not set up and something is calling getFormattedStringAt.");
		}
        // return the name at the passed in index num (+1 so that it makes sense)
		return scores[index - 1];
	}
	
	// didGetHighScore(score) returns true/false
	// -----------------------------------------------------------------------------------------
	
	public bool DidGetHighScore( int aScore )
	{
        // grab the player's rank from our GetHighScoreRank function
        rank = GetHighScoreRank(aScore);

        if (rank<numberOfScores)
		{
			// we have a high score!
			return true;
		}
		
        // the player has no high score, so return false
		return false;
	}
	
	public int GetHighScoreRank( int aScore )
	{
        // to find out if we made it onto the leaderboard, we start out by assuming that the 
        // player is ranked just off the bottom of the board
        rank = numberOfScores+1;

        // to find our rank on the leaderboard, we go from the bottom of the leaderboard
        // up to the top, moving the player's rank up each pass if the score is higher
        // find our rank on the scoreboard
        for ( int i =(numberOfScores-1); i>-1; --i){
			if(scores[i]<aScore){
				rank=i;
			}
		}
	
		if(rank<numberOfScores)
		{
			// we have a high score!
			return 0;
		}
		
        // no high score found, so let's return the rank + 1 (keeping us off the leaderboard if it's a score below the bottom)
		return (rank+1);
	}
	
	// -----------------------------------------------------------------------------------------
	
	public void SubmitLocalScore( string playerName , int theScore )
    {
        // quick check to make sure that the leaderboard was set up before it is accessed
        if (!doneSetup)
        {
            Debug.LogError("ERROR: Leaderboard not set up and something is calling submitLocalScore.");
        }

        // if no name passed in, call him 'Anon'!
        if (playerName == "")
            playerName = "Anon";

        // restrict name lengths to stop long names messing up the high score display
        if (playerName.Length > maxNameLength)
        {
            // truncate the name
            playerName = playerName.Substring(0, maxNameLength);
        }

        rank = numberOfScores + 1;

        // find our rank on the scoreboard
        for ( int r = (numberOfScores - 1); r > -1; --r){
            if (scores[r] < theScore)
            {
                rank = r;
            }
        }

        // build temp score board arrays for shuffling around 
        int[] scoresCopy = new int[numberOfScores];
        string[] namesCopy = new string[numberOfScores];

        for (var i = (numberOfScores - 1); i > -1; --i)
        {
            if (i > rank)
            {
                scoresCopy[i] = scores[i - 1];
                namesCopy[i] = names[i - 1];
            }
            else if (i == rank)
            {
                scoresCopy[i] = theScore;
                namesCopy[i] = playerName;
            }
            else {
                scoresCopy[i] = scores[i];
                namesCopy[i] = names[i];
            }
        }

        // copy our shuffled leaderboard arrays into our 'real' board arrays
        for ( int a = 0; a < numberOfScores; ++a){
            names[a] = namesCopy[a];
            scores[a] = scoresCopy[a];
        }

        // now save our updated array into playerprefs
        SaveScores();
    }

    // file stuff
    // -----------------------------------------------------------------------------------------

    public void SaveScores()
    {
        // save our scores out to player prefs
        for (var i = 0; i < numberOfScores; i++)
        {
            PlayerPrefs.SetString(scoreBoardIndex + "leaderBoardName" + i, names[i]);
            PlayerPrefs.SetInt(scoreBoardIndex + "leaderBoardScore" + i, scores[i]);
        }
    }

    public void LoadScores()
    {
        // set up arrays to store scores in for processing
        names = new string[numberOfScores];
        scores = new int[numberOfScores];

        // load our scores out of player prefs
        for (var i = 0; i < numberOfScores; i++)
        {
            names[i] = PlayerPrefs.GetString(scoreBoardIndex + "leaderBoardName" + i);
            scores[i] = PlayerPrefs.GetInt(scoreBoardIndex + "leaderBoardScore" + i);
        }

        // doneSetup is a bool just so that we can always be sure the leaderboard has been set up
        doneSetup = true;
    }

}