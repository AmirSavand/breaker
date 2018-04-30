using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Ship ship;

    public Transform movePointMiddle;
    public Transform movePointLeft;
    public Transform movePointRight;

    public Camera touchCamera;

    private Utility utility;

    void Awake ()
    {
        // Init vars
        utility = Utility.GetInstance ();

        // Load current ship
        ship = Instantiate (utility.getSelectedShip ().gameObject, transform).GetComponent<Ship> ();
    }

    void LateUpdate ()
    {
        // Game is running
        if (utility.mode.state == ModeStates.Run) {

            // On mouse click and not on ship
            if (Input.GetMouseButton (0) && !isMouseOver ()) {

                // Ship faces mouse and shoots
                faceMouse ();
                ship.fire ();
            }

            // Activate shield if has at least 1 second duration and mouse is over it
            ship.shield.active = isMouseOver () && ship.shield.duration > 0;

            // Update bonus slider (minus 1 to improve performance)
            if (ship.bonusDurationLeft > 0) {
                utility.mode.sliderBonus.value = (int)(ship.bonusDurationLeft / ship.currentBonus.duration * 100) - 1;
            }

            // Update shield slider (add 1 to improve performance)
            if (ship.shield.getDurationPercentage () != 100) {
                utility.mode.sliderShield.value = ship.shield.getDurationPercentage () + 1;
            }

            /**
             * This features is disabled for now till next update.

            // Move point based on device rotation (no rotation, stay middle)
            Transform movePoint = movePointMiddle;

            // Rotated phone enough?
            if (Mathf.Abs (Input.acceleration.x) > 0.3f) {

                // Set move point to righ/left
                movePoint = Input.acceleration.x > 0 ? movePointRight : movePointLeft;
            }

            // Set ship to move to point
            GetComponent<MoveTo> ().target = movePoint;
            */
        }
    }

    void FixedUpdate ()
    {
        // Face up 
        if (ship) {
            ship.transform.rotation = Quaternion.Lerp (ship.transform.rotation, Quaternion.Euler (0, 0, 0), Time.deltaTime);
        }
    }

    /**
     * Check if player mouse/finger is on the ship
     */
    public bool isMouseOver ()
    {
        Vector3 mouse = touchCamera.ScreenToWorldPoint (Input.mousePosition);
        mouse.z = transform.position.z;
        return GetComponent<Collider2D> ().bounds.Contains (mouse) && Input.GetMouseButton (0);
    }

    /**
     * Instantly turn and face where mouse/finger is
     */
    public void faceMouse ()
    {
        Vector3 pos = ship.transform.position;
        Vector3 input = Input.mousePosition;
        input.z = pos.z;
        Vector3 mouse = touchCamera.ScreenToWorldPoint (input);
        ship.transform.rotation = Quaternion.Euler (0, 0, Mathf.Atan2 (mouse.y - pos.y, mouse.x - pos.x) * Mathf.Rad2Deg - 90);
    }
}
