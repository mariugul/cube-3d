using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogBoxes : MonoBehaviour
{
    public void onShow()
    {
        Debug.Log("onShow() called on: " + gameObject.name);
    }

    public void onClose()
    {
        Debug.Log("onClose() called on: " + gameObject.name);
    }
}
 