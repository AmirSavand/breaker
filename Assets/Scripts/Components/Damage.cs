using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
	public float damage = 100;

	public List<Collider2D> whiteList;

	void OnTriggerEnter2D (Collider2D other)
	{
		// If it's in white list
		if (whiteList.Contains (other)) {
			return;
		}
		
		// Get HP controller
		Hitpoint hitpoint = other.GetComponentInParent<Hitpoint> ();

		// If target has hitpoint
		if (hitpoint) {

			// Deal damage to target
			hitpoint.damage (damage);

			// Destroy bullet (self) anyway
			Destroy (gameObject);
		}
	}
}
