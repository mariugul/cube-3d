using System.Diagnostics;
using System.Linq.Expressions;
using UnityEngine;

public class LedLights : MonoBehaviour
{
    // Default LED values
    const int   led_range      = 4;
    const int   led_intensity  = 15;
    const int   halo_range     = 15;
    const float halo_intensity = 0.1f;

    // Childs of LED cube
    const int light_source = 0;
    const int light_halo   = 0;

    // Materials
    public Material material_red;
    public Material material_green;
    public Material material_blue;
    public Material material_yellow;
    public Material material_white;

    // LED colors
    Color led_red   = new Color(0.3764706f, 0f, 0f, 1);
    Color led_green = new Color(0.1121396f, 0.3773585f, 0.1121396f,  1);
    Color led_blue  = new Color(0.05326628f, 0.2159348f, 0.5377358f, 1);
   
    // LED light color
    Color light_red   = Color.red;
    Color light_green = Color.green;
    Color light_blue  = Color.blue;

    // Constants
    const int CUBESIZE = 64;

    void Start()
    {         
        // Set range for LEDs and halos
        SetRange(led_range,  "leds");
        SetRange(halo_range, "halos");

        // Set intensity for LEDs and halos
        SetIntensity(led_intensity, "leds");
        SetIntensity(halo_intensity, "halos");
               
        SetColor(led_red, light_red);
        
        // Disable LEDs and halos by default
        Disable("leds");
        Disable("halos");
    }
  
    public void Enable(string light)
    {
        if (light == "leds")
        {
            // Turns on all LEDs
            for (int led = 0; led < CUBESIZE; led++)
                gameObject.transform.GetChild(led).GetChild(light_source).GetComponent<Light>().enabled = true;
        }

        if (light == "halos")
        {
            // Turns on all halos for LEDs
            for (int led = 0; led < CUBESIZE; led++)
                gameObject.transform.GetChild(led).GetChild(light_source).GetChild(light_halo).GetComponent<Light>().enabled = true;
        }
    }

    public void Disable(string light)
    {
        if (light == "leds")
        {
            // Turns off all LEDs
            for (int led = 0; led < CUBESIZE; led++)
                gameObject.transform.GetChild(led).GetChild(light_source).GetComponent<Light>().enabled = false;
        }

        if (light == "halos")
        {
            // Turns off all halos
            for (int led = 0; led < CUBESIZE; led++)
                gameObject.transform.GetChild(led).GetChild(light_source).GetChild(light_halo).GetComponent<Light>().enabled = false;
        }
    }

    public void SetColor(Color led_color, Color light_color)
    {   
        // iterate over all LEDs
        for (int led = 0; led < CUBESIZE; led++)
        {
            // Set lights color
            gameObject.transform.GetChild(led).GetChild(light_source).GetComponent<Light>().color = light_color;

            // Set halos color
            gameObject.transform.GetChild(led).GetChild(light_source).GetChild(light_halo).GetComponent<Light>().color = light_color;

            // Set LED material color
            MeshRenderer meshRenderer   = gameObject.transform.GetChild(led).GetComponent<MeshRenderer>();
            meshRenderer.material.color = led_color;
        }
    }

    public void SetIntensity(float intensity, string light)
    {
        if (light == "leds")
        { 
            for (int led = 0; led < CUBESIZE; led++)
                gameObject.transform.GetChild(led).GetChild(light_source).GetComponent<Light>().intensity = intensity;
        }

        if (light == "halos")
        {
            for (int led = 0; led < CUBESIZE; led++)
                gameObject.transform.GetChild(led).GetChild(light_source).GetChild(light_halo).GetComponent<Light>().intensity = intensity;
        }
    }

    public void SetRange(int range, string light)
    {
        if (light == "leds")
        {
            for (int led = 0; led < CUBESIZE; led++)
                gameObject.transform.GetChild(led).GetChild(light_source).GetComponent<Light>().range = range;
        }

        if (light == "halos")
        {
            for (int led = 0; led < CUBESIZE; led++)
                gameObject.transform.GetChild(led).GetChild(light_source).GetChild(light_halo).GetComponent<Light>().range = range;
        }
    }
}
