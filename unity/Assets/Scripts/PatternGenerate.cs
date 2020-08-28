using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//using System.Windows.Forms;

public class PatternGenerate : MonoBehaviour
{
    // Input fields
    public TMP_InputField inputField;     // The text input field for the pattern.h file
    public TMP_InputField inputFieldTime; // The integer input field for time [ms]

    // Buttons 
    public UnityEngine.UI.Button editButton;

    // Lists
    List<string> pattern = new List<string>(); // Stored pattern table

    // Variables
    int nrOfPatternsGenerated = 0; // Used for the redo functionality

    // Readonly variables
    readonly int    CUBESIZE = 64;      // Size of the LED cube
    readonly int    DEFAULT_LINES = 13; // Nr of lines before not part of the pattern table in pattern.h
    readonly string PATH = "pattern.h"; // Path to pattern.h

    // Raycast
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        //Adds a listener to the input field to invoke a method when the value changes.
        inputField.onValueChanged.AddListener(delegate { InputFieldValueChange(); });

        // Add listener to button click
        editButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnEditButtonClick);
    }

    void OnEditButtonClick()
    {
        Color highlight_color = new Color(0.94f, 0.96f, 1f);

        if (inputField.readOnly == true)
        {   
            // Make input field editable
            inputField.readOnly = false;

            // Change background color
            inputField.image.color = highlight_color;
        }
        else
        {
            // Make input field read only
            inputField.readOnly = true;

            // Change background color
            inputField.image.color = Color.white;
        }

    }

    // Invoked when the value of the text field changes.
    public void InputFieldValueChange()
    {

    }

    // Hotkeys
    void OnGUI()
    {
        Event e = Event.current;

        // Ctrl + T Clicked
        if (e.type == EventType.KeyDown && e.control && e.keyCode == KeyCode.T)
        {
            inputFieldTime.Select();
        }

        // Ctrl + A Clicked
        else if (e.type == EventType.KeyDown && e.control && e.keyCode == KeyCode.A)
        {
            LedsEnableAll();
        }

        // Shift + A Clicked
        else if (e.type == EventType.KeyDown && e.shift && e.keyCode == KeyCode.A)
        {
            LedsDisableAll();
        }

        // Ctrl + Z clicked
        else if (e.type == EventType.KeyDown && e.control && e.keyCode == KeyCode.Z)
        {
            
        }

        // Enter clicked
        else if (e.type == EventType.KeyDown && e.keyCode == KeyCode.Return)
        {
            // Remove focus from time-input field
            if (inputFieldTime.isFocused)
            {
                inputField.Select();
            }

            // Only genereate pattern outside of edit mode
            if (inputField.readOnly == true)
            {
                // Generate pattern line from LED values and update input field
                inputField.text = GeneratePattern(GetLedStatus());
            }
        }

        // Delete clicked
        else if (e.type == EventType.KeyDown && e.keyCode == KeyCode.Delete)
        {
            // Read input field text
            string inputFieldText = inputField.text;
            
            // Calculate number of pattern lines (inside the array)
            int numLines = inputFieldText.Split('\n').Length - DEFAULT_LINES;

            // Remove pattern lines if there are any
            if (numLines > 0)
            {
                // Remove end of file
                inputFieldText = inputFieldText.Replace("\r\n};" + "\r\n" + "<color=purple>#endif</color>", "");

                // Find the last occurence of newline 
                int lastIndex = inputFieldText.LastIndexOf("\r\n");

                // Remove the last pattern line
                if (lastIndex > 0)
                    inputFieldText = inputFieldText.Substring(0, lastIndex);

                // Add end of file
                inputFieldText += ("\r\n};\r\n<color=purple>#endif</color>");

                // Refresh input field
                inputField.text = inputFieldText;
            }
        }
    }

    // This is a redo of GeneratePattern() (*Under construction)
    string GeneratePattern(ushort[] ledStatus)
    {
        // Read input field text
        string inputFieldText = inputField.text;

        // Remove end of file
        inputFieldText = inputFieldText.Replace("};" + "\r\n" + "<color=purple>#endif</color>", "");
        
        // Add new generated pattern line
        inputFieldText += (
                      "    0x" + ledStatus[0].ToString("X4") +
                      ", 0x"   + ledStatus[1].ToString("X4") +
                      ", 0x"   + ledStatus[2].ToString("X4") +
                      ", 0x"   + ledStatus[3].ToString("X4") +
                      ", "     + inputFieldTime.text + ",\r\n");

        // Add end of file
        inputFieldText += ("};\r\n<color=purple>#endif</color>");

        //Debug.Log(inputFieldText);
        return inputFieldText;
    }

    public ushort[] GetLedStatus()
    {
        // Array with the status of LEDs in each plane
        ushort[] ledStatusPlanes = { 0, 0, 0, 0, };

        Array.Clear(ledStatusPlanes, 0, ledStatusPlanes.Length); // Clear array before every new reading

        // Iterate over every LED lightsource to find the values (on/off)
        ushort ledStatus = 0;
        int j = 0;
        for (int i = 0; i < CUBESIZE; i++)
        {
            // Check if LED is on or off
            if (gameObject.transform.GetChild(i).GetChild(0).GetComponent<Light>().enabled == true)
                ledStatus += (ushort)(1 << j); // Bitshifts a '1' the correct order into a ushort variable
                        
            else
                ledStatus += (ushort)(0 << j); // Bitshifts a '0' the correct order into a ushort variable
                      
            // Save hex value for UInt16 every 16th iteration (4 times total)
            if ((i + 1) % 16 == 0)
            {
                ledStatusPlanes[((i + 1) / 16) - 1] = ledStatus; // Save hex-value of pattern to array
                ledStatus = 0;
            }

            // Needed for correct calculation of bitshift
            if (((j + 1) % 16) == 0)
                j = 0;
            else
                j++;
        }

        return ledStatusPlanes;
    }

    public void LedsEnableAll()
    {
        // Turns on all LEDs
        for (int led = 0; led < 64; led++)
            gameObject.transform.GetChild(led).GetChild(0).GetComponent<Light>().enabled = true;

        // Turns on all halos for LEDs
        for (int led = 0; led < 64; led++)
            gameObject.transform.GetChild(led).GetChild(0).GetChild(0).GetComponent<Light>().enabled = true;   
    }

    void LedsDisableAll()
    {
        // Turns on all LEDs
        for (int led = 0; led < 64; led++)
            gameObject.transform.GetChild(led).GetChild(0).GetComponent<Light>().enabled = false;

        // Turns on all halos for LEDs
        for (int led = 0; led < 64; led++)
            gameObject.transform.GetChild(led).GetChild(0).GetChild(0).GetComponent<Light>().enabled = false;
    }

}
