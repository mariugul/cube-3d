using UnityEngine;

public class WinFormInstance : MonoBehaviour
{
	private WinForm form;

	void Start()
	{
		form = new WinForm();
		form.Show();
	}
}