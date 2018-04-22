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
     * Current state of item (locked/0 or unlocked/1)
     */
    public int unlocked;

    void Start ()
    {
        // Load value from storage
        if (!isUnlocked ()) {
            unlocked = PlayerPrefs.GetInt (getStorageKey ());
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
        unlocked = 1;
        PlayerPrefs.SetInt (getStorageKey (), unlocked);
        Storage.Save ();
    }

    /**
     * Check if item is unlocked
     */
    public bool isUnlocked ()
    {
        return unlocked == 1;
    }

    /**
     * Show popup with info
     */
    public void popup ()
    {
        Utility.GetInstance ().createPopup (title, description);
    }
}

