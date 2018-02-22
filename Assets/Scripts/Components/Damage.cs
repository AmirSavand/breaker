using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
	public float damage = 100;

	public GameObject issuer;

	public List<Collider2D> whiteList;

	private Hitpoint hitpoint;

	void Start ()
	{
		// Get inits
		hitpoint = GetComponent<Hitpoint> ();
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		// If it's in white list
		if (whiteList.Contains (other)) {
			return;
		}
		
		// Get HP controller
		Hitpoint otherHitpoint = other.GetComponentInParent<Hitpoint> ();

		// If target has hitpoint
		if (otherHitpoint) {

			// Deal damage to target
			otherHitpoint.damage (damage, issuer);

			// If self has hitpoint too (like an object like rock)
			if (hitpoint) {

				// Kill by hitpoint
				hitpoint.damage (hitpoint.hitpoints, gameObject);
			}

			// No self hitpoint (like a bullet)
			else {
				
				// Destroy self
				Destroy (gameObject);
			}
		}
	}
}
