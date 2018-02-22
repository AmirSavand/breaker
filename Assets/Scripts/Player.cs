using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	public float firePower = 500;
	public float fireDamage = 100;
	public float fireLifetime = 1;
	public float fireRate = 1;
	private float lastTimeFired;
	public Transform fireFrom;
	public GameObject fireBullet;

	public int coins;
	public Text coinsText;

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
		// Check cooldown
		if (Time.time - lastTimeFired < fireRate) {
			return;
		}

		// Call to rotate to mouse
		rotate ();

		// Create bullet from fire from position
		GameObject bullet = Instantiate (fireBullet, fireFrom.transform.position, transform.rotation);

		// Set bullet issuer
		bullet.GetComponent<Damage> ().issuer = gameObject;

		// Set bullet speed to fire power
		bullet.GetComponent<Move> ().speed = firePower;

		// Limit fire lifetime
		Destroy (bullet, fireLifetime);

		// Fire rate cooldown (save last time)
		lastTimeFired = Time.time;
	}

	public void rotate ()
	{
		// Rotate to click position
		Vector3 mouseScreen = Input.mousePosition;
		Vector3 mouse = Camera.main.ScreenToWorldPoint (mouseScreen);
		transform.rotation = Quaternion.Euler (0, 0, Mathf.Atan2 (mouse.y - transform.position.y, mouse.x - transform.position.x) * Mathf.Rad2Deg - 90);
	}

	public void giveCoin (int amount)
	{
		// Add coins
		coins += amount;

		// Update UI
		coinsText.text = coins.ToString ();
	}
}
