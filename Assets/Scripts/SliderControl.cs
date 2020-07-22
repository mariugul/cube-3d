using UnityEngine;
using UnityEngine.UI;

public class SliderControl : MonoBehaviour
{
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

    // Childs 
    const int light_back_child = 64;
    const int light_fill_child = 65;
    const int light_key_child = 66;
    const int light_roof_child = 67;

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
    }

    // Invoked when the value of the slider changes.
    public void SliderValueChange()
    {
        // Set light intensity
        gameObject.transform.GetChild(light_back_child).GetComponent<Light>().intensity = sliderBackLight.value; 
        gameObject.transform.GetChild(light_fill_child).GetComponent<Light>().intensity = sliderFillLight.value; 
        gameObject.transform.GetChild(light_key_child).GetComponent<Light>().intensity  = sliderKeyLight.value;  
        gameObject.transform.GetChild(light_roof_child).GetComponent<Light>().intensity = sliderRoofLight.value;

        // Set LED intensity
        var leds = new LedLights();
        leds.SetIntensity((int)sliderLedIntensity.value, "leds");
    } 
    
}
