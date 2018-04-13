using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Game game;

    public Ship ship;

    void Start ()
    {
        // Load current ship
        ship = Instantiate (game.ships [Storage.Ship], transform).GetComponent<Ship> ();
    }

    void Update ()
    {
        // On mouse click
        if (Input.GetMouseButton (0)) {

            // Check if game is running
            if (game.state != GameStates.Run) {
                return;
            }

            // Don't fire and rotate if mouse is over ship
            if (!isMouseOver ()) {
             
                // Rotate to mouse
                rotate ();

                // Fire bullet
                ship.fire ();
            }
        }
    }

    void FixedUpdate ()
    {
        // Turn up
        transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (0, 0, 0), Time.deltaTime);
    }

    void OnMouseDown ()
    {
        // Activate shield if has at least 1 second energy
        ship.shield.active = ship.shield.energy > 1;
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

    public void rotate ()
    {
        // Rotate to click position
        Vector3 mouse = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        Vector3 pos = transform.position;
        transform.rotation = Quaternion.Euler (0, 0, Mathf.Atan2 (mouse.y - pos.y, mouse.x - pos.x) * Mathf.Rad2Deg - 90);
    }
}
