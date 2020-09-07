using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Enumerations.Enumerations;

// This script is supposed to send a message when an event happens
public class ToggleDelegate : MonoBehaviour
{
    [Header("Toggle Name")]
    [SerializeField] ToggleID toggleID;

    [Header("Toggle Object")]
    [SerializeField] Toggle toggle;

    public delegate void ToggleClick(int id, Toggle toggle);
    public static event ToggleClick Toggle;
    
    public void OnToggleValueChange()
    {
        Toggle?.Invoke((int)toggleID, toggle);
    }

    
}
