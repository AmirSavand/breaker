using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour
{
    /**
     * Unique identifier for storage key
     */
    public string slug;

    /**
     * Title of the item (used in UI)
     */
    public string title;

    /**
     * Information about the item (used in UI)
     */
    public string description;

    /**
     * Item icon (used in UI)
     */
    public Sprite icon;

    /**
     * Current state of item
     */
    public bool unlocked;

    /**
     * Player level required to unlock
     * Can not be unlocked if value is 0
     */
    public int unlockLevel;

    void Start ()
    {
        // Load value from storage
        if (!unlocked) {
            unlocked = PlayerPrefs.GetInt (getStorageKey ()) == 1 ? true : false;
        }
    }

    /**
     * Get item storage unique string
     */
    public string getStorageKey ()
    {
        return "item-" + slug;
    }

    /**
     * Unlock item and save to storage
     */
    public void unlock ()
    {
        // Save as unlocked
        unlocked = true;
        PlayerPrefs.SetInt (getStorageKey (), unlocked ? 1 : 0);
        Storage.Save ();

        // Popup
        string coloredTitle = "<color=#" + ColorUtility.ToHtmlStringRGB (Utility.GetInstance ().colorUpgrade) + ">" + title + "</color>";
        Utility.GetInstance ().createPopup ("Unlocked", "You've unlocked " + coloredTitle + "!");
    }

    /**
     * Attempt to unlock item via level
     */
    public void levelUnlock ()
    {
        // Check if can be unlocked via level
        if (!unlocked && unlockLevel > 0 && unlockLevel <= Storage.Level) {

            // Unlock it
            unlock ();
        }
    }

    /**
     * Show popup with info
     */
    public void popup ()
    {
        Utility.GetInstance ().createPopup (title, description);
    }
}
