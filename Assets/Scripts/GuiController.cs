using UnityEngine;

public class GuiController : MonoBehaviour
{
	public PanelOpener panel;

	void Start()
	{
		var form = new WinFormToolStrip(panel);
		form.Show();
	}
}