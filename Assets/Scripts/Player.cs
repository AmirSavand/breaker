﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float firePower = 500;
	public float fireDamage = 100;
	public float fireLifetime = 1;
	public float fireRate = 1;
	private float lastTimeFired;
	public Transform fireFrom;
	public GameObject fireBullet;

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
		GetComponent<RotateClick> ().rotate ();

		// Create bullet from fire from position
		GameObject bullet = Instantiate (fireBullet, fireFrom.transform.position, transform.rotation);

		// Set bullet speed to fire power
		bullet.GetComponent<Move> ().speed = firePower;

		// Limit fire lifetime
		Destroy (bullet, fireLifetime);

		// Fire rate cooldown (save last time)
		lastTimeFired = Time.time;
	}
}
