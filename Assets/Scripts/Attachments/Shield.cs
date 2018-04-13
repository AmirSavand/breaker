using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{
    public GameObject shieldObject;

    public float shieldTime;

    private Vector3 scaleTo;

    void Start ()
    {
        scaleTo = transform.localScale;
        transform.localScale = new Vector3 (0, 0, 1);
    }

    void Update ()
    {
        transform.localScale = Vector3.Lerp (transform.localScale, new Vector3 (1, 1, 1), 10 * Time.deltaTime);
    }
}
