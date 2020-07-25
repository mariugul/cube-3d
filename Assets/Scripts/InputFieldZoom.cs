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
        plusBtn.onClick.AddListener(PlusButtonClick);
        minusBtn.onClick.AddListener(MinusButtonClick);
    }

    void PlusButtonClick()
    {
        Debug.Log("Plus Button clicked!");
        
    }

    void MinusButtonClick()
    {
        Debug.Log("Minus Button clicked!");
    }
}
