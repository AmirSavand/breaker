using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
	public float speed = 100;

	private Rigidbody2D rigidBody;

	void Start ()
	{
		// Get inits
		rigidBody = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate ()
	{
		// Move forward
		rigidBody.velocity = transform.up * speed * Time.fixedDeltaTime;
	}
}
