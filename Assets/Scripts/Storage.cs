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
     * Player's experience points
     */
    public static int XP;

    /**
     * Player's current level
     */
    public static int Level;

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

    /**
     * Enable vibrate (used when loss)
     */
    public static bool EnableVibrate;

    void Awake ()
    {
        // Keep it between scenes
        DontDestroyOnLoad (gameObject);

        // Destroy it if it's a duplicate
        if (FindObjectsOfType (GetType ()).Length > 1) {
            Destroy(gameObject);
            return;
        }

        // Load stats
        Stars = PlayerPrefs.GetInt ("stat-stars");
        XP = PlayerPrefs.GetInt ("stat-xp");
        Level = PlayerPrefs.GetInt ("stat-level", 1);
        HighScore = PlayerPrefs.GetInt ("stat-high-score");
        Ship = PlayerPrefs.GetInt ("stat-ship");

        // Load volumes
        VolumeMaster = PlayerPrefs.GetInt ("volume-master");
        VolumeMusic = PlayerPrefs.GetInt ("volume-music");
        VolumEffects = PlayerPrefs.GetInt ("volume-effects");
        VolumeMenu = PlayerPrefs.GetInt ("volume-menu");

        // Load switches (bools)
        EnableVibrate = PlayerPrefs.GetInt ("enable-vibrate", 1) == 1;
    }

    /**
     * Commit all data to storage.
     */
    public static void Save ()
    {
        // Set stats
        PlayerPrefs.SetInt ("stat-stars", Stars);
        PlayerPrefs.SetInt ("stat-xp", XP);
        PlayerPrefs.SetInt ("stat-level", Level);
        PlayerPrefs.SetInt ("stat-high-score", HighScore);
        PlayerPrefs.SetInt ("stat-ship", Ship);

        // Set volumes
        PlayerPrefs.SetInt ("volume-master", VolumeMaster);
        PlayerPrefs.SetInt ("volume-music", VolumeMusic);
        PlayerPrefs.SetInt ("volume-effects", VolumEffects);
        PlayerPrefs.SetInt ("volume-menu", VolumeMenu);

        // Set switches
        PlayerPrefs.SetInt ("enable-vibrate", EnableVibrate ? 1 : 0);

        // Save to disk
        PlayerPrefs.Save ();
    }
}
