using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Enumerations.Enumerations;

// This script is supposed to send a message when an event happens
public class SliderDelegate : MonoBehaviour
{
    [Header("Slider Name")]
    [SerializeField] SliderID sliderID;

    [Header("Slider Object")]
    [SerializeField] Slider slider;

    public delegate void SliderChange(int id, Slider slider);
    public static event SliderChange Slide;

    //[SerializeField] ButtonID.Button button;
    
    public void OnSliderValueChange()
    {
        Slide?.Invoke((int)sliderID, slider);
    }
}
