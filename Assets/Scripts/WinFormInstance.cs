using UnityEngine;

public class WinFormInstance : MonoBehaviour
{
	// Instance of the WinForm.cs class
	private WinForm form;

	void Start()
	{
		form = new WinForm();
		form.Show();
	}
}