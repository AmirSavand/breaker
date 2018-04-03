using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ship : MonoBehaviour
{
    public string shipName;
    public string shipDetails;

    public float firePower = 6;
    public float fireDamage = 50;
    public float fireRate = 0.7f;
    public Transform fireFrom;
    public GameObject fireBullet;
    public AudioSource fireSound;

    private float lastTimeFired;

    private Hitpoint hitpoint;
    private Dictionary<string, Upgrade> upgrades = new Dictionary<string, Upgrade> ();

    void Start ()
    {
        // Init vars
        hitpoint = GetComponent<Hitpoint> ();

        // Save all upgrades
        foreach (Upgrade upgrade in GameObject.Find ("Upgrades/" + shipName).GetComponentsInChildren<Upgrade> ()) {
            upgrades.Add (upgrade.slug, upgrade);   
        }

        // Load all upgrades
        fireDamage += upgrades ["damage"].getAmount ();
        firePower += upgrades ["fire-power"].getAmount ();
        fireRate += upgrades ["fire-rate"].getAmount ();
        hitpoint.maxHitpoints += upgrades ["hitpoint"].getAmount ();
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

        // Set bullet speed to fire power (up times power)
        bullet.GetComponent<Move> ().directionSpeed = bullet.transform.up * firePower;

        // Set bullet damage to fire damage
        bullet.GetComponent<Damage> ().damage = fireDamage;

        // Fire rate cooldown (save last time)
        lastTimeFired = Time.time;
    }
}
