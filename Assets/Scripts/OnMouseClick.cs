using UnityEngine;

public class OnMouseClick : MonoBehaviour
{
    RaycastHit hit;

    // Childs of LED cube
    const int light_source_child = 0;
    const int light_halo_child   = 0;

    // Start is called before the first frame update
    void Start()
    {
        MeshCollider mc = gameObject.AddComponent(typeof(MeshCollider)) as MeshCollider;
    }

    // Update is called once per frame
    void Update()
    {
        // Toggle LED on left mouse click 
        const int left_mouse_click = 0;
        if (Input.GetMouseButtonDown(left_mouse_click))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit, 1200.0f))
            {
                if (hit.transform != null)
                {
                    // Read status of clicked LED
                    GameObject led = hit.collider.transform.gameObject;
                    Light clicked_led_light = led.transform.GetChild(light_source_child).GetComponent<Light>();
                    Light clicked_led_halo  = led.transform.GetChild(light_source_child).GetChild(light_halo_child).GetComponent<Light>();

                    // Toggle clicked LED
                    clicked_led_light.enabled = !clicked_led_light.enabled;
                    clicked_led_halo.enabled  = !clicked_led_halo.enabled;
                }
            }
        }
    }
}
