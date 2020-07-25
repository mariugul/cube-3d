using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldZoom : MonoBehaviour
{
    public Button plusButton;
    public Button minusButton;

    // Start is called before the first frame update
    void Start()
    {
        Button plusBtn = plusButton.GetComponent<Button>();
        Button minusBtn = minusButton.GetComponent<Button>();
        
        // Add click function to button
        plusBtn.onClick.AddListener(OnButtonClick);
        minusBtn.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        Debug.Log("Button clicked!");
    }
}
