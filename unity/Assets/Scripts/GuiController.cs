﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GuiController : MonoBehaviour
{
	public PanelOpener panel;
	public ExportProject export;


	void Start()
	{
		var form = new WinFormToolStrip(panel, export);
		//var form2 = new WinFormSettings();

		form.Show();
		//form2.Show();
	}
}