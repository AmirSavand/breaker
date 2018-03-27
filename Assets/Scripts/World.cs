using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
	void OnTriggerExit2D (Collider2D other)
	{
		// Destroy parents too
		if (other.transform.parent != null) {
			Destroy (other.transform.parent.gameObject);
		}

		// Destroy everything that leaves the world
		else {
			Destroy (other.gameObject);
		}
	}
}
