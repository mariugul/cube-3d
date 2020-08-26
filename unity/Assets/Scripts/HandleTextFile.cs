using UnityEngine;
using UnityEditor;
using System.IO;

public class HandleTextFile
{
    /*
    [MenuItem("Tools/Write file")]
    public static void WriteString(string path)
    {
        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine("Test");
        writer.Close();

        //Re-import the file to update the reference in the editor
        AssetDatabase.ImportAsset(path);
        TextAsset asset = (UnityEngine.TextAsset)Resources.Load("test");

        //Print the text from the file
        Debug.Log(asset.text);
    }

    [MenuItem("Tools/Read file")]
    public static void ReadString(string path)
    {
        StreamReader reader = new StreamReader(path);
        reader.Close();
    }
    */

}