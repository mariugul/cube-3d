using System;
using System.Collections.Generic;
using UnityEngine;

public class LightOnOff : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            GetComponent<Light>().enabled=true;

        else if (Input.GetKeyDown(KeyCode.DownArrow))
            GetComponent<Light>().enabled=false;   
    }
    
    
}
