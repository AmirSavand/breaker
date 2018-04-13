using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{
    public GameObject shield;
    public bool active = false;

    public float duration;
    public float maxDuration = 5;
    public float durationRestoreFactor = 1;

    private Hitpoint hitpoint;
    private Vector3 activeScale;
    private Vector3 deactiveScale;

    void Awake ()
    {
        // Init vars
        duration = maxDuration;
        hitpoint = GetComponent<Hitpoint> ();
        activeScale = shield.transform.localScale;
        deactiveScale = new Vector3 (0, 0, 1);
    }

    void OnEnable ()
    {
        // Reset shield scale
        shield.transform.localScale = new Vector3 (0, 0, 1);
    }

    void Update ()
    {
        // If active and no energy, deactivate
        if (active && duration == 0) {
            active = false;
        }

        // Scale to (de)active size
        Vector3 scaleTo = active ? activeScale : deactiveScale;

        // Smooth scaling up/down
        shield.transform.localScale = Vector3.Lerp (shield.transform.localScale, scaleTo, 10 * Time.deltaTime);

        // Make ship (in)vulnerable
        hitpoint.isInvulnerable = active;

        // Reduce/Gain energy
        duration = Mathf.Clamp (duration + Time.deltaTime * (active ? -1 : durationRestoreFactor), 0, maxDuration);
    }
}
