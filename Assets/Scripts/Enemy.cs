using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float switchMoveTimer = 5;

    private Game game;
    private Ship ship;
    private MoveTo moveTo;
    private GameObject[] movePoints;

    void Start ()
    {
        // Init ship
        game = GameObject.FindGameObjectWithTag ("Game").GetComponent<Game> ();
        ship = Instantiate (game.enemyShips [Random.Range (0, game.enemyShips.Length)], transform).GetComponent<Ship> ();
        moveTo = GetComponent<MoveTo> ();
        movePoints = GameObject.FindGameObjectsWithTag ("Enemy Point");

        // Switch target (and repeat)
        switchMoveTarget ();
        InvokeRepeating ("switchMoveTarget", 0, switchMoveTimer);
    }

    void Update ()
    {
        // Destry enemy if has no ship
        if (!ship) {
            Destroy (gameObject);
            return;
        }

        // Always fire
        ship.fire ();

        // Look at player
        ship.lookAt (game.player.transform);
    }

    /**
     * Find a random movePoints transform to move to
     */
    public void switchMoveTarget ()
    {
        moveTo.target = movePoints [Random.Range (0, movePoints.Length)].transform;
    }
}
