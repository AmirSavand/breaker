using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameStates
{
	Menu,
	Run,
	Pause,
	Lose
}

public class Game : MonoBehaviour
{
	public Player player;

	public GameObject textFloat;
	public Color textFloatCoinColor;
	public Color textFloatScoreColor;
	public Color textFloatHitpointColor;

	public GameObject gameUI;
	public GameObject pauseUI;
	public GameObject loseUI;

	public string[] startMessages;
	public TextMesh startMessageTMesh;

	public Text versionText;
	public Text timeText;
	public Text coinsText;
	public Text hitpointsText;

	public GameStates state;
	public float gameTime;
	public int coins;
	public int score;

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

		// Random message and start text object
		if (startMessageTMesh != null && startMessages.Length > 0) {

			// Set text to a random message
			startMessageTMesh.text = startMessages [Random.Range (0, startMessages.Length)].Replace ("\\n", "\n");
		}

		// In game scene
		if (SceneManager.GetActiveScene ().name == "Game") {

			// Set to run since first state in this scene is Run
			state = GameStates.Run;
		}
	
		// Otherwise game is in menu
		else {
			state = GameStates.Menu;
		}
	}

	void Update ()
	{
		// If in game (not menu)
		if (state != GameStates.Menu) {

			// Pressed pause/back button
			if (Input.GetButtonDown ("Cancel")) {

				// If game is paused, resume it
				// if (state == GameStates.Pause) {
				// 	resumeGame ();
				// }

				// If game is running, pause it
				if (state == GameStates.Run) {
					pauseGame ();
				}
			}

			// Game is running
			if (state == GameStates.Run) {

				// Increase game time
				gameTime += Time.deltaTime;

				// Update time text (UI)
				timeText.text = getGameTimeFormat ();
			}

			// If player is destroyed
			if (!player && state != GameStates.Lose) {

				// Update state
				state = GameStates.Lose;
			
				// Show loss screen after a while
				Invoke ("showLoss", 2);
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
		// Update state
		state = GameStates.Pause;

		// Stop time
		Time.timeScale = 0;

		// Stop camera shake
		cam.shakeDuration = 0;

		// Toggle UIs
		toggleUI ();
	}

	public void resumeGame ()
	{
		// Update state
		state = GameStates.Run;

		// Resume time
		Time.timeScale = 1;

		// Toggle UIs
		toggleUI ();
	}

	public void toggleUI ()
	{
		pauseUI.SetActive (state == GameStates.Pause);
		gameUI.SetActive (state != GameStates.Lose);
		loseUI.SetActive (state == GameStates.Lose);
	}

	public void giveCoin (int amount, Vector3 position)
	{
		// Add coins
		coins += amount;

		// Update UI
		coinsText.text = coins.ToString ();

		// Text float
		createTextFloat ("+" + amount, textFloatCoinColor, position);
	}

	public void giveScore (int amount, Vector3 position)
	{
		// Add coins
		score += amount;

		// Text float
		createTextFloat ("+" + amount, textFloatScoreColor, position);
	}

	public string getGameTimeFormat ()
	{
		// Get game time minutes and seconds
		int minutes = Mathf.FloorToInt (gameTime / 60);
		int seconds = Mathf.FloorToInt (gameTime % 60);

		// Return formatted text like 0:01
		return string.Format ("{0:0}:{1:00}", minutes, seconds);
	}

	public GameObject createTextFloat (string text, Color color, Vector3 position)
	{
		// Floating text
		GameObject instance = Instantiate (textFloat, position, new Quaternion ());

		// Set to score color and amount
		instance.GetComponent<TextMesh> ().color = color;
		instance.GetComponent<TextMesh> ().text = text;

		// Return the clone
		return instance;
	}

	void showLoss ()
	{
		// Update UI (since state is updated)
		toggleUI ();

		// Calculate score
		score += Mathf.FloorToInt (gameTime * 10);
		score += coins * 100;

		// Update results
		GameObject.Find ("Time Result Text").GetComponent<Text> ().text = "Time\n" + getGameTimeFormat ();
		GameObject.Find ("Coin Result Text").GetComponent<Text> ().text = "Coins\n+" + coins;
		GameObject.Find ("Score Result Text").GetComponent<Text> ().text = score.ToString ();

		// Save coins to storage
		Storage.coins += coins;

		// New high score?
		if (Storage.highScore < score) {

			// Save high score to storage
			Storage.highScore = score;

			// Set text so player will know
			GameObject.Find ("High Score Result Text").GetComponent<Text> ().text = "New High Score";
		}

		// Update save data
		Storage.save ();
	}
}
