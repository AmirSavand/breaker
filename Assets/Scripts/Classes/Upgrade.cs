using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    /**
     * Unique identifier for storage key
     */
    public string slug;

    /**
     * Title of the upgrade (used in UI)
     */
    public string title;

    /**
     * Information about the upgrade (used in UI)
     */
    public string description;

    /**
     * Upgrade icon (used in UI)
     */
    public Sprite icon;

    /**
     * Upgrades to this amount
     */
    public float amount;

    /**
     * Base price of upgrade
     */
    public int price;

    /**
     * Increase the base price by stock
     */
    public int pricePerStock;

    /**
     * Times this upgrade is purchased
     */
    public int stock;

    /**
     * Maximum times this upgrade can be purchased
     */
    public int maxStock;

    void Start ()
    {
        // Load stock from storage
        stock = Mathf.Clamp (PlayerPrefs.GetInt (getStorageKey ()), 0, maxStock);
    }

    /**
     * Upgrade and save to storage
     */
    public void upgrade ()
    {
        // Upgrade available
        if (!isOutOfStock () && isAffordable ()) {

            // Decrease stars
            Storage.Stars -= getPrice ();

            // Increase stock
            stock++;

            // Save to storage
            PlayerPrefs.SetInt (getStorageKey (), stock);

            // Save storage
            Storage.Save ();

            // Succesfully upgraded
            return;
        }

        // Out of stock or not enough stars
        return;
    }

    /**
     * Get price based on stock and price per stock
     */
    public int getPrice ()
    {
        return price + (stock * pricePerStock);
    }

    /**
     * Get upgraded amount based on stock
     */
    public float getAmount ()
    {
        return (float)stock * amount;
    }

    /**
     * Calculate the percentage of current stock and max stocks
     */
    public float getStockPercentage ()
    {
        return (float)stock / (float)maxStock * 100;
    }

    /**
     * Add a prefix and return storage key
     */
    public string getStorageKey ()
    {
        return "upgrade-" + slug;
    }

    /**
     * Can upgrade to next level?
     */
    public bool isOutOfStock ()
    {
        return stock == maxStock;
    }

    /**
     * Does playe have enough stars
     */
    public bool isAffordable ()
    {
        return Storage.Stars >= getPrice ();
    }
}
