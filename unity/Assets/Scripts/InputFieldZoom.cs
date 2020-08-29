using System.Collections;
using System.Collections.Generic;
using System.Drawing;
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
    int font_size = 11S;
    const int FONT_SIZE_MIN = 8;
    const int FONT_SIZE_MAX = 14;

    // Size variable for input field
    int inputFieldWidth  = 330; // 360 is the default width
    const int WIDTH_SCALE = 30; // Nr to scale width with
        
    // Start is called before the first frame update
    void Start()
    {
        Button plusBtn = plusButton.GetComponent<Button>();
        Button minusBtn = minusButton.GetComponent<Button>();
        
        // Add click function to button
        plusBtn.onClick.AddListener(PlusButtonClick);
        minusBtn.onClick.AddListener(MinusButtonClick);

        // Initialize input field size
        SetInputFieldSize(inputFieldWidth);
        SetFontSize(font_size);
    }

    void PlusButtonClick()
    {
        // Increment font size when smaller than defined maximum
        if (font_size < FONT_SIZE_MAX)
        {
            font_size += 1;
            SetFontSize(font_size);

            // Increase width of input field accordingly
            RectTransform rt = inputField.GetComponent(typeof(RectTransform)) as RectTransform;
            SetInputFieldSize(inputFieldWidth += WIDTH_SCALE);
        } 
    }

    void MinusButtonClick()
    {
        // Decrement font size when bigger than defined minimum
        if (font_size > FONT_SIZE_MIN)
        {
            font_size -= 1;
            SetFontSize(font_size);

            // Reduce width of input field accordingly
            RectTransform rt = inputField.GetComponent(typeof(RectTransform)) as RectTransform;
            SetInputFieldSize(inputFieldWidth -= WIDTH_SCALE);
        }
    }

    void SetFontSize(int font_size)
    {
        // Set font size of input field
        inputField.textComponent.fontSize = font_size;
    }

    void SetInputFieldSize(int size)
    {
        RectTransform rt = inputField.GetComponent(typeof(RectTransform)) as RectTransform;
        rt.sizeDelta = new Vector2(size, rt.sizeDelta.y);
    }
}
