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

public enum GameMenuStates
{
	Menu,
	Stat
}

public class Game : MonoBehaviour
{
	public Player player;

	[Header ("UI objecs")]
	public GameObject gameUI;
	public GameObject pauseUI;
	public GameObject loseUI;
	[Space ()]
	public GameObject menuUI;
	public GameObject statUI;

	[Header ("UI texts")]
	public Text timeText;
	public Text starsText;
	public Text hitpointsText;
	[Space ()]
	public Text versionText;

	[Header ("Floating text")]
	public GameObject textFloat;
	public Color textFloatStarColor;
	public Color textFloatScoreColor;
	public Color textFloatHitpointColor;

	[Header ("Start message")]
	public string[] startMessages;
	public TextMesh startMessageTMesh;

	[Header ("Level variables")]
	public Color[] backgroundColors;

	[Header ("Game resources")]
	public GameStates state;
	public GameMenuStates menuState;
	public float gameTime;
	public int stars;
	public int score;

	[Header ("Tracks")]
	public AudioSource backgroundMusic;

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

		// Random background
		Camera.main.backgroundColor = backgroundColors [Random.Range (0, backgroundColors.Length)];
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

		// If in menu
		if (state == GameStates.Menu) {
		
			// Pressed pause/back button
			if (Input.GetButtonDown ("Cancel")) {

				// Show menu if in stat
				if (menuState == GameMenuStates.Stat) {
					showMenu ();
				}
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

		// Pause music
		backgroundMusic.Pause ();

		// Toggle UIs
		toggleUI ();
	}

	public void resumeGame ()
	{
		// Update state
		state = GameStates.Run;

		// Resume time
		Time.timeScale = 1;

		// Pause music
		backgroundMusic.UnPause ();

		// Toggle UIs
		toggleUI ();
	}

	public void toggleUI ()
	{
		// In game
		if (state != GameStates.Menu) {
			pauseUI.SetActive (state == GameStates.Pause);
			gameUI.SetActive (state != GameStates.Lose);
			loseUI.SetActive (state == GameStates.Lose);
		}

		// In menu
		else {
			menuUI.SetActive (menuState == GameMenuStates.Menu);
			statUI.SetActive (menuState == GameMenuStates.Stat);
		}
	}

	public void giveStar (int amount, Vector3 position)
	{
		// Add stars
		stars += amount;

		// Update UI
		starsText.text = stars.ToString ();

		// Text float
		createTextFloat ("+" + amount, textFloatStarColor, position);
	}

	public void giveScore (int amount, Vector3 position)
	{
		// Add stars
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
		score += stars * 100;

		// Update results
		GameObject.Find ("Time Result Text").GetComponent<Text> ().text = "Time\n" + getGameTimeFormat ();
		GameObject.Find ("Star Result Text").GetComponent<Text> ().text = "Stars\n+" + stars;
		GameObject.Find ("Score Result Text").GetComponent<Text> ().text = score.ToString ();

		// Save stars to storage
		Storage.stars += stars;

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

	public void showStat ()
	{
		// Show state and update UI
		menuState = GameMenuStates.Stat;
		toggleUI ();
	}

	public void showMenu ()
	{
		// Show menu and update UI
		menuState = GameMenuStates.Menu;
		toggleUI ();
	}

	public void takeScreenshot ()
	{
		// Take a screenshot
		ScreenCapture.CaptureScreenshot (System.IO.Path.Combine (Application.persistentDataPath, "screenshot.png"));

		// Open screenshot
		Application.OpenURL (System.IO.Path.Combine (Application.persistentDataPath, "screenshot.png"));
	}
}
