using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
	// Coins are collected in game then added once player is dead
	public static int coins;

	// Highest score that player got in a single game
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
		coins = PlayerPrefs.GetInt ("coins");
		highScore = PlayerPrefs.GetInt ("highScore");
	}

	public static void save ()
	{
		// Set all data
		PlayerPrefs.SetInt ("coins", coins);
		PlayerPrefs.SetInt ("highScore", highScore);

		// Save to disk
		PlayerPrefs.Save ();
	}
}
