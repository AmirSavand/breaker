using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : MonoBehaviour
{
	public Transform target;
	public float speed = 1;
	public float stopRange = 0.1f;
	public bool destroyTarget = true;

	void Update ()
	{
		// Check target
		if (!target) {
			return;
		}
			
		// Move to position of target smoothly
		transform.position = Vector2.Lerp (transform.position, target.position, Time.deltaTime * speed);

		// Stop at a certain distance
		if (Vector2.Distance (transform.position, target.position) <= stopRange) {

			// Set position to target instantly
			transform.position = target.position;

			// Destroy target if should
			if (destroyTarget) {
				Destroy (target.gameObject);
			}
		}
	}
}
