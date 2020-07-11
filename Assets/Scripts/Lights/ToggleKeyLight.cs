using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ToggleKeyLight : MonoBehaviour
{
    public GameObject KeyLight;
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
            KeyLight.GetComponent<Light>().enabled = true;
        else
            KeyLight.GetComponent<Light>().enabled = false;
    }

}