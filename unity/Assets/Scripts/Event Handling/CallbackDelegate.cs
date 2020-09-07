﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CallbackDelegate : MonoBehaviour
{
    // Button this script is attached to
    Button button;

    // Delegates and events
    public delegate void Callback();
    public static event Callback CloseDropdown;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(CallbackCloseDropdown);
    }

    void CallbackCloseDropdown()
    {
        // Emit "button has been clicked and dropdown can be closed"
        CloseDropdown?.Invoke();
    }
}
