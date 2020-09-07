// This script sends a message when an event happens
// -------------------------------------------------

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Enumerations.Enumerations;

[RequireComponent(typeof(Button))]
public class ButtonDelegate : MonoBehaviour
{
    // Inspector values to set
    [Header("Button Name")]
    [SerializeField] ButtonID buttonID;

    // Button this script is attached to
    Button button;

    // Delegates and events
    public delegate void ButtonClick(int id, Button button);
    public static event ButtonClick Click;

    // Runs on start up
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }
    
    // Invoked on a button click event
    public void OnButtonClick()
    {
        // Emit clicked button and button id
        Click?.Invoke((int)buttonID, button);
    }
}
