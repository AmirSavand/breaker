using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Vector2 directionSpeed;
    public float startAfter;
    public float stopAfter;
    public bool stopMoving = false;

    void Start ()
    {
        // Start the script
        if (startAfter != 0) {
            Invoke ("start", startAfter);
        }

        // Stop the script
        if (stopAfter != 0) {
            Invoke ("stop", stopAfter);
        }
    }

    void Update ()
    {
        // Should be moving
        if (!stopMoving) {
		
            // Move to direction
            transform.position = transform.position + new Vector3 (directionSpeed.x, directionSpeed.y, 0) * Time.deltaTime;
        }
    }

    void stop ()
    {
        // Set to true so Update() won't execute anymore
        stopMoving = true;
    }

    void start ()
    {
        // Set to false so Update() executes
        stopMoving = false;
    }
}
