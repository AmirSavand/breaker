using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Handles main menu (main scene).
 */
public class Main : MonoBehaviour
{
    public GameObject modeList;
    public GameObject modeObject;

    public Classes.Mode[] modes;

    public Text version;

    private Utility utility;
    private bool loadedModes;

    void Start ()
    {
        // Init vars
        utility = Utility.GetInstance ();

        // Set version to current build version
        version.text = Application.version;

        // Prepare modes
        setupModes ();
    }

    /**
     * Setup modes item for selection and show the right trophy.
     */
    public void setupModes ()
    {
        // For all modes
        foreach (Classes.Mode mode in modes) {
        
            // Create mode button
            GameObject instance = Instantiate (modeObject, modeList.transform);

            // Set mode name
            instance.GetComponentInChildren<Text> ().text = mode.modeName;

            // Is locked?
            if (!mode.isUnlocked) {

                // Disable button
                instance.GetComponent<Button> ().interactable = false;

                // Remove trophies
                Destroy (instance.transform.Find ("Trophies").gameObject);
            }

            // Unlocked, show trophies
            else {

                // Button click loads mode
                instance.GetComponent<Button> ().onClick.AddListener (() => utility.loadScene (mode.sceneName));

                // Set trophies
                if (mode.isBronzeUnlocked ()) {
                    Transform trophy = instance.transform.Find ("Trophies/Bronze");
                    trophy.GetComponent<Image> ().color = Color.white;
                    trophy.GetComponent<Shadow> ().enabled = true;
                }

                if (mode.isSilverUnlocked ()) {
                    Transform trophy = instance.transform.Find ("Trophies/Silver");
                    trophy.GetComponent<Image> ().color = Color.white;
                    trophy.GetComponent<Shadow> ().enabled = true;
                }

                if (mode.isGoldUnlocked ()) {
                    Transform trophy = instance.transform.Find ("Trophies/Gold");
                    trophy.GetComponent<Image> ().color = Color.white;
                    trophy.GetComponent<Shadow> ().enabled = true;
                }
            }
        }
    }
}
