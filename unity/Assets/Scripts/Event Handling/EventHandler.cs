using DevionGames.UIWidgets;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.UI;
using static Enumerations.Enumerations;

// This script is supposed to handle the events when notfied by EventListener
public class EventHandler : MonoBehaviour
{
    // GameObjects   
    public Canvas      canvas;        // Canvas to access input field
    public ColorPicker picker;        // Color Picker
    public GameObject  settingsPanel; // Settings panel

    // Scripts
    CheckReleases checkReleases;
    DialogBox     dialogBox;

    // Internet Links
    const string GITHUB_LINK  = "https://github.com/mariugul/cube-3d";
    const string YOUTUBE_LINK = "https://youtube.com";
    const string DISCORD_LINK = "https://discord.gg/ZgxjkC2";
    const string GITHUB_RELEASES = "https://github.com/mariugul/cube-3d/releases";

    void Start()
    {
        // Instantiate scripts
        checkReleases = FindObjectOfType<CheckReleases>();
        dialogBox     = FindObjectOfType<DialogBox>();
    }

    // Handles all 'Button' clicks
    // ------------------------------------------------------------------------------
    void ButtonHandler(int id, Button button)
    {
        // Print the clicked button name
        Debug.Log("Button '" + button.name + "' Clicked.");

        // Edit Button
        // ----------------------------------------
        if (button.name == "Settings")
        {
            // Display settings
            settingsPanel.SetActive(true);

            // Display color picker
            picker.gameObject.SetActive(true);
        }

        // File button
        // ----------------------------------------
        else if (button.name == "Exit")
        {
            // SAVE
            // < Save what needs to be saved here >

            // Close unity application
            Application.Quit();
        }
        // Help button
        // -----------------------------------------
        else if (button.name == "GitHub Repository")
            System.Diagnostics.Process.Start(GITHUB_LINK);

        else if (button.name == "YouTube Tutorials")
            System.Diagnostics.Process.Start(YOUTUBE_LINK);

        else if (button.name == "Chat in Discord")
            System.Diagnostics.Process.Start(DISCORD_LINK);

        else if (button.name == ("Check for Updates"))
        {
            Sprite icon = null; // Empty icon

            bool? isAvailable = checkReleases.IsUpdateAvailable();
            
            if (isAvailable == true)
            {
                Debug.Log("IsAvailable: " + isAvailable.ToString());

                dialogBox.Show("New Update", "A new release is available.\n\nDo you want to download?", icon, null, "Yes", "No");
            }
            else if (isAvailable == false)
            {
                var ver = checkReleases.GetCurrentVersion().ToString();
                dialogBox.Show("No Updates", "You have the newest release installed already.\n\nVersion: " + ver, icon, null, "Ok");
            }
            else if (isAvailable == null)
            {
                string text = "It was not possible to determine if there are new updates available.\n\nThere might be no internet connection or an error reading from the server.";
                dialogBox.Show("Error", text, icon, null, "Ok");
            }
        }
    }

    // Handles all 'Toggles' 
    // ------------------------------------------------------------------------------
    void ToggleHandler(int id, Toggle toggle)
    {
        Debug.Log("Toggle '" + toggle.name + "' Clicked. State = " + toggle.isOn.ToString());

        ToggleID toggleID = (ToggleID)id;

        //  Back Light Toggle
        if (toggleID == ToggleID.backLight)
        {
            if (toggle.isOn)
                GetLightComponent(Child.backLight).enabled = true;
            else
                GetLightComponent(Child.backLight).enabled = false;
            
            return;
        }

        //  Fill Light Toggle
        if (toggleID == ToggleID.fillLight)
        {
            if (toggle.isOn)
                GetLightComponent(Child.fillLight).enabled = true;
            else
                GetLightComponent(Child.fillLight).enabled = false;
            
            return;
        }

        //  Key Light Toggle
        if (toggleID == ToggleID.keyLight)
        {
            if (toggle.isOn)
                GetLightComponent(Child.keyLight).enabled = true;
            else
                GetLightComponent(Child.keyLight).enabled = false;
            
            return;
        }

        //  Roof Light Toggle
        if (toggleID == ToggleID.roofLight)
        {
            if (toggle.isOn)
                GetLightComponent(Child.roofLight).enabled = true;
            else
                GetLightComponent(Child.roofLight).enabled = false;

            return;
        }

        // Cube Box Toggle
        if (toggleID == ToggleID.box)
        {
            if (toggle.isOn)
                GetStructureObject(Child.box).SetActive(true);
            else
                GetStructureObject(Child.box).SetActive(false);

            return;
        }

        // PCB Toggle
        if (toggleID == ToggleID.pcb)
        {
            if (toggle.isOn)
                GetStructureObject(Child.pcb).SetActive(true);
            else
                GetStructureObject(Child.pcb).SetActive(false);

            return;
        }

        // LED legs Toggle
        if (toggleID == ToggleID.legs)
        {
            if (toggle.isOn)
                GetStructureObject(Child.legs).SetActive(true);
            else
                GetStructureObject(Child.legs).SetActive(false);

            return;
        }

        // Pattern File Display Toggle
        if (toggleID == ToggleID.codeEditor)
        {
            int inputFieldChild = 0;

            if (toggle.isOn)
                canvas.transform.GetChild(inputFieldChild).gameObject.SetActive(true);
            else
                canvas.transform.GetChild(inputFieldChild).gameObject.SetActive(false);

            return;
        }

        // Color Picker Toggle
        if (toggleID == ToggleID.colorPicker) 
        {
            if (toggle.isOn)
                picker.gameObject.SetActive(true);
            else
                picker.gameObject.SetActive(false);

            return;
        }
    }

