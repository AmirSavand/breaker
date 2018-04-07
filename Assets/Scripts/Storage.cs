using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    /**
     * Current player's ship
     */
    public static int Ship;

    /**
     * Stars are collected in game then added once player is dead
     */
    public static int Stars;

    /**
     * Highest score that player got in a single game
     */
    public static int HighScore;

    void Awake ()
    {
        // Keep it between scenes
        DontDestroyOnLoad (gameObject);

        // Destroy it if it's a duplicate
        if (FindObjectsOfType (GetType ()).Length > 1) {
            Destroy (gameObject);
        }

        // Load all data
        Ship = PlayerPrefs.GetInt ("ship");
        Stars = PlayerPrefs.GetInt ("stars");
        HighScore = PlayerPrefs.GetInt ("highScore");
    }

    /**
     * Commit all data to storage
     */
    public static void Save ()
    {
        // Set all data
        PlayerPrefs.SetInt ("ship", Ship);
        PlayerPrefs.SetInt ("stars", Stars);
        PlayerPrefs.SetInt ("high-score", HighScore);

        // Save to disk
        PlayerPrefs.Save ();
    }
}
