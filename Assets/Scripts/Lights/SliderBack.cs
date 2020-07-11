using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class SliderBack : MonoBehaviour
{
    public Slider slider;
    public GameObject button;

    // Start is called before the first frame update
    public void Start()
    {
        //Adds a listener to the main slider and invokes a method when the value changes.
        slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    // Invoked when the value of the slider changes.
    public void ValueChangeCheck()
    {
        // Check if toggle is on for light
        if (button.GetComponent<Toggle>().enabled == true)
        {
            // Enable slider
            slider.enabled = true;

            // Set light intensity
            GetComponent<Light>().intensity = slider.value;

            Debug.Log("Button toggled off!");
        }
        else
            slider.enabled = false;

    }
    
}
