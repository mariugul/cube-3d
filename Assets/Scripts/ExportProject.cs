using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
using System;

public class ExportProject : MonoBehaviour
{   
    // Input fields
    public TMP_InputField inputField; // The text input field for the pattern.h file

    // Export button
    public Button exportButton;

    // Directory and file paths
    const string DIR_PATH     = @"cube3d";
    const string PATTERN_FILE = @"pattern.h";

    readonly string ARDUINO_FILE_PATH = DIR_PATH + '/' + DIR_PATH + ".ino";
    readonly string PATTERN_FILE_PATH = DIR_PATH + '/' + PATTERN_FILE;

    void Start()
    {
        // Add listener to button
        exportButton.onClick.AddListener(delegate { OnClick(); });
    }

    void OnClick()
    {
        // Read all text from the input field
        string inputFieldText = inputField.text;

        // Create Directory and files if they don't exist
        CreateDirectory(DIR_PATH);
        CreateFile(PATTERN_FILE_PATH);
        CreateFile(ARDUINO_FILE_PATH);

        // Write input field text to the pattern.h
        File.WriteAllText(PATTERN_FILE_PATH, inputFieldText);

        //Load a text file (Assets/Resources/cube3d.ino)
        TextAsset cube3d_ino = (TextAsset)Resources.Load(DIR_PATH);
        string cube3d = cube3d_ino.text;

        // This line of code throws a "Null reference pointer exception"
        //File.WriteAllText(ARDUINO_FILE_PATH, cube3d);
    }
  
    void CreateDirectory(string path)
    {
        try
        {
            // Determine whether the directory exists.
            if (Directory.Exists(path))
            {
                Debug.Log("That path exists already.");
                return;
            }

            // Try to create the directory.
            DirectoryInfo di = Directory.CreateDirectory(path);
            Debug.Log("The directory was created successfully at {0}." + Directory.GetCreationTime(path));

        }
        catch (Exception e)
        {
            Debug.Log("The process failed: {0}" + e.ToString());
        }
        finally { }
    }

    void CreateFile(string path)
    {
        if (!File.Exists(path))
            File.Create(path);
        else
            Debug.Log(path + " exists already!");
    }
    

}