using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

/**
 * Handles all scenes and stages of the game.
 */
public class Utility : MonoBehaviour
{
    // Scripts
    public Main main;
    public Mode mode;

    [Space ()]

    // Scenes
    public string sceneActive;
    public string sceneMain;

    [Space ()]

    // Ships
    public Ship[] ships;
    public Ship[] shipsPlayer;
    public Ship[] shipsEnemy;

    [Space ()]

    // Turrets
    public Turret[] turretsEnemy;

    [Space ()]

    // Transforms
    public Transform popups;
    public Transform loading;

    [Space ()]

    // Prefabs
    public GameObject prefabTextFloat;
    public GameObject prefabPopup;


    [Space ()]

    // Colors
    public Color colorStar;
    public Color colorScore;
    public Color colorHitpoint;
    public Color colorUpgrade;
    public Color colorPositive;
    public Color colorNegative;

    [Space ()]

    // Other
    public AudioMixer audioMixer;
    public AudioSource sceneMusic;
    public AudioSource selectSound;

    void Awake ()
    {
        // Get current scene
        sceneActive = SceneManager.GetActiveScene ().name;

        // Reset time scale
        Time.timeScale = 1;
    }

    void Start ()
    {
        // Load audio mixer values from storage
        audioMixer.SetFloat ("masterVol", (float)Storage.VolumeMaster);
        audioMixer.SetFloat ("musicVol", (float)Storage.VolumeMusic);
        audioMixer.SetFloat ("effectsVol", (float)Storage.VolumEffects);
        audioMixer.SetFloat ("menuVol", (float)Storage.VolumeMenu);
    }

    void Update ()
    {
        // If press PageUp take a screenshot
        if (Input.GetKeyDown (KeyCode.PageUp)) {
            takeScreenshot ();
        }
    }

    /**
     * Get instance of Utility in scene
     */
    public static Utility GetInstance ()
    {
        return FindObjectOfType (typeof(Utility)) as Utility;
    }

    /**
     * Load a scene by name.
     */
    public void loadScene (string name)
    {
        // Show loading screen
        loading.gameObject.SetActive (true);

        // Load scene
        SceneManager.LoadScene (name);
    }

    /**
     * Get the ship script (via game) of the selected ship (via storage)
     */
    public Ship getSelectedShip ()
    {
        return shipsPlayer [Storage.Ship];
    }

    /**
     * Instantiate a text float.
     */
    public void createTextFloat (string text, Color color, Vector3 position)
    {
        // Instantiate from prefab
        TextMesh instance = Instantiate (prefabTextFloat).GetComponent<TextMesh> ();

        // Set properties
        instance.color = color;
        instance.text = text;
        instance.transform.position = position;
    }

    /**
     * Instantiate a modal in modals with sound.
     */
    public void createPopup (string title, string content)
    {
        // Instantiate from prefab
        Transform instance = Instantiate (prefabPopup, popups).GetComponent<Transform> ();

        // Set properties
        instance.transform.Find ("Dialog/Header/Text").GetComponent<Text> ().text = title;
        instance.transform.Find ("Dialog/Content/Text").GetComponent<Text> ().text = content;

        // Play sound
        selectSound.Play ();
    }

    /**
     * Take a screenshot and open it (Works in standalone only).
     */
    public void takeScreenshot ()
    {
        string fileName = "screenshot-" + System.DateTime.Now.ToString ("HH-mm-ss") + ".png";
        ScreenCapture.CaptureScreenshot (System.IO.Path.Combine (Application.persistentDataPath, fileName));
    }
}
