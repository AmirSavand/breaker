using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{
    public GameObject shield;
    public bool active = false;

    private Hitpoint hitpoint;
    private Vector3 activeScale;
    private Vector3 deactiveScale;

    void Awake ()
    {
        // Init vars
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
        // Scale to active or deactive size
        Vector3 scaleTo = active ? activeScale : deactiveScale;

        // Smooth scaling
        shield.transform.localScale = Vector3.Lerp (shield.transform.localScale, scaleTo, 10 * Time.deltaTime);

        // Make shit invulnerable
        hitpoint.isInvulnerable = active;
    }
}
