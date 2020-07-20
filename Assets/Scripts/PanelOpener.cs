using UnityEngine;
using UnityEngine.UI;

public class PanelOpener : MonoBehaviour
{
    public GameObject Panel;
    public Button SettingsButton;

    public void Start()
    {
        // Add listener to button
        SettingsButton.onClick.AddListener(delegate { ButtonHandler(); });
    }

    public void ButtonHandler()
    {
        TogglePanel();
    }

    void TogglePanel()
    {
        if (Panel != null)
        {
            // Read panel state
            bool isActive = Panel.activeSelf;

            // Set state to the oposite
            Panel.SetActive(!isActive);
        }
    }
}
