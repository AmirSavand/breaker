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
	}
}
