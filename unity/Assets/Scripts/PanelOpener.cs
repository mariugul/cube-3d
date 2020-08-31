using System;
using UnityEngine;
using UnityEngine.UI;

public class PanelOpener : MonoBehaviour
{
    public GameObject panel;
    public GameObject editor;

    public void TogglePanel()
    {
        bool editorIsActive   = editor.activeSelf;

        if (panel != null)
        {
            // Set state to the oposite
            panel.SetActive(!panel.activeSelf);
        }

        if (editor != null)
        { 
            // Settings have just been opened
            if (panel.activeSelf)
            {
                // Switch off code editor 
                editor.SetActive(false);
            }

            // Settings was just closed
            else
            {
                // Switch on code editor
                editor.SetActive(true);
            }
        }
    }
}
