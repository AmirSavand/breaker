using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public Game game;
    public Text starsText;

    public GameObject upgradeList;
    public GameObject upgradeObject;

    public GameObject shipList;
    public GameObject shipObject;

    public Color unavailableColor;
    public Color maxColor;

    private Ship currentShip;

    private Upgrade[] currentShipUpgrades;
    private Dictionary<GameObject, Upgrade> upgradeObjects = new Dictionary<GameObject, Upgrade> ();

    private Item[] shipItems;
    private Dictionary<GameObject, Item> shipObjects = new Dictionary<GameObject, Item> ();

    void Start ()
    {
        setupShips ();
    }

    void OnEnable ()
    {
        // Init var
        currentShip = game.ships [Storage.Ship].GetComponent<Ship> ();

        setupUpgrades ();
        updateStars ();
    }

    /**
     * Update stars text to current stars
     */
    void updateStars ()
    {
        starsText.text = Storage.Stars.ToString ();
    }

    /**
     * Initial upgrades
     */
    void setupUpgrades ()
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
            GameObject instance = Instantiate (upgradeObject, upgradeList.transform);
            Button button = instance.GetComponentInChildren<Button> ();

            // Store button
            upgradeObjects.Add (instance, upgrade);

            // Set button values
            updateUpgradeButton (instance, upgrade);

            // Set button events
            button.onClick.AddListener (upgrade.upgrade);
            button.onClick.AddListener (updateStars);
            button.onClick.AddListener (updateUpgradeButtonsState);
            button.onClick.AddListener (() => updateUpgradeButton (instance, upgrade));
        }

        // Update all button states
        updateUpgradeButtonsState ();
    }

    /**
     * Update button texts
     */
    void updateUpgradeButton (GameObject upgradeObject, Upgrade upgrade)
    {
        // Get button
        Button button = upgradeObject.GetComponentInChildren<Button> ();

        // Set price, bar and icon
        upgradeObject.GetComponentInChildren<Text> ().text = upgrade.getPrice ().ToString ();
        upgradeObject.transform.Find ("Container/Icon/Level").GetComponent<Image> ().fillAmount = upgrade.getStockPercentage () * 100;
        upgradeObject.transform.Find ("Container/Icon").GetComponent<Image> ().sprite = upgrade.icon;

        // Shop the right button text
        button.transform.Find ("Cost").gameObject.SetActive (currentShip.isUnlocked () && !upgrade.isOutOfStock ());
        button.transform.Find ("Full").gameObject.SetActive (currentShip.isUnlocked () && upgrade.isOutOfStock ());
        button.transform.Find ("Lock").gameObject.SetActive (!currentShip.isUnlocked ());
    }

    /**
     * Update all upgrade buttons state and color only
     */
    void updateUpgradeButtonsState ()
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

            // Is not affordable or not unlocked
            else if (!upgrade.isAffordable () || !currentShip.isUnlocked ()) {

                // Disable click
                button.interactable = false;

                // Set disabled color to unavailable color
                colors.disabledColor = unavailableColor;
            }

            // Update colors
            button.colors = colors;
        }
    }

    /**
     * Initial ships
     */
    void setupShips ()
    {
        // Get ship items
        shipItems = GameObject.Find ("Ship Items").GetComponentsInChildren<Item> ();

        // Delete existing (old) item buttons
        foreach (KeyValuePair<GameObject, Item> item in shipObjects) {
            Destroy (item.Key.gameObject);
        }

        // Reset ship buttons dict
        shipObjects.Clear ();

        // Create all ship buttons
        foreach (Item item in shipItems) {

            // Add ship item
            GameObject instance = Instantiate (shipObject, shipList.transform);

            // Store it
            shipObjects.Add (instance, item);

            // Show lock/unlock
            instance.transform.Find ("Lock").gameObject.SetActive (!item.isUnlocked ());
            instance.transform.Find ("Unlock").gameObject.SetActive (item.isUnlocked ());

            // Set icon, name and description
            instance.transform.Find ("Icon").GetComponent<Image> ().sprite = item.icon;
            instance.transform.Find ("Detail/Name").GetComponent<Text> ().text = item.title;
            instance.transform.Find ("Detail/Description").GetComponent<Text> ().text = item.description;
        }
    }
}
