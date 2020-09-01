using UnityEngine;

public class LedLights : MonoBehaviour
{
    // Color Picker Object
    public ColorPicker picker;

    // Default LED values
    const int LED_RANGE = 4;
    const int LED_INTENSITY = 15;

    const int HALO_RANGE = 15;
    const float HALO_INTENSITY = 0.1f;

    const float ALPHA = 0.4f;

    // Childs of LED cube
    const int LIGHT_SOURCE = 0;
    const int LIGHT_HALO = 0;

    // Constants
    const int CUBESIZE = 64;

    // Make class singleton
    public static LedLights LEDLIGHTS;

    private void Awake()
    {
        //if a messageboxes doesn't already exist
        if (LEDLIGHTS == null)
        {
            DontDestroyOnLoad(this);
            LEDLIGHTS = this;
        }
        //if there's already a messageboxes
        else if (LEDLIGHTS != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Color picker add listener to changed values
        picker.onValueChanged.AddListener(color =>
        {
            onColorPickerValueChange();
        });

        SetDefaultValues(Color.green);

        // Disable LEDs by default
        Disable();
    }

    void Update()
    {
        
    }

    void onColorPickerValueChange()
    {
        SetColor(picker.CurrentColor);
    }

    public void Enable()
    {
        // Turns on all LEDs
        for (int led = 0; led < CUBESIZE; led++)
            gameObject.transform.GetChild(led).GetChild(LIGHT_SOURCE).GetComponent<Light>().enabled = true;
        
        // Turns on all halos for LEDs
        for (int led = 0; led < CUBESIZE; led++)
            gameObject.transform.GetChild(led).GetChild(LIGHT_SOURCE).GetChild(LIGHT_HALO).GetComponent<Light>().enabled = true;
        
    }

    public void Disable()
    {
        // Turns off all LEDs
        for (int led = 0; led < CUBESIZE; led++)
            gameObject.transform.GetChild(led).GetChild(LIGHT_SOURCE).GetComponent<Light>().enabled = false;

        // Turns off all halos
        for (int led = 0; led < CUBESIZE; led++)
            gameObject.transform.GetChild(led).GetChild(LIGHT_SOURCE).GetChild(LIGHT_HALO).GetComponent<Light>().enabled = false;
    }

    public void SetColor(Color color)
    {
        color.a = ALPHA;

        // iterate over all LEDs
        for (int led = 0; led < CUBESIZE; led++)
        {
            // Set lights color
            gameObject.transform.GetChild(led).GetChild(LIGHT_SOURCE).GetComponent<Light>().color = color;

            // Set halos color
            gameObject.transform.GetChild(led).GetChild(LIGHT_SOURCE).GetChild(LIGHT_HALO).GetComponent<Light>().color = color;

            // Set LED material color
            MeshRenderer meshRenderer   = gameObject.transform.GetChild(led).GetComponent<MeshRenderer>();
            meshRenderer.material.color = color;
        }
    }

    public void SetIntensity(float intensity, string light)
    {
        if (light == "leds")
        { 
            for (int led = 0; led < CUBESIZE; led++)
                gameObject.transform.GetChild(led).GetChild(LIGHT_SOURCE).GetComponent<Light>().intensity = intensity;
        }

        if (light == "halos")
        {
            for (int led = 0; led < CUBESIZE; led++)
                gameObject.transform.GetChild(led).GetChild(LIGHT_SOURCE).GetChild(LIGHT_HALO).GetComponent<Light>().intensity = intensity;
        }
    }

    public void SetRange(int range, string light)
    {
        if (light == "leds")
        {
            for (int led = 0; led < CUBESIZE; led++)
                gameObject.transform.GetChild(led).GetChild(LIGHT_SOURCE).GetComponent<Light>().range = range;
        }

        if (light == "halos")
        {
            for (int led = 0; led < CUBESIZE; led++)
                gameObject.transform.GetChild(led).GetChild(LIGHT_SOURCE).GetChild(LIGHT_HALO).GetComponent<Light>().range = range;
        }
    }
    
    public void SetDefaultValues(Color color)
    {
        // Set range for LEDs and halos
        SetRange(LED_RANGE, "leds");
        SetRange(HALO_RANGE, "halos");

        // Set intensity for LEDs and halos
        SetIntensity(LED_INTENSITY, "leds");
        SetIntensity(HALO_INTENSITY, "halos");

        // Set default color
        picker.CurrentColor = color;
        SetColor(picker.CurrentColor);
    }

 }
