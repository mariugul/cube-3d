using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitApplication : MonoBehaviour
{
    // Exit button
    //public Button exitButton;

    // Start is called before the first frame update
    void Start()
    {
        // Add click function to button
        //Button exitBtn = exitButton.GetComponent<Button>();
        //exitBtn.onClick.AddListener(OnButtonClick);

    }

    // On button click
    private void OnButtonClick()
    {
        Application.Quit();
        Debug.Log("Exiting Application.");
    }
}
