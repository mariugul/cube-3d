using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ToggleBackLight : MonoBehaviour
{
    public GameObject BackLight;
    private Toggle toggle;

    void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(delegate
        {
            toggleValueChanged(toggle);
        });
    }

    void toggleValueChanged(Toggle changed)
    {
        Debug.Log("status = " + toggle.isOn);

        if (toggle.isOn) 
        {
            BackLight.GetComponent<Light>().enabled = true;
        }
        else
        {
            BackLight.GetComponent<Light>().enabled = false;
        }
    }

}