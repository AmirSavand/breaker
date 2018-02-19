using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
	void OnTriggerExit2D (Collider2D other)
	{
		// Destroy everything that leaves the world
		Destroy (other.gameObject);
	}
}
