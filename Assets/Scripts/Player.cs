using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Game game;

    public Ship ship;

    public TextMesh shieldText;

    void Start ()
    {
        // Load current ship
        ship = Instantiate (game.ships [Storage.Ship], transform).GetComponent<Ship> ();

        // Set ship vars
        ship.shield.text = shieldText;
    }

    void Update ()
    {
        // On mouse click
        if (Input.GetMouseButton (0)) {

            // Check if game is running
            if (game.state != GameStates.Run) {
                return;
            }

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
    }

    void FixedUpdate ()
    {
        // Face ship up
        ship.transform.rotation = Quaternion.Lerp (ship.transform.rotation, Quaternion.Euler (0, 0, 0), Time.deltaTime);
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
