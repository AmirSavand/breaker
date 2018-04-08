using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public Game game;
    public GameObject upgradeList;
    public GameObject upgradeButton;

    public Color unavailableColor;
    public Color maxColor;

    private Upgrade[] currentShipUpgrades;
    private Dictionary<GameObject, Upgrade> upgradeObjects = new Dictionary<GameObject, Upgrade> ();

    void OnEnable ()
    {
        // Get upgrades of current ship
        currentShipUpgrades = GameObject.Find ("Upgrades/" + game.ships [Storage.Ship].name).GetComponentsInChildren<Upgrade> ();

        // Delete existing (old) upgrade buttons
        foreach (KeyValuePair<GameObject, Upgrade> item in upgradeObjects) {
            Destroy (item.Key.gameObject);
        }

        // Reset upgrade buttons dict
        upgradeObjects.Clear ();
            
        // Create all upgrade buttons
        foreach (Upgrade upgrade in currentShipUpgrades) {

            // Add upgrade object and get button
            GameObject upgradeObject = Instantiate (upgradeButton, upgradeList.transform);
            Button button = upgradeObject.GetComponentInChildren<Button> ();

            // Store button
            upgradeObjects.Add (upgradeObject, upgrade);

            // Set button values
            updateButton (upgradeObject, upgrade);

            // Set button events
            button.onClick.AddListener (upgrade.upgrade);
            button.onClick.AddListener (updateButtonStates);
            button.onClick.AddListener (() => updateButton (upgradeObject, upgrade));
        }

        // Update all button states
        updateButtonStates ();
    }

    /**
     * Update button texts
     */
    void updateButton (GameObject upgradeObject, Upgrade upgrade)
    {
        // Get button
        Button button = upgradeObject.GetComponentInChildren<Button> ();

        // Set price, bar and icon
        upgradeObject.GetComponentInChildren<Text> ().text = upgrade.getPrice ().ToString ();
        upgradeObject.GetComponentInChildren<Slider> ().value = upgrade.getStockPercentage ();
        upgradeObject.transform.Find ("Container/Icon").GetComponent<Image> ().sprite = upgrade.icon;

        // Hide cost and show full text (if out of stock)
        button.transform.Find ("Cost").gameObject.SetActive (!upgrade.isOutOfStock ());
        button.transform.Find ("Full").gameObject.SetActive (upgrade.isOutOfStock ());
    }

    /**
     * Update all upgrade buttons state and color only
     */
    void updateButtonStates ()
    {
        // All buttons
        foreach (KeyValuePair<GameObject, Upgrade> item in upgradeObjects) {

            // Get vars
            Button button = item.Key.GetComponentInChildren<Button> ();
            Upgrade upgrade = item.Value;
            ColorBlock colors = button.colors;

            // Is fully upgraded
            if (upgrade.isOutOfStock ()) {

                // Disable click
                button.interactable = false;

                // Set disabled color to max color
                colors.disabledColor = maxColor;
            }

            // Available and affordable
            else if (!upgrade.isAffordable ()) {

                // Disable click
                button.interactable = false;

                // Set disabled color to unavailable color
                colors.disabledColor = unavailableColor;
            }

            // Update colors
            button.colors = colors;
        }
    }
}
