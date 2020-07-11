using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// GameObject.FixedUpdate example.
//
// Measure frame rate comparing FixedUpdate against Update.
// Show the rates every second.

public class FixedUpdateTest : MonoBehaviour
{
    private float updateCount = 0;
    private float fixedUpdateCount = 0;
    private float updateUpdateCountPerSecond;
    private float updateFixedUpdateCountPerSecond;

    // LED related
    const int CUBESIZE  = 64;
    const int PLANESIZE = 16;
    const int PLANES    = 4;
    bool      ledOn     = false;
    uint      line      = 0;                  
    const ulong bitmask = 0x8000000000000000; 
                                
    readonly ulong[,] arr = new ulong[,]
    {
        {0x0000000000000001, 10},
        {0x0000000000000003, 10},
        {0x0000000000000007, 10},
        {0x000000000000000F, 10},
        {0x000000000000008F, 10},
        {0x00000000000000CF, 10},
        {0x00000000000000EF, 10},
        {0x00000000000008FF, 10},
        {0x00000000000001FF, 10},
        {0x00000000000003FF, 10},
        {0x00000000000007FF, 10},
        {0x0000000000000FFF, 10},
        {0x0000000000008FFF, 10},
        {0x000000000000CFFF, 10},
        {0x000000000000EFFF, 10},
        {0x000000000000FFFF, 10},
        {0x000000000000EFFF, 10},
        {0x000000000000CFFF, 10},
        {0x0000000000008FFF, 10},
        {0x0000000000000FFF, 10},
        {0x00000000000007FF, 10},
        {0x00000000000003FF, 10},
        {0x00000000000001FF, 10},
        {0x00000000000000FF, 10},
        {0x00000000000000EF, 10},
        {0x00000000000000CF, 10},
        {0x000000000000008F, 10},
        {0x000000000000000F, 10},
        {0x0000000000000007, 10},
        {0x0000000000000003, 10},
        {0x0000000000000001, 10},
        {0x0000000000000000, 10},
    };

    void Awake()
    {
        // Uncommenting this will cause framerate to drop to 10 frames per second.
        // This will mean that FixedUpdate is called more often than Update.
        //Application.targetFrameRate = 10;
        StartCoroutine(Loop());
    }
    
    // Increase the number of calls to Update.
    void Update()
    {
        updateCount += 1;
    }

    // Increase the number of calls to FixedUpdate.
    void FixedUpdate()
    {
        fixedUpdateCount += 1;

        // Simulation of the LED cube
        //---------------------------
        ulong ledPattern = arr[line, 0];

        for (int i = 0; i < CUBESIZE; i++)
        {
            // If the LED value is '1' turn it on
            if ((ledPattern & bitmask) == bitmask)
                gameObject.transform.GetChild(i).GetChild(0).GetComponent<Light>().enabled = true;
            else
                gameObject.transform.GetChild(i).GetChild(0).GetComponent<Light>().enabled = false;

            ledPattern <<= 1; 
        }

        if (line == arr.GetLength(0) - 1)
            line = 0; // Reset pattern line to the start of array
        else
            line++;   // Increment pattern line to the next line in the array
    }

    // Show the number of calls to both messages.
    void OnGUI()
    {
        GUIStyle fontSize = new GUIStyle(GUI.skin.GetStyle("label"));
        fontSize.fontSize = 24;
        GUI.Label(new Rect(100, 100, 200, 50), "Update: " + updateUpdateCountPerSecond.ToString(), fontSize);
        GUI.Label(new Rect(100, 150, 200, 50), "FixedUpdate: " + updateFixedUpdateCountPerSecond.ToString(), fontSize);
    }

    // Update both CountsPerSecond values every second.
    IEnumerator Loop()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            updateUpdateCountPerSecond = updateCount;
            updateFixedUpdateCountPerSecond = fixedUpdateCount;

            updateCount = 0;
            fixedUpdateCount = 0;
        }
    }

    void readLedValues()
    {
      
    }
}