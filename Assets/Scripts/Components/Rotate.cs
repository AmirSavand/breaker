using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rotate : MonoBehaviour
{
    public Button button;
    public Vector3 direction;

    public float speed = 100;

    void Update ()
    {
        // Stop rotation if button is attached and disabled
        if (button && !button.interactable) {
            return;
        }

        // Rotate
        transform.Rotate (direction * speed * Time.deltaTime);
    }
}
