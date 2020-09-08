using UnityEngine;
using UnityEngine.UI;
using DevionGames.UIWidgets;
using System;
using System.Runtime.CompilerServices;

public class DialogBoxTrigger : MonoBehaviour
{
    // Dialog box 
    [Header("Dialog Box")]
    public string title;
    [TextArea]
    public string text;
    public Sprite icon;
    public string[] options;

    public bool showCallback;

    // Callback 
    [Header("Callback Box")]
    public string callbackTitle;
    [TextArea]
    public string callbackText;
    public Sprite callbackIcon;
    public string[] callbackOptions;

    // Dialog box instance
    private DialogBox m_DialogBox;

    // Button this script is attached to
    private Button button;

    // Send dialog button click to event handler
    public delegate void DialogClick(string title, string result);
    public static event DialogClick Callback;

    void Start()
    {
        this.m_DialogBox = FindObjectOfType<DialogBox>();

        // Get button component and add listener to 'on click'
        button = GetComponent<Button>();
        button.onClick.AddListener(ButtonClick);
    }

    void ButtonClick()
    {
        if (showCallback)
            ShowWithCallback();
        else
            Show();
    }

    public void Show() {
        m_DialogBox.Show(title, text, icon, _OnButtonResult, options);
    }

    public void ShowWithCallback()
    {
        m_DialogBox.Show(title, text, icon, _OnDialogResult, options);
    }

    void _OnDialogResult(int index)
    {
        //m_DialogBox.Show("Result", "Callback Result: " + options[index], icon, null, "OK");
        m_DialogBox.Show(callbackTitle, callbackText, callbackIcon, null, callbackOptions);
    }

    // Sends the clicked button response from dialog box 
    // to event handler
    void _OnButtonResult(int index)
    {
        // Trigger 'dialog box button click' event
        // and send the result as string
        Callback?.Invoke(title, options[index]);
    }

   
}
