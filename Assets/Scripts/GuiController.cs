using UnityEngine;

public class GuiController : MonoBehaviour
{
	public PanelOpener panel;

	void Start()
	{
		var form = new WinFormToolStrip(panel);
		var form2 = new WinFormSettings();

		form.Show();
		//form2.Show();
	}
}