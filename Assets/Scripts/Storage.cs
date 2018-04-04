using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    /**
     * Current player's ship
     */
    public static int ship;

    /**
     * Stars are collected in game then added once player is dead
     */
    public static int stars;

    /**
     * Highest score that player got in a single game
     */
    public static int highScore;

    void Awake ()
    {
        // Keep it between scenes
        DontDestroyOnLoad (gameObject);

        // Destroy it if it's a duplicate
        if (FindObjectsOfType (GetType ()).Length > 1) {
            Destroy (gameObject);
        }

        // Load all data
        ship = PlayerPrefs.GetInt ("ship");
        stars = PlayerPrefs.GetInt ("stars");
        highScore = PlayerPrefs.GetInt ("highScore");

        stars = 5000;
    }

    /**
     * Commit all data to storage
     */
    public static void Save ()
    {
        // Set all data
        PlayerPrefs.SetInt ("ship", ship);
        PlayerPrefs.SetInt ("stars", stars);
        PlayerPrefs.SetInt ("highScore", highScore);

        // Save to disk
        PlayerPrefs.Save ();
    }
}
