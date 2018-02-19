using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateClick : MonoBehaviour
{
	public bool autoRotate = false;

	void Update ()
	{
		// Mouse click and is auto rotate
		if (Input.GetMouseButtonDown (0) && autoRotate) {
			rotate ();
		}
	}

	public void rotate ()
	{
		// Rotate to click position
		Vector3 mouseScreen = Input.mousePosition;
		Vector3 mouse = Camera.main.ScreenToWorldPoint (mouseScreen);
		transform.rotation = Quaternion.Euler (0, 0, Mathf.Atan2 (mouse.y - transform.position.y, mouse.x - transform.position.x) * Mathf.Rad2Deg - 90);
	}
}
