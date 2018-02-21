using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
	public Text versionText;
	public GameObject canvasObject;

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

		// Toggle canvas visbility
		canvasObject.SetActive (isGamePaused ());

		// Stop camera shake
		cam.shakeDuration = 0;
	}

	public void resumeGame ()
	{
		// Resume time
		Time.timeScale = 1;

		// Toggle canvas visbility
		canvasObject.SetActive (isGamePaused ());
	}

	public bool isGamePaused ()
	{
		// If time is stopped, game is paused
		return Time.timeScale == 0;
	}
}
