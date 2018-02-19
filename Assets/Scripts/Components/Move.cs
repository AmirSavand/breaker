using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
	public float speed = 100;
	public Rigidbody2D rb;

	void FixedUpdate ()
	{
		// Move forward
		rb.velocity = transform.up * speed * Time.fixedDeltaTime;
	}
}
