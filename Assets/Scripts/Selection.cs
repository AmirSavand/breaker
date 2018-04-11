using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selection : MonoBehaviour
{
    public Game game;
    public AudioSource sound;
    public Image current;
    public Button play;
    public GameObject locked;
    public GameObject next;
    public GameObject prev;

    void Start ()
    {
        // Update buttons
        onSelect ();
    }

    public void onSelect ()
    {
        // Show current ship
        current.sprite = game.ships [Storage.Ship].GetComponentInChildren<SpriteRenderer> ().sprite;

        // Disable play button and show lock image if current ship is not unlocked
        play.interactable = game.ships [Storage.Ship].GetComponent<Ship> ().isUnlocked ();
        locked.SetActive (!play.interactable);

        // If current ship is the first, hide prev button
        prev.SetActive (Storage.Ship != 0);

        // If current ship is the last, hide next button
        next.SetActive (Storage.Ship != game.ships.Length - 1);

        // Play sound
        sound.Play ();
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
        onSelect ();
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
        onSelect ();
    }
}
