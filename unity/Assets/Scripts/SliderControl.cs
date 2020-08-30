﻿using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    // Settings panel
    public GameObject settingsPanel;

    // Color Picker
    public ColorPicker picker;

    // Canvas to access input field
    public Canvas canvas;

    // Connected Slider Objects
    public Slider sliderBackLight;
    public Slider sliderFillLight;
    public Slider sliderKeyLight;
    public Slider sliderRoofLight;
    public Slider sliderLedIntensity;

    // Connected Toggle Objects
    public Toggle toggleBackLight;
    public Toggle toggleFillLight;
    public Toggle toggleKeyLight;
    public Toggle toggleRoofLight;

    public Toggle toggleCubeBox;
    public Toggle togglePCB;
    public Toggle toggleLEDLegs;

    public Toggle toggleInputField;

    public Toggle toggleColorPicker;

    // Buttons
    public Button exitButton;

    // Childs 
    const int light_back_child = 64;
    const int light_fill_child = 65;
    const int light_key_child  = 66;
    const int light_roof_child = 67;

    const int structure_box_child  = 68;
    const int structure_legs_child = 69;
    const int structure_pcb_child  = 70;

    // Start is called before the first frame update
    public void Start()
    {
        // Add sliders as listeners to handle 'value changed'
        sliderBackLight.onValueChanged.AddListener(delegate { SliderValueChange(); });
        sliderFillLight.onValueChanged.AddListener(delegate { SliderValueChange(); });
        sliderKeyLight.onValueChanged.AddListener (delegate { SliderValueChange(); });
        sliderRoofLight.onValueChanged.AddListener(delegate { SliderValueChange(); });
        sliderLedIntensity.onValueChanged.AddListener(delegate { SliderValueChange(); } );

        // Add toggles as listeners to handle 'value changed'
        toggleBackLight.onValueChanged.AddListener((bool value) => { CheckboxValueChange(); });
        toggleFillLight.onValueChanged.AddListener((bool value) => { CheckboxValueChange(); });
        toggleKeyLight.onValueChanged.AddListener ((bool value) => { CheckboxValueChange(); });
        toggleRoofLight.onValueChanged.AddListener((bool value) => { CheckboxValueChange(); });

        toggleCubeBox.onValueChanged.AddListener((bool value) => { CheckboxValueChange(); });
        togglePCB.onValueChanged.AddListener((bool value) => { CheckboxValueChange(); });
        toggleLEDLegs.onValueChanged.AddListener((bool value) => { CheckboxValueChange(); });

        toggleInputField.onValueChanged.AddListener((bool value) => { CheckboxValueChange(); });
        toggleColorPicker.onValueChanged.AddListener((bool value) => { CheckboxValueChange(); });

        // Add buttons as listeners to handle
        exitButton.onClick.AddListener(ButtonHandler);

    }

    // Invoked when a checkbox is clicked
    public void CheckboxValueChange()
    {
        //  Back Light Checkbox
        if (toggleBackLight.isOn)
            gameObject.transform.GetChild(light_back_child).GetComponent<Light>().enabled = true;
        else
            gameObject.transform.GetChild(light_back_child).GetComponent<Light>().enabled = false;  

        //  Fill Light Checkbox
        if (toggleFillLight.isOn)
            gameObject.transform.GetChild(light_fill_child).GetComponent<Light>().enabled = true; 
        else
            gameObject.transform.GetChild(light_fill_child).GetComponent<Light>().enabled = false;

        //  Key Light Checkbox
        if (toggleKeyLight.isOn)
            gameObject.transform.GetChild(light_key_child).GetComponent<Light>().enabled = true; 
        else
            gameObject.transform.GetChild(light_key_child).GetComponent<Light>().enabled = false; 

        //  Roof Light Checkbox
        if (toggleRoofLight.isOn)
            gameObject.transform.GetChild(light_roof_child).GetComponent<Light>().enabled = true; 
        else
            gameObject.transform.GetChild(light_roof_child).GetComponent<Light>().enabled = false;

        // Cube Box Checkbox
        if (toggleCubeBox.isOn)
            gameObject.transform.GetChild(structure_box_child).gameObject.SetActive(true);
        else
            gameObject.transform.GetChild(structure_box_child).gameObject.SetActive(false);

        // PCB checkbox
        if (togglePCB.isOn)
            gameObject.transform.GetChild(structure_pcb_child).gameObject.SetActive(true);
        else
            gameObject.transform.GetChild(structure_pcb_child).gameObject.SetActive(false);

        // LED legs checkbox
        if (toggleLEDLegs.isOn)
            gameObject.transform.GetChild(structure_legs_child).gameObject.SetActive(true);
        else
            gameObject.transform.GetChild(structure_legs_child).gameObject.SetActive(false);

        // Pattern File Display Checkbox
        int inputFieldChild = 0;

        if (toggleInputField.isOn)
            canvas.transform.GetChild(inputFieldChild).gameObject.SetActive(true);
        else
            canvas.transform.GetChild(inputFieldChild).gameObject.SetActive(false);

        // Color Picker
        if (toggleColorPicker.isOn)
            picker.gameObject.SetActive(true);
        else
            picker.gameObject.SetActive(false);

    }

    // Invoked when the value of the slider changes.
    public void SliderValueChange()
    {
        //Debug.Log("Slider Change!");

        // Set light intensity
        gameObject.transform.GetChild(light_back_child).GetComponent<Light>().intensity = sliderBackLight.value; 
        gameObject.transform.GetChild(light_fill_child).GetComponent<Light>().intensity = sliderFillLight.value; 
        gameObject.transform.GetChild(light_key_child).GetComponent<Light>().intensity  = sliderKeyLight.value;  
        gameObject.transform.GetChild(light_roof_child).GetComponent<Light>().intensity = sliderRoofLight.value;

        // Set leds intensity
        LedLights.LEDLIGHTS.SetIntensity(sliderLedIntensity.value, "leds");

        // Set halos intensity
        LedLights.LEDLIGHTS.SetIntensity(sliderLedIntensity.value / 150, "halos"); // Adjust value to match halo intensity

        // Set halos range
        int halo_range = (int)sliderLedIntensity.value;

        if (halo_range > 15)
            halo_range = 15;
        else if (halo_range < 10)
            halo_range = 10;

        LedLights.LEDLIGHTS.SetRange(halo_range, "halos"); 
    } 
    void ButtonHandler()
    {
        // Close settings panel
        settingsPanel.SetActive(false);
    }

    
}
