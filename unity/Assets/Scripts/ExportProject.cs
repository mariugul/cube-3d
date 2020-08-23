using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

public class ExportProject : MonoBehaviour
{   
    // Input fields
    public TMP_InputField inputField; // The text input field for the pattern.h file

    // Directory and file paths
    const string DIR_PATH     = @"cube3d";
    const string PATTERN_FILE = @"pattern.h";
    const string ARDUINO_FILE = @"cube3d.ino";

    readonly string ARDUINO_FILE_PATH = DIR_PATH + '/' + DIR_PATH + ".ino";
    readonly string PATTERN_FILE_PATH = DIR_PATH + '/' + PATTERN_FILE;

    // Generates an Arduino project folder with cube3d.ino and pattern.h
    public bool ArduinoProjectGenerate(string folderPath)
    {
        // Filter out the folder name from path
        int lastIdx = folderPath.LastIndexOf('/');
        int length  = folderPath.Length;
        string folderName = folderPath.Remove(0, lastIdx);
        Debug.Log("Foldername: " + folderName);

        // Try create Directory 
        if (CreateDirectory(folderPath))
            Debug.Log("Created directory successfully at " + folderPath);
        else
        {
            Debug.Log("Failed to create directory.");
            return false;
        }

        // Try create pattern.h
        if (PatternFileGenerate(folderPath + '/' + PATTERN_FILE))
            Debug.Log("Created pattern file (.h) successfully at " + folderPath + '/' + PATTERN_FILE);
        else
        {
            Debug.Log("Failed to create pattern file.");
            return false;
        }

        // Read the .ino file from assets
        var sr = new StreamReader(UnityEngine.Application.dataPath + "/" + "cube3d.ino");
        var cube3dContents = sr.ReadToEnd();
        sr.Close();
        
        // Try create arduino file
        if (CreateFile(folderPath + '/' + folderName + ".ino", cube3dContents))
        {
            Debug.Log("Created Arduino file (.ino) successfully at " + folderPath + '/' + folderName + ".ino");
        }
        else
        {
            Debug.Log("Failed to create Arduino file.");
            return false;
        }

        // Return true for successfull creation of directory with contents
        return true;
    }

    // Generates the pattern.h file in the selected folder
    public bool PatternFileGenerate(string fileName)
    {
        // Read input field text
        string inputFieldText = inputField.text;

        // Replace includes so they don't vanish on the parse
        inputFieldText = Regex.Replace(inputFieldText, "<stdint.h>", "stdint.h");
        inputFieldText = Regex.Replace(inputFieldText, "<avr/pgmspace.h>", "avr/pgmspace.h");

        // Parse out Rich Text symbols
        inputFieldText = Regex.Replace(inputFieldText, "<.*?>", String.Empty);

        // Put includes back in
        inputFieldText = Regex.Replace(inputFieldText, "stdint.h", "<stdint.h>");
        inputFieldText = Regex.Replace(inputFieldText, "avr/pgmspace.h", "<avr/pgmspace.h>");

        return CreateFile(fileName, inputFieldText);
    }
  
    bool CreateDirectory(string path)
    {
        try
        {
            // Determine whether the directory exists.
            if (Directory.Exists(path))
            {
                Debug.Log("The directory exists already!");
                return false;
            }

            // Try to create the directory.
            DirectoryInfo di = Directory.CreateDirectory(path);
            Debug.Log("The directory was created successfully at {0}." + Directory.GetCreationTime(path));
            
            return true;

        }
        catch (Exception e)
        {
            Debug.Log("The process failed: {0}" + e.ToString());
            return false;
        }
    }

    // Returns true if successful
    bool CreateFile(string path, string content = "")
    {
        try
        {
            // Check if file already exists   
            if (!File.Exists(path))
            {

                // Create a new file     
                using (FileStream fs = File.Create(path))
                {
                    // Write text to file   
                    Byte[] file_content = new UTF8Encoding(true).GetBytes(content);
                    fs.Write(file_content, 0, file_content.Length);
                }
                Debug.Log(path + " was created!");
                return true;
            }
            else
            {
                Debug.Log(path + " exists already!");
                return false;
            }
        }

        catch (Exception Exc)
        {
            Debug.Log(Exc.ToString());
            return false;
        }
    }
    

}