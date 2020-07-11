using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.UI;
using TMPro;
using UnityEditor.PackageManager.Requests;

public class ReadWriteTextToFile : MonoBehaviour
{
    public TMP_InputField inputField;

    
   
    void Start()
    {
        //Adds a listener to the main input field and invokes a method when the value changes.
        inputField.onValueChanged.AddListener(delegate { ValueChangeCheck(); });

        ReadString(inputField);
    }

    void Update()
    {

    }

    // Invoked when the value of the text field changes.
    public void ValueChangeCheck()
    {
        //WriteString(inputField);
    }

    [MenuItem("Tools/Write file")]
    static void WriteString(TMP_InputField inputField)
    {
        string path = "Assets/Resources/test.h";

        // Erase contents of file 
        File.WriteAllText(path, string.Empty);

        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(inputField.text);
        writer.Close();

        //Re-import the file to update the reference in the editor
        AssetDatabase.ImportAsset(path);
        TextAsset asset = (TextAsset)Resources.Load("test");

        //Print the text from the file
        //Debug.Log(asset.text);
    }

    [MenuItem("Tools/Read file")]
    static void ReadString(TMP_InputField inputField)
    {
        string path = "Assets/Resources/test.h";

        //Read the text from file
        StreamReader reader = new StreamReader(path);
        inputField.text = reader.ReadToEnd();

        //Debug.Log(reader.ReadToEnd());
        
        reader.Close();
    }

}


