using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ship : MonoBehaviour
{
    // Ship
    public string shipSlug;
    public string shipName;

    // Fire
    public float firePower = 6;
    public float fireDamage = 50;
    public float fireRate = 0.7f;
    public Transform fireFrom;
    public GameObject fireBullet;
    public AudioSource fireSound;
    private float lastTimeFired;

    // Components
    public Shield shield;
    public Hitpoint hitpoint;

    // Upgrades
    public bool useUpgrades = true;
    private Dictionary<string, Upgrade> upgrades = new Dictionary<string, Upgrade> ();

    // Bonus
    private Bonus currentBonus;
    private float bonusRevertValue;

    void Awake ()
    {
        // Upgrades
        if (useUpgrades) {
            loadUpgrades ();
            applyUpgrades ();
        }
    }

    /**
     * Get upgrade object by slug without ship name
     */
    public Upgrade getUpgrade (string upgradeSlug)
    {
        return upgrades [shipSlug + "-" + upgradeSlug];
    }

    /**
     * Load upgrades and store them
     */
    public void loadUpgrades ()
    {
        // Reset upgrades dict
        upgrades.Clear ();

        // Save all upgrades
        foreach (Upgrade upgrade in GameObject.Find ("Upgrades/" + shipName).GetComponentsInChildren<Upgrade> ()) {
            upgrades.Add (upgrade.slug, upgrade);   
        }
    }

    /**
     * Apply all loaded/stored upgrades
     */
    public void applyUpgrades ()
    {
        // Set all upgrades
        fireDamage += getUpgrade ("damage").getAmount ();
        firePower += getUpgrade ("fire-power").getAmount ();
        fireRate += getUpgrade ("fire-rate").getAmount ();
        hitpoint.maxHitpoints += getUpgrade ("hitpoint").getAmount ();
    }

    /**
     * Check if ship is unlocked from storage
     */
    public bool isUnlocked ()
    {
        return GameObject.Find ("Ship Items/" + name).GetComponent<Item> ().isUnlocked ();
    }

    /**
     * Attempt to fire a bullet
     */
    public void fire ()
    {
        // Check cooldown
        if (Time.time - lastTimeFired < fireRate) {
            return;
        }

        // Fire sound
        fireSound.Play ();

        // Create bullet from fire from position
        GameObject bullet = Instantiate (fireBullet, fireFrom.transform.position, transform.rotation);
        Damage bulletDamage = bullet.GetComponent<Damage> ();

        // Set bullet speed to fire power (up times power)
        bullet.GetComponent<Move> ().directionSpeed = bullet.transform.up * firePower;

        // Set bullet damage to fire damage
        bulletDamage.damage = fireDamage;

        // Add ship hitpoints to white list of bullet damage
        bulletDamage.whiteList.AddRange (GetComponentsInChildren<Collider2D> ());

        // Fire rate cooldown (save last time)
        lastTimeFired = Time.time;
    }

    /**
     * Rotate and face a target
     */
    public void lookAt (Transform target)
    {
        Vector3 dir = target.position - transform.position;
        float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis (angle - 90, Vector3.forward);
    }

    /**
     * Give and activate bonus to ship
     */
    public void applyBonus (Bonus bonus)
    {
        // Hitpoint
        if (bonus.title == "Hitpoint") {
        
            // Give hitpoint (heal)
            hitpoint.heal (bonus.amount);
        }

        // Max fire rate
        if (bonus.title == "Max Fire Rate") {

            // Set fire rate and save current
            bonusRevertValue = fireRate;
            fireRate = bonus.amount;
        }

        // High damage
        if (bonus.title == "High Damage") {

            // Set fire rate and save current
            bonusRevertValue = fireRate;
            fireRate = bonus.amount;
        }

        // Duration bonus
        if (bonus.type == BonusType.Duration) {

            // Store it
            currentBonus = bonus;

            // Revert after duration
            Invoke ("revertBonus", bonus.duration);
        }
    }

    /**
     * Revert ship stats and remove bonus
     */
    public void revertBonus ()
    {
        // Max fire rate
        if (currentBonus.title == "Max Fire Rate") {
            fireRate = bonusRevertValue;
        }

        // High damage
        if (currentBonus.title == "High Damage") {
            fireDamage = bonusRevertValue;
        }
    }
}
