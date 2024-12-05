using UnityEngine;

public class AudioSaveData
{

}

public static class SaveData
{
    // Static Constructor
    // use it to load once when the game is running. 
    // (it will execute only once, even when scene is changed)
    static SaveData()
    {
        // Load data
    }

    public static int GetClassicBestScore()
    {
        return PlayerPrefs.GetInt("ClassicBestScore", 0);
    }

    public static void SetClassicBestScore(int score)
    {
        PlayerPrefs.SetInt("ClassicBestScore", score);
    }

    public static int GetFastBestScore()
    {
        return PlayerPrefs.GetInt("FastBestScore", 0);
    }

    public static void SetFastBestScore(int score)
    {
        PlayerPrefs.SetInt("FastBestScore", score);
    }

    public static int GetTimerBestScore()
    {
        return PlayerPrefs.GetInt("TimerBestScore", 0);
    }

    public static void SetTimerBestScore(int score)
    {
        PlayerPrefs.SetInt("TimerBestScore", score);
    }
}