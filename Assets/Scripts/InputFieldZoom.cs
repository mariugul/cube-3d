using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldZoom : MonoBehaviour
{
    // Button Objects
    public Button plusButton;
    public Button minusButton;

    // Input Field Object
    public TMP_InputField inputField;

    // Size variables for input field font
    int font_size = 12;
    const int font_size_min = 8;
    const int font_size_max = 14;
        
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

        // Increment font size when smaller than defined maximum
        if (font_size < font_size_max)
        {
            font_size += 1;
            SetFontSize(font_size);
        } 
    }

    void MinusButtonClick()
    {
        Debug.Log("Minus Button clicked!");

        // Decrement font size when bigger than defined minimum
        if (font_size > font_size_min)
        {
            font_size -= 1;
            SetFontSize(font_size);
        }
    }

    void SetFontSize(int font_size)
    {
        // Set font size of input field
        inputField.textComponent.fontSize = font_size;
    }
}
