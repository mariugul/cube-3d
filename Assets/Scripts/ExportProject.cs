using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
using System;
using System.Text;

public class ExportProject : MonoBehaviour
{   
    // Input fields
    public TMP_InputField inputField; // The text input field for the pattern.h file

    // Directory and file paths
    const string DIR_PATH     = @"cube3d";
    const string PATTERN_FILE = @"pattern.h";

    readonly string ARDUINO_FILE_PATH = DIR_PATH + '/' + DIR_PATH + ".ino";
    readonly string PATTERN_FILE_PATH = DIR_PATH + '/' + PATTERN_FILE;

    // Generates a Arduino project with cube3d.ino and pattern.h
    public void ArduinoGenerate(string folderName)
    {
        // Read the Arduino code file
        var sr = new StreamReader(Application.dataPath + "/" + "cube3d.ino");
        var cube3dContents = sr.ReadToEnd();
        sr.Close();

        // Create Directory and files if they don't exist
        CreateDirectory(folderName + DIR_PATH);
        CreateFile(folderName + PATTERN_FILE_PATH, inputField.text);
        CreateFile(folderName + ARDUINO_FILE_PATH, cube3dContents);
    }

    // Generates the pattern.h file in the selected folder
    public void PatternFileGenerate(string fileName)
    {
        CreateFile(fileName, inputField.text);
    }
  
    void CreateDirectory(string path)
    {
        try
        {
            // Determine whether the directory exists.
            if (Directory.Exists(path))
            {
                Debug.Log("The directory exists already!");
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

    void CreateFile(string path, string content = "")
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

                    Debug.Log(path + " was created!");
                }
            }
            else 
                Debug.Log(path + " exists already!");
        }

        catch (Exception Exc)
        {
            Debug.Log(Exc.ToString());
        }
    }
    

}