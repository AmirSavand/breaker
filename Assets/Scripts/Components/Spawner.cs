using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header ("Spawn")]
    public float spawnAfter;
    public float spawnTimer = 5;
    public float spawnSpeed = 2;
    public float spawnHitpoints;
    public int spawnDeathStars = 0;

    [Header ("Spawm time")]
    public float decreaseSpawnTimer;
    public float decreaseSpawnFactor;
    public float decreaseSpawnFactorMin;

    [Header ("Spawm speed")]
    public float increaseSpeedTimer;
    public float increaseSpeedFactor;
    public float increaseSpeedFactorMax;

    [Header ("Spawm hitpoints")]
    public float increaseHitpointsTimer;
    public float increaseHitpointsFactor;
    public float increaseHitpointsFactorMax;

    [Header ("Spawm death stars")]
    public float increaseDeathStarsTimer;
    public int increaseDeathStarsFactor;
    public int increaseDeathStarsFactorMax;

    [Header ("Spawm random")]
    public bool spawnRandomBonus = false;
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
            InvokeRepeating ("decreaseSpawn", spawnAfter + decreaseSpawnTimer, decreaseSpawnTimer);
        }

        // Start increasing spawn speed
        if (increaseSpeedTimer > 0) {
            InvokeRepeating ("increaseSpeed", spawnAfter + increaseSpeedTimer, increaseSpeedTimer);
        }

        // Start increasing hitpoints
        if (increaseHitpointsTimer > 0) {
            InvokeRepeating ("increaseHitpoints", spawnAfter + increaseHitpointsTimer, increaseHitpointsTimer);
        }

        // Start increasing death star
        if (increaseDeathStarsTimer > 0) {
            InvokeRepeating ("increaseDeathStars", spawnAfter + increaseDeathStarsTimer, increaseDeathStarsTimer);
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
        Hitpoint instanceHitpoint = instance.GetComponent<Hitpoint> ();

        // Set speed
        if (spawnSpeed > 0) {
            instance.GetComponent<Move> ().directionSpeed.y = -spawnSpeed;
        }

        // Set hitpoints
        if (spawnHitpoints > 0) {

            // Find all hitpoints
            foreach (Hitpoint hitpoint in instance.GetComponentsInChildren<Hitpoint>()) {
            
                // Set hitpoints to current spawn hitpoints
                hitpoint.setMaxHitpoints (hitpoint.maxHitpoints + spawnHitpoints);
            }
        }

        // Set death star
        if (spawnDeathStars > 0 && instanceHitpoint != null && instanceHitpoint.deathStars > 0) {
            instanceHitpoint.deathStars += spawnDeathStars;
        }

        // Set random color
        if (spawnRandomColor) {
            instance.GetComponentInChildren<SpriteRenderer> ().color = spawnColors [Random.Range (0, spawnColors.Length)];
        }

        // Set random rotation
        if (spawnRandomRotation) {
            instance.transform.GetChild (0).GetComponent<Transform> ().eulerAngles = new Vector3 (0, 0, Random.Range (0, 360));
        }

        // Set random bonus (and color)
        if (spawnRandomBonus) {
            Bonus[] bonuses = GameObject.FindGameObjectWithTag ("Storage").GetComponentsInChildren<Bonus> ();
            instanceHitpoint.deathBonus = bonuses [Random.Range (0, bonuses.Length)];
            instance.GetComponentInChildren<SpriteRenderer> ().color = instanceHitpoint.deathBonus.color;
        }

        // Respawn again
        Invoke ("spawn", spawnTimer);
    }

    /**
     * Decrease spawn timer results in faster spawns (clamp to minimum)
     */
    void decreaseSpawn ()
    {
        spawnTimer = Mathf.Clamp (spawnTimer - decreaseSpawnFactor, decreaseSpawnFactorMin, spawnTimer);
    }

    /**
     * Increase speed results in faster spawn speed (clamp to max)
     */
    void increaseSpeed ()
    {
        spawnSpeed = Mathf.Clamp (spawnSpeed + increaseSpeedFactor, spawnSpeed, increaseSpeedFactorMax);
    }

    /**
     * Increase hitpoints results in tankier spawns (clamp to max)
     */
    void increaseHitpoints ()
    {
        spawnHitpoints = Mathf.Clamp (spawnHitpoints + increaseHitpointsFactor, spawnHitpoints, increaseHitpointsFactorMax);
    }

    /**
     * Increase death star (clamp to max)
     */
    void increaseDeathStars ()
    {
        spawnDeathStars = Mathf.Clamp (spawnDeathStars + increaseDeathStarsFactor, spawnDeathStars, increaseDeathStarsFactorMax);
    }
}
