using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Slider levelSlider;
    public Text levelText;
    public GameObject levelUpObject;
    public AudioSource levelUpSound;
    public AudioSource levelUpAvailableSound;

    void Start ()
    {
        // Update UI
        setupUI ();

        // Level up available, play alert sound
        levelUpAvailableSound.Play ();
    }

    void setupUI ()
    {
        // Set level properties
        levelSlider.value = (float)Storage.XP / (float)Level.GetLevelUpXP ();
        levelText.text = Storage.Level.ToString ();
        levelUpObject.SetActive (Level.CanLevelUp ());
    }

    public void levelUp ()
    {
        // Level up
        if (Level.LevelUp ()) {

            // Level up sound
            levelUpSound.Play ();

            // Success, update UI
            setupUI ();

            // Show popup
            Utility.GetInstance ().createPopup ("Leveled Up", "You are now level " + Storage.Level + ".");
        }
    }
}
