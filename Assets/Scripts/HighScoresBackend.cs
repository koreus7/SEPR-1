using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
///  High Scores Backend.
///  
///  An interface to save and load high scores.
/// </summary>
public class HighScoresBackend : MonoBehaviour
{

    //singleton class. referene with HighScoresBackend.instance. ....
    private static HighScoresBackend inst = null;
    public static HighScoresBackend instance
    {
        get
        {
            if (inst == null)
            {
                inst = FindObjectOfType(typeof(HighScoresBackend)) as HighScoresBackend;
            }
            return inst;
        }
    }

    void OnApplicationQuit()
    {
        inst = null;
    }


    /// <summary>
    /// A single high score entry
    /// </summary>
    [Serializable]
    public class HighScore
    {
        public string name;
        public int score;
    }


    /// <summary>
    /// A list of high score entries.
    /// </summary>
    [Serializable]
    public class HighScores
    {
        public List<HighScore> scores;
    }


    /// <summary>
    /// The Currentley loaded high scores.
    /// </summary>
    public HighScores scores;

	// Use this for initialization
	void Start () {
        loadHighScores();

	}

    /// <summary>
    /// Load the high scores from the save.
    /// </summary>
    void loadHighScores()
    {
        if (PlayerPrefs.HasKey("HighScores"))
        {
            scores = JsonUtility.FromJson<HighScores>(PlayerPrefs.GetString("HighScores"));
        }
    }


    /// <summary>
    /// Add a score to the currently loaded scores.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="score"></param>
    public void addHighScore(string name, int score)
    {
        var entry = new HighScore();
        entry.name = name;
        entry.score = score;
        scores.scores.Add(entry);
    }


    public  List<HighScore> getHighScores()
    {
        loadHighScores();
        return scores.scores;
    }

    /// <summary>
    /// Save the currently loaded high scores.
    /// </summary>
    public  void saveHighScores()
    {
        PlayerPrefs.SetString("HighScores", JsonUtility.ToJson(scores));
    }


}
