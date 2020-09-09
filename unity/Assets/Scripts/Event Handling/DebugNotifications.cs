using DevionGames.UIWidgets;
using UnityEngine;

// This script puts debug messages into the notfication window
public class DebugNotifications : MonoBehaviour
{
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

        notification.AddItem(text);
    }

    public void LogInfo(string text)
    {
        notification.AddItem("<color=green><b>INFO:</b> " + text + "</color>");
    }

    public void LogError(string text)
    {
        notification.AddItem("<color=red><b>ERROR:</b> " + text + "</color>");
    }

    public void LogWarning(string text)
    {
        notification.AddItem("<color=yellow><b>WARNING:</b> " + text + "</color>");
    }
}
