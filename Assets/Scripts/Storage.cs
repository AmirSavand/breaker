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

    public static int MasterVolume;
    public static int MusicVolume;
    public static int EffectsVolume;
    public static int MenuVolume;

    void Awake ()
    {
        // Keep it between scenes
        DontDestroyOnLoad (gameObject);

        // Destroy it if it's a duplicate
        if (FindObjectsOfType (GetType ()).Length > 1) {
            Destroy (gameObject);
        }

        // Load stats
        Ship = PlayerPrefs.GetInt ("stat-ship");
        Stars = PlayerPrefs.GetInt ("stat-stars");
        HighScore = PlayerPrefs.GetInt ("stat-high-score");

        // Load volumes
        MasterVolume = PlayerPrefs.GetInt ("volume-master");
        MusicVolume = PlayerPrefs.GetInt ("volume-music");
        EffectsVolume = PlayerPrefs.GetInt ("volume-effects");
        MenuVolume = PlayerPrefs.GetInt ("volume-menu");
    }

    /**
     * Commit all data to storage
     */
    public static void Save ()
    {
        // Set stats
        PlayerPrefs.SetInt ("stat-ship", Ship);
        PlayerPrefs.SetInt ("stat-stars", Stars);
        PlayerPrefs.SetInt ("stat-high-score", HighScore);

        // Set volumes
        PlayerPrefs.SetInt ("volume-master", MasterVolume);
        PlayerPrefs.SetInt ("volume-music", MusicVolume);
        PlayerPrefs.SetInt ("volume-effects", EffectsVolume);
        PlayerPrefs.SetInt ("volume-menu", MenuVolume);

        // Save to disk
        PlayerPrefs.Save ();
    }
}
