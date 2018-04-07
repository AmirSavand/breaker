using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Views : MonoBehaviour
{
    public static Tab[] Tabs;

    void Start ()
    {
        // Get inits
        Tabs = GetComponentsInChildren<Tab> ();
    }

    public static void ResetTabs ()
    {
        // Deactivate all tabs
        foreach (Tab tab in Tabs) {
            tab.deactivate ();
        }
    }
}
