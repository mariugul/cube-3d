using UnityEngine;
using UnityEngine.UI;
using DevionGames.UIWidgets;

public class DialogBoxTrigger : MonoBehaviour
{
    // Dialog box info
    public string title;
    [TextArea]
    public string text;
    public Sprite icon;
    public string[] options;

    // Dialog box instance
    private DialogBox m_DialogBox;

    // Button this script is attached to
    private Button button;

    void Start()
    {
        this.m_DialogBox = FindObjectOfType<DialogBox>();

        // Get button component and add listener to on click
        button = GetComponent<Button>();
        button.onClick.AddListener(ButtonClick);
    }

    void ButtonClick()
    {
        ShowWithCallback();
    }

    public void Show() {
        m_DialogBox.Show(title, text, icon, null, options);
    }

    public void ShowWithCallback()
    {
        m_DialogBox.Show(title, text, icon, _OnDialogResult, options);
    }

    private void _OnDialogResult(int index)
    {
        m_DialogBox.Show("Result", "Callback Result: "+options[index], icon, null, "OK");
    }
}
