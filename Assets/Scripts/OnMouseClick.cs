using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class OnMouseClick : MonoBehaviour
{
    RaycastHit hit;

    // List to store the on or off status of LEDs
    Dictionary<string, bool> ledStatus = new Dictionary<string, bool>();

    // Childs of LED cube
    const int light_source = 0;
    const int light_halo = 0;

    // Start is called before the first frame update
    void Start()
    {
        MeshCollider mc = gameObject.AddComponent(typeof(MeshCollider)) as MeshCollider;

        // Initialize state of LEDs to list
        for (int i = 1; i <= 64; i++)
        {
            ledStatus.Add("led" + i, false);
        }

        foreach (KeyValuePair<string, bool> led in ledStatus)
        {
            //textBox3.Text += ("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
            Console.WriteLine("Key = {0}, Value = {1}", led.Key, led.Value);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // On left mouse click 
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.transform != null)
                {            
                    // Save name of clicked LED
                    string clickedLed = hit.transform.gameObject.name;

                    // Look up the clicked LEDs status in dictionary and save to ledStatus
                    ledStatus.TryGetValue(clickedLed, out bool clickedLedStatus);

                    // Toggle LED light
                    if (clickedLedStatus)
                    {
                        // Deactivate light source
                        hit.transform.gameObject.transform.GetChild(light_source).GetComponent<Light>().enabled = false;

                        // Deactivate halo
                        hit.transform.gameObject.transform.GetChild(light_source).GetChild(light_halo).GetComponent<Light>().enabled = false;

                        // Set led status in dictionary
                        ledStatus[clickedLed] = false;
                          
                    }
                    else
                    {
                        // Activate light source
                        hit.transform.gameObject.transform.GetChild(light_source).GetComponent<Light>().enabled = true;
                        
                        // Activate halo
                        hit.transform.gameObject.transform.GetChild(light_source).GetChild(light_halo).GetComponent<Light>().enabled = true;

                        // Set led status in dictionary
                        ledStatus[clickedLed] = true;
                    }
                    
                }
            }
        }
    }

    private void PrintName(GameObject go)
    {
        print(go.name);
    }

}
