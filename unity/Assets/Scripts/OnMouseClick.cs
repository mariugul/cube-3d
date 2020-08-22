using UnityEngine;

public class OnMouseClick : MonoBehaviour
{
    RaycastHit hit;

    // Childs of LED cube
    const int light_source_child = 0;
    const int light_halo_child   = 0;

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        // Toggle LED on left mouse click 
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit, 1200.0f) && hit.transform != null)
        {
            // Read status of clicked LED
            GameObject led = hit.collider.transform.gameObject;
            Light clicked_led_light = led.transform.GetChild(light_source_child).GetComponent<Light>();
            Light clicked_led_halo  = led.transform.GetChild(light_source_child).GetChild(light_halo_child).GetComponent<Light>();

            // Toggle clicked LED
            clicked_led_light.enabled = !clicked_led_light.enabled;
            clicked_led_halo.enabled  = !clicked_led_halo.enabled;
        }
        /*
        // Ctrl + Left Mousebutton Clicked
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetMouseButton(0))
        {
            Debug.Log("Left mousebutton clicked and ctrl clicked");

            // Read status of clicked LED
            GameObject led = hit.collider.transform.gameObject;
            Light clicked_led_light = led.transform.GetChild(light_source_child).GetComponent<Light>();
            Light clicked_led_halo  = led.transform.GetChild(light_source_child).GetChild(light_halo_child).GetComponent<Light>();

            // Toggle clicked LED
            clicked_led_light.enabled = !clicked_led_light.enabled;
            clicked_led_halo.enabled  = !clicked_led_halo.enabled; 
            
            // ---------------------------------------------------
            // Turn on LEDs between clicks
            // --------------------------------------------------- 
        }
        */
    }
}
