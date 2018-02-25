using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hitpoint : MonoBehaviour
{
	public float hitpoints = 100;
	public float maxHitpoints = 100;
	public bool isDead = false;

	public float shakeOnDeathDuration = 0.1f;

	public GameObject destroyObjectOnDeath;
	public bool destroySelfOnDeath = true;

	public GameObject pieces;
	public float piecesForce = 50;

	public int deathStars;
	public int deathScore;
	public Vector3 deathTextFloatOffset = new Vector3 (0, 0, 0);
	public bool enableDamageTextFloat = false;

	private SpriteRenderer spriteRenderer;
	private Color spriteColor;

	public bool updateHitpointText = false;

	private Cam cam;
	private Game game;

	void Start ()
	{
		// Set HP to max HP
		hitpoints = maxHitpoints;

		// Get inits
		cam = Camera.main.GetComponent<Cam> ();
		game = GameObject.FindWithTag ("Game").GetComponent<Game> ();
		spriteRenderer = GetComponentInChildren<SpriteRenderer> ();
		spriteColor = spriteRenderer.color;
	}

	public void damage (float amount)
	{	
		// Has HP (alive)
		if (hitpoints > 0) {
		
			// Change color to red
			spriteRenderer.color = Color.red;

			// Revert to original color
			Invoke ("revertColor", 0.05f);

			// Show hit text float
			if (enableDamageTextFloat) {

				// Show damage text float
				game.createTextFloat ("-" + amount, game.textFloatHitpointColor, transform.position);
			}
		}

		// Deal damage
		hitpoints = Mathf.Clamp (hitpoints -= amount, 0, maxHitpoints);

		// If has a text to update
		if (updateHitpointText) {
			game.hitpointsText.text = Mathf.FloorToInt (hitpoints / maxHitpoints * 100).ToString ();
		}

		// No HP left (dead)
		if (hitpoints == 0) {

			// If should shake camera on death
			if (shakeOnDeathDuration > 0) {
				cam.shake (shakeOnDeathDuration);
			}

			// If has death piece
			if (pieces) {

				// Activate
				pieces.SetActive (true);

				// Detach from this object
				pieces.transform.parent = null;

				// For each piece
				foreach (Rigidbody2D piece in pieces.GetComponentsInChildren<Rigidbody2D>()) {

					// Create explosion like force for that piece
					piece.AddForce (new Vector2 (Random.Range (-5, 5), Random.Range (-5, 5)) * piecesForce);

					// Set color
					piece.GetComponent<SpriteRenderer> ().color = spriteColor;

					// Detatch piece too
					piece.transform.parent = null;
				}

				// Destroy the piece holder too
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

			// Already dead but not destroyed
			if (!isDead) {

				// Gives stars on death
				if (deathStars > 0) {
					game.giveStar (deathStars, transform.position + deathTextFloatOffset);
				}

				// Gives score on death
				if (deathScore > 0) {
					game.giveScore (deathScore, transform.position + deathTextFloatOffset);
				}
			}

			// Store life status
			isDead = true;
		}
	}

	public void kill ()
	{
		// Damage as much as hitpoint
		damage (hitpoints);
	}

	private void revertColor ()
	{
		// Set to original color
		spriteRenderer.color = spriteColor;
	}
}
