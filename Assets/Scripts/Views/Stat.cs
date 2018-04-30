using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stat : MonoBehaviour
{
    public Text starsText;
    public Text highScoreText;

    public Text shipText;
    public Image shipModelImage;
    public Text shipHitpoints;
    public Text shipDamage;
    public Text shipFireRate;
    public Text shipFirePower;
    public Text shipShield;

    private Utility utility;
    private Ship ship;

    void Awake ()
    {
        // Get inits
        utility = Utility.GetInstance ();

        // Show stats
        updateStats ();
    }

    void OnEnable ()
    {
        // Current ship is changed
        if (ship != utility.getSelectedShip ()) {

            // Get current ship and setup upgrade items
            ship = utility.getSelectedShip ();
            setupShipStats ();
        }
    }

    /**
     * Show/update stats
     */
    void updateStats ()
    {
        starsText.text = Storage.Stars.ToString ();
        highScoreText.text = Storage.HighScore.ToString ();
    }

    /**
     * Show/update current (selected) ship stats
     */
    void setupShipStats ()
    {
        // Load ship upgrades
        ship.loadUpgrades ();

        // Set ship properties
        shipText.text = ship.GetComponentInChildren<SpriteRenderer> ().sprite.name;
        shipModelImage.sprite = ship.GetComponentInChildren<SpriteRenderer> ().sprite;
        shipHitpoints.text = (ship.hitpoint.maxHitpoints + ship.getUpgrade ("hitpoint").getAmount ()).ToString ();
        shipDamage.text = (ship.fireDamage + ship.getUpgrade ("damage").getAmount ()).ToString ();
        shipFireRate.text = (ship.fireRate + ship.getUpgrade ("fire-rate").getAmount ()).ToString ();
        shipFirePower.text = (ship.firePower + ship.getUpgrade ("fire-power").getAmount ()).ToString ();
        shipShield.text = ship.shield.maxDuration + " Sec";
    }
}
