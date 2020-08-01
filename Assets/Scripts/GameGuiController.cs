using UnityEngine;

using System.Windows.Forms;

public class GameGuiController : MonoBehaviour
{
	void Start()
	{
		var form = new Form();
		form.Show();

		// Or show a message.
		MessageBox.Show("Hello World.");
	}
}