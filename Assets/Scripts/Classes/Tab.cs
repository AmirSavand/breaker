using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tab : MonoBehaviour
{
    public Button button;
    public GameObject tabObject;

    private Utility utility;

    void Start ()
    {
        // Get init
        utility = Utility.GetInstance ();

        // Set onClick event
        button.onClick.AddListener (activate);
    }

    public void activate ()
    {
        // Reset all other tabs first
        Views.ResetTabs ();

        // Active tab (button)
        button.interactable = false;

        // Show tab object
        if (tabObject) {
            tabObject.SetActive (true);
        }

        // Play sound
        utility.selectSound.Play ();
    }

    public void deactivate ()
    {
        // Deactive tab (button)
        button.interactable = true;

        // Hide tab object
        if (tabObject) {
            tabObject.SetActive (false);
        }
    }
}

