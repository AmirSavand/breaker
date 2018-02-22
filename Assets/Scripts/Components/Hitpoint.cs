using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hitpoint : MonoBehaviour
{
	public float hitpoints = 100;
	public float maxHitpoints = 100;

	public float shakeOnDeathDuration = 0.1f;

	public GameObject destroyObjectOnDeath;
	public bool destroySelfOnDeath = true;

	public GameObject pieces;
	public float piecesForce = 50;

	public int deathCoin;

	private SpriteRenderer spriteRenderer;
	private Color spriteColor;

	public Text textToUpdate;

	private Cam cam;

	void Start ()
	{
		// Set HP to max HP
		hitpoints = maxHitpoints;

		// Get inits
		spriteRenderer = GetComponentInChildren<SpriteRenderer> ();
		spriteColor = spriteRenderer.color;
		cam = Camera.main.GetComponent<Cam> ();
	}

	public void damage (float amount, GameObject issuer = null)
	{	
		// Has HP (alive)
		if (hitpoints > 0) {
		
			// Change color to red
			spriteRenderer.color = Color.red;

			// Revert to original color
			Invoke ("revertColor", 0.05f);
		}

		// Deal damage
		hitpoints = Mathf.Clamp (hitpoints -= amount, 0, maxHitpoints);

		// If has a text to update
		if (textToUpdate) {
			textToUpdate.text = Mathf.FloorToInt (hitpoints / maxHitpoints * 100).ToString ();
		}

		// No HP left (dead)
		if (hitpoints == 0) {

			// If should shake camera on death
			if (shakeOnDeathDuration > 0) {
				cam.shake (shakeOnDeathDuration);
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

			// If destroys an object on death
			if (destroyObjectOnDeath) {
				Destroy (destroyObjectOnDeath);
			}

			// Destroy if should self distruct
			if (destroySelfOnDeath) {
				Destroy (gameObject);
			}

			// Gives coins on death (and has an issuer (player))
			if (deathCoin > 0 && issuer != null) {
				issuer.GetComponent<Player> ().giveCoin (deathCoin);
			}
		}
	}

	public void kill (GameObject issuer = null)
	{
		// Damage as much as hitpoint
		damage (hitpoints, issuer);
	}

	private void revertColor ()
	{
		// Set to original color
		spriteRenderer.color = spriteColor;
	}
}
