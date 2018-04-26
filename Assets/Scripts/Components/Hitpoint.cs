using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Hitpoint : MonoBehaviour
{
    [Header ("Hitpoint")]
    public float hitpoints;
    public float maxHitpoints = 100;

    public bool isInvulnerable = false;
    public bool isDead = false;

    public Color hitColor = Color.red;

    [Header ("Sound")]
    public GameObject audioHolder;
    public AudioSource hitSound;
    public AudioSource deathSound;

    [Header ("Screen shake and vibrate")]
    public float shakeOnDeathDuration = 0.1f;
    public bool vibrateOnDeath = false;

    [Header ("Destroy")]
    public GameObject destroyModel;
    public GameObject destroyObjectOnDeath;
    public bool destroySelfOnDeath = true;

    [Header ("Particle system")]
    public GameObject explosionParticle;

    [Header ("Pieces")]
    public GameObject pieces;
    public float piecesForce = 50;

    [Header ("Death rewards and texts")]
    public Bonus deathBonus;
    public int deathStars;
    public int deathScore;
    public Vector3 deathTextFloatOffset = new Vector3 (0, 0, 0);
    public bool enableDamageTextFloat = false;

    [Header ("Global text")]
    public bool updatesGlobalHitpointText = false;

    [Header ("Event")]
    public UnityEvent onDeath;

    private Utility utility;
    private SpriteRenderer spriteRenderer;
    private Color spriteColor;

    void Awake ()
    {
        // Set HP to max HP
        hitpoints = maxHitpoints;

        // Init var
        utility = Utility.GetInstance ();
    }

    void Start ()
    {
        // Init vars
        spriteRenderer = GetComponentInChildren<SpriteRenderer> ();
        spriteColor = spriteRenderer.color;
    }

    public void damage (float amount)
    {	
        // Has HP (alive)
        if (hitpoints > 0) {
		
            // Change color to red
            spriteRenderer.color = hitColor;

            // Revert to original color
            Invoke ("revertColor", 0.05f);

            // Hit sound
            if (hitSound) {
                hitSound.Play ();
            }

            // No hit text float for invulnerables
            if (!isInvulnerable) {
                
                // Show hit text float
                if (enableDamageTextFloat) {

                    // Show damage text float
                    utility.createTextFloat ("-" + amount, utility.colorHitpoint, transform.position);
                }
            }
        }

        // Deal damage (if not invulnerable)
        if (!isInvulnerable) {
            hitpoints = Mathf.Clamp (hitpoints -= amount, 0, maxHitpoints);
        }

        // Text update (UI)
        updateHitpoinText ();

        // No HP left (dead)
        if (hitpoints == 0) {

            // Is about to die
            if (!isDead) {

                // Gives stars on death
                if (deathStars > 0) {
                    utility.mode.giveStar (deathStars, transform.position + deathTextFloatOffset);
                }

                // Gives score on death
                if (deathScore > 0) {
                    utility.mode.giveScore (deathScore, transform.position + deathTextFloatOffset);
                }

                // Death sound
                if (deathSound) {

                    // Sperate the holder then destroy after audio finished
                    if (audioHolder) {
                        audioHolder.transform.parent = null;
                        Destroy (audioHolder, deathSound.clip.length);
                    }

                    // Play the audio then destroy the holder
                    deathSound.Play ();
                }

                // Death bonus
                if (deathBonus) {

                    // Apply bonus to ship
                    utility.mode.player.ship.applyBonus (deathBonus);

                    // Show text of bonus
                    utility.createTextFloat (deathBonus.floatText, deathBonus.color, transform.position);
                }

                // On death events
                onDeath.Invoke ();
            }

            // If should shake camera(s) on death
            if (shakeOnDeathDuration > 0) {
                foreach (Cam cam in utility.mode.cams) {
                    cam.shake (shakeOnDeathDuration, vibrateOnDeath);
                }
            }

            // If has death piece
            if (pieces) {

                // Activate
                pieces.SetActive (true);

                // Detach from this object
                pieces.transform.parent = null;

                // For each piece
                foreach (Rigidbody2D piece in pieces.GetComponentsInChildren<Rigidbody2D>()) {

                    // Create explosion like force for that piece
                    piece.AddForce (new Vector2 (Random.Range (-5, 5), Random.Range (-5, 5)) * piecesForce);

                    // Set color
                    piece.GetComponent<SpriteRenderer> ().color = spriteColor;

                    // Detatch piece too
                    piece.transform.parent = null;
                }

                // Destroy the piece holder too
                Destroy (pieces);
            }

            // If destroys an object on death
            if (destroyObjectOnDeath) {
                Destroy (destroyObjectOnDeath);
            }

            // Destroy if should self distruct
            if (destroySelfOnDeath) {

                // If has explosion particle
                if (explosionParticle) {
                    Instantiate (explosionParticle, transform.position, new Quaternion ());
                }

                // If jas destroy model
                if (destroyModel) {
                    destroyModel.SetActive (true);
                    destroyModel.transform.parent = null;
                }

                // Destroy self
                Destroy (gameObject);
            }

            // Store life status
            isDead = true;
        }
    }

    /**
     * Damage as much as hitpoint
     */
    public void kill ()
    {
        damage (hitpoints);
    }

    /**
     * Heal and clamp (0, max hp)
     */
    public void heal (float value)
    {
        hitpoints = Mathf.Clamp (hitpoints + value, 1, maxHitpoints);
        updateHitpoinText ();
    }

    /**
     * Set max hitpoints and current hitpoins to value
     */
    public void setMaxHitpoints (float value)
    {
        maxHitpoints = value;
        hitpoints = value;
    }

    /**
     * Update the global hitpoint text if should
     */
    public void updateHitpoinText ()
    {
        if (updatesGlobalHitpointText) {
            utility.mode.hitpointsText.text = Mathf.FloorToInt (hitpoints / maxHitpoints * 100).ToString ();
        }   
    }

    /**
     * Set to original color
     */
    void revertColor ()
    {
        spriteRenderer.color = spriteColor;
    }
}
