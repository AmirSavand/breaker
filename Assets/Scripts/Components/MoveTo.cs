using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : MonoBehaviour
{
    public Transform target;
    public string targetTag;
    public float speed = 1;
    public float moveAfter = 0;
    public float stopRange = 0.1f;
    public bool destroyTargetOnRange = true;
    public bool destroySelfOnRange = false;
    public AudioClip[] rangeClips;
    public bool resetVelocity = false;

    private float startTime;

    void OnEnable ()
    {
        // Store starting time for moveAfter
        startTime = Time.time;

        // If should find target with tag
        if (!target && targetTag.Length > 0) {
		
            // Get target by tag
            GameObject targetObject = GameObject.FindWithTag (targetTag);

            // Found, store target
            if (targetObject) {
                target = targetObject.GetComponent<Transform> ();
            }
        }
    }

    void Update ()
    {
        // Check target and start time
        if (!target || Time.time - startTime < moveAfter) {
            return;
        }

        // No gravity/rigidbody effect
        if (resetVelocity) {
            GetComponent<Rigidbody2D> ().velocity = new Vector2 ();
        }

        // Move to position of target smoothly
        transform.position = Vector2.Lerp (transform.position, target.position, Time.deltaTime * speed);

        // Stop at a certain distance
        if (Vector2.Distance (transform.position, target.position) <= stopRange) {

            // Play reach clip
            if (rangeClips.Length > 0) {
                AudioSource.PlayClipAtPoint (rangeClips [Random.Range (0, rangeClips.Length)], transform.position);
            }

            // Destroy target if should
            if (destroyTargetOnRange) {
                Destroy (target.gameObject);
            }

            // Destroy self if should
            else if (destroySelfOnRange) {
                Destroy (gameObject);
            }

            // Set position to target instantly
            else {
                transform.position = target.position;
            }
        }
    }
}
