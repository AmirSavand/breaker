using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Ship ship;

    public TextMesh shieldText;
    public TextMesh bonusText;

    private Utility utility;

    void Start ()
    {
        // Init vars
        utility = Utility.GetInstance ();

        // Load current ship
        ship = Instantiate (utility.getSelectedShip ().gameObject, transform).GetComponent<Ship> ();

        // Set ship vars
        ship.shield.text = shieldText;
    }

    void Update ()
    {
        // Game is running
        if (utility.mode.state == ModeStates.Run) {

            // On mouse click
            if (Input.GetMouseButton (0)) {

                // Mouse not over the ship
                if (!isMouseOver ()) {
                    faceMouse ();
                    ship.fire ();
                    ship.shield.active = false;
                }

                // Mouse over ship
                else {

                    // Activate shield if has at least 1 second duration
                    ship.shield.active = ship.shield.duration > 0;
                }
            }

            // Has a bonus currently
            if (ship.currentBonus) {

                // Show bonus text
                bonusText.text = ship.currentBonus.title;
                bonusText.color = ship.currentBonus.color;
            }

            // Show bonus text if has one currently
            bonusText.gameObject.SetActive (ship.currentBonus);
        }
    }

    void FixedUpdate ()
    {
        // Face ship up
        if (ship) {
            ship.transform.rotation = Quaternion.Lerp (ship.transform.rotation, Quaternion.Euler (0, 0, 0), Time.deltaTime);
        }
    }

    /**
     * Check if player mouse/finger is on the ship
     */
    public bool isMouseOver ()
    {
        Vector3 mouse = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        mouse.z = transform.position.z;
        return GetComponent<Collider2D> ().bounds.Contains (mouse);
    }

    /**
     * Instantly turn and face where mouse/finger is
     */
    public void faceMouse ()
    {
        Vector3 mouse = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        Vector3 pos = ship.transform.position;
        ship.transform.rotation = Quaternion.Euler (0, 0, Mathf.Atan2 (mouse.y - pos.y, mouse.x - pos.x) * Mathf.Rad2Deg - 90);
    }
}
