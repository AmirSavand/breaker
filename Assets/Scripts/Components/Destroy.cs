using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Destroy : MonoBehaviour
{
    public bool destroyAfterParticle = false;

    public float destroyAfterTime;

    public bool destroyExitTriggers = false;
    public string destroyExitTriggersTagOnly;

    public GameObject destroyObjectOnClick;
    public Button button;

    void OnEnable ()
    {
        // Destroy after particle duration
        if (destroyAfterParticle) {
            Destroy (gameObject, GetComponent<ParticleSystem> ().main.duration);
        }

        // Destroy after time
        if (destroyAfterTime > 0) {
            Destroy (gameObject, destroyAfterTime);
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
         
        // Destroy object if has RB
        if (other.GetComponent<Rigidbody2D> () != null) {
            Destroy (other.gameObject);
        }

        // Destroy parent (rigidbody is there)
        else if (other.transform.parent) {
            Destroy (other.transform.parent.gameObject);
        } 

        // Destroy object alone
        else {
            Destroy (other);
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
