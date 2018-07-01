using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle : MonoBehaviour
{
    public float missleSpeed = 3;
    public float missleDamage = 200;
    public float missleRate = 5;

    public Transform missleFrom;
    public GameObject missleObject;
    public AudioSource missleSound;

    private float lastTimeFired;

    void Start ()
    {
        // Fire every missleRate seconds
        InvokeRepeating ("fire", missleRate, missleRate);
    }

    /**
     * Attempt to launch a missle
     */
    public void fire ()
    {
        // Check cooldown
        if (Time.time - lastTimeFired < missleRate) {
            return;
        }

        // Fire sound
        missleSound.Play ();

        // Create missle
        GameObject instance = Instantiate (missleObject, missleFrom.transform.position, transform.rotation);

        // Get damage component
        Damage instanceDamage = instance.GetComponent<Damage> ();

        // Set missle damage
        instanceDamage.damage = missleDamage;

        // Add ship hitpoints to white list of missle damage
        instanceDamage.whiteList.AddRange (GetComponentsInChildren<Collider2D> ());

        // Set missle speed
        instance.GetComponent<MoveTo> ().speed = missleSpeed;

        // Fire rate cooldown (save last time)
        lastTimeFired = Time.time;
    }
}

