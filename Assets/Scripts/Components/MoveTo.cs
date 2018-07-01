
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
    public bool moveStraightWhenNoTarget = false;
    public AudioClip[] rangeClips;
    public bool resetVelocity = false;
    public bool linear = false;
    public bool faceTarget = false;

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
        // Check start time
        if (Time.time - startTime < moveAfter) {
            return;
        }

        // Check target
        if (!target) {

            // Move straight
            if (moveStraightWhenNoTarget) {
                transform.position = transform.position + transform.up * speed;
            }

            // Don't continue
            return;
        }

        // Face target
        if (faceTarget) {
            Vector3 dir = target.position - transform.position;
            float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis (angle - 90, Vector3.forward);
        }

        // No gravity/rigidbody effect
        if (resetVelocity) {
            GetComponent<Rigidbody2D> ().velocity = new Vector2 ();
        }

        // Move to position of target smoothly/linear
        if (linear) {
            transform.position = Vector2.MoveTowards (transform.position, target.position, speed);
        } else {
            transform.position = Vector2.Lerp (transform.position, target.position, Time.deltaTime * speed);
        }

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
