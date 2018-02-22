using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
	public GameObject gameUI;
	public GameObject pauseUI;

	public Text versionText;

	public Text timeText;
	public float gameTime;

	private Cam cam;

	void Start ()
	{
		// Get inits
		cam = Camera.main.GetComponent<Cam> ();

		// Reset time scale
		Time.timeScale = 1;

		// Set version to text (Menu)
		if (versionText) {
			versionText.text = "Version " + Application.version;
		}
	}

	void Update ()
	{
		// If in game
		if (getCurrentScene () == "Game") {

			// Pressed pause/back button
			if (Input.GetButtonDown ("Cancel")) {

				// If game is paused, resume
				if (isGamePaused ()) {
					resumeGame ();
				}

				// If game is resumed, pause
				else {
					pauseGame ();
				}
			}

			// Increase game time
			gameTime += Time.deltaTime;

			// Get game time minutes and seconds
			int minutes = Mathf.FloorToInt (gameTime / 60);
			int seconds = Mathf.FloorToInt (gameTime % 60);

			// Update time text (UI)
			timeText.text = string.Format ("{0:0}:{1:00}", minutes, seconds);
		}
	}

	public void startGame ()
	{
		// Load game scene
		SceneManager.LoadScene ("Game");
	}

	public void stopGame ()
	{
		// Load menu scene
		SceneManager.LoadScene ("Menu");
	}

	public void pauseGame ()
	{
		// Stop time
		Time.timeScale = 0;

		// Toggle UIs
		toggleUI ();

		// Stop camera shake
		cam.shakeDuration = 0;
	}

	public void resumeGame ()
	{
		// Resume time
		Time.timeScale = 1;

		// Toggle UIs
		toggleUI ();
	}

	public bool isGamePaused ()
	{
		// If time is stopped, game is paused
		return Time.timeScale == 0;
	}

	public string getCurrentScene ()
	{
		// Return name of current active scene
		return SceneManager.GetActiveScene ().name;
	}

	public void toggleUI ()
	{
		// Toggle UI visibility based on game state (pause/running)
		pauseUI.SetActive (isGamePaused ());
		// gameUI.SetActive (!isGamePaused ());
	}
}
