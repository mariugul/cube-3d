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
}
