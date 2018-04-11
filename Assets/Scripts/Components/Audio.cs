using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Audio : MonoBehaviour
{
    public AudioSource sound;

    private Button button;

    void Start ()
    {
        // Init vars
        button = GetComponent<Button> ();

        // Play sound on click
        button.onClick.AddListener (sound.Play);
    }
}

