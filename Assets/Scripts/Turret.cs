using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turret : MonoBehaviour
{
    // Fire
    public float firePower = 10;
    public float fireDamage = 50;
    public float fireRate = 2f;

    public Transform fireFrom;
    public GameObject fireBullet;
    public AudioSource fireSound;

    private float lastTimeFired;

    // Components
    public Hitpoint hitpoint;

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

        // Add turret hitpoints to white list of bullet damage
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
