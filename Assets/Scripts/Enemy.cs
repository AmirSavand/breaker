using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float switchMoveTimer = 5;

    private Utility utility;
    private Ship ship;
    private MoveTo moveTo;
    private GameObject[] movePoints;

    void Start ()
    {
        // Init ship
        utility = Utility.GetInstance ();
        ship = Instantiate (utility.shipsEnemy [Random.Range (0, utility.shipsEnemy.Length)].gameObject, transform).GetComponent<Ship> ();
        moveTo = GetComponent<MoveTo> ();
        movePoints = GameObject.FindGameObjectsWithTag ("Enemy Point");

        // Switch target (and repeat)
        switchMoveTarget ();
        InvokeRepeating ("switchMoveTarget", 0, switchMoveTimer);
    }

    void LateUpdate ()
    {
        // Destry enemy if has no ship
        if (!ship) {
            Destroy (gameObject);
            return;
        }

        // If game is running
        if (utility.mode.state == ModeStates.Run) {

            // Always fire
            ship.fire ();

            // Look at player
            ship.lookAt (utility.mode.player.transform);
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
