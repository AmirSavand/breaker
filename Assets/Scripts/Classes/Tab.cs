using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tab : MonoBehaviour
{
    public Button button;
    public GameObject tabObject;
    public Color activeColor;
    public Color deactiveColor;
    public bool isActive = false;

    void Start ()
    {
        // Set onClick event
        button.onClick.AddListener (activate);
    }

    public void activate ()
    {
        // Reset all other tabs first
        Views.ResetTabs ();

        // Activate tab
        isActive = true;

        // Set to active color
        ColorBlock colors = button.colors;
        colors.normalColor = activeColor;
        colors.highlightedColor = activeColor;
        button.colors = colors;

        // Show tab object
        if (tabObject) {
            tabObject.SetActive (true);
        }
    }

    public void deactivate ()
    {
        // Activate tab
        isActive = false;

        // Set to active color
        ColorBlock colors = button.colors;
        colors.normalColor = deactiveColor;
        colors.highlightedColor = deactiveColor;
        button.colors = colors;

        // Show tab object
        if (tabObject) {
            tabObject.SetActive (false);
        }
    }
}

