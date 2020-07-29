using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

public class ExportProject : MonoBehaviour
{   
    // Input fields
    public TMP_InputField inputField; // The text input field for the pattern.h file

    // Export button
    public Button exportButton;

    // Directory paths
    string sourceDir = @"C:\Users\Mariu\Documents\FileCopyTest\source";
    string backupDir = @"C:\Users\Mariu\Documents\FileCopyTest\backup";
    string path = @"C:\Users\Mariu\Documents\FileCopyTest\source\pattern.h";

    void Start()
    {
        // Add listener to button
        exportButton.onClick.AddListener(delegate { OnClick(); });
    }

    void OnClick()
    {
        Debug.Log("Clicked Export Button.");
        string patternStr = inputField.text;

        try
        {
            string[] txtList = Directory.GetFiles(sourceDir, "*.txt");

            // Copy text files.
            foreach (string f in txtList)
            {
                // Remove path from the file name.
                string fName = f.Substring(sourceDir.Length + 1);

                try
                {
                    // Will not overwrite if the destination file already exists.
                    //File.Copy(Path.Combine(sourceDir, fName), Path.Combine(backupDir, fName));
                    File.Create(path);
                    //File.WriteAllLines(path, patternStr);
                    Debug.Log(patternStr);
                   
                }

                // Catch exception if the file was already copied.
                catch (IOException copyError)
                {
                    Debug.Log(copyError.Message);
                }
            }

            /*
            // Delete source files that were copied.
            foreach (string f in txtList)
            {
                File.Delete(f);
            }
            */
        }

        catch (DirectoryNotFoundException dirNotFound)
        {
            Debug.Log(dirNotFound.Message);
        }
    }

    public static void ReadString(string path)
    {
        StreamReader reader = new StreamReader(path);
        reader.Close();
    }
}