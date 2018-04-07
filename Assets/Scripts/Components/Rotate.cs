using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Vector3 direction;

    public float speed = 100;

    void Update ()
    {
        // Rotate
        transform.Rotate (direction * speed * Time.deltaTime);
    }
}
