using UnityEngine;

public class LedLights : MonoBehaviour
{
    // Default LED values
    const int   led_range      = 4;
    const int   led_intensity  = 15;
    const int   halo_range     = 15;
    const float halo_intensity = 0.1f;

    Color color = Color.red;

    // Childs of LED cube
    const int light_source = 0;
    const int light_halo   = 0;

    // Materials
    public Material material_red;
    public Material material_green;
    public Material material_blue;

    // Constants
    const int CUBESIZE = 64;

    void Start()
    { 
        // Disable LEDs and halos by default
        Disable("leds");
        Disable("halos");
        
        // Set range for LEDs and halos
        SetRange(led_range, "leds");
        SetRange(halo_range, "halos");

        // Set color for LEDs and halos
        SetColor(color, "leds");
        SetColor(color, "halos");

        // Set intensity for LEDs and halos
        SetIntensity(led_intensity,  "leds");
        SetIntensity(halo_intensity, "halos");

        // Set material
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

    void SetColor(Color color, string light)
    {
        if (light == "leds")
        {
            for (int led = 0; led < CUBESIZE; led++)
                gameObject.transform.GetChild(led).GetChild(light_source).GetComponent<Light>().color = color;
        }
        
        if (light == "halos")
        {
            for (int led = 0; led < CUBESIZE; led++)
                gameObject.transform.GetChild(led).GetChild(light_source).GetChild(light_halo).GetComponent<Light>().color = color;
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

    void SetRange(int range, string light)
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

    void SetMaterial(Material material)
    {
        /*
        for (int led = 0; led < CUBESIZE; led++)
        {
            gameObject.transform.GetChild(led).GetChild(light_source).GetComponent<Material>() = material;
        }
        */
    }
}
