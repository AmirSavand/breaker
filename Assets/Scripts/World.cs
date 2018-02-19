using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
	public float spawnTimer = 5;
	public Transform[] spawnPoints;
	public GameObject[] spawnObjects;
	public Color[] spawnColors;

	void Start ()
	{
		// Start spawining
		InvokeRepeating ("spawn", 0, spawnTimer);
	}

	void OnTriggerExit2D (Collider2D other)
	{
		// Destroy everything that leaves the world
		Destroy (other.gameObject);
	}

	public void spawn ()
	{
		// Random spawn object, point and color
		GameObject spawnObject = spawnObjects [Random.Range (0, spawnObjects.Length)];
		Transform spawnPoint = spawnPoints [Random.Range (0, spawnPoints.Length)];
		Color spawnColor = spawnColors [Random.Range (0, spawnColors.Length)];

		// Spawn object on spawn point
		GameObject spawnInstance = Instantiate (spawnObject, spawnPoint.position, spawnPoint.rotation) as GameObject;

		// Set object color
		spawnInstance.GetComponent<SpriteRenderer> ().color = spawnColor;
	}
}
