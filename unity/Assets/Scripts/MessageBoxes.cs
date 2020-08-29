using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class MessageBoxes : MonoBehaviour
{
    // Panels of Message Boxes
    public GameObject messageBoxYesNo;
    public GameObject messageBoxOk;

    // Buttons
    public Button okButton;
    public Button yesButton;
    public Button noButton;

    // Make class singleton
    public static MessageBoxes MBOXES;


    private void Awake()
    {
        //if a messageboxes doesn't already exist
        if(MBOXES == null)
        {
            DontDestroyOnLoad(this);
            MBOXES = this;
        }
        //if there's already a messageboxes
        else if(MBOXES != this){
            Destroy(gameObject);    
        }
    }

    void Start()
    {
        // Add button handlers
        okButton.onClick.AddListener(delegate { okButtonHandler (); });
        yesButton.onClick.AddListener(delegate { yesButtonHandler(); });
        noButton.onClick.AddListener(delegate { noButtonHandler (); });
    }


    public void Show(string message, string messageBoxType = "Ok", string windowName = "", string question = "")
    {
        Message(message, messageBoxType);
        WindowName(windowName, messageBoxType);
        
        if (messageBoxType == "YesNo")
            Question(question);
        
        Open(messageBoxType);
    }

    void Open(string messageBoxType)
    {
        // Open the message box
        if (messageBoxType == "YesNo")
            messageBoxYesNo.SetActive(true);

        else if (messageBoxType == "Ok")
            messageBoxOk.SetActive(true);
    }
    
    void Close(string messageBoxType)
    {
        // Close the message box
        if (messageBoxType == "YesNo")
            messageBoxYesNo.SetActive(false);

        else if (messageBoxType == "Ok")
            messageBoxOk.SetActive(false);

        Empty(messageBoxType);
    }

    void Message(string message, string messageBoxType)
    {
        int messageChild = 2;

        // Set the text component of message box to the message
        if (messageBoxType == "YesNo")
            messageBoxYesNo.transform.GetChild(messageChild).GetComponent<Text>().text = message;

        else if (messageBoxType == "Ok")
            messageBoxOk.transform.GetChild(messageChild).GetComponent<Text>().text = message;
    }

    void Question(string question) 
    {
        int questionChild = 4;

        messageBoxYesNo.transform.GetChild(questionChild).GetComponent<Text>().text = question;
    }

    void WindowName(string name, string messageBoxType)
    {
        int windowNameChild = 3;
        
        // Set the window name to the name
        if (messageBoxType == "YesNo")
            messageBoxYesNo.transform.GetChild(windowNameChild).GetComponent<Text>().text = name;

        else if (messageBoxType == "Ok")
            messageBoxOk.transform.GetChild(windowNameChild).GetComponent<Text>().text = name;
    }

    void Empty(string messageBoxType)
    {
        if (messageBoxType == "YesNo")
        {
            // Empty all text boxes
            Message("", messageBoxType);
            WindowName("", messageBoxType);
            Question("");
        }

        else if (messageBoxType == "Ok")
        {
            // Empty all text boxes
            Message("", messageBoxType);
            WindowName("", messageBoxType);
        }
    }

    void okButtonHandler() 
    {
        // Close the message box
        Close("Ok");
    }

    void yesButtonHandler()
    {
        // Close the message box
        Close("YesNo");

        // Open GitHub repository
        Application.OpenURL("https://github.com/mariugul/cube-3d/releases");
    }

    void noButtonHandler()
    {
        // Close the message box
        Close("YesNo");
    }
}