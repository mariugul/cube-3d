using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(GameObject))]
[RequireComponent(typeof(Button))]
public class DropdownMenus : MonoBehaviour, IDeselectHandler
{
    // Button this script is attached to
    Button button;

    // The dropdown menu object to toggle
    GameObject dropdownMenu;

    static GameObject previousOpenDropdown;

    const int dropdownChild = 1;

    void OnEnable()
    {
        // Subscribe to callback for closing dropdown
        DropdownCallbackDelegate.CloseDropdown += CloseDropdown;
    }

    void Start()
    {
        // Get dropdown menu 
        GameObject dropdownObject  = transform.GetChild(dropdownChild).gameObject;
        dropdownMenu = dropdownObject;

        // Initialize just so it has a value
        previousOpenDropdown = dropdownMenu;
                
        // Get button component and add listener function
        button = GetComponent<Button>();
        button.onClick.AddListener(ToggleActive);
    }

    void OnDisable()
    {
        // Unsubscribe to callback
        DropdownCallbackDelegate.CloseDropdown -= CloseDropdown;
    }

    // Toggle dropdown menu on tool strip
    public void ToggleActive()
    {
        bool isActive = dropdownMenu.activeSelf;

        if (!isActive)
        {
            // Close the previously open dropdown menu
            previousOpenDropdown.SetActive(false);

            // Open the clicked dropdown menu
            dropdownMenu.SetActive(true);

            // Save this dropdown menu to 'opened dropdown'
            previousOpenDropdown = dropdownMenu;
        }
        else
        {
            dropdownMenu.SetActive(false);
        }
    }

    void CloseDropdown()
    {
        dropdownMenu.SetActive(false);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        // Close dropdown after X seconds
        // This is if click is outside of dropdown menu
        Invoke("CloseDropdown", 0.3f);
    }
}
