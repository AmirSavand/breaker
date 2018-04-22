using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Destroy : MonoBehaviour
{
    public bool destroyAfterParticle = false;

    public bool destroyExitTriggers = false;
    public string destroyExitTriggersTagOnly;

    public GameObject destroyObjectOnClick;
    public Button button;

    void Start ()
    {
        // Destroy after particle duration
        if (destroyAfterParticle) {
            Destroy (gameObject, GetComponent<ParticleSystem> ().main.duration);
        }

        // Set button to destroy game object
        if (destroyObjectOnClick) {
            button.onClick.AddListener (destroyObject);
        }
    }

    void OnTriggerExit2D (Collider2D other)
    {
        // Destroy everything that leaves
        if (!destroyExitTriggers) {
            return;
        }

        // Compare tag if set
        if (destroyExitTriggersTagOnly.Length > 0 && !other.CompareTag (destroyExitTriggersTagOnly)) {
            return;
        }
         
        // Destroy self if has rb
        if (other.GetComponent<Rigidbody2D> () != null) {
            Destroy (other.gameObject);
        }

        // Destroy parent (rigidbody is there)
        else {
            Destroy (other.transform.parent.gameObject);
        }
    }

    /**
     * Destroy the game object
     */
    public void destroyObject ()
    {
        Destroy (destroyObjectOnClick);
    }
}
