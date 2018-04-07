using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selection : MonoBehaviour
{
    public Game game;
    public Image current;
    public GameObject next;
    public GameObject prev;

    void Start ()
    {
        // Update buttons
        updateButtons ();
    }

    public void updateButtons ()
    {
        // Show current ship
        current.sprite = game.ships [Storage.Ship].GetComponentInChildren<SpriteRenderer> ().sprite;

        // If current ship is the first, hide prev button
        prev.SetActive (Storage.Ship != 0);

        // If current ship is the last, hide next button
        next.SetActive (Storage.Ship != game.ships.Length - 1);
    }

    /**
     * Show next selection
     */
    public void showNext ()
    {
        // Set current ship to next one
        Storage.Ship++;
        Storage.Save ();

        // Update buttons state
        updateButtons ();
    }

    /**
     * Show prev selection
     */
    public void showPrev ()
    {
        // Set current ship to prev one
        Storage.Ship--;
        Storage.Save ();

        // Update buttons state
        updateButtons ();
    }
}
