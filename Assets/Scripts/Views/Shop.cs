using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Shop : MonoBehaviour
{
    [TextArea ()]
    public string shipsInfo;
    
    public Text starsText;

    public GameObject upgradeList;
    public GameObject upgradeObject;

    public GameObject shipList;
    public GameObject shipObject;

    public Color unavailableColor;
    public Color maxColor;

    private Utility utility;

    private Ship currentShip;

    private Upgrade[] currentShipUpgrades;
    private Dictionary<GameObject, Upgrade> upgradeObjects = new Dictionary<GameObject, Upgrade> ();

    private Item[] shipItems;
    private Dictionary<GameObject, Item> shipObjects = new Dictionary<GameObject, Item> ();

    void Awake ()
    {
        // Get inits
        utility = Utility.GetInstance ();
    }

    void Start ()
    {
        // Called only once
        setupShips ();
    }

    void OnEnable ()
    {
        // Current ship is changed
        if (currentShip != utility.getSelectedShip ()) {

            // Get current ship and setup upgrade items
            currentShip = utility.getSelectedShip ();
            setupUpgrades ();
        }

        // Called everytime shop is viewed
        updateStars ();
        updateUpgradeButtonsState ();
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
        currentShipUpgrades = GameObject.Find ("Upgrades/" + currentShip.name).GetComponentsInChildren<Upgrade> ();

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
            Button button = instance.transform.Find ("Container/Button").GetComponent<Button> ();

            // Store button
            upgradeObjects.Add (instance, upgrade);

            // Set button values
            updateUpgradeButton (instance, upgrade);

            // Set button events
            button.onClick.AddListener (upgrade.upgrade);
            button.onClick.AddListener (updateStars);
            button.onClick.AddListener (updateUpgradeButtonsState);
            button.onClick.AddListener (() => updateUpgradeButton (instance, upgrade));

            // Set info button event
            instance.transform.Find ("Info Button").GetComponent<Button> ().onClick.AddListener (upgrade.popup);
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
        bool isLocked = !currentShip.isUnlocked () || upgrade.title == "Locked";

        // Set price, bar and icon
        upgradeObject.GetComponentInChildren<Text> ().text = upgrade.getPrice ().ToString ();
        upgradeObject.transform.Find ("Container/Icon/Level").GetComponent<Image> ().fillAmount = upgrade.getStockPercentage () / 100;
        upgradeObject.transform.Find ("Container/Icon").GetComponent<Image> ().sprite = upgrade.icon;

        // Shop the right button text
        button.transform.Find ("Cost").gameObject.SetActive (!isLocked && !upgrade.isOutOfStock ());
        button.transform.Find ("Full").gameObject.SetActive (!isLocked && upgrade.isOutOfStock ());
        button.transform.Find ("Lock").gameObject.SetActive (isLocked);
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

            // Is fully upgraded or locked
            if (upgrade.isOutOfStock ()) {

                // Disable click
                button.interactable = false;

                // Set disabled color to max color
                colors.disabledColor = maxColor;
            }

            // Is not affordable or not unlocked
            else if (!upgrade.isAffordable () || !currentShip.isUnlocked () || upgrade.title == "Locked") {

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
            instance.transform.Find ("Content/Lock").gameObject.SetActive (!item.unlocked);
            instance.transform.Find ("Content/Unlock").gameObject.SetActive (item.unlocked);

            // Set icon, name and description
            instance.transform.Find ("Content/Icon").GetComponent<Image> ().sprite = item.icon;
            instance.transform.Find ("Content/Detail/Name").GetComponent<Text> ().text = item.title;
            instance.transform.Find ("Content/Detail/Description").GetComponent<Text> ().text = item.description;
        }
    }

    /**
     * Show ships info
     */
    public void showShipsInfo ()
    {
        utility.createPopup ("Brekaers", shipsInfo);
    }
}
