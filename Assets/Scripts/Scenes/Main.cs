using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Handles main menu (main scene).
 */
public class Main : MonoBehaviour
{
    public Text version;

    private Utility utility;

    void Start ()
    {
        // Init vars
        utility = Utility.GetInstance ();

        // Set version to current build version
        version.text = Application.version;
    }

    /**
     * Load first mode (easy)
     */
    public void startMode ()
    {
        utility.loadScene (utility.scenesMode [0]);
    }
}

