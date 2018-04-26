using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool isShip = true;
    public bool isTurret = false;

    public float switchMoveTimer = 5;
    public float awareDistance = 7;

    private Utility utility;
    private MoveTo moveTo;
    private GameObject[] movePoints;

    private Ship ship;
    private Turret turret;

    void Start ()
    {
        // Init vars
        utility = Utility.GetInstance ();

        // Enemy ship
        if (isShip) {

            // Init enemy ship
            ship = Instantiate (utility.shipsEnemy [Random.Range (0, utility.shipsEnemy.Length)].gameObject, transform).GetComponent<Ship> ();
            moveTo = GetComponent<MoveTo> ();
            movePoints = GameObject.FindGameObjectsWithTag ("Enemy Point");

            // Switch target (and repeat)
            switchMoveTarget ();
            InvokeRepeating ("switchMoveTarget", 0, switchMoveTimer);
        }

        // Enemy turret
        else if (isTurret) {

            // Init enemy turret
            turret = Instantiate (utility.turretsEnemy [Random.Range (0, utility.turretsEnemy.Length)].gameObject, transform).GetComponent<Turret> ();

            // Set move speed of destroyed object to current speed
            turret.transform.Find ("Model (Dead)").GetComponent<Move> ().directionSpeed = GetComponent<Move> ().directionSpeed;
        }
    }

    void LateUpdate ()
    {
        // Destry enemy if has no ship/turret
        if (!ship && !turret) {
            Destroy (gameObject);
            return;
        }

        // If game is running
        if (utility.mode.state == ModeStates.Run) {

            // Is in awareness distance?
            bool isAware = awareDistance > Vector2.Distance (utility.mode.player.ship.transform.position, transform.position);

            // Look at player and keep shooting (shoot if aware)
            if (isShip) {
                ship.lookAt (utility.mode.player.ship.transform);
                if (isAware) {
                    ship.fire ();
                }
            } else if (isTurret) {
                turret.lookAt (utility.mode.player.ship.transform);
                if (isAware) {
                    turret.fire ();
                }
            }
        }
    }

    /**
     * Find a random movePoints transform to move to
     */
    public void switchMoveTarget ()
    {
        moveTo.target = movePoints [Random.Range (0, movePoints.Length)].transform;
    }
}
