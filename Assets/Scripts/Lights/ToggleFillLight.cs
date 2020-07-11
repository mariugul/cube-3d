using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ToggleFillLight : MonoBehaviour
{
    public GameObject FillLight;
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
            FillLight.GetComponent<Light>().enabled = true;
        }
        else
        {
            FillLight.GetComponent<Light>().enabled = false;
        }
    }

}