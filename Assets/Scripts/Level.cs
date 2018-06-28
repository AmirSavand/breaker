using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    /**
     * XP required per level factor
     */
    public static int XPLevelFactor = 1000;

    /**
     * Give experience points
     */
    public static void GiveXP (int amount)
    {
        Storage.XP = Mathf.Clamp (Storage.XP + amount, Storage.XP, Level.GetLevelUpXP ());
    }

    /**
     * Get required XP for next level (level up)
     */
    public static int GetLevelUpXP ()
    {
        return Storage.Level * Level.XPLevelFactor;
    }

    /**
     * Is next level available
     */
    public static bool CanLevelUp ()
    {
        return Storage.XP >= GetLevelUpXP ();
    }

    /**
     * Level up and reset XP
     * Returns true on success
     */
    public static bool LevelUp ()
    {
        // Check level up availablity
        if (Level.CanLevelUp ()) {

            // Reset XP and increase level
            Storage.XP = 0;
            Storage.Level++;

            // Success
            return true;
        }

        // Fail
        return false;
    }
}
