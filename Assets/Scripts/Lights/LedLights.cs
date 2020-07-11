using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LedLights : MonoBehaviour
{
    public Slider slider;

    // Default LED values
    const int range     = 4;
    const int intensity = 15;
    Color color = Color.red;

    // Start is called before the first frame update
    void Start()
    {
        //Adds a listener to the main slider and invokes a method when the value changes.
        slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });

        // Setup LEDs to default values
        DisableLEDs();
        SetRange(range);
        SetColor(color);
        SetIntensity(intensity);
    }

    public void ValueChangeCheck()
    {
        SetIntensity((int)slider.value);
        
    }

    void EnableLEDs()
    {
        for (int i = 0; i < 64; i++)
            gameObject.transform.GetChild(i).GetChild(0).GetComponent<Light>().enabled = true;
    }

    void DisableLEDs() 
    {
        for (int i = 0; i < 64; i++)
            gameObject.transform.GetChild(i).GetChild(0).GetComponent<Light>().enabled = false;
    }

    void SetColor(Color color)
    {
        for (int i = 0; i < 64; i++)
            gameObject.transform.GetChild(i).GetChild(0).GetComponent<Light>().color = color;
    }

    void SetIntensity(int intensity)
    {
        for (int i = 0; i < 64; i++)
            gameObject.transform.GetChild(i).GetChild(0).GetComponent<Light>().intensity = intensity;
    }

    void SetRange(int range)
    {
        for (int i = 0; i < 64; i++)
            gameObject.transform.GetChild(i).GetChild(0).GetComponent<Light>().range = range;
    }
}
