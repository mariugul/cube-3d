using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ToggleRoofLight : MonoBehaviour
{
    public GameObject RoofLight;
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
            RoofLight.GetComponent<Light>().enabled = true;
        }
        else
        {
            RoofLight.GetComponent<Light>().enabled = false;
        }
    }

}