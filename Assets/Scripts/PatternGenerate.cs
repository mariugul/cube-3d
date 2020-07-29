using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using TMPro;

public class PatternGenerate : MonoBehaviour
{
    // Input fields
    public TMP_InputField inputField;     // The text input field for the pattern.h file
    public TMP_InputField inputFieldTime; // The integer input field for time [ms]

    // Lists
    List<string> pattern = new List<string>(); // Stored pattern table

    // Variables
    int nrOfPatternsGenerated = 0; // Used for the redo functionality

    // Readonly variables
    readonly int    CUBESIZE = 64;      // Size of the LED cube
    readonly int    DEFAULT_LINES = 11; // Nr of lines before the pattern table in pattern.h
    readonly int    END_LINES = 2;      // Nr of lines after the pattern table in pattern.h
    readonly string PATH = "pattern.h"; // Path to pattern.h


    // Start is called before the first frame update
    void Start()
    {
        //Adds a listener to the input field to invoke a method when the value changes.
        inputField.onValueChanged.AddListener(delegate { InputFieldValueChange(); });

        // Create patterh.h
        if (!File.Exists(PATH))
        {
            CreatePatternFile(PATH);
        }

        // Initialize pattern list with the contents of pattern.h
        pattern = File.ReadAllLines(PATH).ToList();

        RefreshInputField(inputField);
    
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Invoked when the value of the text field changes.
    public void InputFieldValueChange()
    {
        /*
        if (!readingFile) 
            WriteString(inputField);
        */
    }

    // Hotkeys
    void OnGUI()
    {
        Event e = Event.current;

        // Ctrl + T Clicked
        if (e.type == EventType.KeyDown && e.control && e.keyCode == KeyCode.T)
        {
            inputFieldTime.Select();
            inputFieldTime.ActivateInputField();
        }

        // Ctrl + A Clicked
        else if (e.type == EventType.KeyDown && e.control && e.keyCode == KeyCode.A)
        {
            LedsEnableAll();

            // Instance of LedLights for turning LEDs on and off
            //var ledLights = gameObject.AddComponent<LedLights>();

            // Turn LEDs on 
            //ledLights.Enable("leds");
            //ledLights.Enable("halos");
        }

        // Shift + A Clicked
        if (e.type == EventType.KeyDown && e.shift && e.keyCode == KeyCode.A)
        {
            LedsDisableAll();

            // Instance of LedLights for turning LEDs on and off
            //var ledLights = gameObject.AddComponent<LedLights>();

            // Turn LEDs off
            //ledLights.Disable("leds");
            //ledLights.Disable("halos");
        }

        // Ctrl + Z clicked
        else if (e.type == EventType.KeyDown && e.control && e.keyCode == KeyCode.Z)
        {
            //prevent IndexOutOfRangeException for empty list
            if (pattern.Any()) 
            {
                // Remove previous generated pattern line
                if (nrOfPatternsGenerated > 0)
                {
                    // Count down the number of generated pattern lines
                    nrOfPatternsGenerated -= 1;

                    // Remove end of file
                    pattern.Remove("#endif");
                    pattern.Remove("};");

                    // Remove last added line
                    pattern.RemoveAt(pattern.Count - 1);

                    // Add end of file text
                    pattern.Add("};");
                    pattern.Add("#endif");

                    // Write to file
                    File.WriteAllLines(PATH, pattern);

                    RefreshInputField(inputField);
                }
            }
        }

        // Enter clicked
        else if (e.type == EventType.KeyDown && e.keyCode == KeyCode.Return)
        {
            GeneratePattern(ReadLedValues());
            RefreshInputField(inputField);

            // Add to generated patterns for redo functionality
            nrOfPatternsGenerated += 1;
        }

        // Delete clicked
        else if (e.type == EventType.KeyDown && e.keyCode == KeyCode.Delete)
        {
            //var contains = File.ReadAllLines(PATH).Contains("const PROGMEM uint16_t pattern_table[] = {");
            
            int lineCount = File.ReadLines(PATH).Count() - DEFAULT_LINES - END_LINES;

            // Only delete the contents of the array in pattern.h
            if (lineCount > 0)
            {
                // Remove end of file
                pattern.Remove("#endif");
                pattern.Remove("};");

                // Remove last added line
                pattern.RemoveAt(pattern.Count - 1);

                // Add end of file text
                pattern.Add("};");
                pattern.Add("#endif");

                // Write to file
                File.WriteAllLines(PATH, pattern);

                RefreshInputField(inputField);

                // Decrement the redo
                if (nrOfPatternsGenerated != 0)
                    nrOfPatternsGenerated -= 1;
            }
        }
    }

    void CreatePatternFile(string PATH)
    {
        // Create pattern.h
        File.Create(PATH).Dispose();

        // Fill with the blank template
        pattern = File.ReadAllLines(PATH).ToList();
        pattern.Add("#ifndef __PATTERN_H__");
        pattern.Add("#define __PATTERN_H__\n");
        pattern.Add("// Includes");
        pattern.Add("//---------------------------------");
        pattern.Add("#include <stdint.h>        // Use uint_t");
        pattern.Add("#include <avr/pgmspace.h>  // Store patterns in program memory\n");
        pattern.Add("// Pattern that LED cube will display");
        pattern.Add("//--------------------------------- ");
        pattern.Add("const PROGMEM uint16_t pattern_table[] = {\n");
        pattern.Add("};");
        pattern.Add("#endif");
        File.WriteAllLines(PATH, pattern);
    }

    void GeneratePattern(ushort[] ledValuesHex)
    {
        // Make list of patterns
        if (File.Exists(PATH))
        {
            pattern = File.ReadAllLines(PATH).ToList();

            // Remove end of file
            pattern.Remove("#endif");
            pattern.Remove("};");

            // Add new pattern to list
            pattern.Add("     0x" + ledValuesHex[0].ToString("X4") +
                           ", 0x" + ledValuesHex[1].ToString("X4") + 
                           ", 0x" + ledValuesHex[2].ToString("X4") + 
                           ", 0x" + ledValuesHex[3].ToString("X4") + 
                           ", "   + inputFieldTime.text + ",");
            pattern.Add("};");
            pattern.Add("#endif");

            // Write list to pattern.h
            File.WriteAllLines(PATH, pattern);
        }
        else
            CreatePatternFile(PATH);
    }

    public ushort[] ReadLedValues()
    {
        // Array with the status of LEDs
        ushort[] ledValuesHex = { 0, 0, 0, 0, };

        Array.Clear(ledValuesHex, 0, ledValuesHex.Length); // Clear array before every new reading

        // Iterate over every LED lightsource to find the values (on/off)
        ushort ledValueHex = 0;
        int j = 0;
        for (int i = 0; i < CUBESIZE; i++)
        {
            // Check if LED is on or off
            if (gameObject.transform.GetChild(i).GetChild(0).GetComponent<Light>().enabled == true)
                ledValueHex += (ushort)(1 << j); // Bitshifts a '1' the correct order into a ushort variable
                        
            else
                ledValueHex += (ushort)(0 << j); // Bitshifts a '0' the correct order into a ushort variable
                      
            // Save hex value for UInt16 every 16th iteration (4 times total)
            if ((i + 1) % 16 == 0)
            {
                ledValuesHex[((i + 1) / 16) - 1] = ledValueHex; // Save hex-value of pattern to array
                ledValueHex = 0;
            }

            // Needed for correct calculation of bitshift
            if (((j + 1) % 16) == 0)
                j = 0;
            else
                j++;
            
        }

        return ledValuesHex;
    }

    //[MenuItem("Tools/Read file")]
    static void RefreshInputField(TMP_InputField inputField)
    {
        string PATH = "pattern.h";

        //Read the text from file
        StreamReader reader = new StreamReader(PATH);
        inputField.text = reader.ReadToEnd();
        
        reader.Close();
    }

    void LedsEnableAll()
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
