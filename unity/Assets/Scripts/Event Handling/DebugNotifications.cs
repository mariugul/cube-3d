using DevionGames.UIWidgets;
using UnityEngine;

// This script puts debug messages into the notfication window
public class DebugNotifications : MonoBehaviour
{
    public GameObject debugWindow;

    Notification notification;

    // Make class singleton
    public static DebugNotifications debug;

    private void Awake()
    {
        //if doesn't already exist
        if (debug == null)
        {
            DontDestroyOnLoad(this);
            debug = this;
        }
        //if there's already
        else if (debug != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        notification = FindObjectOfType<Notification>();
    }

    public void Log(string text)
    {
        // Only put messages if the debug window is active
        if (IsDebugWindowActive())
            notification.AddItem(text);
    }

    public void LogInfo(string text)
    {
        // Only put messages if the debug window is active
        if (IsDebugWindowActive())
            notification.AddItem("<color=green><b>INFO:</b> " + text + "</color>");
    }

    public void LogError(string text)
    {
        // Only put messages if the debug window is active
        if (IsDebugWindowActive())
            notification.AddItem("<color=red><b>ERROR:</b> " + text + "</color>");
    }

    public void LogWarning(string text)
    {
        // Only put messages if the debug window is active
        if (IsDebugWindowActive())
            notification.AddItem("<color=yellow><b>WARNING:</b> " + text + "</color>");
    }

    bool IsDebugWindowActive()
    {
        if (debugWindow.activeSelf)
            return true;
        else
            return false;
    }
}
