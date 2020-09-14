using UnityEngine;

namespace Enumerations
{
	public class Enumerations : MonoBehaviour
	{
		// Add new buttons to the bottom of Enum to avoid
		// the buttons shifting ID. Then you have to re-assign them.
		public enum ButtonID
		{
			// Tool Strip
			// -------------------------------------

			// File buttons
			file,
			fileNewProject,
			fileOpenProject,
			fileSave,
			fileSaveAs,
			fileExit,
		
			// Edit buttons
			edit,
			editSettings,

			// Export buttons
			export,
			exportArduino,
			exportAtmel,
			exportPatternFile,

			// Help buttons
			help,
			helpGithub,
			helpYoutube,
			helpDiscord,
			helpAbout, 
			helpHotkeys,

			// Test buttons
			left,
			right,
		}

		public enum SliderID
		{
			backLight,
			fillLight,
			keyLight,
			roofLight,
			ledIntensity
		}

		public enum ToggleID
		{
			backLight,
			fillLight,
			keyLight,
			roofLight,
			box, 
			pcb,
			legs, 
			codeEditor,
			colorPicker, 
			ledNumbering,
			planeLayout, 
			columnLayout,
			debugWindow,
		}

		// Type of clicked event to pass from delegate
		public enum EventClickType
		{
			button,
			toggle,
			slider
		}

		public enum Child
		{
			backLight = 64,
			fillLight,
			keyLight,
			roofLight,
			box,
			legs,
			pcb,
			frontText,

			layoutText = 0,
			planeText,
			columnText,
		}
	}
}