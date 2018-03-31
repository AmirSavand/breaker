using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float firePower = 2;
    public float fireDamage = 100;
    public float fireLifetime = 1;
    public float fireRate = 1;
    private float lastTimeFired;
    public float laserLifeTime;
    public Transform fireFrom;
    public GameObject fireBullet;
    public GameObject laser;
    public AudioSource fireSound;

    void Update ()
    {
        // Click and fire
        if (Input.GetMouseButton (0)) {

            // Fire bullet
            fire ();
        }
    }

    void FixedUpdate ()
    {
        // Turn up
        transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (0, 0, 0), Time.deltaTime);
    }

    public void fire ()
    {
        // Call to rotate to mouse
        rotate ();

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

        // Limit fire lifetime
        Destroy (bullet, fireLifetime);

        // Fire rate cooldown (save last time)
        lastTimeFired = Time.time;
    }

    public void enableLaser ()
    {
        laser.SetActive (true);
        Invoke ("disableLaser", laserLifeTime);
    }

    public void disableLaser ()
    {
        laser.SetActive (false);
    }

    public void rotate ()
    {
        // Rotate to click position
        Vector3 mouseScreen = Input.mousePosition;
        Vector3 mouse = Camera.main.ScreenToWorldPoint (mouseScreen);
        transform.rotation = Quaternion.Euler (0, 0, Mathf.Atan2 (mouse.y - transform.position.y, mouse.x - transform.position.x) * Mathf.Rad2Deg - 90);
    }
}
