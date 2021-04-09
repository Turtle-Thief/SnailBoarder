using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int currentScore = 0;
    public int parkScore;

    private void ResetScore()
    {
        currentScore = 0;
    }

    // Check if the score is high enough to meet the win condition
    public bool CheckScoreWin()
    {
        // Return true if we've reached the needed score to win
        return (currentScore >= parkScore);
    }

    public int SendDisplayScore()
    {
        return currentScore;
    }

    void AddToScore(int value)
    {
        currentScore += value;
    }

    void AddToScore(int value, int multiplier)
    {
        currentScore += (value * multiplier);
    }
}