    // Handles all 'Slider' changes
    // ------------------------------------------------------------------------------
    void SliderHandler(int id, Slider slider)
    {
        Debug.Log("Slider value = " + slider.value);
        SliderID sliderID = (SliderID)id;

        // Back Light Slider
        if (sliderID == SliderID.backLight)
        {
            GetLightComponent(Child.backLight).intensity = slider.value;
            return;
        }

        // Fill Light Slider
        if (sliderID == SliderID.fillLight)
        {
            GetLightComponent(Child.fillLight).intensity = slider.value;
            return;
        }

        // Key Light Slider
        if (sliderID == SliderID.keyLight)
        {
            GetLightComponent(Child.keyLight).intensity  = slider.value;
            return;
        }

        // Roof Light Slider
        if (sliderID == SliderID.roofLight)
        {
            GetLightComponent(Child.roofLight).intensity = slider.value;
            return;
        }

        // Led Intensity Slider
        if (sliderID == SliderID.ledIntensity)
        {
            // Set leds intensity
            LedLights.LEDLIGHTS.SetIntensity(slider.value, "leds");

            // Set halos intensity
            LedLights.LEDLIGHTS.SetIntensity(slider.value / 150, "halos"); // Adjust value to match halo intensity

            // Set halos range
            int halo_range = (int)slider.value;

            if (halo_range > 15)
                halo_range = 15;
            else if (halo_range < 10)
                halo_range = 10;

            LedLights.LEDLIGHTS.SetRange(halo_range, "halos");
            
            return;
        }
    }

    // Handles all dialog boxes
    // ------------------------------------------------------------------------------
    void DialogBoxHandler(string title, string result)
    {
        // When a new update is available
        if (title.Contains("New Update"))
        {
            // Action based on callback result
            if (result.Contains("Yes"))
                // Open the GitHub releases in web browser
                System.Diagnostics.Process.Start(GITHUB_RELEASES);

            else if (result.Contains("No"))
                Debug.Log("You didn't want to update.");

            else
                Debug.Log("No answer was received from dialog box.");

            return;
        }

        // 
    }

    // Private functions
    // ------------------------------------------------------------------------------
    Light GetLightComponent(Child child)
    {
        return gameObject.transform.GetChild((int)child).GetComponent<Light>();
    }

    GameObject GetStructureObject(Child child)
    {
        return gameObject.transform.GetChild((int)child).gameObject;
    }

    void OnEnable()
    {
        ButtonDelegate.Click  += ButtonHandler;
        ToggleDelegate.Toggle += ToggleHandler;
        SliderDelegate.Slide  += SliderHandler;
        DialogBoxTrigger.Callback += DialogBoxHandler;
        Debug.Log("Subscribed to events.");
    }

    void OnDisable()
    {
        ButtonDelegate.Click  -= ButtonHandler;
        ToggleDelegate.Toggle -= ToggleHandler;
        SliderDelegate.Slide  -= SliderHandler;
        DialogBoxTrigger.Callback -= DialogBoxHandler;
        Debug.Log("Unsubscribed to events.");
    }
}
