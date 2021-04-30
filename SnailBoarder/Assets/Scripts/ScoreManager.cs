using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int currentScore = 0;
    private int trickScore = 0;
    public int parkScore = 20;

    public bool shouldMultiply = false;

    public void ResetScore()
    {
        currentScore = 0;
    }

    // Check if the score is high enough to meet the win condition
    public bool CheckScoreWin()
    {
        // Return true if we've reached the needed score to win
        return (currentScore >= parkScore);
    }

    public int ScoreDifference()
    {
        return parkScore - currentScore;
    }

    public void SendDisplayScore(int totalScore)
    {
        // This is kinda cheating, but I don't care at the moment
        //  Takes the value we give it and reassigns the value to our score
        totalScore = currentScore;
    }

    public int SendDisplayScore()
    {
        return currentScore;
    }

    public void AddToScore(int value)
    {
        trickScore = value;
        currentScore += value;
    }

    public void AddToScore(int value, int multiplier)
    {
        trickScore = value;
        currentScore += (value * multiplier);
    }

    private void OnEnable()
    {
        //GameManager.instance.SetDifficultToCurrent();
    }
}
