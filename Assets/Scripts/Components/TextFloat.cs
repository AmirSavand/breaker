using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFloat : MonoBehaviour
{
    public float floatSpeed = 0.5f;
    public float fadeSpeed = 1;
    public Vector3 offset = new Vector3 (0, 0.5f, 0);

    private TextMesh textMesh;

    void Start ()
    {
        // Get ints
        textMesh = GetComponent<TextMesh> ();

        // Set z index and y offset
        transform.position += offset;
    }

    void Update ()
    {
        // Float text up
        transform.position = transform.position + transform.up * Time.deltaTime * floatSpeed;

        // Get color
        Color color = textMesh.color;

        // Fade text out
        textMesh.color = new Color (color.r, color.g, color.b, textMesh.color.a - Time.deltaTime * fadeSpeed);

        // Destroy if faded completely
        if (textMesh.color.a == 0) {
            Destroy (gameObject);
        }
    }
}
