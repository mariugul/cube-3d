using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Linq;
using UnityEngine;
using TMPro;
using System.Security.Cryptography;

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
    readonly int    DEFAULT_LINES = 13; // Nr of lines before not part of the pattern table in pattern.h
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

        //RefreshInputField(inputField);

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
            // Generate pattern line from LED values and update input field
            inputField.text = GeneratePatternString(GetLedStatus());

            //GeneratePattern(ReadLedValues());
            //RefreshInputField(inputField);

            // Add to generated patterns for redo functionality
            nrOfPatternsGenerated += 1;
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
                inputFieldText = inputFieldText.Replace("\r\n};" + "\r\n" + "#endif", "");

                // Find the last occurence of newline 
                int lastIndex = inputFieldText.LastIndexOf("\r\n");

                // Remove the last pattern line
                if (lastIndex > 0)
                    inputFieldText = inputFieldText.Substring(0, lastIndex);

                // Add end of file
                inputFieldText += ("\r\n};\r\n#endif");

                // Refresh input field
                inputField.text = inputFieldText;
            }


            /*
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
            */
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
        pattern.Add("#include <stdint.h>");
        pattern.Add("#include <avr/pgmspace.h>\n");
        pattern.Add("// Pattern that LED cube will display");
        pattern.Add("//--------------------------------- ");
        pattern.Add("const PROGMEM uint16_t pattern_table[] = {\n");
        pattern.Add("};");
        pattern.Add("#endif");
        File.WriteAllLines(PATH, pattern);
    }

    void GeneratePattern (ushort[] ledValuesHex)
    {
        // Make list of patterns
        if (File.Exists(PATH))
        {
            pattern = File.ReadAllLines(PATH).ToList();

            // Remove end of file
            pattern.Remove("#endif");
            pattern.Remove("};");

            // Add new pattern to list
            pattern.Add("    0x" + ledValuesHex[0].ToString("X4") +
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

    // This is a redo of GeneratePattern() (*Under construction)
    string GeneratePatternString(ushort[] ledStatus)
    {
        // Read input field text
        string inputFieldText = inputField.text;

        // Remove end of file
        inputFieldText = inputFieldText.Replace("};" + "\r\n" + "#endif", "");
        
        // Add new generated pattern line
        inputFieldText += (
                      "    0x" + ledStatus[0].ToString("X4") +
                      ", 0x"   + ledStatus[1].ToString("X4") +
                      ", 0x"   + ledStatus[2].ToString("X4") +
                      ", 0x"   + ledStatus[3].ToString("X4") +
                      ", "     + inputFieldTime.text + ",\r\n");

        // Add end of file
        inputFieldText += ("};\r\n#endif");

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

    static void RefreshInputField(TMP_InputField inputField)
    {
        string PATH = "pattern.h";

        //Read the text from file
        StreamReader reader = new StreamReader(PATH);
        inputField.text = reader.ReadToEnd();
        
        reader.Close();
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
