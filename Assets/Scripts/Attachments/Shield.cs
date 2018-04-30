using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public GameObject shield;
    public bool active = false;
    public bool protecting = false;

    public float duration;
    public float maxDuration = 2;
    public float durationRestoreFactor = 1;
    public float durationCriticalPercentage = 10;

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

        // Protecting only if duration has at least %10
        protecting = active && getDurationPercentage () > durationCriticalPercentage;

        // Scale to (de)active size
        Vector3 scaleTo = protecting ? activeScale : deactiveScale;

        // Smooth scaling up/down
        shield.transform.localScale = Vector3.Lerp (shield.transform.localScale, scaleTo, 10 * Time.deltaTime);

        // Make ship invulnerable if shield is protecting
        hitpoint.isInvulnerable = protecting;

        // Reduce/Gain energy
        duration = Mathf.Clamp (duration + Time.deltaTime * (active ? -1 : durationRestoreFactor), 0, maxDuration);
    }

    public int getDurationPercentage ()
    {
        return (int)(duration / maxDuration * 100);
    }
}
