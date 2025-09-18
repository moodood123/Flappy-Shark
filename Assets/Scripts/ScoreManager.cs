using System;
using System.IO;
using UnityEngine;

public static class ScoreManager
{
    public static int HighScore => GetHighScore();

    private static string FilePath => Path.Combine(Application.persistentDataPath, "highscore.json");
    
    public static bool TrySetHighScore(int newScore)
    {
        Debug.Log("TrySetHighScore");
        if (newScore > HighScore)
        {
            Debug.Log($"HighScore: {newScore}");
            SetHighScore(newScore);
            return true;
        }
        return false;
    }
    
    private static void SetHighScore(int newScore)
    {
        HighScoreData data = LoadHighScoreData();
        data.highScore = newScore;
        
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(FilePath, json);
        Debug.Log($"High score saved to json: {HighScore}");

    }

    private static HighScoreData LoadHighScoreData()
    {
        if (File.Exists(FilePath))
        {
            string json = File.ReadAllText(FilePath);
            return JsonUtility.FromJson<HighScoreData>(json);
        }
        else
        {
            return new HighScoreData { highScore = 0 };
        }
    }

    private static int GetHighScore()
    {
        HighScoreData data = LoadHighScoreData();
        return data.highScore;
    }
}

[Serializable]
public class HighScoreData
{
    public int highScore;
}