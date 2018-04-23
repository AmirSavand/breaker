using UnityEngine;
using System.Collections;

namespace Classes
{
    [AddComponentMenu ("Classes/Mode")]
    public class Mode : MonoBehaviour
    {
        /**
         * Unique identifier for storage key
         */
        public string slug;

        /**
         * Name of the scene of this mode
         */
        public string sceneName;

        /**
         * Name of the mode to show player
         */
        public string modeName;

        /**
         * High score of this mode
         */
        public int highScore;

        /**
         * Minimum score to get bronze trophy
         */
        public int scoreBronze;

        /**
         * Minimum score to get silver trophy
         */
        public int scoreSilver;

        /**
         * Minimum score to get gold trophy
         */
        public int scoreGold;

        /**
         * Mode lock status
         */
        public bool isUnlocked;

        /**
         * Check if bronze trophy is earned
         */
        public bool isBronzeUnlocked ()
        {
            return highScore >= scoreBronze;
        }

        /**
         * Check if silver trophy is earned
         */
        public bool isSilverUnlocked ()
        {
            return highScore >= scoreSilver;
        }

        /**
         * Check if silver trophy is earned
         */
        public bool isGoldUnlocked ()
        {
            return highScore >= scoreGold;
        }

        /**
         * Save score to storage
         */
        public void saveScore (int score, bool commit = false)
        {
            // New high score?
            if (highScore < score) {
                
                // Save to storage
                highScore = score;
                PlayerPrefs.SetInt (slug, score);

                // Commit data
                if (commit) {
                    Storage.Save ();
                }
            }
        }

        void Start ()
        {
            // Load high score from storage
            highScore = PlayerPrefs.GetInt (slug);
        }
    }
}

