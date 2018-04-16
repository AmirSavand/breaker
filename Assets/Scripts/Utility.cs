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
    public Cam cam;

    [Space ()]

    // Scenes
    public string sceneActive;
    public string sceneMain;
    public string[] scenesMode;

    [Space ()]

    // Ships
    public Ship[] ships;
    public Ship[] shipsPlayer;
    public Ship[] shipsEnemy;

    [Space ()]

    // Transforms
    public Transform modals;

    [Space ()]

    // Prefabs
    public GameObject prefabTextFloat;
    public GameObject prefabModal;

    [Space ()]

    // Colors
    public Color colorStar;
    public Color colorScore;
    public Color colorHitpoint;

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
     * Load prepeared scene (scenes: sceneMain, sceneLevels).
     */
    public void loadScene (string name)
    {
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
     * Instantiate a text float
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
     * Instantiate a modal in modals
     */
    public void createModal ()
    {
        // Instantiate from prefab
        Instantiate (prefabModal, modals);
    }

    /**
     * Take a screenshot and open it (Works in standalone only).
     */
    public void takeScreenshot ()
    {
        ScreenCapture.CaptureScreenshot (System.IO.Path.Combine (Application.persistentDataPath, "screenshot.png"));
        Application.OpenURL (System.IO.Path.Combine (Application.persistentDataPath, "screenshot.png"));
    }
}
