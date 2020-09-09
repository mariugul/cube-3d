using DevionGames.UIWidgets;
using UnityEngine;
using UnityEngine.UI;
using static Enumerations.Enumerations;
using static DebugNotifications;

// This script is supposed to handle the events when notfied by EventListener
public class EventHandler : MonoBehaviour
{
    // GameObjects   
    public Canvas      canvas;        // Canvas to access input field
    public ColorPicker picker;        // Color Picker
    public GameObject  settingsPanel; // Settings panel
    public GameObject  cubeLayout;    // 3D numbering, planes and columns 

    // Scripts
    CheckReleases checkReleases;
    DialogBox     dialogBox;

    // Internet Links
    const string GITHUB_LINK     = "https://github.com/mariugul/cube-3d";
    const string YOUTUBE_LINK    = "https://youtube.com";
    const string DISCORD_LINK    = "https://discord.gg/ZgxjkC2";
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
        string debugText = "Button '" + button.name + "' Clicked.";
        debug.Log(debugText);

        // File button
        // ----------------------------------------
        if (button.name == "Exit")
        {
            // SAVE
            // < Save what needs to be saved here >

            // Close unity application
            Application.Quit();
        }
        
        // Edit Button
        // ----------------------------------------

        // Settings
        else if (button.name == "Settings")
        {
            // Display settings
            settingsPanel.SetActive(true);

            // Display color picker
            picker.gameObject.SetActive(true);
        }

        // View button
        // -----------------------------------------

        

        // Help button
        // -----------------------------------------

        // Go to GitHub repository
        else if (button.name == "GitHub Repository")
            System.Diagnostics.Process.Start(GITHUB_LINK);

        // Go to YouTube tutorials
        else if (button.name == "YouTube Tutorials")
            System.Diagnostics.Process.Start(YOUTUBE_LINK);

        // Go to Discord server
        else if (button.name == "Chat in Discord")
            System.Diagnostics.Process.Start(DISCORD_LINK);

        // Check for updates
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

        // Show Hotkeys
        else if (button.name == "Hotkeys")
        {
            Sprite icon = null;
            string text = (
                "Generate pattern                  Enter\n\n"     +
                "Enable all LEDs                   Ctrl + A\n"    +
                "Disable all LEDs                  Shift + A\n\n" +
                "Select pattern time               Ctrl + T\n\n"  +
                "Delete pattern line               Delete\n"
            );

            dialogBox.Show("Hotkeys", text, icon, null, "Ok");
        }

        // Show information about app
        else if (button.name == "About")
        {
            Sprite icon = null;
            string ver = checkReleases.GetCurrentVersion().ToString();
            string title = "v" + ver;
            string text = (
                "<b>Created by Marius C. K.</b>\n\n"                              +
                "For help or feedback, join the Discord server. " +
                "Video tutorials are found on YouTube. " +
                "The source code is found on GitHub.\n\n"                           +
                "Link to Discord, YouTube and GitHub is found under the 'Help' button"
            );
            
            dialogBox.Show(title, text, icon, null, "Ok");
        }
    }

    // Handles all 'Toggles' 
    // ------------------------------------------------------------------------------
    void ToggleHandler(int id, Toggle toggle)
    {
        string debugMsg = "Toggle '" + toggle.name + "' Clicked. State = " + toggle.isOn.ToString();
        debug.Log(debugMsg);

        ToggleID toggleID = (ToggleID)id;

        //  Back Light Toggle
        if (toggleID == ToggleID.backLight)
        {
            if (toggle.isOn)
                GetLightComponent(Child.backLight).enabled = true;
            else
                GetLightComponent(Child.backLight).enabled = false;
        }

        //  Fill Light Toggle
        else if (toggleID == ToggleID.fillLight)
        {
            if (toggle.isOn)
                GetLightComponent(Child.fillLight).enabled = true;
            else
                GetLightComponent(Child.fillLight).enabled = false;
        }

        //  Key Light Toggle
        else if (toggleID == ToggleID.keyLight)
        {
            if (toggle.isOn)
                GetLightComponent(Child.keyLight).enabled = true;
            else
                GetLightComponent(Child.keyLight).enabled = false;
        }

        //  Roof Light Toggle
        else if (toggleID == ToggleID.roofLight)
        {
            if (toggle.isOn)
                GetLightComponent(Child.roofLight).enabled = true;
            else
                GetLightComponent(Child.roofLight).enabled = false;
        }

        // Cube Box Toggle
        else if (toggleID == ToggleID.box)
        {
            if (toggle.isOn)
                GetStructureObject(Child.box).SetActive(true);
            else
                GetStructureObject(Child.box).SetActive(false);
        }

        // PCB Toggle
        else if (toggleID == ToggleID.pcb)
        {
            if (toggle.isOn)
                GetStructureObject(Child.pcb).SetActive(true);
            else
                GetStructureObject(Child.pcb).SetActive(false);
        }

        // LED legs Toggle
        else if (toggleID == ToggleID.legs)
        {
            if (toggle.isOn)
                GetStructureObject(Child.legs).SetActive(true);
            else
                GetStructureObject(Child.legs).SetActive(false);
        }

        // Pattern File Display Toggle
        else if (toggleID == ToggleID.codeEditor)
        {
            int inputFieldChild = 0;

            if (toggle.isOn)
                canvas.transform.GetChild(inputFieldChild).gameObject.SetActive(true);
            else
                canvas.transform.GetChild(inputFieldChild).gameObject.SetActive(false);
        }

        // Color Picker Toggle
        else if (toggleID == ToggleID.colorPicker) 
        {
            if (toggle.isOn)
                picker.gameObject.SetActive(true);
            else
                picker.gameObject.SetActive(false);
        }

        // View Tool Strip Dropdown Menu
        // ------------------------------------------------------

        // Toggle LED Numbering
        else if (toggle.name == "LED Numbering")
        {
            // Get gameobject
            var go = cubeLayout.transform.GetChild((int)Child.layoutText).gameObject;

            // Toggle the 3D LED numbering
            if (go.activeSelf)
                go.SetActive(false);
            else
                go.SetActive(true);
        }

        else if (toggle.name == "Plane Layout")
        {
            // Get gameobject
            var go = cubeLayout.transform.GetChild((int)Child.planeText).gameObject;

            // Toggle the 3D plane text
            if (go.activeSelf)
                go.SetActive(false);
            else
                go.SetActive(true);
        }

        else if (toggle.name == "Column Layout")
        {
            // Get gameobject
            var go = cubeLayout.transform.GetChild((int)Child.columnText).gameObject;

            // Toggle the 3D column text
            if (go.activeSelf)
                go.SetActive(false);
            else
                go.SetActive(true);
        }
    }

    // Handles all 'Slider' changes
    // ------------------------------------------------------------------------------
    void SliderHandler(int id, Slider slider)
    {
        string debugMsg = ("Slider value = " + slider.value);
        debug.Log(debugMsg);

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
        string debugMsg = "Dialog box: " + title + " Callback: " + result;
        debug.Log(debugMsg);

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
        //CheckReleases.UpdateAvailable += 
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
