using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Enumerations.Enumerations;

// This script is supposed to send a message when an event happens
public class ToggleDelegate : MonoBehaviour
{
    [Header("Toggle Name")]
    [SerializeField] ToggleID toggleID;

    // Toggle this script is attached to
    Toggle toggle;

    public delegate void ToggleClick(int id, Toggle toggle);
    public static event ToggleClick Toggle;

    // Runs on start up
    void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(OnValueChange);
    }

    void OnValueChange(bool _)
    {
        Toggle?.Invoke((int)toggleID, toggle);
    }

    
}
