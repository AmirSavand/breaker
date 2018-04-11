using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float spawnTimer = 5;
    public float spawnAfter = 0;
    public float spawnSpeed = 2;

    public float decreaseSpawnTimer;
    public float decreaseSpawnFactor;
    public float decreaseSpawnFactorMin;

    public float increaseSpeedTimer;
    public float increaseSpeedFactor;
    public float increaseSpeedFactorMax;

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
        Invoke ("spawn", spawnAfter);

        // Start decreasing spawn timer
        if (decreaseSpawnTimer > 0) {
            InvokeRepeating ("decreaseSpawn", spawnAfter, decreaseSpawnTimer);
        }

        // Start increasing spawn speed
        if (increaseSpeedTimer > 0) {
            InvokeRepeating ("increaseSpeed", spawnAfter, increaseSpeedTimer);
        }
    }

    /**
     * Spawn a random object on a random child point
     */
    void spawn ()
    {
        // Get random object and point
        Transform spawnPoint = spawnPoints [Random.Range (0, spawnPoints.Length)];
        GameObject spawnObject = spawnObjects [Random.Range (0, spawnObjects.Length)];

        // Spawn object on spawn point
        GameObject instance = Instantiate (spawnObject, spawnPoint.position, spawnPoint.rotation) as GameObject;

        // Set speed
        instance.GetComponent<Move> ().directionSpeed.y = -spawnSpeed;

        // Set random color
        if (spawnRandomColor) {
            instance.GetComponentInChildren<SpriteRenderer> ().color = spawnColors [Random.Range (0, spawnColors.Length)];
        }

        // Set random rotation
        if (spawnRandomRotation) {
            instance.transform.GetChild (0).GetComponent<Transform> ().eulerAngles = new Vector3 (0, 0, Random.Range (0, 360));
        }

        // Respawn again
        Invoke ("spawn", spawnTimer);
    }

    /**
     * Decrease timer results in faster spawns (clamp to minimum)
     */
    void decreaseSpawn ()
    {
        spawnTimer = Mathf.Clamp (spawnTimer - decreaseSpawnFactor, decreaseSpawnFactorMin, spawnTimer);
    }

    /**
     * Increase speed results in faster spawn movement (clamp to max)
     */
    void increaseSpeed ()
    {
        spawnSpeed = Mathf.Clamp (spawnSpeed + increaseSpeedFactor, spawnSpeed, increaseSpeedFactorMax);
    }
}
