using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public Game game;
    public GameObject upgradeList;
    public GameObject upgradeButton;

    public Color availableColor;
    public Color unavailableColor;
    public Color maxColor;

    private Upgrade[] currentShipUpgrades;
    private Dictionary<Button, Upgrade> upgradeButtons = new Dictionary<Button, Upgrade> ();

    void OnEnable ()
    {
        // Get upgrades of current ship
        currentShipUpgrades = GameObject.Find ("Upgrades/" + game.ships [Storage.Ship].name).GetComponentsInChildren<Upgrade> ();

        // Delete existing (old) upgrade buttons
        foreach (KeyValuePair<Button, Upgrade> item in upgradeButtons) {
            Destroy (item.Key.gameObject);
        }

        // Reset upgrade buttons dict
        upgradeButtons.Clear ();
            
        // Create all upgrade buttons
        foreach (Upgrade upgrade in currentShipUpgrades) {

            // Add button
            Button button = Instantiate (upgradeButton, upgradeList.transform).GetComponent<Button> ();

            // Store button
            upgradeButtons.Add (button, upgrade);

            // Set button values
            updateButton (upgrade, button);

            // Set button events
            button.onClick.AddListener (upgrade.upgrade);
            button.onClick.AddListener (updateButtonStates);
            button.onClick.AddListener (() => updateButton (upgrade, button));
        }

        // Update all button states
        updateButtonStates ();
    }

    /**
     * Update button texts
     */
    void updateButton (Upgrade upgrade, Button button)
    {
        // Set price
        button.transform.GetComponentInChildren<Text> ().text = upgrade.getPrice ().ToString ();

        // Set bar
        button.transform.GetComponentInChildren<Slider> ().value = upgrade.getStockPercentage ();

        // Set icon
        button.transform.Find ("Container/Icon").GetComponent<Image> ().sprite = upgrade.icon;

        // Is fully upgraded
        if (upgrade.isOutOfStock ()) {

            // Hide cost
            button.transform.Find ("Container/Cost").gameObject.SetActive (false);

            // Show full text
            button.transform.Find ("Container/Full").gameObject.SetActive (true);
        }
    }

    /**
     * Update all upgrade buttons state and color only
     */
    void updateButtonStates ()
    {
        // All buttons
        foreach (KeyValuePair<Button, Upgrade> item in upgradeButtons) {

            // Get button and its upgrade
            Button button = item.Key;
            Upgrade upgrade = item.Value;

            // Set avilable color
            button.GetComponent<Image> ().color = availableColor;

            // Is fully upgraded
            if (upgrade.isOutOfStock ()) {

                // Disable click
                button.interactable = false;

                // Set color to max
                button.GetComponent<Image> ().color = maxColor;
            }

            // Available and affordable
            else if (!upgrade.isAffordable ()) {

                // Set color to unavailable
                button.GetComponent<Image> ().color = unavailableColor;

                // Disable click
                button.interactable = false;
            }
        }
    }
}
