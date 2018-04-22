using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    /**
     * Stars are collected in game then added once player is dead.
     */
    public static int Stars;

    /**
     * Highest score that player got in a single game.
     */
    public static int HighScore;

    /**
     * Current player's ship.
     */
    public static int Ship;

    /**
     * Volume of Master audio mixer group.
     */
    public static int VolumeMaster;

    /**
     * Volume of Music audio mixer group.
     */
    public static int VolumeMusic;

    /**
     * Volume of Effects audio mixer group.
     */
    public static int VolumEffects;

    
    /**
     * Volume of Menu audio mixer group.
     */
    public static int VolumeMenu;

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
        VolumeMaster = PlayerPrefs.GetInt ("volume-master");
        VolumeMusic = PlayerPrefs.GetInt ("volume-music");
        VolumEffects = PlayerPrefs.GetInt ("volume-effects");
        VolumeMenu = PlayerPrefs.GetInt ("volume-menu");
    }

    /**
     * Commit all data to storage.
     */
    public static void Save ()
    {
        // Set stats
        PlayerPrefs.SetInt ("stat-ship", Ship);
        PlayerPrefs.SetInt ("stat-stars", Stars);
        PlayerPrefs.SetInt ("stat-high-score", HighScore);

        // Set volumes
        PlayerPrefs.SetInt ("volume-master", VolumeMaster);
        PlayerPrefs.SetInt ("volume-music", VolumeMusic);
        PlayerPrefs.SetInt ("volume-effects", VolumEffects);
        PlayerPrefs.SetInt ("volume-menu", VolumeMenu);

        // Save to disk
        PlayerPrefs.Save ();
    }
}
