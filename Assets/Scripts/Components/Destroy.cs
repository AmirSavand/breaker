using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public bool destroyAfterParticle = false;

    void Start ()
    {
        // Destroy after particle duration
        if (destroyAfterParticle) {
            Destroy (gameObject, GetComponent<ParticleSystem> ().main.duration);
        }
    }
}
