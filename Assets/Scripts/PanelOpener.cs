using System;
using UnityEngine;
using UnityEngine.UI;

public class PanelOpener : MonoBehaviour
{
    public GameObject panel;

    public void TogglePanel()
    {
        if (panel != null)
        {
            // Read panel state
            bool isActive = panel.activeSelf;

            // Set state to the oposite
            panel.SetActive(!isActive);
        }
    }
}
