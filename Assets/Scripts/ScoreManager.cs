using UnityEngine;

public static class ScoreManager
{
    public static int HighScore { get; private set; } = 0;

    public static void ResetHighScore()
    {
        HighScore = 0;
    }
    
    public static bool TrySetHighScore(int newScore)
    {
        Debug.Log("TrySetHighScore");
        if (newScore > HighScore)
        {
            Debug.Log($"HighScore: {newScore}");
            HighScore = newScore;
            return true;
        }
        else return false;
    }
}
