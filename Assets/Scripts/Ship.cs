using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ship : MonoBehaviour
{
    [Header ("Ship")]
    public string shipSlug;
    public string shipName;
    [Space ()]
    public bool useUpgrades = true;

    [Header ("Fire")]
    public float firePower = 6;
    public float fireDamage = 50;
    public float fireRate = 0.7f;
    public Transform fireFrom;
    public GameObject fireBullet;
    public AudioSource fireSound;

    private float lastTimeFired;

    [Header ("Attachments")]
    public Shield shield;

    private Hitpoint hitpoint;
    private Dictionary<string, Upgrade> upgrades = new Dictionary<string, Upgrade> ();

    void Start ()
    {
        // Init vars
        hitpoint = GetComponent<Hitpoint> ();
        shield = GetComponent<Shield> ();

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
}
