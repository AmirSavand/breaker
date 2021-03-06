﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public float damage = 100;
    public string damageTagOnly;

    public List<Collider2D> whiteList;

    private Hitpoint hitpoint;

    void Start ()
    {
        // Get inits
        hitpoint = GetComponent<Hitpoint> ();
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        // Skip white listed collider
        if (whiteList.Contains (other)) {
            return;
        }

        // Skip if not tag only
        if (damageTagOnly.Length > 0 && other.tag != damageTagOnly && other.transform.root.tag != damageTagOnly) {
            return;
        }
		
        // Get HP controller
        Hitpoint otherHitpoint = other.GetComponentInParent<Hitpoint> ();

        // If target has hitpoint
        if (otherHitpoint) {

            // Deal damage to target
            otherHitpoint.damage (damage);

            // If self has hitpoint too (like an object like rock)
            if (hitpoint) {

                // Kill by hitpoint
                hitpoint.damage (hitpoint.hitpoints);
            }

			// No self hitpoint (like a bullet)
			else {
				
                // Destroy self
                Destroy (gameObject);
            }
        }
    }
}
