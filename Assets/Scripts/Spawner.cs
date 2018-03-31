using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float spawnTimer = 5;
    public float spawnAfter = 0;
    public bool spawnRandomRotation = true;
    public bool spawnRandomColor = true;
    public GameObject[] spawnObjects;
    public Color[] spawnColors;

    private Transform[] spawnPoints;

    void Start ()
    {
        // Get spawn points (in children)
        spawnPoints = GetComponentsInChildren<Transform> ();

        // Start spawining
        InvokeRepeating ("spawn", spawnAfter, spawnTimer);
    }

    void spawn ()
    {
        // Get random object and point
        Transform spawnPoint = spawnPoints [Random.Range (0, spawnPoints.Length)];
        GameObject spawnObject = spawnObjects [Random.Range (0, spawnObjects.Length)];

        // Spawn object on spawn point
        GameObject spawnInstance = Instantiate (spawnObject, spawnPoint.position, spawnPoint.rotation) as GameObject;

        // Set random color
        if (spawnRandomColor) {
            spawnInstance.GetComponentInChildren<SpriteRenderer> ().color = spawnColors [Random.Range (0, spawnColors.Length)];
        }

        // Set random rotation
        if (spawnRandomRotation) {
            spawnInstance.transform.GetChild (0).GetComponent<Transform> ().eulerAngles = new Vector3 (0, 0, Random.Range (0, 360));
        }
    }
}
