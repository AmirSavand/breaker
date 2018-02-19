using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float firePower = 500;
	public float fireDamage = 100;
	public Transform fireFrom;
	public GameObject fireBullet;

	void Update ()
	{
		// Click and fire
		if (Input.GetMouseButtonDown (0)) {

			// Fire bullet
			fire ();
		}
	}

	public void fire ()
	{
		// Call to rotate to mouse
		GetComponent<RotateClick> ().rotate ();

		// Create bullet from fire from position
		GameObject bullet = Instantiate (fireBullet, fireFrom.transform.position, transform.rotation) as GameObject;

		// Set bullet speed to fire power
		bullet.GetComponent<Move> ().speed = firePower;
	}
}
