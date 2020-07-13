using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
using System.Globalization;

public class PatternGenerate : MonoBehaviour
{
    public TMP_InputField inputField;       
    public TMP_InputField inputFieldTime; 

    private const int CUBESIZE = 64;
    //ushort[] ledValuesHex = { 0, 0, 0, 0 };

    // Stored pattern table
    List<string> pattern = new List<string>();

    // Path to pattern.h
    readonly string path = "pattern.h";

    // Used for the redo functionality
    int nrOfPatternsGenerated = 0;

    // Nr of lines before the pattern table in pattern.h
    readonly int defaultLines = 11;

    // Nr of lines after the pattern table in pattern.h
    readonly int endLines = 2;

    
    // Start is called before the first frame update
    void Start()
    {
        //Adds a listener to the input field and invokes a method when the value changes.
        inputField.onValueChanged.AddListener(delegate { ValueChangeCheck(); });

        // Create patterh.h
        if (!File.Exists(path))
        {
            CreatePatternFile(path);
        }

        // Initialize pattern list with the contents of pattern.h
        pattern = File.ReadAllLines(path).ToList();

        RefreshInputField(inputField);
    
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Redo functionality
    void OnGUI()
    {
        Event e = Event.current;

        // Shift + A Clicked
        if (e.type == EventType.KeyDown && e.shift && e.keyCode == KeyCode.A)
        {
            // Instance of LedLights for turning LEDs on and off
            var ledLights = gameObject.AddComponent<LedLights>();

            // Turn LEDs off
            ledLights.Disable("leds");
            ledLights.Disable("halos");
        }

        // Ctrl + A Clicked
        else if (e.type == EventType.KeyDown && e.control && e.keyCode == KeyCode.A)
        {
            // Instance of LedLights for turning LEDs on and off
            var ledLights = gameObject.AddComponent<LedLights>();

            // Turn LEDs on 
            ledLights.Enable("leds");
            ledLights.Enable("halos");   
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
                    File.WriteAllLines(path, pattern);

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
            //var contains = File.ReadAllLines(path).Contains("const PROGMEM uint16_t pattern_table[] = {");
            
            int lineCount = File.ReadLines(path).Count() - defaultLines - endLines;

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
                File.WriteAllLines(path, pattern);

                RefreshInputField(inputField);

                // Decrement the redo
                if (nrOfPatternsGenerated != 0)
                    nrOfPatternsGenerated -= 1;
            }
        }
    }

    // Invoked when the value of the text field changes.
    public void ValueChangeCheck()
    {
        /*
        if (!readingFile) 
            WriteString(inputField);
        */
    }

    void CreatePatternFile(string path)
    {
        // Create pattern.h
        File.Create(path).Dispose();

        // Fill with the blank template
        pattern = File.ReadAllLines(path).ToList();
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
        File.WriteAllLines(path, pattern);
    }

    void GeneratePattern(ushort[] ledValuesHex)
    {
        // Make list of patterns
        if (File.Exists(path))
        {
            pattern = File.ReadAllLines(path).ToList();

            // Remove end of file
            pattern.Remove("#endif");
            pattern.Remove("};");

            // Add new pattern to list
            pattern.Add("    {0x" + ledValuesHex[0].ToString("X4") +
                           ", 0x" + ledValuesHex[1].ToString("X4") + 
                           ", 0x" + ledValuesHex[2].ToString("X4") + 
                           ", 0x" + ledValuesHex[3].ToString("X4") + 
                           ", "   + inputFieldTime.text + "},");
            pattern.Add("};");
            pattern.Add("#endif");

            // Write list to pattern.h
            File.WriteAllLines(path, pattern);
        }
        else
            CreatePatternFile(path);
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
        string path = "pattern.h";

        //Read the text from file
        StreamReader reader = new StreamReader(path);
        inputField.text = reader.ReadToEnd();
        
        reader.Close();
    }
    
}
