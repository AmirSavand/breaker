using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
	public float damage = 100;

	void OnTriggerEnter2D (Collider2D other)
	{
		// Get HP controller
		Hitpoint hitpoint = other.GetComponent<Hitpoint> ();

		// If target has hitpoint
		if (hitpoint) {

			// Deal damage to target
			hitpoint.damage (damage);

			// Destroy bullet (self) anyway
			Destroy (gameObject);
		}
	}
}
