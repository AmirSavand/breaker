using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public enum ModeStates
{
    Run,
    Pause,
    Lose
}

/**
 * Handles modes (mode scenes).
 */
public class Mode : MonoBehaviour
{
    public Player player;
    public ModeStates state;
    public Cam[] cams;
    public Camera touchCamera;
    public GameObject spawners;

    [Space ()]

    public float time;
    public int stars;
    public int score;

    [Space ()]

    public float timeScoreFactor = 10;

    [Space ()]

    public GameObject runUI;
    public GameObject pauseUI;
    public GameObject loseUI;

    [Space ()]

    public Text textTime;

    public Slider sliderHitpoints;
    public Slider sliderShield;
    public Slider sliderBonus;

    [Space ()]

    public GameObject prefabStar;

    [Space ()]

    public Color[] backgroundColors;

    [Space ()]

    [TextArea ()]
    public string[] startMessages;
    public TextMesh startMessageTextMesh;

    private Utility utility;

    void Start ()
    {
        // Init vars
        utility = Utility.GetInstance ();
        
        // Random start message
        startMessageTextMesh.text = startMessages [Random.Range (0, startMessages.Length)];

        // Random background color
        touchCamera.backgroundColor = backgroundColors [Random.Range (0, backgroundColors.Length)];
    }

    void Update ()
    {
        // Pressed pause/back button, pause game
        if (Input.GetButtonDown ("Cancel")) {
            pause ();
        }

        // Game is running
        if (state == ModeStates.Run) {

            // Increase game time
            time += Time.deltaTime;

            // Update time text (UI)
            textTime.text = getTimeFormat ();
        }

        // If player's ship is destroyed
        if (!player.ship && state != ModeStates.Lose) {

            // Lose
            lose ();
        }

        // If press PageUp take a screenshot
        if (Input.GetKeyDown (KeyCode.PageUp)) {
            utility.takeScreenshot ();
        }
    }

    /**
     * Make player lose
     */
    void lose ()
    {
        // Update state
        state = ModeStates.Lose;

        // Stop music
        utility.sceneMusic.Stop ();

        // Destroy all spawners
        if (spawners) {
            Destroy (spawners.gameObject);
        }

        // Show loss screen after a moment
        Invoke ("gameOver", 2);
    }

    /**
     * Show loss UI and stats
     */
    void gameOver ()
    {
        // Check if lost
        if (state != ModeStates.Lose) {
            return;
        }

        // Update UI since state is updated
        updateUI ();

        // Calculate score
        score += (int)(time * timeScoreFactor);
        score += stars * 100;

        // Update results
        GameObject.Find ("Time Result Text").GetComponent<Text> ().text = "Time\n" + getTimeFormat ();
        GameObject.Find ("Star Result Text").GetComponent<Text> ().text = "Stars\n+" + stars;
        GameObject.Find ("Score Result Text").GetComponent<Text> ().text = score.ToString ();

        // Save stars to storage
        Storage.Stars += stars;

        // New high score?
        if (Storage.HighScore < score) {

            // Save high score to storage
            Storage.HighScore = score;

            // Set text so player knows
            GameObject.Find ("High Score Result Text").GetComponent<Text> ().text = "New High Score";
        }

        // Update mode score
        GameObject.Find (utility.sceneActive).GetComponent<Classes.Mode> ().saveScore (score);

        // Update save data
        Storage.Save ();
    }

    /**
     * Reload current mode (scene)
     */
    public void retry ()
    {
        utility.loadScene (utility.sceneActive);
    }

    /**
     * Exit to main menu
     */
    public void exit ()
    {
        utility.loadScene (utility.sceneMain);
    }

    /**
     * Pause game
     */
    public void pause ()
    {
        // Check if game is running
        if (state != ModeStates.Run) {
            return;
        }

        // Update state
        state = ModeStates.Pause;

        // Stop time
        Time.timeScale = 0;

        // Stop camera(s) shake
        foreach (Cam cam in cams) {
            cam.shakeDuration = 0;
        }

        // Pause music
        utility.sceneMusic.Pause ();

        // Select sound
        utility.selectSound.Play ();

        // Toggle UIs
        updateUI ();
    }

    /**
     * Resume game
     */
    public void resume ()
    {
        // Check if game is paused
        if (state != ModeStates.Pause) {
            return;
        }

        // Update state
        state = ModeStates.Run;

        // Resume time
        Time.timeScale = 1;

        // Pause music
        utility.sceneMusic.UnPause ();

        // Select sound
        utility.selectSound.Play ();

        // Toggle UIs
        updateUI ();
    }

    /**
     * Update UI object's status based on game state.
     */
    public void updateUI ()
    {
        pauseUI.SetActive (state == ModeStates.Pause);
        runUI.SetActive (state != ModeStates.Lose);
        loseUI.SetActive (state == ModeStates.Lose);
    }

    /**
     * Give stars with star objects
     */
    public void giveStar (int amount, Vector3 position)
    {
        // Add stars
        stars += amount;

        // Spawn star objects for player
        for (int i = 0; i <= amount; i++) {

            // Spawn star
            Rigidbody2D instance = Instantiate (prefabStar, position, new Quaternion ()).GetComponent<Rigidbody2D> ();

            // Add random force to it
            instance.AddForce (new Vector2 (Random.Range (-5, 5), Random.Range (-5, 5)) * 50);
        }
    }

    /**
     * Give score and create text float
     */
    public void giveScore (int amount, Vector3 position)
    {
        // Add stars
        score += amount;

        // Text float
        utility.createTextFloat ("+" + amount, utility.colorScore, position);
    }

    /**
     * Get time in 0:00 format.
     */
    public string getTimeFormat ()
    {
        // Get game time minutes and seconds
        int minutes = Mathf.FloorToInt (time / 60);
        int seconds = Mathf.FloorToInt (time % 60);

        // Return formatted text like 0:01
        return string.Format ("{0:0}:{1:00}", minutes, seconds);
    }
}

