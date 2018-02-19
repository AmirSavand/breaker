using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitpoint : MonoBehaviour
{
	public float hitpoints = 100;
	public float maxHitpoints = 100;

	public bool shakeOnDeath = false;
	public float shakeOnDeathDuration = 0.1f;

	public GameObject pieces;
	public float piecesForce = 50;

	private SpriteRenderer spriteRenderer;
	private Color spriteColor;

	void Start ()
	{
		// Set HP to max HP
		hitpoints = maxHitpoints;

		// Get inits
		spriteRenderer = GetComponent<SpriteRenderer> ();
		spriteColor = spriteRenderer.color;
	}

	public void damage (float amount)
	{	
		// Deal damage
		hitpoints = Mathf.Clamp (hitpoints -= amount, 0, maxHitpoints);

		// No HP left
		if (hitpoints == 0) {

			// Shake camera
			if (shakeOnDeath) {
				Camera.main.GetComponent<CameraShake> ().shake (shakeOnDeathDuration);
			}

			// If has death peice
			if (pieces) {

				// Activate
				pieces.SetActive (true);

				// Detach from this object
				pieces.transform.parent = null;

				// For each peice
				foreach (Rigidbody2D piece in pieces.GetComponentsInChildren<Rigidbody2D>()) {

					// Create explosion like force for that peice
					piece.AddForce (new Vector2 (Random.Range (-5, 5), Random.Range (-5, 5)) * piecesForce);

					// Set color
					piece.GetComponent<SpriteRenderer> ().color = spriteColor;

					// Detatch piece too
					piece.transform.parent = null;
				}

				// Destroy the peice holder too
				Destroy (pieces);
			}

			// Kill (destroy)
			Destroy (gameObject);
		}

		// Damaged but not dead
		else {
		
			// Change color to red
			spriteRenderer.color = Color.red;

			// Revert to normal color
			Invoke ("revertColor", 0.05f);
		}
	}

	private void revertColor ()
	{
		// Set to original color
		spriteRenderer.color = spriteColor;
	}
}
